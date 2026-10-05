# TopImmo – Dokumentation des KI-Einsatzes

Zeitraum: **Ende Juli bis 05.10.2026**

## Angaben von Edhar

Bis einschließlich August nutzte Edhar KI **als Chatbot in Microsoft Visual Studio**, ausdrücklich **nicht als Agenten**. Beim Frontend wurde KI **weniger und überwiegend ebenfalls als Chatbot** eingesetzt. Das genaue Chatbotprodukt, damalige Prompts und Umfang sind nicht angegeben; aus dem IDE-Namen wird kein bestimmtes KI-Produkt abgeleitet.

Für September und die Zeit bis 02.10. ist der konkrete KI-Modus nicht ausreichend dokumentiert. Ein früheres Startdatum des Agenteneinsatzes wurde nicht genannt. **Am 04. und 05.10. ist der Einsatz eines Codex-Agenten mit ausführenden Werkzeugen dokumentiert.** Das ist der früheste hier belegte Agenteneinsatz, keine Aussage über seine allererste Nutzung.

## Nachvollziehbare Verwendung

| Zeitraum / Aufgabe | Art des Einsatzes | Ergebnis und Grundlage |
|---|---|---|
| Ende Juli–August: Entwicklungsunterstützung | Chatbot in Microsoft Visual Studio; Frontend seltener, meist Chatbot | Selbstauskunft von Edhar. Einzelne damals mit KI bearbeitete Dateien, Prompts und Nutzungszeiten sind nicht vollständig überliefert. |
| September–02.10.: Entwicklungsstände | KI-Modus nicht belegt | Git zeigt unter anderem Backend-Duplikatbehandlung und Strukturänderungen. Diese Commits beweisen keinen Agenteneinsatz. |
| 04.10.: Code- und Anforderungsanalyse | Codex-Agent liest Quellen und führt Prüfungen aus | Backendbuild, vorhandene Backend-/Frontendprüfungen, zusätzliche isolierte Auth-/SQLiteprüfung und Abgleich der Anforderungen. Grenzen des Systems dokumentiert. [Issue #35](https://github.com/LotAnderson/TopImmo/issues/35) nennt Assistenzunterstützung ausdrücklich. |
| 04.10.: Projekt- und Scrumunterlagen | Analyse und Formulierung mit KI | Backlogabgleich, Retrospektive, Architektur, Pseudocode, Prüfbericht und vorbereitete Issue-Texte. [Issue #40](https://github.com/LotAnderson/TopImmo/issues/40) dokumentiert die lokale Vorbereitung mit Assistenz. Spätere Onlineänderungen sind von der lokalen Vorbereitung zu unterscheiden. |
| 04.10.: echte Demoprobe und Sicherung | Codex-Agent führt Browser- und Sicherungswerkzeuge aus | Registrierung, Login, echter RapidAPI-Abruf mit 28 Angeboten, Bezirkfilter, Detailseite/Bilder, Rückkehr und Logout; Screenshots sowie private Git-/Datei-/SQLite-Sicherungen mit Wiederherstellungsprüfungen. Bei der ursprünglichen Probe unveränderter Produktcode. [Issue #37](https://github.com/LotAnderson/TopImmo/issues/37). |
| 04.10.: Konfiguration | Codex-Agent ändert Dateien und prüft den neuen Stand | Private Schlüsselkonfiguration ausgelagert, Vorlage/Setup-Skript und Startvalidierung ergänzt, lokale DB-/Builddateien aus der Versionierung entfernt; lokales JWT-Geheimnis erneuert und bestehende lokale Refresh-Tokens widerrufen. [bd61d774](https://github.com/LotAnderson/TopImmo/commit/bd61d7747864498bf1fcb4e42b8d07e1cc410517). Anschließend bestand eine weitere Liveprobe mit neuer lokaler API-Konfiguration. |
| 04.10.: Bezirke und Karte | Codex-Agent implementiert und testet nach Nutzerauftrag | Referenz für 23 Bezirke/152 Stadtteile, Adressnormalisierung, Pointer-/Klickkorrektur, vollständige Legende und zuverlässige Auswahl nach Detailrückkehr; Regressionstests sowie echte Chrome-/WebKit-Klickprüfungen. [60d91eec](https://github.com/LotAnderson/TopImmo/commit/60d91eec47d65c8823a9aa7621c0f42bb6c9cfbd). |
| 04.10.: Birkach-Süd | Nutzer meldet Fehler; Codex-Agent korrigiert und prüft | Grundfarbe und dicke Bezirksgrenze berichtigt, Browser-Geometrie und Regression geprüft. [20a7b374](https://github.com/LotAnderson/TopImmo/commit/20a7b374dc9855c0881c2f00f54d9473a020c0a6), [e6567da8](https://github.com/LotAnderson/TopImmo/commit/e6567da8dc165339783c86ae98f7f6a8e14ca7de). |
| 04.–05.10.: Lern- und Präsentationsunterlagen | Codex-Agent erstellt Texte/Dateien; Chat erklärt Technik | NotebookLM-Quelltext, deutsche Folien, PDF/HTML, Sprechnotizen und technische Vertiefung zu JSON, SQLite, Cache, Semaphore, JWT und lokalen Voraussetzungen. [Issue #39](https://github.com/LotAnderson/TopImmo/issues/39) und [Arbeitsjournal](KONTEXT-UEBERGABE.md). |
| 04.–05.10.: Git und Hauptbranch | Codex-Agent arbeitet lokal; Nutzer veröffentlicht auf GitHub | Sicherung und Branchwechsel vorbereitet, getrennte Historien in einer Prüfkopia verbunden, lokalen Branch `Presentation` vorbereitet; Nutzer pusht und mergt PR #42, Agent gleicht anschließend lokales `main` ab. [3ca57306](https://github.com/LotAnderson/TopImmo/commit/3ca57306c001a0686a622b1e1c673beea887033a), [PR #42](https://github.com/LotAnderson/TopImmo/pull/42). |
| 05.10.: UML und aktuelle Nachweise | Codex-Agent mit unabhängigen Quellprüfungen | Vier Klassenansichten in [architektur.md](architektur.md), SVG/Mermaid und zusätzliche [PlantUML-Datei](architektur.puml); tatsächliche lokale Renderprüfung. Anschließend Commit-Beitragsprüfung und dieser KI-Bericht. Aktuelle Dokumentationsänderungen sind noch nicht neu eingecheckt. |

Die Aussage „Frontend weniger und meist Chatbot“ beschreibt Edhars Angabe zur bisherigen Entwicklung. Die konkreten **Kartenkorrekturen am 04.10. wurden mit einem Agenten umgesetzt**. Für diese Arbeiten umfasst KI-Unterstützung ausdrücklich Quellcodeänderungen, Tests und Werkzeugausführung.

## Beispiele für Lernunterstützung

In den technischen Rückfragen erklärte die Assistenz den tatsächlichen Code und Ablauf: externe JSON-Antwort → C#-Modelle → Verarbeitung → JSON-Antwort an Angular; SQLite für Benutzer/RefreshTokens; Ablaufzeit des 5-Minuten-Caches und Warten auf die Semaphore; JWT-Richtung, Claims und Issuer; Angular-Guard/Interceptor und backendseitiges `JwtBearer`; lokale Installation, Start/Stop sowie GitHub-Branch- und PR-Schritte.

Diese Erklärungen dienten der Präsentationsvorbereitung. Die bereits in Juli-Commits vorhandenen JWT-, Refresh-, SQLite- und Angular-Authfunktionen werden dadurch nicht zu neu von der KI implementierten Oktoberfunktionen.

## Aufgaben von Edhar und Aussagegrenzen

Edhar gab Aufgaben und Korrekturwünsche vor, berichtete Bedienungs- und Darstellungsfehler – insbesondere Birkach-Süd sowie den fehlerhaften Keynote-Import –, präzisierte die Folien und stellte technische Verständnisfragen. Die Assistenz setzte beauftragte lokale Änderungen und Prüfungen um. Die Veröffentlichung und der Merge auf GitHub erfolgten durch den Nutzer; der ursprüngliche Agentenversuch, GitHub-Issues zu ändern, scheiterte an fehlenden Rechten. Lokal erstellte Issue-Texte belegen deshalb nicht automatisch eine durch diesen Agenten ausgeführte Boardänderung.

Als Ergebnis für NotebookLM wurde ein **Quelltext** erstellt; eine Audioübersicht wurde nicht im NotebookLM-Konto erzeugt. Der PPTX-Import in Keynote blieb fehlerhaft beziehungsweise unbestätigt; die Präsentation wurde als **PDF** weitergeführt. Die Birkach-Prüfung bestätigt die interne SVG-Geometrie, keine amtliche GIS-Neuvermessung.

Ein vollständiger KI-Anteil in Prozent, Arbeitsstunden oder der genaue erste Tag des Agenteneinsatzes lassen sich nicht belegen. Commitautor und Commitdatum zeigen einen gespeicherten Stand; sie weisen keine alleinige menschliche Urheberschaft nach. Insbesondere [2d447a7b](https://github.com/LotAnderson/TopImmo/commit/2d447a7b17d72a2f7b36fdb03ba70b0d6b211037) bündelt zuvor lokalen Bestand, Tests und Unterlagen. Eine pauschale Zuordnung aller darin erstmals gespeicherten Zeilen an Edhar oder an KI wäre unbelegt.

## Kurzer Text für die Präsentation

> Seit Ende Juli habe ich KI zur Unterstützung eingesetzt. Bis einschließlich August nutzte ich sie in Microsoft Visual Studio als Chatbot und nicht als Agenten. Beim Frontend setzte ich KI seltener ein, ebenfalls meistens als Chatbot. Für die Vorbereitung am 04. und 05. Oktober ist zusätzlich der Einsatz eines Codex-Agenten dokumentiert: Er analysierte Code, führte Prüfungen aus, unterstützte konkrete Karten- und Konfigurationskorrekturen und erstellte Dokumentation, Präsentationsunterlagen und UML-Diagramme. Ich gab die Aufgaben und Korrekturwünsche vor und nutzte die technischen Erklärungen zur Vorbereitung. Die dabei erzeugten Änderungen und Unterlagen kennzeichne ich als KI-unterstützt. Der genaue KI-Anteil und der erste Tag des Agenteneinsatzes lassen sich aus den Git-Commits nicht bestimmen.

## Quellen und Prüfstand

Die persönlichen Angaben stammen aus Edhars Antworten in dieser Unterhaltung. Konkrete Agentenarbeit ist im [Kontextjournal](KONTEXT-UEBERGABE.md), in den verlinkten Commits und in den GitHub-Issues dokumentiert. Git belegt die Änderungen; die Angabe des KI-Modus stammt ergänzend aus dem Arbeitsjournal und der Unterhaltung.

Zusätzliche historische Quellen sind der [damalige Backlog](https://github.com/LotAnderson/TopImmo/blob/e6567da8dc165339783c86ae98f7f6a8e14ca7de/docs/backlog.md), die [Scrum-Nachträge](https://github.com/LotAnderson/TopImmo/blob/e6567da8dc165339783c86ae98f7f6a8e14ca7de/docs/scrum-nachtraege.md) und der [damalige NotebookLM-Text](https://github.com/LotAnderson/TopImmo/blob/e6567da8dc165339783c86ae98f7f6a8e14ca7de/docs/notebooklm-projekttext.md). Frühere Aussagen zu offenen Liveprüfungen oder altem `main` beschreiben jeweils den damaligen Zwischenstand. Bei der heutigen GitHub-Prüfung sind die Issues #35, #37, #39 und #40 geschlossen und PR #42 gemergt.

Dieser Bericht wurde am 05.10.2026 selbst mit Codex-Unterstützung erstellt und gegen Nutzerangaben, GitHub-Metadaten, Dateiherkunft und dokumentierte Aktionen geprüft. Personenbeiträge: [beitragsverteilung.md](beitragsverteilung.md).
