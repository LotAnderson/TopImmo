# Echte Demoprobe und Sicherung – #37

**04.10.2026, 16:23–16:24 Uhr (Europe/Berlin). Ergebnis: bestanden.** Die Probe lief automatisiert in Google Chrome gegen das echte Angular-Frontend auf Port 4200 und das ASP.NET-Core-Backend auf Port 5197. Es wurden keine HTTP-Antworten ersetzt und keine Fixture-Daten verwendet. Ein neuer Browserkontext hatte zunächst keine gespeicherte Anmeldung.

## Geprüfter Ablauf

| Schritt | Tatsächliches Ergebnis |
|---|---|
| Formularvalidierung | Ungültige E-Mail und Ein-Zeichen-Passwort zeigen zwei Fehler; keine Registrierungsanfrage abgeschickt. |
| Gültige Registrierung | HTTP 200; Weiterleitung zum Login. Zwei synthetische Demokonten wurden im Verlauf der Probe in der lokalen SQLite angelegt. |
| Frischer Login | HTTP 200; Access-/RefreshToken werden im Browser gespeichert; geschützte Übersicht erreichbar. |
| Echte externe Anfrage | Backend vor der Probe neu gestartet. HttpClient-Log belegt RapidAPI-Antwort HTTP 200; anschließend Suchantwort HTTP 200. |
| Übersicht | 28 Mietangebote angezeigt. Die Zahlen beschreiben die damals geladene erste API-Seite, keine vollständige Marktstatistik. |
| Bezirksauswahl | „Bad Cannstatt“: ein Angebot, Suchantwort HTTP 200; Legende aktiv und 18 SVG-Pfade markiert. |
| Detail | Angebot `144472924`: HTTP 200, „Home & Co – Easy Living \| Möbliertes All-Inclusive Wohnen“, 709 €, 13 m², ein Zimmer, fünf Bilder. |
| Bildwechsel | Zweites Thumbnail gewählt; Hauptbild-URL geändert, Bild erfolgreich geladen und Thumbnail aktiv. |
| Rückkehr | Ein Angebot und aktiver Bezirksfilter bleiben erhalten; Kartenmarkierung verschwindet (0 markierte Pfade). |
| Abmelden | Weiterleitung zum Login; beide Token aus localStorage entfernt. Erneuter Aufruf von `/` führt wieder zum Login. |

Es wurden keine JavaScript-Seitenfehler erfasst. Der erste Durchlauf prüfte das Bild unmittelbar nach dem Klick und wartete nicht auf Angulars Aktualisierung. Dieser Prüfablauf wurde korrigiert; die anschließende Loginprobe und ein vollständiger neuer Durchlauf bestanden. Der Produktcode wurde dafür nicht verändert. Die ursprüngliche Prüfspur ist als [erster Durchlauf](demo/demoprobe-erster-durchlauf.json) erhalten, der maßgebliche erfolgreiche Nachweis steht in [demoprobe.json](demo/demoprobe.json).

## Material für die Präsentation

- [Offline-Screenshotansicht](demo/index.html): direkt öffnen; Pfeiltasten oder Vor/Zurück verwenden. Das sind gespeicherte echte Demoansichten, keine offline laufende Produktoberfläche.
- [Übersicht](demo/02-uebersicht.png), [Bezirksauswahl](demo/03-bezirksauswahl.png), [Details](demo/04-details.png), [Bildwechsel](demo/05-bildwechsel.png).
- [Folien](praesentation.html) und [PDF für die Präsentation](praesentation-2026-10-05.pdf).

Vor dem Vortrag ein neues Browserfenster öffnen bzw. die Seite neu laden, dann das vorbereitete Konto anmelden. „Bad Cannstatt“ und das Angebot oben sind geeignete Beispiele, solange der externe Datenbestand unverändert ist. Nach Ablauf des fünfminütigen Backendcaches kann eine andere Antwort eintreffen; morgen die Übersicht kurz erneut prüfen. Zugangsdaten liegen privat unter `/Users/edhar_myronchuk/Documents/TopImmo-Sicherungen/demokonto.json`; Passwort und Tokens nicht projizieren.

Bei Netz-/API-Ausfall zur Screenshotansicht wechseln: „Diese Ansichten wurden bei der erfolgreichen Liveprobe am 04.10. gespeichert.“ Anschließend die kontrollierten Fixture-Tests getrennt erklären. Das heutige Ergebnis garantiert keine morgige API-Verfügbarkeit.

## Gesicherter Projektstand

Branch: `LotAnderson-patch-1`. Git-Basis: `4af8130`. Die Produktquellen blieben während dieser Probe unverändert; ergänzt wurden Demokonten, Nachweise und Präsentationsmaterial. Es wurde kein Commit, Push oder Merge ausgeführt.

Die private Sicherung vor der Probe liegt unter:

```text
/Users/edhar_myronchuk/Documents/TopImmo-Sicherungen/2026-10-04_16-22-31-vor-demoprobe
```

Die abschließende Sicherung inklusive aktualisierter Unterlagen liegt unter:

```text
/Users/edhar_myronchuk/Documents/TopImmo-Sicherungen/2026-10-04-praesentationsstand
```

Jede Sicherung enthält die vorhandenen versionierten und unversionierten Dateien sowie `.git`, einen separat geprüften Git-Bundle, Gitstatus, Binärdiff und ein SHA-256-Manifest. Ignorierte Abhängigkeiten sind nicht Bestandteil der Sicherung. SQLite wurde bei laufendem Backend mit der SQLite-Backupfunktion konsistent kopiert. Archivdateihashes und Datenbankintegrität wurden geprüft; die abschließende Sicherung wurde zusätzlich in einen leeren Ordner entpackt und mit Gitstatus und SQLite geprüft. Die Sicherungen enthalten private Konfiguration und Datenbank und gehören nicht ins öffentliche Repository.

Wiederherstellung: `arbeitskopie.tar.gz` in einen **neuen leeren Ordner** entpacken; `HINWEIS.txt` und `manifest.json` daneben erläutern den Stand. Bei Bedarf anschließend `npm ci` im Frontend und `dotnet restore Backend/ImmscoutAPI/ImmscoutAPI.csproj` ausführen. Die ursprüngliche Arbeitskopie dabei erhalten.

## Verbleibende Grenzen

Die erfolgreiche Probe deckt diesen konkreten Desktopablauf ab. Produktionsbuild, serverseitige Eingabevalidierung, automatischer Tokenrefresh, mobile Darstellung, Filterreset und Kartenmarkierung beim Rückweg bleiben offen. 14 der 28 Angebote wurden lediglich „Stuttgart“ zugeordnet; dieser Sammelwert ist kein Stadtbezirk und lässt sich nicht als einzelner Bezirk auf der Karte hervorheben. Die Daten aus der externen Quelle enthalten teilweise ältere Veröffentlichungsangaben.

Die Veröffentlichung des aktuellen Gesamtprodukts auf `main` ist weiterhin nicht nachgewiesen. #37 betrifft die dokumentierte Probe und lokale Sicherung; daraus folgt kein Abschluss von #19 oder #29.

## Online-Abgleich

Issue #37 wurde vor der Änderung frisch gelesen. Der Versuch, die Ergebnisse einzutragen, LotAnderson zuzuweisen und als completed zu schließen, wurde von GitHub mit HTTP 403 (`Resource not accessible by integration`) abgewiesen. Issue und Board bleiben online unverändert. Der [fertige Abschlusstext](issue-37-abschluss.md) kann manuell übernommen werden. Es wurden keine neuen Issues angelegt.
