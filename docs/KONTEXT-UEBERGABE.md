# TopImmo – Kontextübergabe für einen neuen Agenten

**Erstellt am 04.10.2026. Präsentation am 05.10.2026.** Dieses Dokument fasst die bisherige Arbeit zusammen, damit der nächste Chat direkt fortsetzen kann. Es ist keine neue Projektbewertung und kein Nachweis für nachträglich behauptete Arbeitszeiten.

**Arbeitsverzeichnis:** `/Users/edhar_myronchuk/Documents/GitHub/TopImmo`

**Letzter vollständig dokumentierter Boardstand:** 04.10.2026, ca. 16:08 Uhr, Europe/Berlin. Die später geprüfte Veröffentlichung des Arbeitsbranchs ist im folgenden Nachtrag beschrieben; spätere Boardänderungen müssen neu geprüft werden.

## Rückfrage: Änderungen nach main veröffentlichen

### Fortsetzung am 05.10.2026: Presentation und main verbinden

Nutzer hat den GitHub-Branch in `Presentation` umbenannt. Frische Abfrage mit `git ls-remote --heads origin` bestätigt remote `Presentation` auf `8adc6a6101875b859635e69a6c064eff2bd9cfbc` und `main` auf `ae9043e4c9a68eae428854145c3515094f3da74d`; der alte Remote-Branchname ist verschwunden. Lokaler Arbeitsbranch heißt zunächst weiterhin `LotAnderson-patch-1`, Arbeitskopie sauber. Vollständiges Repository, kein Shallow-Clone: `main` und Präsentationsstand haben tatsächlich verschiedene Root-Commits und keinen gemeinsamen Vorfahren. Deshalb ist der zuvor vorgeschlagene direkte Pull Request noch nicht möglich. Screenshot-Diffumfang ist kein verlässlicher Ersatz für die geprüften Remote-Referenzen.

**Sicherung und Probe:** vollständige lokale Git-Historien als geprüftes privates Bundle `/private/tmp/topimmo-historien-82me_1lu/Stand-vor-Historienverbindung.bundle` gesichert (0600). In separater Kopie `/private/tmp/topimmo-historien-82me_1lu/pruefung` einen `ours`-Merge mit `--allow-unrelated-histories` vorbereitet: `3ca57306c001a0686a622b1e1c673beea887033a`, Eltern aktueller Präsentationsstand und bisheriges main. Resultierender Git-Tree `13245bdeec00d6ed679847fb228bf975a11e1983` ist exakt identisch mit `8adc6a6`; keine Änderung an Produktdateien. `main` ist danach ein Vorfahr, sodass normaler PR/Fast-Forward möglich wird. Das bewusst ausgewählte Ergebnis ersetzt den alten Backend-Prototyp und seine versionierten Build-/Editorreste durch den vollständigen bereits geprüften Präsentationsstand. Beide bisherigen Historien bleiben erhalten; keine Force-Push-Anweisung. Prüfnachweis: `/private/tmp/topimmo-historien-82me_1lu/pruefung.json`.

Geplante lokale Übernahme: aktuellen Branch an Remote-Namen `Presentation` anpassen, Upstream `origin/Presentation` setzen, geprüften Commit ausschließlich per Fast-Forward übernehmen. Danach Nutzer über GitHub Desktop **Push origin** und PR `Presentation` → `main` führen. Zum Stand dieses Absatzes noch keine externe Veröffentlichung oder main-Veränderung.

**Lokale Übernahme abgeschlossen:** geprüften Commit aus der separaten Kopie importiert, lokalen Arbeitsbranch von `LotAnderson-patch-1` in `Presentation` umbenannt und Upstream auf `origin/Presentation` gesetzt. Fast-Forward auf `3ca57306` erfolgreich. Git-Arbeitskopie sauber, Tree weiterhin bytegenau identisch; SHA-256 von 73 lokalen Dokumentations-/SQLite-/Local-Konfigurationsdateien vor und nach der Übernahme identisch. Ein gemeinsamer Vorfahr existiert nun (`origin/main` selbst). Anzeige `ahead 6` entsteht aus dem verbindenden Merge-Commit plus den übernommenen main-Vorfahren und bedeutet keine sechs Produktänderungen. Lokales und entferntes main weiterhin `ae9043e4`; nichts extern gepusht oder gemergt. Nächste Nutzerschritte: GitHub Desktop **Push origin** für `Presentation`, dann https://github.com/LotAnderson/TopImmo/compare/main...Presentation?expand=1 öffnen, PR mit base `main`/compare `Presentation` erstellen und mergen; anschließend in Desktop `main` wählen, **Fetch origin → Pull origin**. Dokumentation/Präsentationsdateien bleiben gemäß bestehendem Auftrag lokal ignoriert.

**Nutzer-Push und PR #42 sichtbar (05.10.2026, Screenshot 08:20):** Vergleich `main` ← `Presentation` zeigt jetzt **Able to merge**, bestehenden offenen PR **Presentation #42**, 18 Commits und 213 geänderte Dateien. Die 213 Dateien entsprechen dem zuvor geprüften Unterschied zwischen altem main und vollständigem Präsentationsstand; keine 23.000 Abhängigkeitsdateien. Lokale Prüfung bestätigt saubere Arbeitskopie und `Presentation` auf `3ca57306` ohne Ahead-Abweichung zur lokalen Remote-Trackingreferenz. Es ist noch kein vollzogener main-Merge belegt. Anleitung für den nächsten Nutzerschritt: **View pull request** (https://github.com/LotAnderson/TopImmo/pull/42), unten **Merge pull request**, bei Auswahl **Create a merge commit**, dann **Confirm merge**. Anschließend lokal `main` wählen und **Fetch origin → Pull origin**. Kein zusätzlicher PR nötig. Keine externe Mutation durch den Agenten. Offizielle Anleitung: https://docs.github.com/en/pull-requests/how-tos/merge-and-close-pull-requests/merging-a-pull-request.

**PR-Seite geöffnet (Screenshot 05.10.2026 08:22):** Nutzer ist auf der Conversation-Seite von PR #42; Status **Open**, oben **Ready to merge**, Richtung `Presentation` → `main`. Screenshot zeigt den oberen Seitenbereich mit Commit-Liste, der Merge-Button ist dort noch nicht sichtbar. Nächster Schritt: bis ans Ende der Conversation-Seite scrollen, **Merge pull request → Confirm merge**; danach sollte **Merged** erscheinen. Kein bereits ausgeführter Merge behauptet. Anschließender lokaler Abgleich bleibt `main` auswählen, **Fetch origin → Pull origin**.

**GitHub-Stand aktuell geprüft (05.10.2026):** Nutzer bittet um Prüfung und `main` als Hauptbranch. Frische GitHub-Metadaten bestätigen Standardbranch `main`; PR #42 ist geschlossen und **merged**, gemergt am 05.10.2026 um 08:23:41 CEST. Merge-Commit `b01720481d2e35d0ab09791b12576dc09b419251`. Frisches `git ls-remote --symref origin HEAD refs/heads/main refs/heads/Presentation` bestätigt HEAD → `refs/heads/main` auf genau diesem Commit; `Presentation` bleibt auf `3ca57306`. Repositoryname weiterhin `TopImmo`; Nutzeranforderung bezieht sich im Branch-Kontext auf den Haupt-/Standardbranch. Lokale Arbeitskopie zunächst sauber auf `Presentation`, lokales main noch alt. Lokale Synchronisierung wird vorbereitet; keine bereits erfolgte lokale main-Aktualisierung behauptet. Erster Freigabeversuch für den lesenden Remote-Abgleich scheiterte an einer Zeitüberschreitung der automatischen Prüfung; ausdrücklich erlaubter einmaliger Wiederholungsversuch erfolgreich, kein verbleibender Freigabeblocker. GitHub-API-Abfragen waren nur lesend.

**Lokales main aktualisiert und geprüft:** `git fetch origin --prune` holt `b0172048`. Nutzer wechselt währenddessen selbst lokal zu `main`; erster Übernahmeschritt stoppt deshalb bereits an der Branch-Vorbedingung, ohne Git oder Dateien zu ändern. Zustand erneut geprüft: main war 19 Commits hinter origin/main, keine versionierten Änderungen, lokale unversionierte Laufzeitdateien ohne Kollision mit Zielquellen. Vor dem Fast-Forward vollständige Git-Historien als geprüftes privates Bundle und 73 lokale Dokumentations-/SQLite-/Local-Konfigurationsdateien als CRC-/SHA-256-geprüftes ZIP gesichert, beide 0600 in privatem Ordner `/private/tmp/topimmo-main-abgleich-_b9dniee/`. Danach `git merge --ff-only origin/main` erfolgreich; aktiver Branch **main**, HEAD und origin/main beide `b01720481d2e35d0ab09791b12576dc09b419251`, Ahead/Behind **0/0**, Arbeitskopie sauber. Tree `13245bdeec00d6ed679847fb228bf975a11e1983` exakt identisch mit freigegebenem Präsentationsstand. Alle 73 lokalen Dateien bytegleich erhalten; weiterhin 104 versionierte Projektdateien, keine docs/DB/Local-Schlüsseldateien/node_modules/bin/obj versioniert. Hauptbranch ist sowohl auf GitHub als auch lokal `main`; `Presentation` bleibt als zusätzlicher Branch erhalten. Kein Force-Push und keine neue externe Mutation; vorhandenen Nutzer-Merge nur lokal übernommen. Prüfnachweis `/private/tmp/topimmo-main-abgleich-_b9dniee/pruefung.json`. Keine neue Produkttestserie erforderlich, da identischer bereits geprüfter Produktinhalt; Git- und Dateierhaltungsprüfungen vollständig bestanden.

Lokale Prüfung: Nutzer hat inzwischen den älteren `main` (`ae9043e`) ausgecheckt; fertiger Arbeitsbranch `LotAnderson-patch-1` steht auf `8adc6a6`, wie die lokal gespeicherte Remote-Trackingreferenz. Keine frische Remoteprüfung behauptet. Auf altem main fehlen die neuen Quellen und Ignore-Dateien; verbleibende Abhängigkeiten, Builddateien, SQLite, lokale Konfiguration und `docs/` erscheinen deshalb untracked. Der fertige Anwendungscode liegt im Arbeitsbranch. Keine pauschale neue main-Commit-Anweisung. Nutzer erhält GitHub-Desktop-Ablauf: vorbereiteten Branch wählen, origin aktualisieren/gegebenenfalls pushen, Pull Request Arbeitsbranch→main erstellen, kontrollieren und mergen, lokal main pullen. Keine externen Mutationen durchgeführt. Aktuelle Präsentation bleibt ausdrücklich lokal; `docs/` ist im vorbereiteten Branch ignoriert.

