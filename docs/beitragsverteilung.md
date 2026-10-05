# TopImmo – Beiträge nach GitHub-Commits

Stand: **05.10.2026**. Geprüfte Grundlage ist der Hauptbranch `main` auf [b02e4dc5](https://github.com/LotAnderson/TopImmo/commit/b02e4dc54cfb59650d9cf92df499ccbbd3093750). Die Zuordnung beruht auf GitHub-Autoridentitäten, tatsächlichen Dateiänderungen und dem Vergleich früherer Dateien mit dem heutigen Quellstand.

## Personen und Funktionen

| Name | GitHub-Nickname | Funktionen und Beiträge |
|---|---|---|
| **Edhar Myronchuk** | [LotAnderson](https://github.com/LotAnderson) | Früherer Backend-Prototyp mit RapidAPI-Anbindung und JSON-Speicherung; spätere Projektintegration, Such-/Detailvertrag zwischen Frontend und Backend, Prüfprogramme, lokale Konfiguration sowie Bezirks- und Kartenkorrekturen. Fortführung und Präsentationsvorbereitung, im Oktober mit dokumentierter KI-Unterstützung. |
| **Toktoraly Oskonbaev** | [Toktoraly](https://github.com/Toktoraly) | Grundlage des heutigen Angular-Frontends und C#-Backends: Immobilienliste, Karte und Detailseite; Login/Registrierung, JWT und Refresh-Tokens, Angular-Guard/Interceptor sowie SQLite/EF Core für Konten und Sitzungsdaten. |

Die Tabelle beschreibt die unter den jeweiligen Accounts gespeicherten Beiträge. Ein Commit beweist weder alleinige Programmierarbeit noch Entwicklung ohne KI. Konkrete Oktoberarbeiten mit Agentenunterstützung sind im [KI-Bericht](ki-einsatz.md) ausgewiesen.

## Funktionale Zuordnung mit Belegen

| Person / Account | Funktion | Nachweisbarer Beitrag | Commit und Datum |
|---|---|---|---|
| Edhar / `LotAnderson` | Früherer Backend-Prototyp | Eigene Projektbasis, RapidAPI-Client und URL-Zusammenstellung, Speicherung externer JSON-Antworten in SQLite; später Endpunkte, einfacher Login mit Session-ID und Duplikatbehandlung. Dieser historische Ansatz wurde durch den heutigen Backendansatz ersetzt. | [1fad2a5a](https://github.com/LotAnderson/TopImmo/commit/1fad2a5ab3b01dbbe4814895d45448981b1279ca), 30.06.; [c411b2af](https://github.com/LotAnderson/TopImmo/commit/c411b2afc10834b2b62039abd4ecb442996fd675), 16.07.; [fa3f8289](https://github.com/LotAnderson/TopImmo/commit/fa3f82896423e4d815fcccb142fa73280fa53dbd), 21.07.; [ae9043e4](https://github.com/LotAnderson/TopImmo/commit/ae9043e4c9a68eae428854145c3515094f3da74d), 28.09. |
| Toktorally / `Toktoraly` | Grundlage des heutigen Backends | Externer API-Adapter, JSON-Deserialisierung, Datenmodelle und erste Bezirkszuordnung; EF-Core-Kontext, SQLite-Migrationen, Benutzer/RefreshTokens, BCrypt und Token-Service. | [8a91593b](https://github.com/LotAnderson/TopImmo/commit/8a91593b93a25ec4059aff5ff22a59c5abed3dca), 02.07. |
| Toktorally / `Toktoraly` | Angular-Grundlage | Erster Angular-Import mit Immobilienliste, SVG-Stuttgartkarte, Hover/Auswahl, Bezirkfilter und HTTP-Service. | [d59e6b38](https://github.com/LotAnderson/TopImmo/commit/d59e6b38695b7721efbaa93bf2088bb69739795f), 02.07. |
| Toktorally / `Toktoraly` | Sicherheit im Backend | Login-/Register-/Refresh-Ablauf vervollständigt; Refresh-Token-Rotation, `AddJwtBearer`, Authentifizierungs-Middleware und `[Authorize]` für Immobilienendpunkte. | [23ceb19d](https://github.com/LotAnderson/TopImmo/commit/23ceb19d2f4d4f19c95968fe9e5412fae77553d7), 16.07. |
| Toktorally / `Toktoraly` | Sicherheit und Details im Frontend | Login-/Registrierungsseiten, AuthService mit lokalem Token-Speicher, Guard, Bearer-Interceptor und geschützte Routen; eigene Detailseite mit Bilderauswahl und Frontend-Zwischenspeicher. | [23ceb19d](https://github.com/LotAnderson/TopImmo/commit/23ceb19d2f4d4f19c95968fe9e5412fae77553d7), 16.07. |
| Edhar / `LotAnderson` | Projektstruktur und Integration | Angular-Projekt und bestehendes Backend reorganisiert. Viele Dateien wurden unverändert verschoben; die Verschiebung überträgt ihre ursprüngliche Implementierung nicht auf den neuen Commitautor. | [65d0d315](https://github.com/LotAnderson/TopImmo/commit/65d0d315d904320ba952024a1054af576b5b8036), 02.10.; [2d447a7b](https://github.com/LotAnderson/TopImmo/commit/2d447a7b17d72a2f7b36fdb03ba70b0d6b211037), 04.10. |
| Edhar / `LotAnderson` | Suche, Details und gemeinsamer Datenvertrag | Aktuelles `ListingSearchResult` mit Angeboten, Bezirkszahlen und Kartenzuordnung; Bezirkfilter als Query-Parameter und Detailendpunkt. Backendcontroller mit gemeinsamem 5-Minuten-Cache und Semaphore; Angular-Service, Lade-/Fehlerzustände und Prüfungen abgestimmt. Dieser Stand ist erstmals in diesem Integrationscommit gespeichert; der genaue Einzelautor jeder zuvor lokalen Änderung ist nicht überliefert. | [2d447a7b](https://github.com/LotAnderson/TopImmo/commit/2d447a7b17d72a2f7b36fdb03ba70b0d6b211037), 04.10. |
| Edhar / `LotAnderson`, mit Codex-Agent | Lokale Konfiguration | Private Konfiguration ausgelagert, sichere Vorlage und Setup-Skript ergänzt, Startvalidierung erweitert; lokale Datenbanken und Builddateien aus der laufenden Versionierung entfernt. | [bd61d774](https://github.com/LotAnderson/TopImmo/commit/bd61d7747864498bf1fcb4e42b8d07e1cc410517), 04.10. |
| Edhar / `LotAnderson`, mit Codex-Agent | Bezirkszuordnung und Kartenbedienung | Kanonische Referenz für 23 Bezirke/152 Stadtteile, Adressnormalisierung und Prüfungen; Pointer-/Klickfehler dekorativer SVG-Ebenen behoben, alle Bezirke in der Legende verfügbar, Tastaturbedienung und Markierung nach Detailrückkehr verbessert. | [60d91eec](https://github.com/LotAnderson/TopImmo/commit/60d91eec47d65c8823a9aa7621c0f42bb6c9cfbd), 04.10. |
| Edhar / `LotAnderson`, mit Codex-Agent | Birkach-Süd | Nach Edhars Fehlerberichten Grundfarbe und dicke Bezirksgrenze Birkach/Plieningen korrigiert; Kartenregression und Browser-Geometrie geprüft. | [20a7b374](https://github.com/LotAnderson/TopImmo/commit/20a7b374dc9855c0881c2f00f54d9473a020c0a6), [e6567da8](https://github.com/LotAnderson/TopImmo/commit/e6567da8dc165339783c86ae98f7f6a8e14ca7de), 04.10. |

Alle Datumsangaben beziehen sich auf **2026** und den gespeicherten Commitstand. Der KI-Bericht beginnt nach Nutzerangabe **Ende Juli**; die früheren Juli-Beiträge stehen hier zusätzlich, damit die übernommene Grundlage sichtbar bleibt.

## Herkunft unverändert übernommener Dateien

Der Inhaltsvergleich ist aussagekräftiger als die Anzahl späterer Commits. Folgende Dateien stimmen auf dem geprüften `main` inhaltlich mit Toktoralys Juli-Versionen überein:

| Aktuelle Datei | Ursprünglicher Stand |
|---|---|
| [TokenService](<../Backend/ImmscoutAPI/Service/TokenService .cs>) und [AppDbContext](../Backend/ImmscoutAPI/DataBase/AppDbContext.cs) | `8a91593b`, 02.07. |
| [AuthService im Backend](<../Backend/ImmscoutAPI/Service/AuthService .cs>) | `23ceb19d`, 16.07. |
| [AuthService in Angular](../Frontend/TomInnoFrondEnd/src/app/services/auth.ts), [Guard](../Frontend/TomInnoFrondEnd/src/app/guards/auth.guard.ts) und [Interceptor](../Frontend/TomInnoFrondEnd/src/app/interceptors/auth.interceptor.ts) | `23ceb19d`, 16.07. |
| [Login](../Frontend/TomInnoFrondEnd/src/app/pages/login.component/login.component.ts), [Registrierung](../Frontend/TomInnoFrondEnd/src/app/pages/register.component/register.component.ts) und [Routen](../Frontend/TomInnoFrondEnd/src/app/app.routes.ts) | `23ceb19d`, 16.07. |

Die Backenddateien wurden am 02.10. nach `Frontend/ImmscoutAPI` und anschließend nach `Backend/ImmscoutAPI` verschoben. Git erkennt die genannten Verschiebungen als **R100**, also als unveränderte Dateien. Daraus folgt: Edhars Integrationscommit enthält diese Funktionen, ihre ursprüngliche Implementierung bleibt Toktoraly zugeordnet.

Beim JSON-/Datenbankthema sind zwei Entwicklungsstände zu unterscheiden: Edhars früherer Prototyp speicherte Immobilien-JSON in SQLite. Das aktuelle `AppDbContext` aus Toktoralys Grundlage speichert **Konten und Refresh-Tokens**; die aktuell abgerufenen Immobilienangebote liegen im **RAM-Cache**. Die ursprüngliche Session-ID im Prototyp ist ebenfalls von der heutigen JWT-/Refresh-Implementierung zu unterscheiden.

## Aussagegrenzen und Dokumentation

Commitmetadaten und Dateiänderungen belegen gespeicherte Stände. Sie belegen keine vollständige Verteilung früherer Paararbeit, keine Arbeitsstunden und keine Prozentanteile. Import-, Verschiebe- und Merge-Commits werden deshalb nicht als vollständige Neuprogrammierung gewertet. Der Integrationscommit `2d447a7b` bündelt zuvor lokalen Bestand und vorbereitete Unterlagen; seine Zeilen lassen sich nicht pauschal einer einzelnen Person oder einem KI-Werkzeug zuordnen.

Die Liveprobe und Sicherung sind in [Issue #37](https://github.com/LotAnderson/TopImmo/issues/37) dokumentiert. Architektur-/Präsentationsvorbereitung mit Assistenzunterstützung wird in [Issue #40](https://github.com/LotAnderson/TopImmo/issues/40) und [Issue #39](https://github.com/LotAnderson/TopImmo/issues/39) ausdrücklich genannt. Die Historienverbindung [3ca57306](https://github.com/LotAnderson/TopImmo/commit/3ca57306c001a0686a622b1e1c673beea887033a) und der anschließend vom Nutzer gemergte [PR #42](https://github.com/LotAnderson/TopImmo/pull/42) betreffen die Übernahme des fertigen Stands, nicht die ursprüngliche Implementierung sämtlicher enthaltenen Dateien.

Die vier zusätzlichen UML-Klassenansichten und dieser Beitragsbericht wurden am 05.10. mit Codex-Unterstützung lokal erstellt. Sie sind beim Erstellen dieses Berichts noch nicht als neue Commits veröffentlicht. Details zum Einsatz der KI: [ki-einsatz.md](ki-einsatz.md). Technischer Aufbau: [architektur.md](architektur.md).
