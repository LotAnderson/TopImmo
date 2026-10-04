# Backlog und Rekonstruktion des Projektstands

Stand: **04.10.2026**. Präsentation: **05.10.2026**. Ob dieser Termin zugleich die endgültige Abgabe ist, ist nicht bestätigt.

[Öffentliches Scrumboard](https://github.com/users/LotAnderson/projects/2) · [Issues](https://github.com/LotAnderson/TopImmo/issues)

Dieses Dokument ergänzt die bisher kaum fortgeführte Planung durch einen **am 04.10.2026 erstellten Rückblick**. Es beschreibt den historischen Bestand, den Zeitraum **17.07.–02.10.2026** und die Weiterentwicklung **nach dem Stand vom 02.10. bis 04.10.2026**. Diese Zeiträume sind Rekonstruktionsabschnitte; sie belegen keine damals geplanten oder durchgeführten Sprints.

Die Lehrkraft hat laut Projektverantwortlichem die Weiterarbeit allein akzeptiert. Der Umfang der Anforderungen bleibt bestehen. Beiträge des bisherigen Partners und eigene spätere Arbeit werden getrennt dargestellt; eine zugesagte besondere Punkte- oder Notenregel liegt nicht vor.

## Wie die Nachweise zu lesen sind

- **Boardstatus:** öffentlich gelesener Zustand vor der Aktualisierung am 04.10.2026. Alle 28 vorhandenen Boarditems wurden am 17.07.2026 angelegt; ihre letzten sichtbaren Updates lagen ebenfalls auf diesem Datum. 16 standen auf Done, 9 auf Backlog und 3 auf Front End. Die historische Spalte bleibt erhalten, auch wenn das Board anschließend aktualisiert wird.
- **Commit:** belegt, dass ein bestimmter Quellcodestand zu diesem Commit gehört. Commitdatum ist kein nachgewiesener Arbeitstag und liefert keine Arbeitsdauer. Der Autor eines Commits ist kein sicherer Nachweis für die alleinige Autorschaft sämtlicher darin erstmals eingecheckter Dateien.
- **Implementiert:** die beschriebenen Elemente sind im Quellcode vorhanden. Dies bedeutet ohne gesonderten Prüfbeleg weder erfolgreichen Live-API-Abruf noch einen vollständig geprüften Browserablauf.
- **Lokaler Stand:** Änderungen gegenüber `4af8130` sind im Arbeitsverzeichnis vorhanden, aber noch nicht durch einen neuen Commit belegt. Ihr genauer Entstehungszeitpunkt und die Autorschaft einzelner Änderungen sind aus Git nicht bestimmbar. Die Einordnung in den zweiten Zeitraum verwendet den vom Projektverantwortlichen gewünschten Vergleichsstand vom 02.10.2026.
- **Zeit und Verantwortung:** bei sämtlichen 28 ursprünglichen Boarditems waren Estimate und Assignees leer. Kein Istzeitfeld, keine Zeiten in den Issuebeschreibungen bzw. im vorhandenen Kommentar. Historische Schätzzeit und tatsächlicher Aufwand bleiben **unbekannt**. Sekunden zwischen Erstellen und Schließen eines Issues sind keine Arbeitszeit. „Backend EM“ im Titel ist eine historische Kennzeichnung, keine vollständige Autoren- oder Stundenliste.

## Historischer Bestand bis zum ersten Review am 17.07.2026

Es existieren zwei unterschiedliche Implementierungsansätze. Der ältere Ansatz `Backend/ImmoScoutRapidApi` wurde unter der Git-Autoridentität **LotAnderson** eingecheckt. Der ASP.NET-/Angular-Ansatz erschien in Commits unter **Dr. Aly**. Diese Belege bewahren die frühere gemeinsame Arbeit. Sie dürfen nicht als ausschließlich neue Arbeit nach dem 17.07.2026 ausgewiesen werden.

### Älterer API-/SQLite-/Konsolenansatz

Die genannten Dateien sind in den historischen Commits bzw. auf dem lokal sichtbaren `main` nachweisbar. Das heutige Arbeitsverzeichnis verwendet das andere Backend unter `Backend/ImmscoutAPI`; es enthält keine aktuelle Umsetzung der gesamten JsonData-/Konsolenfunktionalität.

| Vorhandenes Issue | Boardstatus vor Aktualisierung | Nachweis und sachlicher Stand |
|---|---|---|
| [#5 Projektstruktur](https://github.com/LotAnderson/TopImmo/issues/5) | Done | Projekt-/Solutiondateien in `1fad2a5`; `/Api` und `/Model` in `c411b2a`. Historischer Quellcodebestand. |
| [#6 NuGet-Pakete](https://github.com/LotAnderson/TopImmo/issues/6) | Done | SQLite-Paketreferenz bereits in `1fad2a5`. Ein Commit belegt die Referenz, nicht den Zeitpunkt einer lokalen Paketinstallation. |
| [#7 SQLite-Anbindung](https://github.com/LotAnderson/TopImmo/issues/7) | Done | `JsonDataRepository`, Verbindung und Tabellenanlage in `c411b2a`. Im heutigen Ansatz wird SQLite über EF Core für Konten/Tokens genutzt. |
| [#9 JsonData-Tabelle](https://github.com/LotAnderson/TopImmo/issues/9) | Done | `CREATE TABLE JsonData` mit Id, City, Body und HouseUrl in `c411b2a`. Historischer Ansatz. |
| [#10 JsonData-Tabelle](https://github.com/LotAnderson/TopImmo/issues/10) | Done | Gleicher Titel und gleicher sichtbarer Funktionsnachweis wie #9. Keine getrennte zweite Leistung nachgewiesen; als historisches Duplikat kennzeichnen. |
| [#11 Migrationslogik für neue Spalten](https://github.com/LotAnderson/TopImmo/issues/11) | Done | `ALTER TABLE ... ADD COLUMN HouseUrl` in `c411b2a`. Gemeint ist hier Schemaanpassung im älteren Repository, keine EF-Core-Migration. |
| [#13 ImmoScoutApiClient](https://github.com/LotAnderson/TopImmo/issues/13) | Done | Klasse und externe HTTP-Anfrage in `c411b2a`. Historische Anbindung; aktuelle API-Erreichbarkeit dadurch nicht belegt. |
| [#14 QueryComposer](https://github.com/LotAnderson/TopImmo/issues/14) | Done | Dynamische URL mit Stadtparameter in `c411b2a`. Heutiger API-Service verwendet eine fest konfigurierte Stuttgart-Anfrage. |
| [#15 API-Antwort speichern](https://github.com/LotAnderson/TopImmo/issues/15) | Done | Insert mit City, Body und HouseUrl in `c411b2a`; passender Aufruf mit HouseUrl in `d178848` am 17.07. belegbar. Kein neuer Nachweis für Speicherung der heutigen Listings in SQLite. |
| [#16 Erste Haus-URL extrahieren](https://github.com/LotAnderson/TopImmo/issues/16) | Done | Methode `ExtractFirstHouseUrl` in `d178848`; später in `ae9043e` von `units` auf `listings` angepasst. Historische Funktion. |
| [#18 GetByCity](https://github.com/LotAnderson/TopImmo/issues/18) | Done | SQL-Abfrage und Methode in `c411b2a`. Im heutigen Backend keine entsprechende JsonData-Tabelle. |
| [#20 City/Body/HouseUrl ausgeben](https://github.com/LotAnderson/TopImmo/issues/20) | Done | City/Body-Ausgabe in `c411b2a`, HouseUrl beim Speichern in `d178848`. Die Abfrageausgabe `GetByCity` gibt im untersuchten Quellcode nicht zusätzlich HouseUrl aus; Titel ist daher nicht vollständig durch diesen Ablauf belegt. |
| [#21 Konsolendialog](https://github.com/LotAnderson/TopImmo/issues/21) | Done | Optionen Lesen/Anfragen/Beenden in `c411b2a`. Historischer Ansatz; kein Bestandteil des aktuellen Angular-Bedienablaufs. |

### Früher ASP.NET-/Angular-Ansatz

Die folgenden Grundlagen lagen überwiegend schon am 02.07. bzw. 16.07.2026 im Repository. Die heute vorgeschlagenen Statuskorrekturen sind ein **nachträglicher Bestandsabgleich**, keine Rückdatierung neuer Soloarbeit.

| Vorhandenes Issue | Boardstatus vor Aktualisierung | Belegcommit und heutiger Stand |
|---|---|---|
| [#8 Backend: Interfaces/Services/DI](https://github.com/LotAnderson/TopImmo/issues/8) | Backlog | Interfaces, Services und DI in `8a91593`; aktueller Bestand in [Program.cs](../Backend/ImmscoutAPI/Program.cs). Implementiert. |
| [#12 EF Core/SQLite](https://github.com/LotAnderson/TopImmo/issues/12) | Backlog | DbContext, SQLite-Konfiguration und Migrationen in `8a91593`; [AppDbContext](../Backend/ImmscoutAPI/DataBase/AppDbContext.cs). Implementiert und Konten-/Tokenpersistenz separat geprüft. |
| [#17 Controller](https://github.com/LotAnderson/TopImmo/issues/17) | Done | Controller in `8a91593`, Auth-/Schutzergänzungen in `23ceb19`. Der heutige Detail-/Suchvertrag enthält zusätzliche lokale Änderungen. |
| [#22 HTTP-Client](https://github.com/LotAnderson/TopImmo/issues/22) | Done | Im älteren Ansatz `c411b2a`, im ASP.NET-Ansatz `8a91593`. Die externe Anbindung ist vorhanden; aktueller Liveabruf nicht durch diese Commits geprüft. |
| [#23 Angularprojekt](https://github.com/LotAnderson/TopImmo/issues/23) | Front End | Angular-Dateien in `d59e6b3`. Ein Angularprojekt ist vorhanden; nachträgliche Statuskorrektur auf Done sachlich begründet. |
| [#24 JSON-Interfaces](https://github.com/LotAnderson/TopImmo/issues/24) | Front End | Listing/Address/Response-Interfaces in `d59e6b3`; Auth-Interfaces in `23ceb19`; `ListingSearchResult` ist eine zusätzliche lokale Änderung. Grundaufgabe implementiert. |
| [#25 HttpClient/Map-API-Brücke](https://github.com/LotAnderson/TopImmo/issues/25) | Front End | RealEstateService und DistrictFilterService in `d59e6b3`, spätere Anpassung in `23ceb19`. Aktueller Backendfilter-/Responsevertrag lokal weiterentwickelt. |
| [#26 Guard](https://github.com/LotAnderson/TopImmo/issues/26) | Done | Guard in `23ceb19`. Bekannte Grenze: prüft Token-Anwesenheit; automatische Reaktion auf Ablauf/401 ist eine separate offene Verbesserung. |
| [#27 Access-/RefreshToken und Speicherung](https://github.com/LotAnderson/TopImmo/issues/27) | Backlog | Generierung und Speicherung in `8a91593`, Refreshablauf in `23ceb19`. Backendgrundaufgabe implementiert und Rotation separat geprüft; automatische Frontenderneuerung offen. |
| [#28 Seiten und Routen](https://github.com/LotAnderson/TopImmo/issues/28) | Backlog | Home/Login/Registrierung/Details und Routen in `23ceb19`. Implementiert; nicht als erst nach Juli entstandene Seiten deklarieren. |
| [#30 Benutzer/Login/Sessions](https://github.com/LotAnderson/TopImmo/issues/30) | Backlog | Registrierung/Login in `8a91593`, JWT-/Refreshintegration in `23ceb19`. Grundablauf implementiert; aktuelle Architektur verwendet Tokens. Backendvalidierung und Sitzungsauslauf bleiben offen. |
| [#31 Endpoint anlegen](https://github.com/LotAnderson/TopImmo/issues/31) | Backlog | Controller und Listenendpoint in `8a91593`/`23ceb19`; zusätzlich Authcontroller im älteren Ansatz in `fa3f828`. Grundaufgabe implementiert; neue lokale Endpunkte gesondert dokumentieren. |

### Weiterhin offene ursprüngliche Aufgaben

| Vorhandenes Issue | Boardstatus vor Aktualisierung | Einordnung am 04.10.2026 |
|---|---|---|
| [#19 Alle Endpunkte mit Postman prüfen](https://github.com/LotAnderson/TopImmo/issues/19) | Backlog | Backend-/HTTP- und Frontendprüfungen sind vorhanden. Kein vollständiger früherer Postman-Durchlauf oder entsprechender Export nachgewiesen. Offen lassen oder die Prüfdefinition ausdrücklich auf dokumentierte HTTP-Prüfungen ändern; erst danach anhand der Abdeckung abschließen. |
| [#29 Preis-Speicherung](https://github.com/LotAnderson/TopImmo/issues/29) | Backlog | Aktuelle Listings samt Preisen werden im Speicher verarbeitet; SQLite speichert Konten/Tokens. Immobilien-/Preis-Persistenz ist keine ausdrückliche Pflicht aus dem PDF. Als geplante Erweiterung führen oder begründet aus dem aktuellen Umfang nehmen; keine Umsetzung behaupten. |
| [#32 UML-Diagramm](https://github.com/LotAnderson/TopImmo/issues/32) | Backlog | In der ursprünglichen Analyse fehlend. Für die Präsentation Architektur-/Datenmodellnachweis vorbereiten; Boardstatus erst nach vorhandenem und geprüftem Dokument auf Done setzen. |

## Zeitraum 1: 17.07.–02.10.2026

Die folgenden Änderungen sind durch Commits belegt. Alle genannten Commits dieses Zeitraums tragen die Git-Autoridentität **LotAnderson**. Tatsächlicher Aufwand, ursprüngliche Schätzung und genaue Arbeitstage sind unbekannt. Die alten offenen EF-/Token-/Seitenaufgaben aus dem vorherigen Abschnitt werden hier nicht als neue Implementierung gezählt.

| Beleg | Nachweisbare Änderung | Beziehung zum Backlog |
|---|---|---|
| `d178848` – 17.07.2026, „Bugfix“ | HouseUrl-Extraktion und Insert-Aufruf mit drei Argumenten im älteren Konsolenansatz. | Historische Ergänzung zu #15/#16. |
| `fa3f828` – 21.07.2026, „end point wurde erstellt“ | Älteres Projekt wird Webanwendung mit AuthController, Loginservice und Controller-Routing; Konsolendialog erhält Login/Sessionfunktion. | Ergänzung zu #30/#31 am älteren Ansatz; nicht identisch mit dem bereits vorhandenen ASP.NET-JWT-Login. |
| `ae9043e` – 28.09.2026, „test endpoint+ deduplicating“ | ListingIds-Tabelle und Duplikatbereinigung im JsonDataRepository; Datenbankstart-/Pfadkonfiguration; HouseUrl-Auswertung aus `listings`; `requests.http` mit GET-Anfrage. | Historische Datenqualität/HTTP-Prüfung. Die Datei mit einem GET-Aufruf beweist keine Prüfung aller Endpunkte und keinen Postman-Durchlauf. |
| `1a5a76a`, `a2686c5`, `7a7d838`, `a6955bd` – 28.09.2026 | Platzhalterdatei `front_end`, Merge-/Strukturcommits und Umbenennung nach `front/front_end`. | Repositoryorganisation; daraus keine neue fachliche Frontendfunktion ableiten. |
| `65d0d31` – 02.10.2026, „front_end“ | Backend- und Angular-Dateien werden nach `Frontend/ImmscoutAPI` bzw. `Frontend/TomInnoFrondEnd` verschoben; zahlreiche Dateien als R100 unverändert umbenannt, generierte Artefakte entfernt. | Strukturarbeit am übernommenen Gesamtprojekt, kein Beleg für erneute Implementierung dieser alten Funktionen. |
| `4af8130` – 02.10.2026, Merge | Letzter eingecheckter lokaler Vergleichsstand des aktuellen Branchs. | Baseline für Zeitraum 2; Merge allein ist kein zusätzlicher Fachfunktionsnachweis. |

Die lokal sichtbaren Branchs bleiben unterschiedlich: `main` enthält `Backend/ImmoScoutRapidApi`, `LotAnderson-patch-1` den ASP.NET-/Angular-Ansatz. Der Hauptbranch ist deshalb noch kein Nachweis für das aktuelle Gesamtprodukt. Die heutige lokale Umordnung nach `Backend/ImmscoutAPI` ist noch nicht in diesen Commits enthalten.

## Zeitraum 2: nach Stand 02.10.–04.10.2026

Vergleich: `4af8130` gegen das aktuelle Arbeitsverzeichnis. Die folgenden Arbeiten sind als **am 04.10.2026 nachträglich erfasste lokale Änderungen** zu dokumentieren. Sie haben bislang keinen Belegcommit. Der Projektverantwortliche arbeitet laut eigener Angabe inzwischen allein; die Autorschaft jeder einzelnen uncommitteten Änderung ist durch Git nicht gesichert. Arbeitsdauer, genaue Ausführungstage und ursprüngliche Zeitschätzungen bleiben unbekannt.

| Nachtrag | Gegenüber dem Vergleichsstand vorhandener Inhalt | Nachweis / Grenze |
|---|---|---|
| L1 Aktuelle Ordnerstruktur und Startanleitung | Backend jetzt unter `Backend/ImmscoutAPI`; Root-README beschreibt Start, Ports und Checks. | [README](../README.md); `git status` zeigt unversionierte Dateien und gelöschten früheren Backendpfad. Keine Behauptung eines bereits aktualisierten Hauptbranchs. |
| L2 Verarbeitung im Backend | Mietwohnungsfilter, Bezirksnormalisierung, Bezirkszahlen, Sortierung und Auswahl werden im Backend verarbeitet; fünf Minuten Cache. | [DistrictDataService](../Backend/ImmscoutAPI/Service/DistrictDataService.cs). Der alte Service aus `23ceb19` ordnete bereits Bezirke zu; der heute breitere Verarbeitungsschritt ist eine Weiterentwicklung. |
| L3 Neuer Such-/Detailvertrag | Suchantwort mit `listings`, `districtCounts` und `mapDistricts`; Bezirksparameter; Detailendpoint mit 404 bei unbekannter ID. | [RealEstateController](../Backend/ImmscoutAPI/Controllers/RealEstateController.cs), [ListingSearchResult](../Backend/ImmscoutAPI/Model/ListingSearchResult.cs). Aktueller API-Vertrag unterscheidet sich von der früheren Listenarray-Antwort. |
| L4 Frontend an Backendvertrag angepasst | Liste fragt den Bezirk beim Backend an; Karte nutzt dort berechnete Counts/Zuordnungen; Detailseite lädt per ID; Lade-, Leer- und Fehlerzustände. | [RealEstateService](../Frontend/TomInnoFrondEnd/src/app/services/realestate.ts), [Listenkomponente](../Frontend/TomInnoFrondEnd/src/app/component/realestate.component/realestate.component.ts). Live-Browserablauf bleibt gesondert zu prüfen. |
| L5 Automatisierte Verarbeitung-/HTTP-Prüfungen | Backendchecks mit Fixture-Upstream und Testauth; Frontendtests für Listen-/Filter-/Detailfluss. | [Backendchecks](../Backend/ImmscoutAPI.Checks/Program.cs), [Frontendtests](../Frontend/TomInnoFrondEnd/src/app/listing-flow.spec.ts). Vorhandene Prüfungen bestanden am 04.10.; keine echte RapidAPI-/vollständige Browserprüfung. Die Entstehung der Checks ist nicht mit Arbeitsstunden belegt. |
| L6 Aktuellen Stand analysiert | Vollständiger Backendbuild und isolierte Auth-/SQLiteprüfung sowie Frontendprüfungen wurden bei der Projektanalyse durchgeführt. | Bericht `TopImmo-Projektanalyse-2026-10-04.md` außerhalb des Repo. Diese vom Assistenzwerkzeug durchgeführten Prüfungen sind Prüfunterstützung am 04.10., keine eigenständig programmierte Schülerleistung und kein historischer Postman-Nachweis. |
| L7 Board, Dokumentation und Präsentation vorbereiten | Rückblick in zwei Zeiträumen, lokaler Backlog, Retrospektive, Architektur/Algorithmus und Vortrag für den 05.10.2026. | Dieser Backlog ist am 04.10. erstellt. Weitere Dokumente und Boardänderungen erst nach Fertigstellung/Verifikation als abgeschlossen markieren. Assistenzunterstützung bei Formulierung und Aufbereitung transparent benennen. |

## Fachliche Ziele und aktueller offener Umfang

Diese Ziele wurden **am 04.10.2026 zur heutigen Einordnung formuliert**, nicht als ursprüngliche User Stories ausgegeben.

| Ziel | Verknüpfte Aufgaben | Aktueller Stand / nächster Schritt |
|---|---|---|
| Als Wohnungssuchender möchte ich mich anmelden und geschützte Angebote öffnen. | #12, #17, #26, #27, #30 | Backendgrundablauf vorhanden und isoliert geprüft. Serverseitige Eingabevalidierung sowie sauberer Umgang mit abgelaufener Sitzung sind offen. |
| Als Wohnungssuchender möchte ich Stuttgarter Angebote nach Bezirk ansehen und Details öffnen. | #22–#25, #28, #31; L2–L4 | Implementiert und mit Testantworten geprüft. Liveabruf und reale Bedienung prüfen. Counts beziehen sich auf die geladenen maximal 30 Angebote der ersten API-Seite. |
| Als Prüfer möchte ich Architektur, Verarbeitung und persönliche Beiträge nachvollziehen können. | #32; L7 | Unterlagen vorbereiten, eigenen Anteil mit Belegen erklären, historische Beiträge erhalten. |

| Priorität bis zur Präsentation am 05.10. | Aufgabe | Abschlusskriterium |
|---|---|---|
| P0 | Präsentationsstand sichern und den normalen Ablauf im Entwicklungsmodus proben | Konkreter vorgeführter Stand benannt; Login, Bezirkswahl und Details im Browser ausprobiert. Erfolg bzw. API-Ausfall ehrlich dokumentiert. |
| P0 | Retrospektive/eigene Leistung erklären | Frühere Beiträge und eigene Weiterentwicklung mit Dateien/Commits getrennt; genehmigte Soloarbeit genannt; unbekannte Zeiten als unbekannt markiert. |
| P0 | Architektur und wichtigen Algorithmus bereitstellen | Komponentenbild/Datenmodell und Pseudocode vorhanden, mit tatsächlichem Code abgeglichen und erklärbar. |
| P0 | Board und Backlog nachvollziehbar aktualisieren | Nachträge vom 04.10. ausdrücklich datiert, ursprüngliche erledigte Arbeit erhalten, aktuelle offene Arbeit sichtbar. |
| P1 | Bekannte technische Grenzen einordnen | Produktionsbudget, Registrierungsvalidierung, Sitzungsauslauf und externe API-/Datenumfangsgrenzen auf der Offenliste. Kein ungesicherter Live-Erfolgsclaim. |

Weitere technische Arbeit: Produktionsbuild scheitert am Bundlebudget; Backend akzeptiert unzulässige Registrierungsdaten; Frontend nutzt Refresh nicht automatisch; API-Fehlerantworten, Filterreset und Pagination sind Verbesserungen am vorhandenen Kern. Für die lokale Präsentation kann der bereits kompilierbare Entwicklungsmodus genutzt werden. Eine vollständige Endabgabe mit aktuellem Hauptbranch/frischem Checkout bleibt eine zusätzliche Prüfung, falls sie tatsächlich verlangt wird.

## Fortführung ab diesem Nachtrag

Für neue Aufgaben werden Verantwortlicher, vor Arbeitsbeginn geschätzte Dauer, tatsächlich benötigte Zeit, Ergebnis und Beleg künftig geführt. Für die hier rekonstruierten alten Aufgaben werden diese Werte **nicht nachträglich erfunden**. Wenn sich frühere Zeiten aus echten Notizen belegen lassen, können sie mit Quelle und Kennzeichnung „nachträglich rekonstruiert“ ergänzt werden. Da bislang keine belastbaren Stundennotizen vorliegen, enthält dieser Backlog keine numerischen historischen Soll-/Istzeiten.

Empfohlene Korrektur der ursprünglichen Issues: #8, #12, #23, #24, #25, #27, #28, #30 und #31 als implementierten Bestand abschließen und separate bekannte Verbesserungen offen halten. #19 bleibt ohne vollständigen Prüfnachweis offen; #29 als optionale Erweiterung/Umfangsentscheidung führen; #32 nach geprüftem Architekturnachweis abschließen. Historische Done-Issues erhalten und ihren Ansatz kennzeichnen; #10 als Duplikat erläutern. L1–L7 als heutige Nachträge mit Bezug zu Vergleichsstand/Zeitraum erfassen, statt ihre Erstellungsdaten oder Arbeitsstunden zurückzudatieren.
