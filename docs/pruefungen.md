# Prüfungsnachweis – Stand 04.10.2026

Die Prüfungen beziehen sich auf die aktuelle lokale Arbeitskopie einschließlich noch nicht eingecheckter Dateien. Seit diesen Prüfungen wurden für die Präsentationsvorbereitung Dokumente ergänzt; die Implementierung wurde dabei nicht geändert. Die Originaldatenbank wurde durch die zusätzlichen isolierten Authprüfungen nicht verändert. Die anschließende echte Demoprobe legte zwei synthetische Demokonten in der Produktdatenbank an; vorher und nachher wurde der Stand gesichert.

| Prüfung | Ergebnis | Reichweite |
|---|---|---|
| Vollständiger Backend-Build | 0 Fehler, 28 CS8618-Nullability-Warnungen | Kompilierung, keine vollständige Laufzeitprüfung. |
| Vorhandene Backendchecks | Bestanden | Verarbeitung, Cache, Konkurrenz, Suche, Details/404, Leerfälle, Fehlererholung sowie HTTP-Routing/JSON. Fixture-API und eigener Testauth-Handler. |
| Zusätzlicher isolierter Auth-HTTP-Test | Reguläre Registrierung, Login und Refreshrotation erfolgreich | Eigenständige temporäre SQLite und ContentRoot; kein Live-Immobilienabruf. Der Test ist eine zusätzliche Auditprüfung, kein eingecheckter Auth-Regressionstest. |
| Frontendtests | 8/8 in zwei Testdateien bestanden | Komponenten-/HTTP-Tests mit Mockantworten; produktiver Auth-Interceptor und vollständiger Browserablauf nicht abgedeckt. |
| Frontend-Entwicklungsbuild | Erfolgreich | Lokale Kompilierbarkeit im Entwicklungsmodus. |
| Standard-Produktionsbuild | Fehlgeschlagen | Initiales Bundle ca. 1,71 MB überschreitet das Fehlerbudget von 1 MB. |
| Echte API-/Desktop-Browserprobe, 16:23–16:24 Uhr | Bestanden | Registrierung, frischer Login, 28 Angebote, Bad Cannstatt, Details/Bildwechsel, Rückkehr, Logout/Guard. Keine Mocks; RapidAPI HTTP 200. [Nachweis und Screenshots](demoprobe.md). |

## Reproduzierbare Befehle

Im Repositoryhauptordner:

```sh
dotnet build Backend/ImmscoutAPI/ImmscoutAPI.csproj --no-restore --no-incremental
dotnet run --project Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj --no-restore
```

Im Ordner `Frontend/TomInnoFrondEnd`:

```sh
npm test -- --watch=false
npm run build -- --configuration development
npm run build
```

`--no-restore` setzt bereits wiederhergestellte .NET-Pakete voraus. Bei einem frischen Checkout die Backendpakete zunächst mit `dotnet restore Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj` wiederherstellen. npm benötigt die installierten Frontendabhängigkeiten. Ohne neue Implementierungsänderung müssen die bereits erfolgreichen Prüfungen für die Dokumentation nicht wiederholt werden.

## Ergebnisse der zusätzlichen Authprüfung

| Fall | HTTP-Ergebnis |
|---|---:|
| Immobilienanfrage ohne Token | 401 |
| Reguläre Registrierung | 200 |
| Doppelte Registrierung | 409 |
| Falsches Passwort | 401 |
| Regulärer Login mit Tokenausgabe | 200 |
| Refresh mit neuem RefreshToken | 200 |
| Wiederverwendung des widerrufenen RefreshTokens | 401 |
| Registrierung mit leerer E-Mail und leerem Passwort | 200, fehlerhafte Eingaben gespeichert |
| Ungültige E-Mail mit Ein-Zeichen-Passwort | 200, fehlerhafte Eingaben gespeichert |

In dieser isolierten Authprüfung wurde der gültige JWT nicht gegen einen Immobilienabruf mit echter externer API geprüft. Die spätere Browserprobe prüfte hingegen einen frischen produktiven Login und geschützte Immobilienanfragen erfolgreich. Die ungültigen Registrierungen zeigen eine konkrete fehlende Servervalidierung; die Frontendvalidierung allein verhindert direkte ungültige API-Aufrufe nicht.

## Offene Arbeit für Demo und Qualität

