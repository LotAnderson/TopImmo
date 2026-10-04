# Wichtige Algorithmen von TopImmo

Stand: 04.10.2026. Der Pseudocode beschreibt die vorhandene Implementierung und lässt technische C#-Details weg. Die zugehörigen Komponenten und Datenmodelle stehen in der [Systemarchitektur](architektur.md).

## 1. Mietangebote laden, verarbeiten und zwischenspeichern

Ziel: Externe Anfragen reduzieren und dieselben verarbeiteten Daten für Liste, Detailansicht und Bezirkszählung verwenden.

```text
FUNKTION LadeVerarbeiteteAngebote():
    WENN Cache einen gültigen Eintrag "stuttgart-listings" enthält:
        GIB den Cache-Eintrag zurück

    WARTE auf die gemeinsame Ladesperre
    VERSUCHE:
        WENN der Cache inzwischen einen gültigen Eintrag enthält:
            GIB den Cache-Eintrag zurück

        JSON = rufe externe API auf (Seite 1, Seitengröße 30)
        Antwort = deserialisiere JSON
        Rohangebote = Antwort.Listings oder leere Liste
        Angebote = alle Rohangebote mit RealEstateType = "apartmentrent"
                   (Vergleich ohne Beachtung der Groß-/Kleinschreibung)

        FÜR jedes Angebot:
            Angebot.MappedDistrict = ErmittleBezirk(Angebot.Address?.Line)

        speichere Angebote unter "stuttgart-listings" für 5 Minuten im Cache
        GIB Angebote zurück
    IMMER, auch bei Rückgabe oder Fehler:
        GIB die Ladesperre frei
```

Die zweite Cache-Prüfung ist nötig, weil eine andere Anfrage während der Wartezeit bereits Daten geladen haben kann. Die Sperre verhindert gleichzeitige externe Ladeversuche innerhalb eines Backend-Prozesses. Falls die API-Anfrage oder JSON-Verarbeitung scheitert, wird kein neuer Eintrag gespeichert und die Sperre trotzdem freigegeben. Der Fehler wird in der aktuellen Implementierung weitergegeben.

