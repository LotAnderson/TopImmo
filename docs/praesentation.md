# TopImmo – Präsentation am 05.10.2026

**Arbeitsstand dieser Vorbereitung: 04.10.2026.** Lokale Folien: [praesentation.html](praesentation.html), im Browser direkt öffnen. Pfeiltasten bzw. die Navigation wechseln Folien; **N** blendet Sprechnotizen ein; **P** öffnet den Druckdialog. Ausdruck als PDF: Querformat wählen. Die geprüfte achtseitige [PDF-Datei](praesentation-2026-10-05.pdf) liegt ebenfalls bereit. Die Folien benötigen weder Internet noch npm-Abhängigkeiten.

**Zeitannahme: 8–10 Minuten, hier auf 9 Minuten geplant.** Eine verbindliche Vorgabe zur Vortragsdauer wurde nicht genannt; Dauer an die Lehrkraft anpassen. Bei sechs Minuten Folie 2 und 4 kürzen sowie die Demo auf Login → Bezirk → Detail begrenzen. Bei zehn Minuten eine Rückfrage zur Architektur aufnehmen. Die Live-Demo benötigt separat das laufende Produkt und eine funktionierende externe API; die Foliendatei selbst benötigt beides nicht.

**Begleitunterlagen:** [Architektur](architektur.md), [Algorithmen](algorithmen.md), [Backlog](backlog.md), [Retrospektive](retrospektive.md), [Prüfungen](pruefungen.md) und [Scrum-Nachträge / Boardabgleich](scrum-nachtraege.md). Der Produktstand vom 02.10.2026 und spätere Änderungen werden getrennt beschrieben. Das Datum ist kein bestätigter Beginn der Alleinarbeit.

## Ablauf und Zeitbudget

| Folie | Inhalt | Zeit | Bis |
|---|---|---:|---:|
| 1 | Problem und Produktidee | 0:45 | 0:45 |
| 2 | Schulanforderungen und Umfang | 0:45 | 1:30 |
| 3 | Architektur und Datenspeicherung | 1:15 | 2:45 |
| 4 | Verarbeitung der API-Daten | 1:00 | 3:45 |
| 5 | Live-Demo bzw. Ausweichablauf | 2:00 | 5:45 |
| 6 | Prüfungen und ihre Grenzen | 1:15 | 7:00 |
| 7 | Zusammenarbeit, Scrum und eigener Beitrag | 1:15 | 8:15 |
| 8 | Stand, offene Punkte und nächste Schritte | 0:45 | 9:00 |

## Folie 1 – Wohnungen nach Bezirk finden

**Auf der Folie:** TopImmo; Mietwohnungen in Stuttgart nach Bezirk durchsuchen; Registrierung → Login → Karte/Übersicht → Details.

**Sprechnotiz, ungefähr 45 Sekunden:**

> „TopImmo ist ein Schulprojekt für Softwareentwicklung. Die Anwendung hilft dabei, geladene Mietwohnungsangebote in Stuttgart nach Stadtbezirk zu durchsuchen. Eine Karte und eine Liste mit Trefferzahlen erleichtern die Auswahl. Zu einem Angebot zeigt die Detailseite Bilder und wichtige Angaben. Das Projekt wurde zu zweit begonnen und wird inzwischen von mir weitergeführt. Ich zeige heute den aktuellen Produktstand, die Architektur, überprüfte Ergebnisse und die noch offenen Punkte.“

Nicht behaupten, dass alle Angebote in Stuttgart vollständig erfasst werden: Die aktuelle API-Anfrage lädt nur die erste Seite mit bis zu 30 Treffern.

## Folie 2 – Anforderungen und bewertbarer Umfang

**Auf der Folie:** C#-Backend; externe API; SQLite; Scrum und Dokumentation; Gewichtung 10 % Scrum / 40 % Implementierung / 10 % Präsentation / 40 % individuelle Leistung.

**Sprechnotiz, ungefähr 45 Sekunden:**

