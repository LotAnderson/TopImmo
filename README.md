# TopImmo

TopImmo helps users browse rental apartments in Stuttgart by city district.

The Angular app lives in `Frontend/TomInnoFrondEnd`. The ASP.NET Core API lives
in `Backend/ImmscoutAPI` and handles registration, login, token refresh, and
protected listing requests. Account data is stored in its SQLite database,
`Backend/ImmscoutAPI/immoApp.db`; passwords are stored as BCrypt hashes.

## Run locally

Install Node.js 24 (including npm) and the .NET 9 SDK.

From the repository root, create local backend configuration:

```bash
node scripts/setup-local-config.mjs
```

This creates the ignored file `Backend/ImmscoutAPI/appsettings.Local.json`
and generates a random JWT signing key without printing it. Repeating the
command preserves existing values. Open this local file in your editor and
set `RapidApi.ApiKey` to your own working RapidAPI key for the ImmoScout24 API.
The versioned `appsettings.Local.example.json` is a safe configuration template.

The backend loads `appsettings.Local.json` after its regular configuration;
environment variables `Jwt__Key` and `RapidApi__ApiKey` take precedence over
the local file. Versioned `appsettings.json` keeps both keys empty. Startup
requires a JWT key of at least 32 UTF-8 bytes, a nonempty issuer, and a positive
`Jwt.ExpireMinutes`. A RapidAPI key is only required when loading live listings;
registration and login work without an external listing request.

Start the backend:

```bash
cd Backend/ImmscoutAPI
dotnet run --launch-profile http
```

In a second terminal, from the repository root, start the frontend:

```bash
cd Frontend/TomInnoFrondEnd
npm ci
npm start
```

Open http://localhost:4200/register to create an account, then log in.
The frontend sends credentials to http://localhost:5197/api/auth/login;
the backend verifies them against the `Users` table and issues login tokens.

On a fresh checkout, backend startup applies migrations and creates the local
SQLite database automatically. Local configuration, account databases and build
outputs are ignored by Git; existing local files remain on disk.

Apartment listings require internet access and access to the configured
ImmoScout24 API through RapidAPI. Keep API keys in local configuration or
environment variables.

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

To check a fresh checkout, restore and build the backend, run the fixture checks,
and install and build the frontend:

```bash
dotnet restore Backend/ImmscoutAPI/ImmscoutAPI.csproj
dotnet build Backend/ImmscoutAPI/ImmscoutAPI.csproj --no-restore
dotnet run --project Backend/ImmscoutAPI.Checks
cd Frontend/TomInnoFrondEnd
npm ci
npm run build -- --configuration development
```

After setup, also check registration, login, and refresh with the regular JWT
authentication against a separate temporary SQLite database. A passing fixture
check alone does not verify this authentication flow or live RapidAPI access.

## Projektunterlagen für die Präsentation am 05.10.2026

Die Unterlagen beschreiben den Stand vom 04.10.2026. Der Präsentationsstand
wurde als `2d447a7` auf dem öffentlichen Arbeitsbranch nachgewiesen; die
anschließende Konfigurationspflege ist ein eigener Nachtrag. Die Lehrkraft hat die Weiterarbeit allein
akzeptiert; der geforderte Umfang bleibt unverändert.

- [Scrumboard](https://github.com/users/LotAnderson/projects/2)

Die Projektunterlagen liegen lokal im Ordner `docs/`. Dieser Ordner ist bewusst
von Git ausgeschlossen und wird nicht mit einem neuen Checkout heruntergeladen.
Er enthält die Kontextübergabe, Präsentationsfolien/PDF, Demo-Prüfungen sowie
`notebooklm-projekttext.md` und die Anleitung zum Testen der Anwendung.
Die Unterlagen müssen bei Bedarf separat weitergegeben oder gesichert werden.

Die HTML-Folien direkt im Browser öffnen; sie benötigen keine externen Assets.
Die Live-Demo benötigt das gestartete Produkt und funktionierenden API-Zugriff.
Der Entwicklungsbuild besteht; der Produktionsbuild überschreitet derzeit sein
Bundlebudget. Die echte Desktop-Browserprobe am 04.10.2026 bestand: Registrierung, Login,
28 Mietangebote, Bezirksauswahl, Details/Bildwechsel, Rückkehr und Logout.
Screenshots liegen für einen Ausweichablauf bereit; die private Sicherung
inklusive Arbeitskopie/Git/Datenbank liegt außerhalb des Repositorys.
Details und verbleibende Grenzen stehen in `docs/demoprobe.md`.

Die Probe unter #37 gilt für den damals vorgeführten Stand. Anschließend wurden
die Schlüssel in lokale Konfiguration ausgelagert und der lokale JWT-Schlüssel
ersetzt. Die neue lokale API-Konfiguration wurde inzwischen ebenfalls live geprüft. Alle 152 Stadtteilflächen und 23 Bezirkslegenden bestanden echte Klickprüfungen in Chrome und WebKit; die Detailrückkehr erhält jetzt die Kartenmarkierung.
Der zuvor veröffentlichte Commit bleibt im Git-Verlauf. Den darin enthaltenen
RapidAPI-Schlüssel muss der Kontoinhaber bei RapidAPI ersetzen und den alten
Schlüssel widerrufen; die Auslagerung allein ersetzt ihn nicht. Die neuen
Startanweisungen stehen oben; HTML-Folien, PDF und NotebookLM-Lerntext beschreiben den aktualisierten Stand.

Der zuletzt geprüfte Boardstand vom 04.10.2026, ca. 16:08 Uhr, hat 33 Done- und
3 Backlog-Einträge. Nutzer/Copilot haben die Nachträge #34–#41 angelegt und die
Statuskorrekturen vorgenommen; der eigene Schreibversuch dieser Assistenz wurde
zuvor mit HTTP 403 abgewiesen. Verbleibende Metadaten und Zustandsabweichungen
sind in der Kontextübergabe beschrieben. Ältere Boardberichte enthalten
überholte Statusangaben. Historische Schätz-/Istzeiten werden nicht erfunden.
Der aktuelle Gesamtstand ist noch nicht im lokal sichtbaren Hauptbranch
nachgewiesen.
