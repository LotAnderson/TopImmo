# TopImmo

TopImmo helps users browse rental apartments in Stuttgart by city district.

The Angular app lives in `Frontend/TomInnoFrondEnd`. The ASP.NET Core API lives
in `Backend/ImmscoutAPI` and handles registration, login, token refresh, and
protected listing requests. Account data is stored in its SQLite database,
`Backend/ImmscoutAPI/immoApp.db`; passwords are stored as BCrypt hashes.

## Run locally

Install Node.js 24 (including npm) and the .NET 9 SDK.

From the repository root, start the backend:

```bash
cd Backend/ImmscoutAPI
dotnet run --launch-profile http
```

In a second terminal, from the repository root, start the frontend:

```bash
cd Frontend/TomInnoFrondEnd
npm install
npm start
```

Open http://localhost:4200/register to create an account, then log in.
The frontend sends credentials to http://localhost:5197/api/auth/login;
the backend verifies them against the `Users` table and issues login tokens.

Apartment listings require a working RapidAPI key and access to the
ImmoScout24 API configured in `Backend/ImmscoutAPI/Service/ImmoScoutAPIService.cs`.

## Listing processing

The backend filters rental apartments, normalizes district names, calculates
district counts, and provides the SVG element-to-district mapping. Processed
listings are cached for five minutes. The frontend renders API results and
handles map selection, navigation, and image selection.

- `GET /api/realestate/stuttgart-listings?district=Mitte` returns `listings`,
  `districtCounts` for all loaded districts, and `mapDistricts`. Omit `district`
  for all loaded rentals. The upstream request currently loads only the first
  page, requesting at most 30 offers; these counts are not complete market totals.
  An empty search returns HTTP 200 with an empty listings array.
- `GET /api/realestate/listings/{id}` returns one rental listing or HTTP 404.

Both endpoints require authentication. The search response replaces the previous
listing-array response, so the frontend and backend must be updated together.

Run backend processing checks without a live RapidAPI request:

```bash
dotnet run --project Backend/ImmscoutAPI.Checks
```

The backend checks use fixture upstream responses and a test authentication
scheme, including real HTTP routing and JSON responses. They do not call
RapidAPI or validate production JWT configuration. Frontend component/HTTP
tests run with `npm test -- --watch=false` from the frontend directory.

## Projektunterlagen für die Präsentation am 05.10.2026

Die Unterlagen beschreiben den lokalen Stand vom 04.10.2026 einschließlich noch
nicht eingecheckter Änderungen. Die Lehrkraft hat die Weiterarbeit allein
akzeptiert; der geforderte Umfang bleibt unverändert.

- [Scrumboard](https://github.com/users/LotAnderson/projects/2)
- [Kontextübergabe für einen neuen Chat](docs/KONTEXT-UEBERGABE.md)
- [Backlog und Verlauf: 17.07.–02.10. sowie aktueller Stand](docs/backlog.md)
- [Vorbereitete Scrum-Nachträge und Statuskorrekturen](docs/scrum-nachtraege.md)
- [Retrospektive und Beitragsnachweise](docs/retrospektive.md)
- [Architektur, UML und SQLite-Datenmodell](docs/architektur.md)
- [Pseudocode wichtiger Algorithmen](docs/algorithmen.md)
- [Prüfungsergebnisse und ihre Grenzen](docs/pruefungen.md)
- [Echte Demoprobe und Sicherung (#37)](docs/demoprobe.md)
- [Offline-Screenshotansicht der erfolgreichen Probe](docs/demo/index.html)
- [Vortrag und Demo-Ablauf](docs/praesentation.md)
- [Lokale Präsentationsfolien](docs/praesentation.html)
- [Präsentations-PDF](docs/praesentation-2026-10-05.pdf)

Die HTML-Folien direkt im Browser öffnen; sie benötigen keine externen Assets.
Die Live-Demo benötigt das gestartete Produkt und funktionierenden API-Zugriff.
Der Entwicklungsbuild besteht; der Produktionsbuild überschreitet derzeit sein
Bundlebudget. Die echte Desktop-Browserprobe am 04.10.2026 bestand: Registrierung, Login,
28 Mietangebote, Bezirksauswahl, Details/Bildwechsel, Rückkehr und Logout.
Screenshots liegen für einen Ausweichablauf bereit; die private Sicherung
inklusive Arbeitskopie/Git/Datenbank liegt außerhalb des Repositorys.
Details und verbleibende Grenzen stehen in `docs/demoprobe.md`.

Der zuletzt geprüfte Boardstand vom 04.10.2026, ca. 16:08 Uhr, hat 33 Done- und
3 Backlog-Einträge. Nutzer/Copilot haben die Nachträge #34–#41 angelegt und die
Statuskorrekturen vorgenommen; der eigene Schreibversuch dieser Assistenz wurde
zuvor mit HTTP 403 abgewiesen. Verbleibende Metadaten und Zustandsabweichungen
sind in der Kontextübergabe beschrieben. Ältere Boardberichte enthalten
überholte Statusangaben. Historische Schätz-/Istzeiten werden nicht erfunden.
Der aktuelle Gesamtstand ist noch nicht im lokal sichtbaren Hauptbranch
nachgewiesen.