> „Die verpflichtenden technischen Bestandteile sind ein C#-Backend, eine externe API und SQLite. Die Oberfläche darf eine andere Sprache verwenden; hier wird Angular eingesetzt. Bewertet werden außerdem der Scrumprozess, Dokumentation und Präsentation sowie die eigene fachliche Leistung. Die Gewichtung stammt aus der Excel-Datei. Das Projekt hat dafür einen konkreten Immobilienablauf. Zusätzliche Funktionen wie Favoriten oder das Speichern von Wohnungspreisen wären Erweiterungen; sie werden im Anforderungs-PDF nicht als Pflicht genannt.“

Die Alleinarbeit wurde laut Nutzerangabe von der Lehrkraft akzeptiert. Daraus folgt keine automatisch geänderte Punkteverteilung. Die bereits eingetragenen 80 Punkte der Excel-Datei sind ohne bestätigte Zuordnung keine Bewertung dieses Projekts.

## Folie 3 – Architektur: Oberfläche, Backend, zwei Datenquellen

**Auf der Folie:** Komponentenbild mit zwei Backendwegen: Auth → SQLite und Immobilien → RapidAPI → Verarbeitung/Cache.

Vertiefung: [Architekturdokument](architektur.md).

```text
Browser: Angular
  Login/Register ── AuthService ─────────────┐
  Karte/Liste/Details ── RealEstateService ──┤ HTTP/JSON
  Guard + Interceptor: UI-Status/Bearer     │
                                           ▼
ASP.NET Core / C# (localhost:5197)
  AuthController ── AuthService ── EF Core ── SQLite
                  └─ TokenService             Users, RefreshTokens
  RealEstateController [Authorize]
       └─ DistrictDataService ── ImmoScoutAPIService ── RapidAPI
               └─ RAM-Cache (5 Minuten), Filter, Bezirke, Trefferzahlen
```

**Sprechnotiz, ungefähr 75 Sekunden:**

> „Angular übernimmt Formulare, Navigation, Kartenauswahl und Darstellung. Die HttpClient-Services senden JSON-Anfragen an das ASP.NET-Core-Backend. Die Auth-Controller verwenden einen Service und EF Core. SQLite speichert Benutzer mit Passwort-Hashes sowie Refresh-Tokens. Wohnungsangebote werden dagegen von einer externen API geladen und im Backend verarbeitet. Sie liegen derzeit im Arbeitsspeicher-Cache, nicht als Immobilientabelle in SQLite. Der Server schützt die Immobilienendpunkte mit Authorize; der Frontend-Guard allein wäre dafür keine Sicherheitsgrenze. Der Interceptor sendet den Access-Token mit. Ein vollständiger Refreshablauf im Browser ist noch offen.“

**Code für Rückfragen, keine geheimen Werte öffnen:**

| Aussage | Geeignete Stelle |
|---|---|
| Angular-Routen und Guard | [app.routes.ts](../Frontend/TomInnoFrondEnd/src/app/app.routes.ts), Zeile 8 |
| HTTP-Abfrage mit district | [realestate.ts](../Frontend/TomInnoFrondEnd/src/app/services/realestate.ts), Zeile 14 |
| Geschützte Immobilienendpunkte | [RealEstateController.cs](../Backend/ImmscoutAPI/Controllers/RealEstateController.cs), Zeile 8 |
| DI, EF Core/SQLite, Auth-Middleware | [Program.cs](../Backend/ImmscoutAPI/Program.cs), Zeilen 14, 20, 70 |
| Tatsächliche DB-Entitäten | [AppDbContext.cs](../Backend/ImmscoutAPI/DataBase/AppDbContext.cs), Zeile 11 |
| BCrypt und Tokenrotation | [AuthService .cs](../Backend/ImmscoutAPI/Service/AuthService%20.cs), Zeilen 29, 40, 75 |

## Folie 4 – Aus API-Angeboten werden Bezirksdaten