Belege: [Cache, Sperre, Filter und Verarbeitung](../Backend/ImmscoutAPI/Service/DistrictDataService.cs#L22), [Externe Anfrage](../Backend/ImmscoutAPI/Service/ImmoScoutAPIService.cs#L17).

## 2. Bezirk aus einer Adresse ermitteln

Ziel: Amtliche Stadtteil- und Bezirksnamen eindeutig einem der 23 Stadtbezirke zuordnen.

```text
BEIM START:
    lade Bezirksnamen aus map-districts.json
    lade alle 152 Stadtteile mit Elternbezirk aus stuttgart-subdistricts.json
    baue Nachschlagetabelle: normalisierter Ortsname → Stadtbezirk

FUNKTION ErmittleBezirk(Adresszeile):
    WENN Adresszeile fehlt oder leer ist:
        GIB "Unknown" zurück

    Teile = trenne Adresszeile an Kommas
    WENN mehrere Teile vorliegen:
        überspringe erste Komponente (Straße)

    FÜR jede verbleibende Ortskomponente:
        vereinheitliche Unicode, Leerzeichen und Gedankenstriche
        entferne gegebenenfalls führende PLZ, Stuttgart-Präfix,
                 abschließendes "(Stuttgart)"
        schlage vollständigen Ortsnamen in Referenz nach
        sammle jeden gefundenen Elternbezirk ohne Dubletten

    WENN genau ein eindeutiger Bezirk gefunden wurde:
        GIB diesen Bezirk zurück
    SONST:
        GIB "Unknown" zurück
```

`Beispielstraße 1, 70174 Stuttgart, stuttgart-mitte` ergibt `Mitte`; `Beispielstraße 1, 70329 Stuttgart, Uhlbach` ergibt `Obertürkheim`. Eine allgemeine Stadtangabe, ein unbekannter Ort oder widersprüchliche Bezirke bleiben `Unknown`. Das Frontend nennt diese Gruppe „Ohne Bezirksangabe“. Es wird kein Bezirk aus Straßen, PLZ oder Koordinaten geschätzt. 189 Adressfälle und alle 23 Bezirksfilter wurden geprüft.

Belege: [DistrictDataService](../Backend/ImmscoutAPI/Service/DistrictDataService.cs), [Kartenzuordnung](../Backend/ImmscoutAPI/Data/map-districts.json), [amtliche Stadtteilreferenz](../Backend/ImmscoutAPI/Data/stuttgart-subdistricts.json), [Vollvergleich mit Quellen](stadtbezirke-pruefung.md).

## 3. Bezirkszählungen und gefilterte Suche

Ziel: Eine gefilterte Wohnungsliste anzeigen und gleichzeitig die Zählungen für alle geladenen Bezirke behalten.

```text
FUNKTION SucheAngebote(GesuchterBezirk):
    AlleAngebote = LadeVerarbeiteteAngebote()
    Gruppen = gruppiere AlleAngebote nach MappedDistrict
    Zählungen = für jede Gruppe: (Bezirksname, Anzahl)
    sortiere Zählungen nach Anzahl absteigend,
                          bei Gleichstand nach Bezirksname aufsteigend

    WENN GesuchterBezirk fehlt, leer ist oder nur Leerzeichen enthält:
        Ergebnisliste = AlleAngebote
    SONST:
        Ergebnisliste = Angebote, deren MappedDistrict GesuchterBezirk entspricht
                        (Groß-/Kleinschreibung egal)

    GIB { listings: Ergebnisliste,
          districtCounts: Zählungen,
          mapDistricts: Kartenzuordnung } zurück
```

Die Zählung geschieht vor dem Suchfilter. Bei drei geladenen Mietangeboten, davon zwei in Mitte und eines in Nord, liefert die Suche nach Mitte zwei Angebote, aber weiterhin die Zählungen `Mitte: 2` und `Nord: 1`. Die Summen beziehen sich auf die geladenen Treffer der ersten API-Seite, nicht auf alle verfügbaren Angebote in Stuttgart.

Belege: [SearchAsync](../Backend/ImmscoutAPI/Service/DistrictDataService.cs#L51), [Antwortmodell](../Backend/ImmscoutAPI/Model/ListingSearchResult.cs#L3).

## 4. Registrierung und Login

Ziel: Benutzer dauerhaft speichern, Passwörter als Hash behandeln und angemeldeten Benutzern Tokens ausgeben.

```text
FUNKTION Registrieren(E-Mail, Passwort):
    WENN SQLite bereits einen Benutzer mit dieser E-Mail enthält:
        GIB Fehler "User already exists" zurück
    Benutzer = (E-Mail, BCrypt-Hash des Passworts)
    speichere Benutzer in SQLite
    GIB Erfolg zurück

FUNKTION Einloggen(E-Mail, Passwort):
    Benutzer = suche Benutzer mit dieser E-Mail in SQLite
    WENN kein Benutzer gefunden wurde:
        GIB Fehler "User not found" zurück
    WENN BCrypt-Prüfung gegen den gespeicherten Hash fehlschlägt:
        GIB Fehler "Invalid password" zurück

    AccessToken = erzeuge signierten JWT für Benutzer
    RefreshToken = erzeuge kryptografisch zufälligen Token
    speichere RefreshToken mit Benutzer-ID und Ablaufzeit JetztUTC + 7 Tage
    GIB Erfolg, AccessToken und RefreshToken zurück
```

Der Controller übersetzt eine doppelte Registrierung in HTTP 409 und einen fehlgeschlagenen Login in HTTP 401. Der JWT enthält Benutzer-ID und E-Mail sowie eine konfigurierte Ablaufzeit; die aktuelle Konfiguration verwendet 60 Minuten. RefreshTokens entstehen aus 64 zufälligen Bytes. Passwörter werden mit BCrypt geprüft, nicht entschlüsselt.

Die derzeitige Registrierung prüft kein E-Mail-Format und keine Mindestlänge des Passworts. Die E-Mail-Prüfung im Service ersetzt auch keinen `UNIQUE`-Constraint bei gleichzeitigen Registrierungen.

Belege: [RegisterAsync und LoginAsync](../Backend/ImmscoutAPI/Service/AuthService%20.cs#L21), [TokenService](../Backend/ImmscoutAPI/Service/TokenService%20.cs#L21), [HTTP-Antworten](../Backend/ImmscoutAPI/Controllers/AuthController.cs#L18).

## 5. RefreshToken prüfen und rotieren

Ziel: Ein neues Tokenpaar ausgeben und den zuvor verwendeten RefreshToken widerrufen.

```text
FUNKTION TokensErneuern(EingereichterRefreshToken):
    AlterToken = suche Token in SQLite, einschließlich zugehörigem Benutzer
    WENN AlterToken nicht existiert ODER widerrufen ist
         ODER seine Ablaufzeit vor JetztUTC liegt:
        GIB Fehler "Invalid or expired refresh token" zurück

    markiere AlterToken als widerrufen
    NeuerAccessToken = erzeuge signierten JWT für den Benutzer
    NeuerRefreshToken = erzeuge kryptografisch zufälligen Token
    füge NeuerRefreshToken für denselben Benutzer hinzu,
          mit Ablaufzeit JetztUTC + 7 Tage
    speichere die Änderungen in SQLite

    GIB Erfolg, NeuerAccessToken und NeuerRefreshToken zurück
```

Der Controller antwortet bei ungültigem RefreshToken mit HTTP 401. Eine spätere erneute Verwendung des alten Tokens wird abgewiesen, weil `IsRevoked` gesetzt wurde. Eine spezielle Absicherung gegen zwei gleichzeitig eingereichte Refresh-Anfragen ist im aktuellen Code nicht vorhanden; sie wird durch diesen Ablauf nicht zugesichert.

Belege: [RefreshAsync](../Backend/ImmscoutAPI/Service/AuthService%20.cs#L75), [Refresh-Endpunkt](../Backend/ImmscoutAPI/Controllers/AuthController.cs#L44).

## Nachweis durch Prüfungen

Die vorhandenen [Backend-Checks](../Backend/ImmscoutAPI.Checks/Program.cs#L22) prüfen unter anderem Mietwohnungsfilter, Bezirksnormalisierung, Zählungen, Suche, gemeinsame Cache-Nutzung, Fehler-Erholung sowie HTTP-Antworten. Sie verwenden simulierte externe API-Antworten und einen Testauth-Handler. Diese Checks waren am 04.10.2026 erfolgreich.

Der echte Registrierungs-, Login- und Refresh-Ablauf wurde zusätzlich lokal mit einer separaten temporären SQLite-Datenbank geprüft; Tokenrotation und Abweisung des alten Tokens funktionierten. Dieser zusätzliche Prüflauf gehört bisher nicht zu den automatisierten Repository-Checks. Es fand dabei kein Aufruf der Live-RapidAPI statt. Die Prüfungen beweisen daher keine aktuelle Verfügbarkeit oder vollständige Live-Datenlieferung des externen Dienstes.