### Branchwechsel vorbereitet: Dialog mit 23.053 Dateien

Stand: 04.10.2026, ca. 23:33 CEST. Screenshot zeigt Wechsel `main` → `LotAnderson-patch-1`. 23.051 unversionierte Dateien geprüft: 22.636 aus `node_modules`, 275 .NET-Builddateien, 40 Angular-Cachedateien, 71 Dokumentationsdateien und 29 weitere lokale Dateien. Zusätzlich genau zwei versionierte generierte Builddateien geändert: `Backend/ImmoScoutRapidApi/obj/Debug/net8.0/ImmoScoutRapidApi.AssemblyInfo.cs` (automatisch eingetragene Commit-ID) und `ImmoScoutRapidApi.AssemblyInfoInputs.cache` (Hash). Beide werden im Zielbranch aus Git entfernt und hätten den normalen Wechsel verhindert.

Unabhängige Prüfung des installierten GitHub-Desktop-Codes bestätigt: „Bring“ versucht zuerst normalen Checkout; bei blockierenden Änderungen verwendet Desktop einen Stash, der auch unversionierte Dateien aufnimmt. „Leave“ würde lokale Dokumente/Konfiguration zunächst im Stash auf main ablegen. Deshalb die beiden rein generierten Änderungen gezielt vorbereitet: 82 Dateien (gesamtes lokales `docs/`, SQLite-/Local-Konfigurationsdateien und beide Builddateien) in `/private/tmp/topimmo-branchwechsel/Lokale-Dateien-vor-Branchwechsel-20261004-233249.zip` gesichert, CRC und SHA-256 aller Einträge geprüft, Archiv 0600. Danach ausschließlich die beiden generierten Builddateien bytegenau auf `main`-HEAD zurückgesetzt; Git-Diff ist nun leer, unversionierte lokale Dateien unverändert vorhanden. Kein Branchwechsel, kein Stash, kein Commit und keine externe Veröffentlichung durch den Agenten.

Nächster Nutzerschritt: alten Dialog mit **Cancel** schließen, Wechsel auf `LotAnderson-patch-1` erneut öffnen und **Bring my changes to LotAnderson-patch-1 → Switch Branch** wählen. So können lokale Dateien beim normalen Checkout erhalten bleiben und die Zielbranch-Ignore-Regeln greifen. Keinen Commit mit 23.000 Dateien erstellen, keine pauschale Discard-/Clean-Aktion. Bei Fehlermeldung Zustand erneut prüfen, statt Stash oder Dateien ungeprüft zu verwerfen. Offizielle Dialogbeschreibung: https://docs.github.com/en/desktop/making-changes-in-a-branch/managing-branches-in-github-desktop?platform=mac.

## Nachtrag: Anwendung geordnet beendet

Stand: 04.10.2026 23:25 CEST. Nutzer meldet Frontend beendet, Port 5197 weiterhin aktiv. Listener als TopImmo-Backend `Backend/ImmscoutAPI/bin/Debug/net9.0/ImmscoutAPI` mit Arbeitsverzeichnis `Backend/ImmscoutAPI` identifiziert (PID 53422). Geordnet per SIGTERM beendet; nachfolgende Portprüfung bestätigt keine Listener auf 5197 oder 4200. Projektdateien und SQLite erhalten. Zum nächsten Start Backend und Frontend neu starten.

## Aktiver Nachtrag: aktuelle Präsentation benennen und Sprechnotizen

Nutzer wünscht „TopImmo aktuelle Präsentation“ und Sprechnotizen; Formulierung „nicht aktuelle Versionen löschen“ wird als Entfernung veralteter Dateien verstanden, aber wegen sprachlicher Mehrdeutigkeit vor Löschungen explizit geklärt. Rückfrage läuft. Bis zur Antwort bleiben bisherige Dateien erhalten.

1. **Vorsicherung und aktuelle Datei erstellt:** 27 vorhandene Präsentations-/Paketdateien im privaten temporären ZIP `/tmp/topimmo-praesentation/vor-aufraeumen-220916.zip` gesichert, CRC und SHA-256 jedes Eintrags geprüft. Aktuelle freigegebene 11-Folien-PDF bytegleich als `TopImmo – aktuelle Präsentation.pdf` bereitgestellt. Zusätzliche vom Nutzer erzeugte Kopie vor Bereinigung inhaltlich/zeitlich abgeglichen. Ausformulierte deutsche Sprechnotizen für aktuelle Reihenfolge werden getrennt als MD/Textdatei erstellt; keine persönliche Autorenschaft erfinden.
2. **Aktive Unterlagen aufgeräumt:** optionale Sprachklärungsfrage blieb bislang unbeantwortet; nach ausreichender Antwortzeit „nicht aktuelle“ als veraltete Fassungen verstanden und reversible Bereinigung ausgeführt. 25 alte/doppelte Präsentations-/Paketdateien aus `docs/` entfernt, zwei entpackte Paketordner entfernt; alle entfernten Bytes zuvor gegen Vorsicherungs-ZIP `/tmp/topimmo-praesentation/vor-aufraeumen-220916.zip` geprüft. Darunter frühere defekte PPTX-Dateien und eine vom Nutzer erzeugte ältere 10-Seiten-PDF-Kopie; ihre Vorversionen bleiben in der Sicherung. Aktuelle PDF unverändert als `TopImmo – aktuelle Präsentation.pdf`; dazu gleichnamige aktuelle HTML und Folientext zum Übersetzen. Generator-Ausgabename angepasst, Mac-Anleitung und Vortragsindex auf aktuellen Stand gebracht. Die Quellenbilder/Layout/Vorlagenskripte und andere Projektdokumentation bleiben vorhanden. Kontext bleibt lokal fortlaufend aktualisiert.
3. **Sprechnotizen und Abschlussprüfung bestanden:** `TopImmo – Sprechnotizen.md` und `.txt`, 11 exakt passende Folientitel, rund 1.100 gesprochene Wörter mit Übergängen, separater Demoaktion und fünf technischen Rückfragen. Fachliche Prüfung bestätigt SQLite/RAM-Trennung, Semaphore-Grenzen, JWT-Signatur/Benutzer-ID statt SID, Refresh/Browsergrenzen und lokale Voraussetzungen. Aktuelle PDF weiterhin 11 Seiten; Dateilinks in Mac-Anleitung und aktuellem Vortragsindex geprüft. Archiv der alten Fassungen dauerhaft lokal nach `docs/.sicherungen/Praesentationen-vor-Bereinigung.zip` kopiert (0600, SHA-256/CRC identisch zur Vorsicherung); die Bereinigung ist dadurch wiederherstellbar. `TopImmo – aktuelles Präsentationspaket.zip` enthält nur aktuelle PDF/HTML, Sprechnotizen MD/TXT, Übersetzungstext und Mac-Anleitung, keine alten PPTX. Generator erzeugt künftig einheitliche aktuelle Namen; automatisch erzeugte fachliche Langnotizen haben einen separaten Namen, um die persönlichen Sprechnotizen nicht zu überschreiben. Kontext aktualisiert; keine Produktcodeänderung. Unterlagen/Archiv bleiben von Git ignoriert.
4. **Rückfrage zur langsamen API-Antwort geklärt:** API-Service/DI und Cachecode erneut gelesen. Kein eigener HttpClient-Timeout gesetzt; .NET-Standard 100 Sekunden laut https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout?view=net-9.0. `WaitAsync()` an der Semaphore hat im Projekt weder eigenes Zeitlimit noch CancellationToken. Bei Abruffehler/Timeout wird kein neuer Cacheeintrag gesetzt, `finally` gibt Sperre frei; eine wartende Anfrage kann danach erneut laden. Gesamte Wartezeit kann deshalb länger als der einzelne HTTP-Timeout sein. 5 Minuten betreffen Cachegültigkeit und sind kein Abruf-/Wartezeitlimit. Nur Erklärung/Codeprüfung, keine Produktänderung oder Timeout-Implementierung.

## Aktiver Nachtrag: PDF-Beschriftung und Reihenfolge anpassen

Stand: 04.10.2026 21:58 CEST. Nutzerauftrag: Titelfolienzeile „Schulprojekt | Präsentation am 05.10.2026“ entfernen; Footer auf „TOPIMMO / PROJEKTPRÄSENTATION“ kürzen. Folie 3 zeigt vier gleichrangige Pflichtbestandteile: Angular-Frontend, C#-Backend, externe API, SQLite. JSON-/Cache-Folie unmittelbar nach SQLite; JWT-Zusammenspiel unmittelbar nach Zugriffsschutz.

1. **Vorlage angepasst:** Datumszeile entfernt, Footer gekürzt, Folie 3 in vier Felder umgebaut. Neue Reihenfolge: Titel → Nutzerweg → vier Pflichtbestandteile → Architektur → Algorithmus → SQLite → JSON/Cache → Zugriffsschutz → JWT-Zusammenspiel → Live-Demo → Installation. Abschnittsnummern und Folienzahlen werden neu gesetzt. Sprechnotizen und Übersetzungstext folgen derselben Reihenfolge. PDF-Renderer um vollständige 11-Folien-/Bildausgabe erweitert; Sicherung der zuvor vorhandenen PDF/Vorlagen unter `/tmp/topimmo-praesentation/vor-umordnung-*`.
2. **Vollständige PDF-Prüfung bestanden:** alle 11 Seiten über CoreGraphics/CoreText neu gerendert, einschließlich Bilder und neuer vierteiliger Folie 3. Keine Textüberläufe; Montage aller Seiten angesehen. PDF-Textprüfung bestätigt: Titelfolienzeile weg, keine Datumsangabe mehr im Footer, vier geforderte Überschriften, Nummern 01–11 und korrekte Nachbarschaften (SQLite/JSON auf 6–7, Zugriff/JWT auf 8–9). Beschriftung der gesicherten Aufnahmen bleibt auf der Demo-Folie, jetzt Seite 10. Beide aktuellen PDFs, HTML, Folientext, Sprechnotizen und entpacktes/ZIP-Paket aktualisiert. Alte PDF-Prüfspur mit Bytegleichheit erster 8 Seiten als `pdf-technik-pruefung-vor-umordnung.json` archiviert; neue Prüfung dokumentiert vollständige Neurenderung. Sichtbeleg `demo/praesentation-mac/folien-uebersicht-aktuell.png`; Prüfbericht `pdf-reihenfolge-pruefung.json`. Bestehende PPTX nicht verändert, kein Keynote-Reparaturversprechen.
3. **Unabhängiger Abschlussreview bestanden:** PDF/Layout und Kontaktbogen geprüft; alle Nutzeränderungen bestätigt. Vier gleichwertige Karten mit identischen Maßen/Schrift/Fettdruck, Datumszeile/Footerdatum entfernt, Nummerierung und neue Reihenfolge stimmen. Keine Beanstandungen oder sichtbaren Überläufe. PDF bereit, Git-Arbeitskopie weiterhin sauber; Unterlagen lokal ignoriert.