**Auf der Folie:** Laden → Mietwohnungen filtern → Bezirk normalisieren → zählen/sortieren → Auswahl filtern → JSON anzeigen; fünf Minuten Cache.

Vertiefung: [Algorithmen mit Darstellung](algorithmen.md).

**Sprechnotiz, ungefähr 60 Sekunden:**

> „Der wichtigste Verarbeitungsweg liegt in DistrictDataService. Er liest zunächst den Cache. Bei fehlenden Daten lädt er JSON, behält apartmentrent-Angebote und normalisiert die Bezirksnamen aus der Adresse. Anschließend gruppiert er nach Bezirk und berechnet Trefferzahlen. Ein ausgewählter Bezirk filtert die Angebotsliste; die Legende behält die Zahlen über alle geladenen Bezirke. Das Backend liefert auch die Zuordnung zwischen SVG-Elementen und Bezirken. Ein Cache über fünf Minuten und eine Sperre verhindern unnötige gleichzeitige API-Anfragen. Die Zahlen beschreiben die geladenen Treffer der ersten API-Seite, keine vollständige Marktstatistik.“

**Pseudocode passend zum vorhandenen Code:**

```text
wenn Cache vorhanden: verwende gespeicherte Mietangebote
sonst:
    Ladesperre abwarten, Cache erneut prüfen
    externe API laden und JSON lesen
    nur realEstateType == apartmentrent behalten
    für jedes Angebot Bezirk aus Adresse ableiten
    für 5 Minuten cachen; Ladesperre immer freigeben

Trefferzahlen = alle Mietangebote nach Bezirk gruppieren und zählen
Trefferzahlen nach Anzahl absteigend, dann Bezirk sortieren
Auswahl = alle Angebote oder nur gewählter Bezirk
liefere { listings: Auswahl, districtCounts: Trefferzahlen, mapDistricts }
```

**Code zeigen:** [DistrictDataService.cs](../Backend/ImmscoutAPI/Service/DistrictDataService.cs), Zeilen 22, 51, 62. Bei Rückfrage erklären: Ohne Adresse entsteht `Unknown`; die Adressauswertung ist eine einfache Komma-/Präfixlogik und kein allgemeiner Geocoder.

## Folie 5 – Live-Demo und verlässlicher Ausweichablauf

**Probe am 04.10.2026 bestanden:** Registrierung → Login → 28 Mietangebote → Bad Cannstatt (1) → Detail `144472924` mit Bildwechsel → Rückkehr → Logout. [Nachweis und Sicherung](demoprobe.md); [Offline-Screenshotansicht](demo/index.html). Die Daten können sich bis zum Vortrag ändern.

**Auf der Folie:** 1. Konto/Login; 2. Übersicht/Karte; 3. Bezirk; 4. Detail/Bilder; 5. Abmelden. Kennzeichnung: lokale Entwicklungsdemo; Internet/API erforderlich für aktuelle Angebote.

**Live-Demo, ungefähr zwei Minuten:**

1. Registrierung öffnen, ungültige E-Mail oder zu kurzes Passwort im Browser eingeben und die sichtbare Formularvalidierung zeigen. Danach gültiges vorbereitetes Demokonto anmelden. Falls ein neues Konto angelegt wird, eine neue Demoadresse verwenden, um einen Dublettenfehler nicht mit einem Ausfall zu verwechseln.
2. Übersicht zeigen: Karte, Legende und Angebotskarten. Zahl der tatsächlich geladenen Treffer benennen, keine feste Zahl versprechen.
3. Einen Bezirk mit Treffern in der Legende wählen. Zeigen, dass Karte/Legende und angezeigte Angebote zusammenhängen. Kein Zurücksetzen auf „alle“ versprechen: Dieser UI-Knopf fehlt derzeit.
4. „Zum Angebot“ öffnen; Titel, Preis, Fläche, Adresse und Bilder zeigen. Thumbnails nur vorführen, wenn dieses Angebot mehrere Bilder enthält.
5. Zur Übersicht zurückkehren und abmelden. Darauf hinweisen, dass die Legende den Filter behält; Kartenhervorhebung kann beim Rückweg verloren gehen.