- Live-/Browserprobe abgeschlossen; vor dem Vortrag API-Verfügbarkeit erneut kurz kontrollieren. Gesicherte Screenshots als Ausweichmaterial bereitlegen.
- Ungültige Registrierungen serverseitig abweisen und passende Fehlerantworten liefern.
- Abgelaufene Anmeldung im Frontend durch Refresh oder erneute Anmeldung behandeln.
- Bundlegröße, Filterreset, mobile Darstellung, Aktualisierung des Frontendcaches und begrenzten Datenumfang verbessern.
- Auth- und vollständige Browserintegration bei späterer Implementierungsarbeit als Regression absichern.

Diese Dokumentation behauptet keine erfolgreiche vollständige Postman-Prüfung. Die echte Browser-/API-Demoprobe ist separat dokumentiert und deckt einen konkreten Desktopablauf ab; sie ersetzt keine umfassende Regression oder mobile Prüfung.

## Nachtrag: Konfigurationsbereinigung nach Commit 2d447a7

Der neue Stand wurde aus einem frischen, bereinigten Gitindex-Snapshot geprüft: Backendrestore/-build (0 Fehler, 28 Warnungen), Backendchecks einschließlich konfiguriertem API-Header/fehlendem Schlüssel, reguläre Registrierung/Login/Refreshrotation mit temporärer SQLite und `npm ci`/Frontend-Entwicklungsbuild bestanden. Setup erzeugt lokale Konfiguration und erhält vorhandene Werte. Umgebungsvariablen übersteuern die lokale Datei. Kein neuer Live-API-Abruf; RapidAPI-Rotation erfolgt durch den Nutzer. Details: [Konfigurationsbereinigung](konfigurationsbereinigung.md).

## Nachtrag: echte Probe mit neuer lokaler API-Konfiguration

Am 04.10.2026 wurde ein vom Nutzer bereitgestellter, vom veröffentlichten Wert abweichender RapidAPI-Schlüssel ausschließlich lokal eingetragen. Nach Backendneustart bestand die echte Desktop-Browserprobe: frischer Login, RapidAPI HTTP 200, 28 Angebote, Bezirk Ost (1), Detail `171201192`, Bildwechsel, Rückkehr und Logout/Guard. Die Kartenmarkierung verschwindet weiterhin beim Rückweg. [Prüfspur](demo/demoprobe-neue-konfiguration.json). Der Widerruf des alten Schlüssels bei RapidAPI wurde nicht bestätigt.

## Nachtrag: vollständige Karten- und Gebietsprüfung

Am 04.10.2026 wurden alle 23 amtlichen Stadtbezirke, 152 Stadtteile und 458 SVG-Zuordnungen abgeglichen. Die Zuordnungen waren vollständig richtig. Das Backend erkennt nun Stadtteilnamen als Ortsangabe und ordnet sie ihrem Bezirk zu; 189 Adressfälle und alle 23 Bezirksfilter bestanden.

Der Cursorfehler wurde durch überlagernde SVG-Dekorationen verursacht und mit durchgelassenen Mausereignissen behoben. In **Chrome und WebKit bestanden jeweils 152/152 echte Stadtteilklicks und 23/23 Legendenklicks**, einschließlich der Bezirke ohne Treffer. Alle Flächen zeigten Pointer, passende Hover-/Auswahlgruppen und korrekte Bezirksanfragen mit HTTP 200. [Vollständiger Bericht und Quellen](stadtbezirke-pruefung.md), [Browserprüfspur](demo/karten-vollpruefung.json).

[Prüfspur der neuen Live-Detailprobe](demo/demoprobe-kartenkorrektur.json): regulärer Login, externem RapidAPI HTTP 200 und 28 Angeboten, Ost (1), Detail mit 11 Bildern, geladenem Bildwechsel, Rückkehr mit weiterhin 8 markierten Ost-Flächen und Logout/Guard. **Die vorher dokumentierte verlorene Rückwegmarkierung ist jetzt behoben.** Historische Probeberichte beschreiben ihren jeweiligen damaligen Stand.

Frontendtests: **12/12** in zwei Dateien bestanden, einschließlich vier neuer Kartenregressionen. Angular-Entwicklungsbuild und Backendbuild bestanden. Das bekannte Produktionsbundlebudget und die bisher dokumentierten serverseitigen Validierungs-/Sitzungsgrenzen bleiben offen. WebKit ist ein Engine-Test, kein automatisierter Lauf der nativen Safari-App. Geometrische GIS-Vermessung und vollständige Markt-/Mobil-/Fehlerfallabdeckung wurden nicht durchgeführt.