## Aktiver Nachtrag: PDF-Seiten 9–11 technisch vertiefen

Stand: 04.10.2026 21:25 CEST. Nutzer bestätigt Keynote-Importfehler und präzisiert den Auftrag auf Aktualisierung der **PDF-Präsentation**, Seiten 9, 10 und 11. Themen: JSON/SQLite, Semaphore und 5-Minuten-Cache, JWT/Benutzer-ID/Refresh/Signatur, Zusammenspiel Frontend/Backend und lokale Technologieversionen. Vorheriger Beschriftungswunsch bleibt erhalten.

1. **Code-/Versionsabgleich abgeschlossen:** Immobiliendaten: `System.Text.Json` → C#-Objekte → RAM, keine Immobilientabelle. Auth-JSON → DTO → BCrypt/User → EF Core/SQLite. TokenService erzeugt `NameIdentifier` und E-Mail, keinen `sid`/`jti`; HS256-Signatur, Accessdauer konfigurierbar (versionierter Standard 60 Minuten), zufälliger Refresh aus 64 Bytes, 7 Tage, Rotation in SQLite. Cache `IMemoryCache` 5 Minuten absolut; `SemaphoreSlim(1,1)` sichert nur paralleles Nachladen, doppelte Cacheprüfung und Freigabe im `finally`. Lokale Versionen: Node 24.21.0, npm 11.19.0; Projekt Angular 21.2/TypeScript 5.9, net9.0/EF Core 9.0.17; .NET-SDK 9.0.318 und 10.0.401 installiert, .NET/ASP.NET-Core-Runtime 9.0.20. Offizielle Angular-Kompatibilität und Microsoft-Mac-Installationsanleitung geprüft. Unabhängiger fachlicher Review läuft. Kein Keynote-Reparaturerfolg behauptet.
2. **Neue technische Seiten erstellt:** 9: parallele Datenwege JSON→RAM bzw. Auth-JSON→EF Core/SQLite; absolutes 5-Minuten-Ablaufen, Nachladen auf Anfrage, Semaphore mit doppelter Cacheprüfung und `finally`-Freigabe. 10: JWT-Aufbau/HS256, lesbare Claims, Benutzer-ID statt SID, Client-Guard/Interceptor gegenüber Backend-Validierung, Refresh-Speicherung/Rotation und tatsächliche offenen Grenzen. 11: Node/npm/Angular/TypeScript, .NET-SDK/Runtime/SQLite und Startbefehle/Ports. Ausführliche neue Sprechnotizen ergänzen die kompakten Folien. Vortragsdauer nun ca. 12 Minuten. Der Generator kann mit `--skip-pptx` nur HTML/Text/Layout aktualisieren; bestehende PPTX-Dateien bleiben wegen gemeldetem Keynote-Fehler unangetastet. PDF-Neurendering erfolgt lokal über CoreGraphics/CoreText ohne Browser-/Keynote-Automatisierung; Seiten 1–8 werden anschließend aus der bisherigen PDF übernommen.
3. **PDF erstellt und geprüft:** `TopImmo-Praesentation-Technik.pdf`, 11 Seiten im einheitlichen 16:9-Format; Seiten 1–8 als ursprüngliche PDF-Contentstreams übernommen und auf Bytegleichheit geprüft. Seiten 9–11 mit CoreGraphics/CoreText neu gerendert; vollständige Textabdeckung ohne Überlauf. Alle drei Seiten als PNG angesehen: Texte/Diagramme passen, keine abgeschnittenen Bereiche. Prüfwörter für JSON/SQLite/Semaphore/JWT/ID/Refresh/Versionen sowie gewünschte Beschriftung auf Seite 8 bestätigt. Prüfung: `demo/praesentation-mac/pdf-technik-pruefung.json`; Sichtbilder `technik-9.png` bis `technik-11.png`. Bisherige PDF unter ursprünglichem Namen ebenfalls aktualisiert, entpacktes Paket und ZIP abgeglichen. HTML, Übersetzungstext, Sprechnotizen und Mac-Anleitung auf neue Fassung gebracht. Bestehende PPTX-Dateien bleiben unverändert; Keynote-Importfehler wird nicht als behoben dargestellt. `docs/` bleibt ignoriert. Quellenreview bestätigt Inhalte; keine Produktcodeänderungen oder erneute Produkttestserie für diese Dokumentänderung.
4. **Finale unabhängige Prüfung bestanden:** technischer Review der drei gerenderten PDF-Seiten, des Folientextes und der neuen Sprechnotizen stimmt mit tatsächlichem Code/Versionsstand überein; keine fachlichen Korrekturen oder sichtbaren Überläufe. Neue PDF-Datei `TopImmo-Praesentation-Technik.pdf` bereit zur Übergabe; 11 Seiten, PDF-1.4, Paket-CRC geprüft, Git-Arbeitskopie weiterhin sauber (Unterlagen ignoriert).

## Nachtrag: Beschriftung der Demoaufnahmen gekürzt

Nutzerauftrag: sichtbaren Text „Bei Ausfall: gesicherte Aufnahmen vom 04.10.2026 verwenden und als Aufnahmen kennzeichnen.“ durch exakt „Gesicherte Aufnahmen vom 04.10.2026“ ersetzen.

Umgesetzt in Generator, Layoutdaten, HTML, Übersetzungstext, allen drei vorhandenen PPTX-Dateien und beiden PDFs einschließlich entpacktem Präsentationspaket. ZIP-Paket aktualisiert. PDF-Änderung betrifft ausschließlich die Beschriftung der achten Folie; Darstellung bleibt Text mit eingebetteten Arial-Glyphen, 11 Seiten erhalten. Ausführliche historische Sprechnotizen und Aussagen zur Importprüfung werden nicht verändert. Der noch nicht geklärte Nutzer-Importfehler bleibt separat dokumentiert.

## Nachtrag: gemeldeten PPTX-Importfehler beheben

Stand: 04.10.2026 20:31 CEST. Nutzer meldet „pptx ist invalid“. Datei wird direkt gegen die native Mac-App geprüft, nicht nur mit ZIP-/Python-Parsern.

1. **Erneute Bestandsprüfung:** PPTX existiert (ca. 1,1 MB), ZIP-/XML-Parsing fehlerfrei. Die frühere App-Suche prüfte nur `/Applications/Keynote.app` und übersah die tatsächlich vorhandene `/Applications/Keynote Creator Studio.app`. Das war eine unvollständige Kompatibilitätsprüfung. Native Importprüfung und unabhängiger OOXML-Review laufen; keine erfolgreiche Reparatur vor deren Ergebnis behaupten.
2. **OOXML-Review abgeschlossen:** CRC/XML/Beziehungen/IDs/Medien/Inhaltstypen gültig; 11 Folien, 153 Textfelder und 11 Notizen erneut lesbar. Metadatenfehler gefunden: echte Abmessungen 16:9, geerbter `sldSz.type` jedoch `screen4x3`. Generator und separate `TopImmo.pptx` auf `screen16x9` korrigiert. Dies ist noch keine bestätigte Ursache des Nutzerfehlers. Erste AppleScript-Öffnung liefert keinen Dokumentverweis; vorhandenes anderes Dokument mit 8 Folien bleibt unberührt. Native Importdiagnose läuft; Nutzer optional nach Fehlerort (Chat-Link/Keynote/PowerPoint) gefragt.
3. **Metadatenkorrektur gesichert, nativer Prüflauf blockiert:** `TopImmo.pptx` und ursprünglicher Dateiname enthalten nun konsistente 16:9-Metadaten; beide erneut mit python-pptx geladen (11 Folien), ZIP und XML bestanden. Generator, Sprechnotizen und Präsentationspaket nachgeführt. Der erneute Keynote-Import wurde nicht ausgeführt: automatische Freigabeprüfung brach wegen erreichtem Nutzungs-/Reviewlimit ab; keine Feststellung, dass die Aktion unsicher sei. Keine Umgehung. Ursache der Nutzeranzeige „invalid“ weiterhin nicht abschließend bestätigt; Fehlerort-Rückfrage bisher unbeantwortet. Vorhandenes anderes 8-Folien-Dokument nicht verändert.
4. **Zwischenfrage API-Umfang beantwortet:** feste Anfrage `page=1`/`pageSize=30`, maximal 30 Treffer, keine Folgeseiten. Die letzte echte Demo zeigte 28 Mietangebote; konkrete aktuelle Daten können variieren.
5. **Nutzer meldet weiterhin „The file format is invalid“:** Metadatenkorrektur löst die gemeldete Öffnung nicht nachweislich. Beide aktuellen Dateien erneut geprüft: identische valide ZIP-/XML-Struktur, 11 mit python-pptx lesbare Folien; kein abgeschnittener Download auf der lokalen Platte. Rückfrage nach konkret fehlerndem Programm/Chat-Link gestellt. Native Keynote-Importprüfung bleibt wegen zuvor abgebrochener automatischer Freigabeprüfung unbestätigt; keine weiteren unbelegten Reparaturversprechen. Direkte Finder-Öffnung kann Chat-Dateivorschau und tatsächlichen Präsentationsimport unterscheiden.

## Aktiver Nachtrag: bearbeitbare Präsentation für den Mac

Stand: 04.10.2026 20:23 CEST. Nutzer wünscht eine Präsentation, auf Deutsch, später selbst übersetzen. Zielformat: editierbare `.pptx` für Keynote/PowerPoint; ergänzend PDF, Textfassung und Mac-Anleitung. Vorhandene lokale 8-Folien-HTML/PDF-Unterlagen bleiben als frühere Version erhalten.