**Sprechtext zum Einstieg:**

> „Die Demo läuft lokal mit dem Entwicklungsserver. Aktuelle Immobilien brauchen Internet, einen gültigen RapidAPI-Zugang und verfügbare API-Kapazität. Die Funktionsprüfung mit festen Testdaten zeige ich getrennt von diesem Live-Abruf.“

**Wenn die externe API oder das Netzwerk ausfällt:**

1. Fehlerzustand kurz zeigen und benennen: „Der Live-Abruf ist heute nicht verfügbar; daraus lässt sich der aktuelle Datenbestand nicht prüfen.“ Nicht mehrfach neu laden und nicht während des Vortrags API-Zugänge bearbeiten.
2. Zuerst die [gesicherten echten Demoansichten](demo/index.html) öffnen und als Aufnahmen vom 04.10.2026 kennzeichnen. Danach zur Testausgabe wechseln: vorhandene Backendchecks mit Fixtures und Frontendtests vorführen. Diese sichern Verarbeitung und HTTP-Verträge ohne Live-RapidAPI.
3. [listing-flow.spec.ts](../Frontend/TomInnoFrondEnd/src/app/listing-flow.spec.ts), Zeile 12, zeigt ausdrücklich Testdaten; Zeilen 37 und 69 zeigen Bezirksauswahl und Detailabruf. Backendchecks zeigen etwa Filter/Counts in [Program.cs](../Backend/ImmscoutAPI.Checks/Program.cs), Zeile 22. Testdaten als Fixtures benennen.
4. „Der Test belegt diese Abläufe mit kontrollierten Daten. Ein erfolgreicher Live-Gesamtablauf mit der externen API ist damit nicht bewiesen.“ Anschließend regulär mit Folie 6 fortsetzen.

Dieser Ausweichablauf ist eine Test-/Codevorführung. Eine offline mit Fixture-Daten laufende Produktoberfläche ist im Repo nicht eingerichtet.

## Folie 6 – Welche Qualität wurde überprüft?

**Auf der Folie:** acht Frontendtests bestanden; Backendbuild erfolgreich mit Warnungen; Backendchecks bestanden; Entwicklungsbuild erfolgreich; Produktionsbuild offen.

Vertiefung: [Prüfungen und Grenzen](pruefungen.md).

**Nachgewiesene Ergebnisse vom 04.10.2026:**

| Prüfung | Ergebnis | Aussagegrenze |
|---|---|---|
| `npm test -- --watch=false` | 8/8 Tests, 2 Dateien bestanden | Komponenten/Mock-HTTP; kein Browser-E2E, kein echter Authflow |
| `npm run build -- --configuration development` | Erfolgreich | Entwicklungsbuild; Initialbundle 4,43 MB |
| `npm run build` | Fehlgeschlagen | Produktionsbundle 1,71 MB überschreitet 1-MB-Budget |
| Backend vollständig neu kompiliert | 0 Fehler, 28 CS8618-Warnungen | Nicht initialisierte nicht-nullbare Modelleigenschaften offen |
| Vorhandene Backendchecks | Alle Verarbeitungs-/HTTP-Checks bestanden | Fixture-Upstream und Testauth; keine produktive JWT-Prüfung |
| Zusätzlicher isolierter Backend-HTTP-Test | Reguläre Registrierung/Login/Refresh, Dublette, falsches Passwort, Tokenrotation geprüft | Temporäre SQLite; kein Live-Immobilienabruf; kein bereits eingecheckter Auth-Regressionstest |
| Echte Desktop-Demoprobe | Registrierung/Login → Angebote → Bezirk → Details/Bilder → Logout bestanden | RapidAPI HTTP 200, keine Mocks; konkreter Ablauf, keine umfassende Regression. [Nachweis](demoprobe.md) |

