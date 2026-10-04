# Konfiguration und Gitstand bereinigt – 04.10.2026

Der öffentliche Commit `2d447a7` enthielt API-/JWT-Schlüssel, Kontodatenbank und Builddateien. Die Bereinigung entfernt diese Inhalte aus dem aktuellen Gitbaum; der frühere Verlauf wird nicht umgeschrieben.

## Änderung

- `.gitignore` gilt projektübergreifend für `bin`, `obj`, SQLite-Dateien und private Konfiguration. 95 bisher versionierte Dateien wurden ausschließlich aus der Git-Verfolgung entfernt; lokale Dateien bleiben vorhanden.
- `ImmoScoutAPIService` liest `RapidApi:ApiKey` aus der Konfiguration. Ohne Schlüssel wird vor einer HTTP-Anfrage ein verständlicher Konfigurationsfehler ausgelöst.
- Versionierte `appsettings.json` und die lokale Beispielvorlage enthalten keine Schlüsselwerte. Die ignorierte `appsettings.Local.json` enthält die privaten Werte; Umgebungsvariablen haben Vorrang. Die private Datei wird nicht in Build-/Publishausgaben kopiert.
- `node scripts/setup-local-config.mjs` erzeugt auf einer neuen Maschine einen zufälligen JWT-Schlüssel und erhält bereits konfigurierte Werte. Kein Schlüssel wird ausgegeben.
- Der lokale JWT-Schlüssel wurde ersetzt und bestehende lokale Refresh-Tokens wurden widerrufen, weil die veröffentlichte Datenbank diese enthielt. Benutzer und Passwort-Hashes bleiben erhalten. Für die Demo neu anmelden.

## Tatsächliche Prüfungen

Aus dem bereinigten Gitindex wurde ein eigener leerer Snapshot erzeugt. Er enthielt weder `bin`/`obj` noch Datenbank/private Konfiguration. Es wurde keine vorhandene `node_modules`-Installation hinein kopiert.

| Prüfung | Ergebnis |
|---|---|
| Setup auf frischem Snapshot | Neue Datei mit Berechtigung 0600, zufälliger JWT-Schlüssel, API-Feld leer; zweiter Aufruf erhält vorhandene Werte. |
| .NET-Restore und vollständiger Build | Bestanden; 0 Fehler, 28 bestehende CS8618-Warnungen. |
| Backendchecks | Bestanden; zusätzlich geprüft: konfigurierter API-Schlüssel wird als Header an den Fixture-Handler übergeben; fehlender Schlüssel löst keine HTTP-Anfrage aus. |
| Startup ohne JWT-Konfiguration | Klarer Startfehler; keine Datenbank angelegt. |
| Regulärer Backendstart | Eigene SQLite automatisch per Migration angelegt; unautorisierte Immobilienanfrage HTTP 401. |
| Registrierung und Login | Je HTTP 200; echte JWT-Ausgabe und HMAC-Signatur geprüft. |
| Konfigurationsvorrang | Signatur passt zum temporären Umgebungswert und nicht zum abweichenden Wert der lokalen Datei. |
| JWT-Prüfung | Falsche Signatur HTTP 401. Gültiger Token erreicht den Immobilienservice; bei absichtlich leerem API-Schlüssel HTTP 500 mit Konfigurationsfehler, ohne externen Abruf. |
| Refreshrotation | HTTP 200; alter RefreshToken danach HTTP 401. |
| Temporäre SQLite | Integritätsprüfung bestanden; produktive Benutzer nicht durch diese isolierten Authprüfungen verändert. |
| Frisches `npm ci` und Entwicklungsbuild | Bestanden; Initialbundle 4,43 MB. |
| Gitindex-Prüfung | Keine Datenbank-/Builddateien; kein lokaler oder zuvor eingebetteter Schlüsselwert in Text- oder UTF-16-Binärform. |

Die Authprüfung war ein zusätzlicher Auditablauf, kein neuer eingecheckter Auth-Regressionstest. Die bestehende echte Live-Demoprobe dokumentiert den früheren Stand. Für den neuen Konfigurationsstand wurde keine neue RapidAPI-Live-Anfrage ausgeführt. Der bekannte Produktionsbundlefehler wurde durch diese Bereinigung nicht behoben.

## Noch beim Anbieter erforderlich

Der RapidAPI-Schlüssel wurde privat ausgelagert, aber nicht durch einen neuen Provider-Schlüssel ersetzt. Der Nutzer hat angekündigt, ihn nach der Bereinigung zu ersetzen. Den neuen Schlüssel direkt in `Backend/ImmscoutAPI/appsettings.Local.json` unter `RapidApi.ApiKey` eintragen und den veröffentlichten alten Schlüssel bei RapidAPI widerrufen. Danach das Backend neu starten und die Live-Demo kurz prüfen.

Das Entfernen aus dem aktuellen Baum löscht veröffentlichte Werte und die alte Datenbank nicht aus früheren Commits. Die Sicherung vor der Änderung liegt privat außerhalb des Repositorys unter `TopImmo-Sicherungen/2026-10-04-vor-konfigurationsbereinigung`.