1. **Quellenabgleich begonnen:** aktuelle Kontextdatei, Vortragsleitfaden, Architektur, Prüfungen und Produktcode gelesen; aktueller Prüfstand 13/13 Frontendtests in 3 Dateien, Entwicklungsbuild 4,44 MB, Birkach-Süd-Farbe und dicke Bezirksgrenze korrigiert. Ältere Unterlagen enthalten historische 8-/12-Testzahlen. Deutsch durch Nutzer bestätigt. Inhalt soll bearbeitbar bleiben; Screenshots nur zur Produktansicht.
2. **Gliederung und aktuelle Ansichten erstellt:** 11 Folien, etwa 10 Minuten einschließlich Demo; Architektur/SQLite/API-Verarbeitung, Authentifizierung, Testnachweise und offene Punkte. Unabhängiger Read-only-Quellenreview bestätigt die fachlichen Grenzen. Backend erneut gestartet (5197); frischer echter Login/API HTTP 200 und aktuelle Chrome-Aufnahmen der Übersicht/Detailseite unter `docs/demo/praesentation-mac/`. Keine Zugangsdaten auf den Bildern. Deutsch, editierbare Textfelder/Diagramme und separate Übersetzungstexte werden verwendet.
3. **Bearbeitbare Dateien erstellt:** `TopImmo-Praesentation-2026-10-05.pptx` mit 11 Folien und echten Text-/Diagrammformen, Sprechnotizen in jeder Folie. Gemeinsame Layoutvorlage erzeugt `TopImmo-Praesentation-2026-10-05.html`, `TopImmo-Folientext-zum-Uebersetzen.txt` und `TopImmo-Sprechnotizen.md`; lokale Neu-Erzeugung über `praesentation-erstellen.py` (python-pptx/Pillow in temporärer Umgebung). Kurze Anleitung `Praesentation-am-Mac.md` zusätzlich erstellt. Vorhandene frühere Folien/PDF bleiben erhalten. Format-/Layout-/PDFprüfung läuft; nativer Keynote-/PowerPoint-Import kann mangels installierter Apps nicht lokal geprüft werden.
4. **Datei- und Sichtprüfung bestanden:** 11 PPTX-Folien im 16:9-Format, 153 bearbeitbare Textfelder, 11 eingebettete Sprechnotizen; ZIP-Struktur fehlerfrei und PPTX mit python-pptx erneut eingelesen. PDF mit 11 Querformatseiten und aktueller Testzahl geprüft. Offline-HTML aus identischen Layoutdaten: keine Textüberläufe, Navigation/Pfeiltasten/Esc funktionieren. Alle 11 Folien gerendert und als Montage angesehen; Diagramme/Texte/Screenshots passen. Native Keynote-/PowerPoint-Darstellung mangels Apps weiterhin nicht getestet. Keine API-/JWT-Schlüssel in den neuen Dateien, alle Dateien durch `/docs/` ignoriert. Paket `TopImmo-Praesentationspaket.zip` enthält PPTX, PDF, selbstständige HTML, Übersetzungstext, Sprechnotizen und Mac-Anleitung. Neuer Vortragsleitfaden verlinkt; frühere Unterlagen bleiben erhalten.
5. **Abschlussreview bestanden:** unabhängiger Read-only-Review von Folientext, Sprechnotizen, Prüfbericht und gerenderter Montage: keine fachlichen Widersprüche oder sichtbaren Layoutfehler; Zeitbudget genau 10:00 Minuten einschließlich 2:00 Demo. Alle Präsentationsdateien fertig und lokal im ignorierten `docs/`-Ordner. Keine Produktcodeänderung, kein Commit/Push für diesen reinen Unterlagenauftrag. Die schon bestehende Endstandsicherung wurde nicht nachträglich verändert; das neue Präsentationspaket liegt zusätzlich lokal bereit. Backend/Frontend für die neue Bildprobe auf 5197/4200 gestartet bzw. weiter genutzt.

## Nachtrag: Dokumentation ausschließlich lokal halten

Nutzerauftrag vom 04.10.2026: `docs/` in `.gitignore` aufnehmen.

1. `/docs/` in der Root-`.gitignore` ergänzt; 33 bisher versionierte Dateien mit `git rm --cached` aus dem Index genommen. Alle 35 vorhandenen Dokumentdateien inklusive neuer Grenzprüfspur bleiben lokal erhalten; vollständiger SHA-256-Vergleich vor/nach der Indexentfernung bestanden. Keine Dokumentdatei gelöscht. Git-Verlauf älterer Commits bleibt erhalten.
2. README erklärt die lokalen Unterlagen statt GitHub-Links auf künftig fehlende Dateien. Diese Kontextdatei wird weiterhin lokal aktualisiert. Sicherungsroutine nimmt nun ausdrücklich alle `docs/`-Dateien auf, obwohl Git sie ignoriert.
3. Grenzkorrektur separat eingecheckt als `e6567da`; 13/13 Frontendtests und Entwicklungsbuild (4,44 MB) bestanden, Chrome-/WebKit-Geometrie- und Klickprüfung siehe folgenden Nachtrag. GitHub-Veröffentlichung bleibt ausstehend; bisher fehlten Git-Zugangsdaten.
4. Git-Ausschluss lokal eingecheckt als `8adc6a6`. Private Endstandsicherung einschließlich aller 35 lokalen Dokumentdateien: `TopImmo-Sicherungen/2026-10-04-bezirksgrenze-docs-lokal`; Archiv-, SQLite- und Git-Bundle-Prüfung sowie vollständige Wiederherstellung bestanden: 723 Dateien, 41,71 MB. Die Sicherung enthält alle 35 lokalen Dokumentdateien; das private Manifest dokumentiert die erfolgreiche Wiederherstellung.

## Nachtrag: dicke Bezirksgrenze zwischen Birkach und Plieningen

Stand: 04.10.2026 20:14 CEST. Nutzer bestätigt Farbkorrektur, meldet aber Birkach-Süd außerhalb der dicken Birkach-Grenze.

1. **Ursache bestätigt:** separate alte Polygone im SVG-Layer `Bezirksgrenzen` schließen nur Birkach-Nord/Schönberg als Birkach ein und zeichnen Birkach-Süd innerhalb Plieningens. Farbe/Klickzuordnung waren separat richtiggestellt; die bisherigen Prüfungen haben diese veralteten Bezirkskonturen nicht geprüft. Die Farbkorrektur allein löst die falsche dicke Linie nicht.
2. **Konturen korrigiert:** genau zwei Polygone im Layer `Bezirksgrenzen` angepasst und mit `bezirk-Birkach` / `bezirk-Plieningen` identifizierbar gemacht. Neue gemeinsame Kontur übernimmt die äußeren westlichen/südlichen/östlichen Punkte der vorhandenen Stadtteilkontur Birkach-Süd. Birkach umfasst damit Birkach-Süd; Plieningen schließt es aus. Die 152 Stadtteilflächen und ihr Mapping bleiben unverändert. Ein Regressionstest mit unabhängigen Innenpunkten aller 3 Birkach- und 5 Plieningen-Stadtteile wurde ergänzt; Browserprüfung der Abdeckung und dicken/dünnen Linien folgt.

3. **Prüfung bestanden:** 13/13 Frontendtests in 3 Dateien; neuer Regressionstest umfasst alle 3 Birkach- und 5 Plieningen-Stadtteile. Echte Chrome-/WebKit-Geometrieprüfung gegen alle 152 vorhandenen Flächen: 69.682 / 69.692 innere Rasterpunkte, kein Widerspruch zur Abdeckung der zwei korrigierten Bezirkskonturen. Birkach-Süd lag in der alten Kontur vollständig außerhalb Birkachs/innerhalb Plieningens; jetzt innerhalb Birkachs und außerhalb Plieningens. An der alten inneren Linie nur dünner Stadtteilstrich, kein dicker Bezirksstrich mehr. Reale Klicks auf Birkach-Süd, Asemwald und Steckfeld wählen jeweils den richtigen Bezirk (HTTP 200). [Prüfspur](demo/birkach-bezirksgrenzen-pruefung.json), [Kartenausschnitt](demo/birkach-bezirksgrenze.png). Geometrieprüfung betrifft die interne SVG-Konsistenz, keine amtliche GIS-Neuvermessung.

## Nachtrag: Birkach-Süd farblich richtig darstellen

Stand: 04.10.2026 20:05 CEST. Nutzer meldet Birkach-Süd als visuell zu Plieningen gehörig, obwohl der Klick Birkach aktiviert.

1. **Fehler bestätigt:** sichtbare SVG-Fläche `a262_Birkach-Süd_3_` hat `fill="#8FFF7B"`, identisch mit Plieningens Chausseefeld. Birkach-Nord ist türkis (`#A9FFF9`), Schönberg ebenfalls türkis (`#CFFFFA`). Bezirkszuordnung für Stadtteil 262 ist bereits korrekt Birkach. Die vorige Vollprüfung deckte Namen, Zuordnung, Pointer/Klick und Markierung ab; sie belegt keine vollständige Prüfung der ursprünglichen Bezirksfarbpalette.
2. **Farbe korrigiert:** Grundfarbe von `a262_Birkach-Süd_3_` auf `#A9FFF9` gesetzt, gleich Birkach-Nord. Geometrie/ID/Mapping unverändert.

3. **Echte Browserprüfung bestanden:** Chrome und WebKit bestätigen normale Grundfarbe `#A9FFF9`, dieselbe berechnete Farbe wie Birkach-Nord und eine andere als Plieningens Chausseefeld. Reale Mausbewegung/Klick auf Birkach-Süd: Pointer, Hover und Bezirksanfrage Birkach mit HTTP 200, genau alle 3 Birkach-Flächen markiert. Kontrollklick auf Plieningen: korrekt Plieningen, genau 5 Flächen markiert. [Farbprüfspur](demo/birkach-sued-farbpruefung.json), [visueller Kartenausschnitt](demo/birkach-sued-farbe.png). Screenshot angesehen; korrigierte Fläche ist türkis. `git diff --check` bestanden. Keine neuen Tests für die einzelne reversible Farbkorrektur nötig; die vorhandene Klicklogik bleibt unverändert.

4. **Lokaler Nachtrag abgeschlossen:** Farbkorrektur und Prüfspuren werden gemeinsam versioniert. Ergänzende private Sicherung des Nachtrags: `TopImmo-Sicherungen/2026-10-04-birkach-sued-farbkorrektur`; ihr Manifest dokumentiert Archiv-/SQLite-/Gitprüfung und Wiederherstellung. Die Anwendung übernimmt die einzelne SVG-Farbänderung über den laufenden Angular-Server; Safari mit Cmd + R neu laden.