**Sprechnotiz, ungefähr 75 Sekunden:**

> „Die vorhandenen Frontendtests laufen erfolgreich und sichern unter anderem Filter, Fehlerzustände und Details. Der Entwicklungsbuild funktioniert. Der Produktionsbuild scheitert dagegen am Bundlebudget; die große Inline-SVG-Karte ist die wahrscheinliche Hauptursache. Das Backend kompiliert ohne Fehler, aber mit 28 Nullability-Warnungen. Seine vorhandenen Checks prüfen Verarbeitung, JSON, Routing und den Schutz der Endpunkte mit Testauth. Zusätzlich wurde die echte Authentifizierung isoliert mit einer temporären Datenbank geprüft. Dabei wurde auch eine Lücke reproduziert: ungültige Registrierungsdaten werden serverseitig angenommen. Tests und Live-Demo ergänzen sich; der konkrete Desktopablauf mit echtem Login, API-Abruf und Bildwechsel wurde anschließend erfolgreich geprobt. Die Kartenmarkierung geht beim Rückweg verloren; umfassende End-to-End-Regression bleibt offen.“

**Befehle zum Vorführen, Backend ab Repo-Wurzel:**

```bash
dotnet build Backend/ImmscoutAPI/ImmscoutAPI.csproj --no-restore --no-incremental
dotnet run --project Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj --no-restore
```

**Frontend ab `Frontend/TomInnoFrondEnd`:**

```bash
npm test -- --watch=false
npm run build -- --configuration development
```

`--no-restore` setzt bereits verfügbare Pakete voraus. Bei neuer Maschine zunächst regulär restaurieren/installieren. Aktuelle Testausgaben vor der Präsentation erneut ansehen; frühere Resultate als Prüfstand vom 04.10.2026 benennen.

## Folie 7 – Von gemeinsamer Arbeit zur eigenen Fortführung

**Auf der Folie:** gemeinsamer Start → Übergabe/aktueller Stand → eigene Fortführung; individueller Beitrag benötigt Aufgabe, Datei/Commit, Ergebnis und Erklärung. Gewichtung: 40 % individuelle Kriterien.

Vertiefung: [Backlog](backlog.md), [Retrospektive](retrospektive.md), [heutiger Boardabgleich und Phasen](scrum-nachtraege.md). Der Zeitpunkt, ab dem allein gearbeitet wurde, ist nicht bestätigt. „Seit dem Stand vom 02.10.“ beschreibt Änderungen gegenüber diesem Stand und setzt keine frühere Feature-Autorenschaft fest.

**Sprechnotiz, ungefähr 75 Sekunden:**

> „Das Projekt wurde zu zweit begonnen und wird inzwischen von mir weitergeführt; die Alleinarbeit ist von der Lehrkraft akzeptiert. Für die Bewertung muss die frühere gemeinsame Arbeit ebenso nachvollziehbar bleiben wie die spätere eigene Leistung. Ich trenne deshalb Produktnachweis und persönliche Autorenschaft. Zu meinem Beitrag gehören nur Arbeiten, die ich anhand konkreter Aufgaben, Dateien oder Commits und ihres Ergebnisses bestätigen kann. Den ausgewählten Code erkläre ich fachlich. Aufgaben und Zeiten werden im Board und in der Retrospektive nachvollziehbar dokumentiert; fehlende historische Zeiten werden als unbekannt markiert.“

**Vor dem Vortrag persönlich bestätigen und konkretisieren:** Die folgende Tabelle ist eine Vorbereitung, keine behauptete Autorenzuordnung. Codebelege beweisen Produktbestand, nicht automatisch, wer ihn geschrieben hat. Ohne Bestätigung keine Zeile als eigene Implementierung darstellen.

