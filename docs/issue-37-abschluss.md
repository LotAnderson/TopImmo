# Vorbereiteter Abschluss für GitHub-Issue #37

Die echte Probe und lokale Sicherung sind erledigt. Der Schreibversuch dieser Sitzung scheiterte am 04.10.2026 mit HTTP 403: `Resource not accessible by integration`. Issue #37 bleibt online offen; der Projects-V2-Status wurde nicht geändert.

Für die manuelle Pflege: Beschreibung durch den folgenden Text ersetzen, LotAnderson zuweisen, als completed schließen und im Board auf Done setzen. Das ist eine vorbereitete Änderung, kein Nachweis ihrer Onlineausführung.

---

## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Vor der Präsentation frisch anmelden, Übersicht laden, Bezirk auswählen, Details/Bilder ansehen und Rückkehr prüfen. Erfolg oder Ausfall dokumentieren. Bei Erfolg Screenshots/kurze Aufnahme sichern; aktuellen vorgeführten Stand eindeutig benennen.

### Nachweise und Grenzen

Am 04.10.2026 um 16:23–16:24 Uhr (Europe/Berlin) erfolgreich geprobt: echte Registrierung und frischer Login, Übersicht mit 28 Mietangeboten, Bezirksauswahl Bad Cannstatt (1), Detail 144472924, geladene Bilder und Bildwechsel, Rückkehr sowie Abmelden/Guard. Chrome gegen das tatsächliche Angular-/ASP.NET-Produkt; keine Request-Mocks. Backendneustart und HttpClient-Log belegen einen echten RapidAPI-Abruf mit HTTP 200.

Fünf Screenshots und eine offline aufrufbare Screenshotansicht liegen lokal unter docs/demo/; Prüfnachweis: docs/demoprobe.md und docs/demo/demoprobe.json. Folien und achtseitige PDF wurden aktualisiert. Das sind lokale Dateien; ihre Veröffentlichung wird damit nicht behauptet.

Arbeitskopie einschließlich uncommitteter Dateien, .git/Git-Bundle und konsistenter SQLite vor und nach der Probe außerhalb des Repositorys gesichert. SHA-256, Git-Bundle und tatsächliches Entpacken in einen leeren Ordner mit SQLite-Integrität, Branch, Gitstatus und Binärdiff geprüft. Stand: LotAnderson-patch-1, Basis 4af8130. Kein Commit, Push oder Merge.

Bestätigte Grenze: Bezirksfilter bleibt beim Rückweg erhalten, Kartenmarkierung geht verloren. Produktionsbuild, Servervalidierung und automatischer Refresh bleiben offen. Die konkrete Probe garantiert keine zukünftige API-Verfügbarkeit und ersetzt keine umfassende Regression. Veröffentlichung auf main bleibt eine getrennte Aufgabe.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done für die tatsächlich dokumentierte Probe und lokale Sicherung. Einchecken/Veröffentlichen bleiben getrennte Aufgaben; eine erfolgreiche Änderung des Projects-V2-Status wird nur nach gesondertem Nachweis behauptet.