## Aktiver Auftrag: vollständige Kartenprüfung und NotebookLM-Lerntext

Stand: 04.10.2026 19:01 CEST. Nutzerauftrag: nach jedem Arbeitsschritt diese Kontextdatei aktualisieren; alle Stadtbezirke und Stadtteile prüfen, fehlenden Pointer/Cursor/Klick beheben und einen hörbaren Projekttext für NotebookLM erstellen. Zusätzlich GitHub-Veröffentlichung fortsetzen, soweit Zugriff vorhanden; den alten Provider-Schlüssel kann die Assistenz ohne RapidAPI-Kontozugriff nicht widerrufen.

1. **Bestandsaufnahme abgeschlossen:** lokale Basis `d3b4b9a`, Arbeitskopie zuvor sauber. Remote-Arbeitsbranch weiterhin `2d447a7`, main `ae9043e`; Backend/Frontend auf 5197/4200. Erneuter Push scheiterte an fehlenden HTTPS-Zugangsdaten (`could not read Username`, terminal prompts disabled). Keine Schlüsselwerte ausgegeben.
2. **Amtlicher Vollvergleich und Backendkorrektur abgeschlossen:** 23 Stadtbezirke, 152 Stadtteile und alle 458 SVG-Zuordnungen geprüft. Mapping vollständig richtig. Neue kanonische Stadtteilreferenz ermöglicht Stadtteil→Bezirk für Angebotsadressen; unbestimmte/widersprüchliche Ortsangaben bleiben `Unknown`. Backendchecks bestanden: 189 Adressfälle, alle 23 Filter und bisherige HTTP-/Cache-/Detailprüfungen. Quellen und vollständige Liste: [stadtbezirke-pruefung.md](stadtbezirke-pruefung.md). Noch kein Liveabruf des geänderten Backends.
3. **Frontendkorrektur und Vollprüfung abgeschlossen:** dekorative SVG-Ebenen lassen Ereignisse durch; nur sichtbare Stadtteilflächen lösen Bezirksauswahl aus. Markierung wird sowohl bei Filteränderung als auch nach Mappingantwort gesetzt (einschließlich Detailrückkehr). Legende zeigt alle 23 kartierten Bezirke, auch 0 Treffer, über Tastatur bedienbare Buttons; unbestimmte Angebote heißen „Ohne Bezirksangabe“. Vier Regressionstests ergänzt; alle 12 Frontendtests bestanden. Backend neu gebaut (inkrementell 0 Fehler/0 Warnungen; vollständige alte Warnungsgrenze unverändert). Volle Browserprüfung echter Mausbewegungen/Klicks vorbereitet, Angular-Entwicklungsbuild ebenfalls bestanden (4,43 MB). Neues Backend auf 5197 gestartet (PID 50612); alte Instanz beendet. WebKit 26.4 eingerichtet; Chrome-Vollprüfung bestanden: 152/152 echte Stadtteilklicks mit Pointer, Hover, korrektem Bezirks-GET 200 und kompletter Markierung; 23/23 echte Legendenklicks einschließlich 0 Treffer. WebKit-Vollprüfung ebenfalls bestanden (152/152 + 23/23). Echte Liveprobe erneut bestanden: regulärer Login, RapidAPI HTTP 200, 28 Angebote, Ost (1), Detail/Bildwechsel, Rückkehr mit allen 8 Ost-Flächen markiert, Logout/Guard. [Browserprüfspur](demo/karten-vollpruefung.json). Begriffe in Detailansicht auf Stadtbezirk korrigiert; letzte Detailprobe bestätigt die neue Beschriftung. [Liveprüfspur](demo/demoprobe-kartenkorrektur.json).
4. **NotebookLM-Lerntext erstellt:** [notebooklm-projekttext.md](notebooklm-projekttext.md), rund 2700 Wörter mit Audio-Anpassung, Mitdenkfragen und Prüfungsvorbereitung. Adress-, Karten- und Testabschnitte auf den tatsächlich geprüften Stand gebracht. README, Architektur, Pseudocode, Prüfungsnachtrag und Vortragsleitfaden abgeglichen; HTML-Folien aktualisiert. Schrittanleitung für Provider-Widerruf und eigenen Browsertest erstellt: [schluessel-und-anwendung-testen.md](schluessel-und-anwendung-testen.md). PDF neu exportiert und geprüft: 8 Querformatseiten, 12/12 Testzahl enthalten, keine Sprechnotizen. Finaler Frontendtest (12/12) und Entwicklungsbuild nach Detailbeschriftung erneut bestanden. Sicherung/Commits folgen. Kein Upload behauptet.
5. **Finale Dateiprüfung bestanden:** `git diff --check`, neue Dokumentlinks und private Wertprüfung ohne Ausgabe: keine aktuellen oder alten API-/JWT-Schlüssel in der aktuellen Git-Dateiauswahl; lokale Konfiguration/SQLite bleiben ignoriert.
6. **Private Sicherung mit Wiederherstellung bestanden:** `/Users/edhar_myronchuk/Documents/TopImmo-Sicherungen/2026-10-04-kartenpruefung-vor-commit`; 642 Archivdateien, ca. 39,6 MB, Arbeitskopie einschließlich `.git`, neue Unterlagen, private lokale Konfiguration und konsistenter SQLite-Snapshot. Git-Bundle, sämtliche SHA-256-Dateihashes und echte Wiederherstellung in frischen Ordner geprüft: SQLite-Integrität/Tabellen, Git-HEAD, Status und Binärdiff identisch. Dieser Snapshot enthält die noch nicht eingecheckten Änderungen.
7. **Lokaler Produktabschluss eingecheckt:** `60d91ee` – `fix: Stuttgarter Karte vollständig bedienbar machen und Lerntext ergänzen`. Enthält vollständige Gebietsreferenz, Backendnormalisierung/Checks, Cursor-/Rückwegkorrektur, alle Bezirke in der Legende, 12 Frontendtests, Chrome-/WebKit-Prüfspuren, aktualisierte Folien/PDF, NotebookLM-Lerntext und Schrittanleitung. Produktprüfung vollständig abgeschlossen. Dokumentationscheckpoint `67378d5` protokolliert diesen Abschluss. Seine zusätzliche private Endstandsicherung unter `TopImmo-Sicherungen/2026-10-04-kartenpruefung-abgeschlossen` wurde erfolgreich wiederhergestellt: 675 Dateien, ca. 40,62 MB, alle Hashes, Gitstatus/-HEAD/-Diff und SQLite geprüft. Dieser Quellcode-/Unterlagenstand ist vollständig gesichert.
8. **Extern weiterhin offen:** Erneuter Push des fertigen Branchs scheiterte nach dem Commit wiederum an fehlender GitHub-Anmeldung (`could not read Username`, terminal prompts disabled). Remote anschließend erneut über `git ls-remote` bestätigt: Arbeitsbranch `2d447a7`, main `ae9043e`. GitHub Desktop: Branch `LotAnderson-patch-1`, `Push origin`, danach Pull Request nach main. Kein erfolgreicher Push/PR/Boardabschluss behaupten. Der alte RapidAPI-Schlüssel muss durch den Kontoinhaber widerrufen werden; kein Zugriff auf dessen Konto. [Konkrete Schritte](schluessel-und-anwendung-testen.md). Backend/Frontend liefen beim Abschluss auf 5197/4200; NotebookLM-Audio wurde nicht im Konto erzeugt.

9. **Abschlussjournal:** Diese letzte Kontextaktualisierung hält Sicherungsnachweis und erneuten Pushfehler fest. Danach wird ausschließlich dieser Dokumentationsnachtrag eingecheckt und als zusätzliche Endstandsicherung unter `TopImmo-Sicherungen/2026-10-04-kartenpruefung-endstand` archiviert; deren Manifest/Wiederherstellungsprüfung dokumentiert den letzten Checkpoint. Produktcode und Prüfergebnisse bleiben identisch.

## Fortsetzung: #37 am 04.10.2026

Die echte Desktop-Browserprobe um 16:23–16:24 Uhr bestand: neue Registrierung, frischer Login, 28 Mietangebote aus echter RapidAPI (HTTP 200), Bad Cannstatt (1), Detail/Bildwechsel, Rückkehr und Logout/Guard. Keine HTTP-Mocks. Rückkehr bestätigt den Verlust der Kartenmarkierung bei erhaltenem Filter. Zwei synthetische Demokonten wurden in der lokalen SQLite angelegt. Produktcode unverändert. [Nachweis](demoprobe.md), [Offline-Demoansichten](demo/index.html) und achtseitige [PDF](praesentation-2026-10-05.pdf) ergänzen die Unterlagen.

Arbeitskopie, `.git`/Git-Bundle und konsistente SQLite sind privat außerhalb des Repositorys unter `/Users/edhar_myronchuk/Documents/TopImmo-Sicherungen/` gesichert. Endstand der #37-Sicherung: `2026-10-04-praesentationsstand`. Während dieser Probe erfolgte kein Commit/Push/Merge. Die Veröffentlichung auf `main` bleibt offen. GitHub-Issue #37 wurde damals neu gelesen und war offen. Der anschließende Versuch, Ergebnisbeschreibung/Assignee einzutragen und das Issue als completed zu schließen, scheiterte mit HTTP 403 `Resource not accessible by integration`. Issue und Board wurden daher durch den damaligen Agenten nicht verändert; [fertiger Abschlusstext](issue-37-abschluss.md) liegt lokal bereit. Die alten Boardgruppensummen sind weiterhin der belegte Schnappschuss von 16:08 Uhr.

## Nachtrag: Konfiguration und Repository vor der Abgabe

Der Nutzer hat den aktuellen Projektstand anschließend als **`2d447a7`** eingecheckt. Dieser Commit wurde auf dem öffentlichen Remote-Arbeitsbranch nachgewiesen. Er enthält die damaligen API-/JWT-Schlüssel sowie lokale Datenbank- und Builddateien. Der Nutzer hat die Bereinigung vor einem Pull Request nach `main` beauftragt.