| Möglicher Beitrag zur Auswahl | Produktbeleg | Persönlicher Nachweis vor Vortrag |
|---|---|---|
| Registrierung/Login/Refresh | AuthService im Backend; AuthService im Frontend | Eigene konkrete Aufgabe + Datum/Commit oder nachvollziehbarer Arbeitsnachweis; aktuell unbestätigt |
| API-Verarbeitung/Bezirksfilter | DistrictDataService | Eigener geänderter Algorithmusschritt + Ergebnis; aktuell unbestätigt |
| Karte, Liste und Detailseite | StutgartsmapComponent, RealEstateComponent, ListingDetailComponent | Eigene Komponentenänderung + Begründung; aktuell unbestätigt |
| Tests, Dokumentation und Übergabe | Vorhandene Checks und Dokumente | Eigene Prüfung/Erstellung/Überarbeitung und eingesetzte Unterstützung benennen; aktuell unbestätigt |

**Formulierung nach bestätigter Auswahl:** „Meine konkrete Aufgabe war … . In … habe ich … umgesetzt/geprüft. Der Nachweis ist … . Das Ergebnis ist … . Dabei habe ich gelernt/entschieden … .“ Historische Arbeit des Partners und eingesetzte KI-/andere Unterstützung nach tatsächlichem Arbeitsablauf benennen. Keine rückwirkenden Stunden erfinden. Wenn persönliche Zuordnung vor Vortrag unklar bleibt, ausdrücklich sagen, dass die Zuordnung noch offen ist, und den Produktcode nur fachlich erklären.