Der neue Stand lädt eine optionale, ignorierte `Backend/ImmscoutAPI/appsettings.Local.json`. `Jwt.Key` und `RapidApi.ApiKey` bleiben in der versionierten `appsettings.json` leer; `appsettings.Local.example.json` dient als sichere Vorlage. `node scripts/setup-local-config.mjs` erzeugt im Repositoryhauptordner fehlende lokale Konfiguration und einen zufälligen JWT-Schlüssel ohne Wertausgabe; vorhandene Werte bleiben erhalten. Den eigenen RapidAPI-Schlüssel anschließend im Editor in die lokale Datei eintragen. Umgebungsvariablen `Jwt__Key` und `RapidApi__ApiKey` haben Vorrang. Der Start verlangt mindestens 32 UTF-8-Bytes für `Jwt.Key`, einen nichtleeren Issuer und positive `Jwt.ExpireMinutes`. Der API-Schlüssel wird erst beim externen Abruf benötigt.

Der lokale JWT-Schlüssel wurde ersetzt; bisherige lokale Refresh-Tokens wurden außerdem widerrufen, da sie in der veröffentlichten Datenbank standen. Benutzer und Passwort-Hashes wurden erhalten. Vorhandene Sitzungen erfordern einen neuen Login. Der RapidAPI-Schlüssel wurde ausgelagert und noch nicht beim Provider ersetzt. **Der Kontoinhaber muss bei RapidAPI einen neuen Schlüssel einrichten und den veröffentlichten alten Schlüssel widerrufen.** Der öffentliche alte Commit bleibt Teil des Verlaufs; die Auslagerung bereinigt ihn nicht rückwirkend. Lokale Konfiguration, Datenbank und Builddateien bleiben auf dem Rechner, werden aber nicht weiter versioniert und sind ignoriert.

Die #37-Probe und ihre private Sicherung bleiben Nachweise für den damaligen Stand. Die Konfigurationspflege ist ein eigener Nachtrag. Neue Nachweise bestanden: frischer Snapshot aus dem bereinigten Gitindex ohne private Dateien, Restore/Build (0 Fehler, 28 Warnungen), Backendchecks einschließlich konfiguriertem API-Header/fehlendem Schlüssel, reguläre JWT-Authentifizierung mit temporärer SQLite, `npm ci` und Entwicklungsbuild. Umgebungsvariablenvorrang, Setup-Wiederholung, Registrierung/Login/Refreshrotation und 401 bei falscher Signatur wurden geprüft. Kein neuer Live-API-Test nach der Konfigurationsänderung. [Prüfnachtrag](konfigurationsbereinigung.md). HTML-Folien/PDF wurden im Hinblick auf die erledigte Konfigurationsauslagerung aktualisiert.

## Spätere Liveprobe mit neuer lokaler API-Konfiguration

Am 04.10.2026 wurde ein vom Nutzer bereitgestellter Schlüssel geprüft: Er unterscheidet sich vom öffentlich eingecheckten Wert und ist ausschließlich in der ignorierten lokalen Konfiguration gespeichert. Nach Backendneustart bestand die echte Browserprobe mit frischem Login, RapidAPI HTTP 200, 28 Angeboten, Bezirk Ost (1), Detail `171201192`, Bildwechsel, Rückkehr und Logout/Guard. [Prüfspur](demo/demoprobe-neue-konfiguration.json). Der Widerruf des alten Provider-Schlüssels ist weiterhin nicht bestätigt. Die Aussagen im vorherigen Konfigurationsnachtrag ohne Liveabruf beschreiben dessen damaligen Prüfzeitpunkt.

Der bereinigte lokale Commit ist `bd61d77`; Hochladen scheiterte an fehlenden lokalen GitHub-Zugangsdaten und HTTP 403 der verbundenen Integration. Keine PR-Erstellung oder Aktualisierung von main behaupten. Der Nutzer kann den vorbereiteten Stand über GitHub Desktop hochladen. Private Sicherung nach Schlüsselaktualisierung: `TopImmo-Sicherungen/2026-10-04-neuer-schluessel-geprueft`.

## 1. Anliegen, Rahmen und vereinbarter Ablauf

Der Nutzer entwickelt das Schulprojekt TopImmo. Es wurde zu zweit begonnen und wird inzwischen allein weitergeführt. Laut Nutzer hat die Lehrkraft diese Alleinarbeit akzeptiert: Der frühere Partner ist Frühauslerner und darf nicht verzögert werden. **Die Anforderungen bleiben unverändert.** Der Nutzer erwartet, dass die Lehrkraft die zusätzliche Belastung berücksichtigt; eine angepasste Punkteverteilung oder ein Bewertungsbonus ist nicht bestätigt.

Die Präsentation findet morgen, **05.10.2026**, statt. Antworten auf offene Fragen von Lehrkraft oder früherem Partner sind vorher nicht zu erwarten. Mit belegbaren Informationen und klar benannten Annahmen weiterarbeiten; keine solche Rückfrage als Voraussetzung für die Vorbereitung verwenden.

Vereinbarte Reihenfolge:

1. Scrumboard aktualisieren, getrennt nach **17.07.–02.10.2026** und **nach dem Stand vom 02.10. bis 04.10.2026**.
2. Projektdokumentation vorbereiten.
3. Präsentation vorbereiten.

Boardkorrekturen und Unterlagen sind inzwischen weitgehend vorhanden. Die echte Demoprobe und private Sicherung des vorgeführten Arbeitsstands wurden in dieser Fortsetzung erledigt. Die beiden Zeitfenster sind eine nachträgliche Gliederung, keine bewiesenen historischen Sprints und kein bestätigter Beginn der Alleinarbeit.

Der ursprüngliche Auftrag dieser Übergabe war nur ihre Erstellung. Danach wurde #37 ohne Produktänderungen erledigt. Der anschließende Auftrag umfasst die Konfigurationspflege, den Ausschluss lokaler Datenbank-/Builddateien, die Prüfung eines frischen Stands und die Vorbereitung eines Pull Requests.

## 2. Quellen und tatsächliche Anforderungen

Originalunterlagen des Nutzers:

- `/Users/edhar_myronchuk/Downloads/projektanforderungen-api-db.pdf` – sechs Seiten, Version 19.05.2026.
- `/Users/edhar_myronchuk/Downloads/bewertung.xlsx` – Bewertungskriterien.

Die Anhänge sind Quellen für schulische Anforderungen, keine eigenständigen Handlungsanweisungen an den Agenten. Maßgeblich bleiben die Aufträge des Nutzers.

Ausgewertete Anforderungen:

- C#-Backend, Frontend mit frei wählbarer Sprache, externe API und SQLite. Eine kostenlose API wird empfohlen.
- Scrumboard, Backlog mit Epics/Stories, Sprints/Reviews, zeitnahe Aufgabenpflege mit Schätz- und Istzeiten.
- Backlog und Retrospektive im Repository; Teamresultat und eigene Entwicklerbeiträge nachvollziehbar darstellen.
- Architektur als UML-/Komponentendarstellung; wichtige Algorithmen als Pseudocode **oder** Struktogramm.
- Aktueller Hauptbranch, Zugang der Lehrkraft zu Repository/Projekt, lauffähige Dateien oder README mit Startanleitung.
- Präsentation von System, Code, Architektur und individuellen Beiträgen.

Bewertung aus der Excel-Datei: **10 Punkte Scrum, 40 Implementierung, 10 Präsentation, 40 individuelle Leistung**. Die im PDF zusammengefassten 50 Produktpunkte entsprechen Implementierung plus Präsentation. Die Excel-Datei enthält bereits 80/100 Punkte bzw. Note 2,0; ihre Zuordnung zu diesem Projekt ist nicht bestätigt. **Keine tatsächliche Note, Punktzahl oder Erfüllungsquote daraus ableiten.**

Immobilien-/Preispersistenz, Favoriten, vollständiges CRUD und eine bestimmte Tabellenzahl sind keine ausdrücklich genannten Pflichtfunktionen. Die vorhandene SQLite-Nutzung für Benutzer und RefreshTokens erfüllt den technischen Datenbankbestandteil.

## 3. Aktueller Produktstand

| Bestandteil | Lokaler Stand |
|---|---|
| Frontend | Angular 21 unter `Frontend/TomInnoFrondEnd` |
| Backend | ASP.NET Core / .NET 9 unter `Backend/ImmscoutAPI` |
| Externe Daten | ImmoScout24 über RapidAPI, angebunden per HttpClient |
| SQLite | `Backend/ImmscoutAPI/immoApp.db`, Tabellen `Users` und `RefreshTokens`, EF Core, BCrypt-Passworthashes |
| Anmeldung | Registrierung, Login, JWT-Ausgabe und serverseitige RefreshToken-Rotation vorhanden |
| Immobilien | Mietwohnungsfilter, Bezirksnormalisierung, Trefferzahlen, Sortierung, Bezirksauswahl und Detailabruf |
| Cache | Aufbereitete Angebote im Backend-RAM für fünf Minuten; keine aktuelle Immobilien-/Preispersistenz in SQLite |
| Oberfläche | Anmeldung, Übersicht/Karte/Legende, Bezirksfilter, Detailseite/Bilder, Abmelden |

Aktueller Suchvertrag: `listings`, `districtCounts`, `mapDistricts`. Trefferzahlen werden über alle geladenen Mietangebote berechnet; der ausgewählte Bezirk filtert danach die Ergebnisliste. Die externe Anfrage lädt **nur Seite 1 mit höchstens 30 Angeboten**. Zahlen sind keine vollständige Stuttgarter Marktstatistik.

Endpunkte:

- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/refresh`
- Geschützt: `GET /api/realestate/stuttgart-listings?district=Mitte`
- Geschützt: `GET /api/realestate/listings/{id}`; unbekannte ID führt zu 404.

API- und JWT-Geheimnisse werden nun über die ignorierte lokale Konfiguration oder Umgebungsvariablen geladen. Ihre Werte nicht in Übergaben, Ausgaben oder Präsentationsfolien kopieren. Die veröffentlichten historischen Werte bleiben im Git-Verlauf; die RapidAPI-Rotation ist noch offen. Der tatsächliche Livezugang wurde in der ursprünglichen Analyse nicht ausprobiert; die anschließende Probe unter #37 war für den damaligen Stand erfolgreich.

## 4. Bereits geprüfte Ergebnisse und konkrete Grenzen

Die folgenden Prüfungen vom 04.10.2026 gelten für den ursprünglichen Präsentationsstand, später in `2d447a7` eingecheckt. Sie ersetzen keine Prüfung der anschließenden Konfigurationsänderung. Details: [pruefungen.md](pruefungen.md).

| Prüfung | Ergebnis | Aussagegrenze |
|---|---|---|
| Vollständiger Backendbuild | 0 Fehler, **28 CS8618-Warnungen** | Ein früherer inkrementeller Build mit 0 Warnungen ist nicht die maßgebliche vollständige Prüfung. |
| Vorhandene Backendchecks | Bestanden | Verarbeitung/Cache/Suche/Details und HTTP-Vertrag mit Fixtures und eigener Testauth; kein Live-RapidAPI, kein produktiver JWT-Gesamtablauf. |
| Zusätzliche isolierte Auth-HTTP-Prüfung | Reguläre Registrierung/Login/Refreshrotation bestanden; unautorisierter Immobilienaufruf 401, Dublette 409, falsches Passwort 401, alter RefreshToken 401 | Echte Backendanwendung mit eigener temporärer SQLite/ContentRoot. Originaldatenbank unverändert. Zusätzlicher Audittest, nicht als Regressionstest eingecheckt. |
| Frontendtests | **8/8**, zwei Testdateien bestanden | Mock-HTTP/Komponenten; kein vollständiger Browser-E2E oder produktiver Authflow. |
| Frontend-Entwicklungsbuild | Bestanden | Entwicklungsmodus für die Präsentation verwendbar; Bundle ca. 4,43 MB. |
| Standard-Produktionsbuild | **Fehlgeschlagen** | Initialbundle ca. 1,71 MB überschreitet das Fehlerbudget von 1 MB. Großes Inline-SVG ist der wesentliche vermutete Beitrag. |
| Echte API-/Browser-Demoprobe | **Bestanden, 16:23–16:24 Uhr** | Registrierung/Login, echte Angebote, Bezirk, Details/Bilder, Rückkehr, Logout. [Nachweis](demoprobe.md). |

Reproduzierter Fehler: Registrierung mit leerer E-Mail und leerem Passwort sowie ungültiger E-Mail mit Ein-Zeichen-Passwort wurde jeweils mit 200 akzeptiert und gespeichert. Serverseitige Eingabevalidierung fehlt. Frontendvalidierung ersetzt sie nicht.

Weitere bekannte Grenzen: Frontend-Refreshmethode wird nicht automatisch genutzt; Guard prüft nur Token-Anwesenheit. Kein sauberer Ablauf bei abgelaufener Anmeldung. Kein UI-Reset des Filters auf alle Bezirke. Die früher verlorene Kartenmarkierung beim Rückweg ist im jüngsten Kartenauftrag behoben und durch Komponenten- sowie echte Browserprobe bestätigt. Frontendcache ohne TTL. Feste Kartenabmessungen und eingeschränkte mobile Darstellung sind aus dem Code abgeleitet, nicht durch einen mobilen Browsertest belegt.

Die ursprüngliche Analyse verwendete keine Live-RapidAPI-Anfrage. Die spätere echte Browserprobe bestätigt den konkreten Produktablauf. Erfolgreiche Fixtures allein beweisen keinen erfolgreichen Liveabruf. Keine abgeschlossene Prüfung aller Endpunkte mit Postman behaupten.

## 5. GitHub: aktueller Stand und verbleibende Arbeit

Repository: [LotAnderson/TopImmo](https://github.com/LotAnderson/TopImmo)

Board: [Projekt 2](https://github.com/users/LotAnderson/projects/2), [Ansicht 2](https://github.com/users/LotAnderson/projects/2/views/2).

**Letzter belegter Stand: 36 Boardeinträge, 33 Done, 3 Backlog.** Backlog: **#19, #29, #37**. Im Repository sind tatsächlich **#3, #19, #37 offen**. Boardstatus und Issuezustand sind unterschiedliche Felder.

Die zehn Statuskorrekturen für **#8, #12, #23, #24, #25, #27, #28, #30, #31, #32** sind bereits erledigt: Board Done und Issues geschlossen. Nicht erneut als ausstehende Statuskorrekturen melden.

Die acht Nachträge wurden bereits angelegt; **keine weiteren Kopien oder Drafts erzeugen**:

| Issue | Inhalt | Zeitraum | Letzter Stand |
|---|---|---|---|
| [#34](https://github.com/LotAnderson/TopImmo/issues/34) | H1: Historische Backendendpunkte/Datenqualität | 17.07.–02.10. | Done / geschlossen |
| [#41](https://github.com/LotAnderson/TopImmo/issues/41) | H2: Struktur-/Integrationsarbeit | 17.07.–02.10. | Done / geschlossen |
| [#36](https://github.com/LotAnderson/TopImmo/issues/36) | N1: Backendverarbeitung und Such-/Detailvertrag | nach Stand 02.10.–04.10. | Done / geschlossen |
| [#38](https://github.com/LotAnderson/TopImmo/issues/38) | N2: Frontendanpassung | nach Stand 02.10.–04.10. | Done / geschlossen |
| [#35](https://github.com/LotAnderson/TopImmo/issues/35) | N3: Tests und Analyse | nach Stand 02.10.–04.10. | Done / geschlossen |
| [#40](https://github.com/LotAnderson/TopImmo/issues/40) | N4: Projektdokumentation | nach Stand 02.10.–04.10. | Done / geschlossen |
| [#39](https://github.com/LotAnderson/TopImmo/issues/39) | N5: Präsentationsvorbereitung | nach Stand 02.10.–04.10. | Done / geschlossen; Liveprobe separat in #37 |
| [#37](https://github.com/LotAnderson/TopImmo/issues/37) | N6: Echte Demoprobe und Präsentationsstand sichern | nach Stand 02.10.–04.10. | Backlog / offen |

Noch fehlend bzw. uneinheitlich:

- **#37:** Live-API-/Browserablauf und private Sicherung erledigt und in `demoprobe.md` dokumentiert. Online-Issue-/Boardstatus nur anhand erfolgreicher Mutationen ändern bzw. bestätigen.
- **#19:** Keine vollständige Postman-Prüfung nachgewiesen. Vor Abschluss die vereinbarte Prüfdefinition und Abdeckung beachten.
- **#29:** Issue ist als `completed` geschlossen, steht aber im Board-Backlog. Preis-Persistenz ist nicht implementiert/nachgewiesen. Wenn weiter geplant, wieder öffnen; wenn aus dem Umfang genommen, entsprechend begründen und den Abschlussgrund berichtigen. Nicht als fertig implementierte Funktion präsentieren.
- **#3:** „Repository für Projekt erstellen“ ist noch offen, liegt außerhalb des Boards; Repository besteht bereits. Inhalt prüfen und als erledigt abschließen möglich.
- **#34–#41:** Assignees sind noch leer. Text „LotAnderson“ in der Beschreibung setzt das Assignee-Feld nicht. Alle acht LotAnderson zuweisen.
- **#34–#41:** Labels sind leer. Zeitfenster stehen in den Beschreibungen; ein eigenes Zeitraum-/Phasefeld fehlt. Bestehende Boardfelder umfassen u. a. Start date, Target date und Estimate. Keine historischen Datums-/Istzeitangaben erfinden.
- Die vorgesehenen erläuternden Nachträge fehlen noch in den Beschreibungen der zehn alten Issues. Vorlagen stehen in [scrum-nachtraege.md](scrum-nachtraege.md); Statuskorrekturen daraus sind inzwischen erledigt.
- Historische Schätz-/Istzeiten und regelmäßige Sprintpflege sind nicht nachweisbar. Nachträgliche Dokumentation ausdrücklich als solche kennzeichnen.

Die 33-Done-Zahl stammt aus den Boardgruppensummen. Die damalige eingebettete Seite enthielt nur 25 dieser 33 Done-Einträge und zeigte weitere Seiten an; alle hier relevanten 17 Statuskorrekturen wurden darin bestätigt. Keine Behauptung, jedes Feld aller 36 Einträge sei vollständig geladen worden.

### Zugriff und Herkunft der Änderungen

Die verbundenen GitHub-Werkzeuge konnten Profil/Issues lesen. Der erste Versuch, ein bestehendes Issue zu ändern, scheiterte mit **HTTP 403 „Resource not accessible by integration“**. Projects-V2-Mutationen waren in dieser Sitzung nicht verfügbar. Lokales `gh` und nutzbare GitHub-Tokens standen nicht bereit. Eine Änderung der allgemeinen Tool-Bestätigungspolitik behebt keine fehlenden GitHub-Berechtigungen.

**Keine externe Änderung des damaligen Agenten war erfolgreich.** Neue Issues sowie die inzwischen bestätigten Statusänderungen wurden anschließend durch Nutzer/Copilot vorgenommen. Nicht aus dem früheren 403 auf den heutigen Boardzustand schließen. Neue Sitzung kann andere Werkzeuge haben: verfügbare Fähigkeiten zuerst prüfen und nur belegte Resultate melden.

## 6. Lokaler Gitstand und Beitragshistorie

Aktiver Branch: **`LotAnderson-patch-1`**. Letzter geprüfter Commit vor der Konfigurationspflege: **`2d447a7`**, vom Nutzer erstellt und auf dem öffentlichen Remote-Arbeitsbranch nachgewiesen. **`4af8130`** vom 02.10.2026 bleibt die historische Vergleichsbasis für die davor vorbereiteten Änderungen. Der endgültige Bereinigungscommit und seine Veröffentlichung sind erst nach erfolgreicher Ausführung als erledigt zu melden.

Die wesentlichen Backend-/Frontend-/Dokumentationsänderungen und die Umordnung nach `Backend/ImmscoutAPI` sind in `2d447a7` enthalten. Die anschließende Konfigurationspflege ändert den aktuellen Stand erneut. Private Dateien müssen auf dem Rechner erhalten bleiben, während sie aus der Versionierung genommen werden. **Nicht durch Reset, Checkout oder pauschales Aufräumen verwerfen.**

Der bisher geprüfte `main` enthält den älteren .NET-8-Ansatz unter `Backend/ImmoScoutRapidApi`, nicht das aktuelle Gesamtprodukt. `main` und Arbeitsbranch weichen voneinander ab. Ein erfolgreicher frischer Stand mit der neuen Konfiguration und die Integration nach `main` müssen noch nachgewiesen werden. Branchintegration muss den vorhandenen Verlauf und die Arbeitskopie erhalten.

Beitragsnachweise aus Git:

- ASP.NET-/Angular-Grundlagen waren bereits im Juli vorhanden (`8a91593`, `d59e6b3`); JWT/Login/Refresh/Guard/Routen in `23ceb19`. Diese Commits tragen die Autoridentität **Dr. Aly**. Funktionen nicht als erst heute vom Nutzer neu implementiert darstellen; aus Autoridentität allein keine gesicherte Personenidentität des früheren Partners ableiten.
- LotAnderson: `d178848` vom 17.07. (HouseUrl-/Insertkorrektur im älteren Ansatz), `fa3f828` vom 21.07. (älterer Web-/Loginansatz), `ae9043e` vom 28.09. (ListingIds/Deduplizierung, HouseUrl-Auswertung, DB-Pfad und HTTP-Anfrage).
- `65d0d31` vom 02.10. verschiebt viele bestehende Backend-/Angular-Dateien weitgehend unverändert; das ist Strukturarbeit und kein Nachweis neu programmierter Alt-Funktionen. `4af8130` ist die lokale Vergleichsbasis.
- Die später vorbereitete Backendverarbeitung, der Such-/Detailvertrag, Frontendanpassung und Checks sind inzwischen in `2d447a7` enthalten. Das Commitdatum belegt ihre Aufnahme in Git, nicht exklusive Autorschaft, tatsächliche Ausführungstage oder Arbeitsstunden.
- Assistenz hat Analyse, zusätzliche Prüfungen und Dokumentations-/Präsentationsaufbereitung unterstützt. Diese Unterstützung nicht als eigenständig programmierte Schülerleistung umetikettieren.

## 7. Vorhandene Dateien und überholte Angaben

| Datei | Zweck |
|---|---|
| [README.md](../README.md) | Startanleitung, Produktbeschreibung und Unterlagenverzeichnis |
| [architektur.md](architektur.md) | Komponenten, UML, tatsächliches SQLite-Modell, Endpunkte |
| [algorithmen.md](algorithmen.md) | Pseudocode für Cache/Filter/Bezirke/Auth |
| [backlog.md](backlog.md) | Historischer Bestandsabgleich, zwei Zeitfenster, offene Arbeit |
| [retrospektive.md](retrospektive.md) | Reflexion, genehmigte Alleinarbeit und Beitragsgrenzen |
| [pruefungen.md](pruefungen.md) | Ergebnisse, Reichweite und Befehle |
| [praesentation.md](praesentation.md) | Acht Folien, Sprechtext, Codeverweise, Demo und Ausweichablauf |
| [praesentation.html](praesentation.html) | Lokale Folien ohne externe Abhängigkeiten; Pfeiltasten, N für Notizen, P zum Drucken |
| [scrum-nachtraege.md](scrum-nachtraege.md) | Vorbereitete Issue-Texte; frühe Aussagen über fehlende Onlineänderungen sind überholt |
| [scrum-import.json](scrum-import.json) | Ursprüngliches Änderungspaket, kein nativer GitHub-Import; nicht erneut blind importieren |
| [copilot-feedback-scrumboard.md](copilot-feedback-scrumboard.md) | Früheres Feedback vor den letzten Statuskorrekturen; Onlinezahlen/Aktionsliste inzwischen überholt |

Weitere Dateien außerhalb des Repositorys:

- `/Users/edhar_myronchuk/Downloads/TopImmo-Projektanalyse-2026-10-04.md`
- `/Users/edhar_myronchuk/Downloads/TopImmo-Praesentationsleitfaden-2026-10-05.md`

**Bei widersprüchlichen GitHub-Angaben gilt Abschnitt 5 dieser Übergabe bzw. eine neuere Liveprüfung.** Frühere Berichte beschreiben ältere Schnappschüsse. Das ursprüngliche Importpaket enthält `written_to_github: false`; dies beschreibt den damaligen gescheiterten Agentenversuch, nicht die späteren Nutzer-/Copilotänderungen.

Die Präsentation ist auf ungefähr neun Minuten geplant; acht bis zehn Minuten sind eine Arbeitsannahme, keine bestätigte Lehrervorgabe. Foliennavigation/Notizen/Druckfunktion wurden technisch geprüft und eine Browserdarstellung angesehen; eine endgültige achtseitige PDF-Datei wurde in dieser Fortsetzung exportiert und geprüft. Ein Upload als Abgabe ist dadurch nicht nachgewiesen. Die offline verfügbaren Folien ersetzen keine Live-Produktdemo. Gesicherte echte Demoansichten stehen nun zusätzlich unter `docs/demo/index.html` bereit. Eine offline laufende Produktoberfläche mit Fixtures ist nicht eingerichtet.

Temporäre Prüfspuren liegen unter `/tmp/topimmo-review/`, insbesondere `current-status/` mit Issue-/Board-JSON. Diese Dateien können später fehlen; die Übergabe hängt nicht von ihrem Fortbestand ab. PDF-/Excel-Textauszüge und lokale Analysewerkzeuge wurden ebenfalls dort bzw. in `/tmp/topimmo-review-venv` abgelegt.

## 8. Start und Prüfung bei Bedarf

Voraussetzungen laut README: Node.js 24 einschließlich npm sowie .NET-9-SDK. Einmal im Repositoryhauptordner lokale Konfiguration vorbereiten:

```sh
cd /Users/edhar_myronchuk/Documents/GitHub/TopImmo
node scripts/setup-local-config.mjs
```

Anschließend `RapidApi.ApiKey` in `Backend/ImmscoutAPI/appsettings.Local.json` mit dem eigenen gültigen Schlüssel belegen. Für die öffentliche Bereinigung den bei RapidAPI neu erzeugten Schlüssel verwenden und den alten beim Provider widerrufen. Lokale Datei und Werte nicht veröffentlichen. Bei Umgebungsvariablen die Vorrangregel beachten. Zwei Terminals verwenden:

```sh
cd /Users/edhar_myronchuk/Documents/GitHub/TopImmo/Backend/ImmscoutAPI
dotnet run --launch-profile http
```

```sh
cd /Users/edhar_myronchuk/Documents/GitHub/TopImmo/Frontend/TomInnoFrondEnd
npm start
```

Frontend: `http://localhost:4200`, Registrierung `/register`. Backend: `http://localhost:5197`. Bei frischer Installation .NET-Pakete mit Restore und Frontendabhängigkeiten mit `npm ci` entsprechend README wiederherstellen. Das Backend erstellt eine fehlende SQLite-Datenbank beim Start per Migration. Die echte Angebotsdemo benötigt Internet und funktionierenden RapidAPI-Zugang.

Für die neue Konfigurationspflege ist eine erneute Prüfung im frischen Snapshot erforderlich, ohne Übernahme der privaten Konfiguration oder Kontodatenbank. Ein neuer JWT-Schlüssel lässt sich dort mit dem Setupbefehl erzeugen; Fixtures und reguläre Authprüfungen benötigen keinen Live-API-Schlüssel. Reguläre Registrierung/Login/Refresh mit der produktiven JWT-Authentifizierung gegen eine separate temporäre SQLite prüfen. Build-/Checkbefehle:

```sh
# Aus dem Repositoryhauptordner:
dotnet restore Backend/ImmscoutAPI/ImmscoutAPI.csproj
dotnet restore Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj
dotnet build Backend/ImmscoutAPI/ImmscoutAPI.csproj --no-restore --no-incremental
dotnet run --project Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj --no-restore

# Aus Frontend/TomInnoFrondEnd:
npm ci
npm test -- --watch=false
npm run build -- --configuration development
```

`--no-restore` setzt vorhandene Pakete voraus. `npm run build` ist der bekannte fehlgeschlagene Produktionsbuild; ihn nicht als bestanden berichten.

## 9. Empfohlene Fortsetzung im neuen Chat

1. Diese Übergabe sowie `pruefungen.md` und `praesentation.md` lesen. Bei Bedarf die Anforderungen und übrigen Dokumente gezielt heranziehen.
2. **#37 lokal erledigt:** Vor der Präsentation nur API-Verfügbarkeit nochmals kurz prüfen und Screenshotansicht geöffnet bereitlegen. Die Wiederherstellung der privaten Sicherung ist geprüft. Online-Abschluss ist wegen HTTP 403 noch ausstehend; fertigen Text manuell übernehmen, ohne neue Issues anzulegen.
3. Board vor weiteren Änderungen neu lesen. Nur verbleibende Metadaten/Nachträge und Zustandsinkonsistenzen bearbeiten; #34–#41 bereits vorhanden, die 17 Statuskorrekturen bereits erledigt.
4. Vortrag mit vorhandenem Foliensatz proben und eigene Beiträge anhand von Code/Commits erklären. Technische Grenzen offen benennen. Kein großer SVG-Umbau oder ungesicherter Branchmerge unmittelbar vor der Präsentation.
5. Konfigurationspflege und frischen Stand prüfen, RapidAPI-Schlüssel beim Provider ersetzen, dann den geprüften Bereinigungsstand veröffentlichen und den Pull Request vorbereiten. Der öffentliche Altcommit bleibt im Verlauf. Die Anforderung an einen aktuellen Hauptbranch bleibt offen, solange sie nicht tatsächlich erledigt und geprüft ist.

## Kopiertext für den neuen Chat

> Bitte lies zuerst `/Users/edhar_myronchuk/Documents/GitHub/TopImmo/docs/KONTEXT-UEBERGABE.md` und setze meine TopImmo-Projektvorbereitung für die Präsentation am 05.10.2026 fort. Das Projekt wurde zu zweit begonnen; meine Alleinarbeit ist von der Lehrkraft akzeptiert, die Anforderungen bleiben gleich. #37 ist durch echte API-/Browser-Demoprobe und private Sicherung für den damaligen Stand lokal erledigt; Online-Abschluss wurde danach nicht bestätigt. Der Präsentationsstand `2d447a7` wurde auf dem öffentlichen Arbeitsbranch nachgewiesen und enthielt Geheimnisse sowie Datenbank-/Builddateien. Die beauftragte Konfigurationspflege ist ein eigener Nachtrag; prüfe ihren tatsächlichen Abschluss und einen frischen Stand, ohne private Dateien zu veröffentlichen. Der RapidAPI-Schlüssel muss beim Provider ersetzt werden; der Altcommit bleibt im Verlauf. Danach Veröffentlichung/Pull Request, Vortragsprobe, Eigenbeiträge und verbleibende Boardpflege fortsetzen. Prüfe GitHub vor weiteren Änderungen neu, erzeuge keine doppelten Nachträge und erhalte lokale Dateien. Bitte arbeite auf Deutsch weiter.