**Scrumquelle:** [öffentliches Projektboard](https://github.com/users/LotAnderson/projects/2). Letzter vollständig dokumentierter Boardstand vom 04.10.2026, ca. 16:08 Uhr: 36 Items, davon 33 Done und 3 Backlog. Diese Gruppensummen sind ein damaliger Schnappschuss; historische Zeitschätzungen oder tatsächliche Zeiten sind nicht belegt. Im Vortrag den dann aktuellen Stand öffnen, nicht diese historischen Zahlen als aktuelle Pflege verkaufen.

## Folie 8 – Was ist erreicht, was bleibt offen?

**Auf der Folie:** Produktweg vorhanden; C#/API/SQLite eingebunden; Tests für wesentliche Datenverarbeitung; nächste Schritte in Reihenfolge.

**Sprechnotiz, ungefähr 45 Sekunden:**

> „Der zentrale Produktweg und die technischen Pflichtbestandteile sind vorhanden. Für die Abgabe sind ein konsistenter aktueller Codebestand, Startanleitung, Scrumunterlagen und die nachvollziehbare eigene Leistung entscheidend. Die nächsten technischen Schritte sind das Produktionsbundle, serverseitige Eingabevalidierung, Sitzungsauslauf im Browser und robuste externe API-Konfiguration beziehungsweise Fehlerbehandlung. Danach folgen mobile Darstellung und Filterbedienung. Die heutigen Nachweise zeigen den funktionierenden Stand und seine Grenzen; offene Arbeiten werden transparent geführt.“

**Prioritäten, keine Behauptung bereits erledigter Fixes:**

1. Aktuellen lauffähigen Stand und Dokumente vollständig abgeben; Board, Architektur/Algorithmus und individuellen Beitrag nachvollziehbar halten.
2. Produktionsbundle reduzieren; Backendvalidierung ergänzen; 401-/Refresh-Verhalten im Frontend umsetzen; API-Zugang außerhalb des Quellcodes konfigurieren und API-Fehler kontrolliert behandeln.
3. Optionaler Produktfeinschliff: mobile Ansicht, „alle Bezirke“-Reset, Kartenhervorhebung beim Rückweg, Bildfallback und Tastaturbedienung.

## Vorbereitung heute / vor dem Vortrag

- Eigenbeitragstabelle wahrheitsgemäß konkretisieren und einmal laut erklären. Keine vollständige Solo-Autorenschaft aus der heutigen Alleinarbeit ableiten.
- Zwei Terminals und Browserfenster vorab öffnen; Startup/Tests nicht erst während der Präsentation installieren.
- Aktuelle Branch-/Arbeitsversion, Docs und Board auf Konsistenz prüfen. Der ursprüngliche Auditstand hatte untracked Backend-/README-/Testdateien; lokale Existenz allein beweist keine Abgabe auf main.
- Produkt im Entwicklungsmodus starten; Standardproduktionsbuild ist im geprüften Stand fehlerhaft.
- Echte Probe am 04.10. abgeschlossen. Vor dem Vortrag nochmals kurz API-/Internetstatus und ein Angebot prüfen; gespeicherte Ansichten als Ausweichmaterial geöffnet halten.
- Browserzoom für gut lesbare Demo einstellen; alternativ die Folien als PDF vorbereiten. Demopasswort, vollständige Tokens, Konfigurationsschlüssel und unredigierte API-Service-Datei nicht projizieren. Bei API-Codefrage stattdessen Aufruf/Verarbeitung in DistrictDataService zeigen.
- Offline erreichbare Testausgaben bereitlegen und den Ausweichablauf einmal üben. Acht Folien einmal mit Zeitmessung durchgehen.

## Lokaler Produktstart

Voraussetzungen laut Projekt: Node.js 24/npm und .NET 9 SDK; notwendige Pakete bereits installiert/restore durchgeführt. Die Produktdemo liest/schreibt die konfigurierte SQLite; für die Vorführung ein vorbereitetes Demokonto verwenden.

```bash
# Terminal 1 ab Repo-Wurzel
cd Backend/ImmscoutAPI
dotnet run --launch-profile http
```

```bash
# Terminal 2 ab Repo-Wurzel
cd Frontend/TomInnoFrondEnd
npm start
```

Produkt öffnen: `http://localhost:4200/register` bzw. `http://localhost:4200/login`. Backendziel: `http://localhost:5197/api`. Die Ausgabe am eigenen Rechner beachten; hier wird kein bereits laufender Server behauptet. SQLite-Migrationen werden vom Backend beim Start angewendet.

## Typische Rückfragen – kurze Antworten

| Frage | Fachlich belastbare Antwort |
|---|---|
| Warum SQLite? | Es ist verpflichtend und speichert in diesem Produkt Accounts/RefreshTokens über EF Core. Listings liegen nur im RAM-Cache. |
| Was wird aus der externen API verarbeitet? | Miettypfilter, Bezirksableitung, Zählung/Sortierung und Auswahlfilter; Backend liefert die SVG-Zuordnung. |
| Sind die Bezirkszahlen alle Wohnungen in Stuttgart? | Nein. Es sind Treffer im geladenen API-Datenbestand; aktuell nur erste Seite mit bis zu 30 Angeboten. |
| Was schützt die Daten? | Serverseitiges Authorize/JWT-Prüfung; Frontend-Guard unterstützt nur die Navigation. |
| Funktioniert Tokenrefresh automatisch? | Backendrotation wurde isoliert geprüft. Frontend-Refresh ist vorhanden, wird noch nicht automatisch verwendet. |
| Sind Formulare ausreichend validiert? | Im Browser gibt es Validierung; serverseitige Registrierung akzeptiert derzeit ungültige Eingaben. Das ist ein reproduzierter offener Fehler. |
| Was ist getestet? | Acht Frontendtests und vorhandene Backend-Verarbeitungs-/HTTP-Checks; zusätzlich isolierter Auth-Test. Zusätzlich erfolgreicher konkreter Desktopablauf mit echter API; keine umfassende Browser-E2E-Regression. |
| Was habe ich selbst gemacht? | Nur persönlich bestätigte Beiträge mit konkretem Nachweis nennen; gemeinsame/historische Arbeit und Unterstützung transparent lassen. |

Quellen für die Pflichtanforderungen und Gewichtung: `projektanforderungen-api-db.pdf` (Version 19.05.2026) und `bewertung.xlsx`. Die technischen Aussagen beschreiben den geprüften Projektstand vom 04.10.2026; historische Beiträge und Zeiten brauchen die jeweils angegebenen Nachweise.
