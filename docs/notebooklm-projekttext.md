# TopImmo verstehen – Lerntext für NotebookLM

Stand: 04.10.2026. Zur Vorbereitung der mündlichen Präsentation am 05.10.2026. Dieser Text beschreibt den überprüften Projektstand und erklärt seine Grenzen. Er enthält keine Zugangsdaten oder Schlüsselwerte.

## So verwendest du den Text

1. Öffne NotebookLM und erstelle ein Notebook für TopImmo.
2. Füge diese Markdown-Datei als Quelle hinzu. Alternativ kopiere den Inhalt ab „1. Was TopImmo für einen Nutzer macht“ und füge ihn als Textquelle ein.
3. Wähle im Bereich Studio die Audio-Zusammenfassung und stelle Deutsch ein. Für ein Lerngespräch eignet sich das Format „Detaillierte Analyse“.
4. Kopiere den folgenden optionalen Anpassungstext in das Promptfeld und starte die Erstellung. Hier wurde nur die Quelle vorbereitet; es wurde keine Audiodatei in deinem Konto erzeugt.

Die [Google-Hilfe für Quellen](https://support.google.com/notebooklm/answer/16215270?hl=de) bestätigt Markdown und eingefügten Text als unterstützte Quellen. Die [Google-Hilfe zur Audio-Zusammenfassung](https://support.google.com/notebooklm/answer/16212820?hl=de) beschreibt Studio, Sprachauswahl und Anpassungsprompt.

## Optionaler Text für die Audio-Anpassung in NotebookLM

Diese Anpassung kann zusätzlich zur Quelle in das Feld für die Audio-Anpassung kopiert werden:

> Erstellt ein ruhiges Lerngespräch auf Deutsch mit zwei Moderierenden für einen Schüler vor seiner mündlichen Projektpräsentation. Erklärt TopImmo anhand dieser Quelle. Sprecht Fachbegriffe verständlich aus und erklärt sie beim ersten Auftreten. Verbindet Architektur und Code mit dem konkreten Nutzerablauf. Unterscheidet überprüfte Funktionen, offene Fehler und geplante Verbesserungen. Erfindet keine Arbeitszeiten, Noten, Prüfungen oder persönlichen Leistungen. Benennt gemeinsame Vorarbeit und KI-Unterstützung ehrlich. Baut ungefähr alle zwei Minuten eine kurze Denkfrage mit einer Pause vor der Erklärung ein. Wiederholt besonders: SQLite speichert Konten und RefreshTokens; Angebote liegen im Arbeitsspeicher; die Bezirkszahlen gelten nur für die geladenen Treffer; Frontend-Guard und serverseitige JWT-Prüfung haben unterschiedliche Aufgaben. Endet mit einigen typischen Prüfungsfragen und kurzen fachlich richtigen Antworten.

## 1. Was TopImmo für einen Nutzer macht

TopImmo ist eine Webanwendung zur Suche nach Mietwohnungen in Stuttgart. Ein Nutzer registriert sich, meldet sich an und erhält eine Übersicht mit einer Stadtkarte und einer Wohnungsliste. Über die Karte wählt er einen Stadtbezirk. Die Liste zeigt dann die geladenen Angebote dieses Bezirks. Ein Angebot führt zur Detailseite mit Informationen und Bildern. Anschließend kann der Nutzer zurückgehen oder sich abmelden.

Die Karte unterstützt eine räumliche Auswahl. TopImmo vermittelt keine Wohnung und schließt keine Mietverträge ab. Es ist ein Schulprojekt, das externe API-Daten verarbeitet, eine eigene Datenbank nutzt und ein Backend in C# besitzt. Die wichtigste Produktfrage lautet: Wie gelangen die Angebote einer externen Quelle nachvollziehbar und geschützt bis zur richtigen Darstellung im Browser?

Für die Präsentation ist ein konkreter Ablauf hilfreich: Anmeldung, Übersicht, Bezirk auswählen, Angebot öffnen, Bild wechseln, zurückgehen und abmelden. Der Ablauf verbindet die technischen Teile mit einem sichtbaren Ergebnis.

**Denkfrage:** Was müsste ich einem Nutzer zuerst zeigen? Eine Karte mit einer verständlichen Auswahl und die dazugehörigen Ergebnisse. Die technische Erklärung folgt diesem sichtbaren Zweck.

## 2. Die Architektur in drei Stationen

Die Oberfläche ist mit Angular 21 und TypeScript umgesetzt. Sie läuft im Browser und enthält Komponenten für Anmeldung, Registrierung, Karte, Wohnungsliste und Details. Angular organisiert außerdem die Navigation innerhalb der Anwendung. Der Browser lädt bei einem Seitenwechsel innerhalb dieser Single Page Application nicht jedes Mal eine vollständig neue Webseite vom Server.

Das eigene Backend verwendet C# mit ASP.NET Core auf .NET 9. Controller nehmen HTTP-Anfragen entgegen. Services erledigen die eigentliche Arbeit, beispielsweise Passwortprüfung oder Verarbeitung der Immobilienangebote. Über Dependency Injection erhält ein Controller die benötigten Services vom Framework. Er muss diese Abhängigkeiten dadurch nicht selbst zusammenbauen.

Das Backend besitzt zwei unterschiedliche Datenzugriffe. Es liest und schreibt Kontodaten in einer lokalen SQLite-Datenbank. Für die aktuellen Mietangebote fragt es die konfigurierte ImmoScout24-API über RapidAPI ab. Der Browser kommuniziert mit dem eigenen Backend; dieses kommuniziert mit dem externen Dienst.

Bei der lokalen Entwicklung läuft Angular normalerweise auf Port 4200 und das Backend auf Port 5197. Das sind zwei laufende Programme, auch wenn der Nutzer nur eine Oberfläche sieht.

## 3. Ein Angebot auf seinem Weg durch das System

Die externe Suchanfrage verwendet Stuttgart als Ort und den Immobilientyp Mietwohnung. Sie fordert momentan die erste Seite mit höchstens 30 Angeboten an. Weitere Seiten werden nicht automatisch geladen. In der tatsächlichen Demoprobe kamen 28 Mietangebote an. Diese Anzahl ist ein Ergebnis der damaligen Anfrage und kann sich ändern.

Der API-Service nutzt einen HTTP-Client und sendet den RapidAPI-Schlüssel als Anfrageheader. Die Antwort kommt als JSON. JSON ist ein Textformat für strukturierte Daten, beispielsweise Listen, Adressen und Preise. Das Backend deserialisiert diesen Text: Es wandelt die JSON-Struktur in C#-Objekte um.

Der DistrictDataService behält die Angebote des Typs „apartmentrent“. Er ermittelt für jedes Angebot den Bezirk, gruppiert die geladenen Angebote nach Bezirk und zählt sie. Die Suchantwort enthält drei Teile: die Liste, die Bezirkszählungen und die Zuordnung der Kartenelemente zu Bezirken. Dieser Datenvertrag sagt dem Frontend, welche Struktur es erwarten darf.

Die Zählung erfolgt vor dem Bezirksfilter. Bei einer Suche nach Mitte kann die Liste nur Mitte enthalten, während die Zählung weiterhin den gesamten geladenen Bestand beschreibt. Die Zählungen sind keine vollständige Statistik des Stuttgarter Wohnungsmarkts.

## 4. Wie ein Bezirk aus einer Adresse entsteht

Die Bezirksableitung arbeitet mit der Adresszeile des Angebots. Eine amtlich abgeglichene Referenz enthält alle 23 Stadtbezirke und 152 Stadtteile Stuttgarts. Das Backend untersucht die durch Kommas getrennten Ortsbestandteile. Es vereinheitlicht Leerzeichen und Schreibweisen, entfernt gegebenenfalls Postleitzahl, das Präfix „Stuttgart-“ oder den Zusatz „(Stuttgart)“ und vergleicht dann vollständige Ortsnamen.

Ein bekannter Stadtteil wird seinem übergeordneten Stadtbezirk zugeordnet. Beispielsweise gehört Uhlbach zu Obertürkheim. Ein bekannter Bezirksname erhält seine einheitliche Schreibweise. Fehlt eine eindeutige Ortsangabe oder widersprechen sich erkannte Bezirke, lautet das Ergebnis „Unknown“. Eine allgemeine Angabe wie „Stuttgart“ wird keinem Bezirk zugeschrieben. Die Oberfläche nennt diese Ergebnisgruppe „Ohne Bezirksangabe“.

Das ist eine Verarbeitung von Text. Sie berechnet keine Position aus geografischen Koordinaten und schätzt keinen Bezirk aus einer Straße oder Postleitzahl. Unbekannte Adressformate können deshalb unbestimmt bleiben. Die neue Zuordnung wurde mit allen 152 Stadtteilnamen, allen 23 Bezirksnamen und zusätzlichen Randfällen geprüft: insgesamt 189 Adressfälle.

**Denkfrage:** Warum kann „Keine Immobilien gefunden“ erscheinen, obwohl in einem Bezirk Wohnungen angeboten werden? Weil TopImmo nur einen begrenzten Datenbestand geladen hat, weil darin gerade kein passendes Angebot liegt oder weil eine Adresszuordnung ungenau ist. Ein leerer Filter beweist keinen leeren Wohnungsmarkt.

## 5. Karte, Stadtbezirke und Stadtteile

Die Karte ist eine SVG-Grafik. SVG beschreibt Formen und Texte als einzelne Elemente. Das Backend besitzt eine JSON-Zuordnung zwischen SVG-IDs und Stadtbezirken. Das Frontend verbindet eine Auswahl auf der Karte mit dem DistrictFilterService. Dieser hält den aktuellen Bezirk bereit. Die Wohnungsliste reagiert darauf und ruft die entsprechende Suche auf.

Stadtbezirk und Stadtteil sind unterschiedliche Ebenen. Die Oberfläche muss eindeutig vermitteln, welche Ebene tatsächlich gefiltert wird. Im hier beschriebenen Suchvertrag wird nach Stadtbezirk gefiltert. Eine anklickbare Teilfläche darf deshalb den zugehörigen Bezirk auswählen; daraus folgt keine eigene Suche nach jedem einzelnen Stadtteil.

Am 04.10.2026 wurden alle 23 Stadtbezirke, 152 Stadtteile und 458 Karten-IDs mit amtlichen Referenzen abgeglichen. Alle Zuordnungen waren richtig. Der fehlende Handzeiger entstand durch dekorative SVG-Ebenen: Beschriftungen, Flüsse und Grenzlinien lagen über den Flächen und fingen Mausereignisse ab. Diese Ebenen lassen Ereignisse jetzt durch. Die eigentliche Stadtteilfläche erhält den Handzeiger, hebt ihren Elternbezirk hervor und löst dessen Suche aus.

In Google Chrome und WebKit, der Browser-Engine von Safari, wurden jeweils alle 152 Flächen mit echten Mausbewegungen und Klicks geprüft. Für jede Fläche stimmten Handzeiger, Hover, Bezirksanfrage mit HTTP 200 und die markierte Pfadgruppe. Zusätzlich bestanden in beiden Browsern alle 23 Legendenklicks. Auch Bezirke ohne Treffer bleiben auswählbar. Die Legende bietet dafür Buttons mit Tastaturbedienung.

Die frühere verlorene Markierung beim Rückweg aus den Details ist behoben. Die Karte setzt die Auswahl sowohl bei einer Filteränderung als auch nach dem Laden der Kartenzuordnung. Eine neue echte Liveprobe bestätigte den Rückweg: Die acht Stadtteilflächen von Ost blieben markiert, und die gefilterte Liste zeigte weiter das passende Angebot. WebKit ist ein Test der Safari-Engine; die eigentliche Safari-App wurde damit nicht automatisiert geprüft. Die SVG-Grenzen wurden nicht gegen amtliche GIS-Polygone vermessen.

## 6. Warum es einen Cache gibt

Das Backend speichert die verarbeiteten Angebote fünf Minuten im Arbeitsspeicher. Dieser Zwischenspeicher heißt Cache. Eine wiederholte Suche kann damit dieselben verarbeiteten Daten verwenden, ohne sofort eine weitere externe Anfrage auszulösen. Auch die Detailseite sucht innerhalb dieses verarbeiteten Bestands.

Wenn der Cache leer oder abgelaufen ist, lädt das Backend neue Daten. Eine Semaphore koordiniert parallele Ladeversuche. Man kann sich das als Zugangsschranke vorstellen: Innerhalb eines Backend-Prozesses lädt jeweils ein Aufruf neue Daten. Nach dem Warten prüft ein weiterer Aufruf den Cache erneut, weil die Daten inzwischen bereits vorhanden sein können. Die Sperre wird auch bei einem Fehler freigegeben.

Der Cache verschwindet beim Beenden des Backends. Er erzeugt keine dauerhafte Offline-Datenbank. Außerdem puffert das Frontend die ungefilterte Übersicht über einen gemeinsamen Datenstrom. Für diese Frontendübersicht ist derzeit keine zeitgesteuerte Erneuerung vorgesehen. Die fünf Minuten des Backends garantieren deshalb keine automatisch aktualisierte Browseransicht.

**Denkfrage:** Ist ein Cache eine Datenbank? Er kann Daten halten, aber hier ist er flüchtig. Dauerhaft gespeicherte Konten und vorübergehend geladene Angebote haben unterschiedliche Lebenszyklen.

## 7. SQLite und das Datenmodell

SQLite ist eine relationale Datenbank in einer lokalen Datei. TopImmo greift mit Entity Framework Core darauf zu. Dieses Framework verbindet C#-Objekte mit Tabellen. Beim Backendstart werden Migrationen angewendet. Migrationen beschreiben Änderungen am Datenbankschema und erzeugen bei einem frischen Start die benötigte Struktur.

Die fachlichen Tabellen heißen Users und RefreshTokens. Ein Benutzer besitzt eine ID, eine E-Mail-Adresse und einen Passwort-Hash. Ein RefreshToken besitzt unter anderem seinen Tokenwert, sein Ablaufdatum, einen Widerrufsstatus und eine Benutzer-ID. Ein Benutzer kann mehrere RefreshTokens haben. Jeder RefreshToken gehört zu genau einem Benutzer. Das ist eine Eins-zu-viele-Beziehung.

Die Benutzer-ID im RefreshToken ist ein Fremdschlüssel. Er verbindet den Token mit dem passenden Benutzer. Für die E-Mail gibt es aktuell keinen eindeutigen Datenbankconstraint. Der Service prüft vor dem Anlegen auf eine bereits vorhandene Adresse. Eine Programmprüfung und eine durch die Datenbank garantierte Eindeutigkeit sind jedoch unterschiedliche Dinge.

Immobilienangebote und Preise werden im aktuellen Produkt nicht in SQLite gespeichert. Ein älterer Projektansatz besaß eine JsonData-Tabelle. Diese historische Umsetzung darf nicht mit der heutigen Konten- und Tokenpersistenz verwechselt werden.

## 8. Registrierung, Login und Tokens

Bei der Registrierung speichert das Backend das Passwort als BCrypt-Hash. Ein Hash ist keine entschlüsselbare Kopie des Passworts. Beim Login prüft BCrypt das eingegebene Passwort gegen den gespeicherten Hash. Der Service sucht zuerst den Benutzer und erzeugt bei erfolgreicher Prüfung zwei Tokens.

Der AccessToken ist ein JWT, ausgesprochen häufig „Jot“ oder Buchstabe für Buchstabe. Es enthält Benutzerinformationen und eine Ablaufzeit. Das Backend signiert es mit einem lokalen Schlüssel. Bei einer geschützten Anfrage überprüft es Signatur, Aussteller und Gültigkeitsdauer. Die Signatur erkennt Veränderungen; der Inhalt eines JWT ist dadurch nicht automatisch geheim oder verschlüsselt.

Der RefreshToken ist ein zufällig erzeugter Wert mit sieben Tagen Gültigkeit. Er wird in SQLite gespeichert. Bei einer erfolgreichen Erneuerung widerruft das Backend den alten RefreshToken und erzeugt einen neuen. Das heißt Rotation. Die Wiederverwendung des alten Tokens wird abgewiesen.

Angular speichert beide Tokens im Local Storage des Browsers. Ein HTTP-Interceptor hängt den AccessToken als Bearer-Token an die geschützten Anfragen. Der Refreshaufruf ist im Frontend vorhanden, wird bei abgelaufenen AccessTokens aber noch nicht automatisch ausgelöst.

## 9. Was den Zugriff tatsächlich schützt

Der Frontend-Guard prüft, ob ein AccessToken vorhanden ist. Ohne Token führt die Navigation zum Login. Ein vorhandener Token kann aber bereits abgelaufen sein. Der Guard ist deshalb eine Hilfe für den Bedienablauf. Die entscheidende Zugriffsprüfung liegt im Backend am geschützten Immobiliencontroller.

Eine nicht erlaubte Anfrage erhält HTTP 401. Ein nicht gefundenes Detailangebot erhält HTTP 404. Eine erfolgreiche Suche ohne Treffer erhält HTTP 200 mit einer leeren Liste. Diese Fälle haben unterschiedliche Bedeutungen und sollten auch unterschiedlich angezeigt werden.

Beim Abmelden entfernt Angular die beiden Tokens aus dem Local Storage. Die aktuelle Logoutfunktion widerruft keinen Token über einen Backend-Endpunkt. Auch die Registrierung benötigt weitere serverseitige Validierung: In einer isolierten Prüfung wurden ungültige E-Mails und sehr kurze oder leere Passwörter akzeptiert. Eine Prüfung im Browser reicht dafür allein nicht aus.

Die API- und JWT-Schlüssel liegen inzwischen in ignorierter lokaler Konfiguration. Ein Setupskript erzeugt einen zufälligen JWT-Schlüssel, ohne ihn auszugeben. Datenbank und Builddateien werden ebenfalls nicht mehr neu versioniert. Ein neuer lokaler RapidAPI-Schlüssel wurde erfolgreich geprüft. Der Widerruf des zuvor veröffentlichten alten Schlüssels beim Anbieter ist noch unbestätigt; die Gitbereinigung ersetzt diesen Widerruf nicht.

## 10. Was geprüft wurde und was daraus folgt

Die Backendchecks prüfen Verarbeitung, Bezirksfilter, Cache, paralleles Laden, leere Antworten, Details und Fehlererholung. Einige Prüfungen senden echte HTTP-Anfragen an ein lokal gestartetes Testbackend. Die externe API-Antwort und die Authentifizierung werden dabei kontrolliert simuliert. Das macht Ergebnisse wiederholbar und verbraucht keine Live-Anfragen.

Zusätzlich wurde der reguläre Authablauf mit einer getrennten temporären SQLite-Datenbank überprüft. Registrierung, Duplikatprüfung, falsches Passwort, Login und Refreshrotation verhielten sich in den geprüften Fällen wie dokumentiert. Dieser zusätzliche Auditablauf ist noch kein eingecheckter automatischer Auth-Regressionstest.

Der aktuelle Frontendprüfstand umfasst zwölf bestandene Tests. Vier neue Regressionstests prüfen unter anderem Bezirke ohne Treffer, wiederhergestellte Auswahl, das Ignorieren versteckter Kartenebenen und unbestimmte Adressen. Der Backendbuild und der Angular-Entwicklungsbuild bestanden. Der reguläre Produktionsbuild überschritt sein Bundlebudget: Das initiale Paket lag ungefähr bei 1,71 Megabyte bei einem Fehlerbudget von einem Megabyte. Erfolgreiche Entwicklungskompilierung belegt deshalb noch keinen erfolgreichen Produktionsbuild.

Die echte Desktopprobe bestand mit regulärer Anmeldung und RapidAPI HTTP 200: 28 Angebote, Bezirksauswahl, Details, Bildwechsel, Rückkehr und Abmelden. Nach der neuen lokalen API-Konfiguration wurde dieser Ablauf erneut erfolgreich geprüft. Das belegt konkrete Live-Abläufe. Die zusätzliche Kartenprüfung deckt alle Stadtteilflächen und Bezirkslegenden in Chrome und WebKit ab. Das ersetzt keine Prüfung aller Fehlerfälle, verschiedener Mobilgeräte oder sämtlicher Browser.

## 11. Zusammenarbeit, Scrum und eigener Beitrag

TopImmo wurde zu zweit begonnen. Die Lehrkraft hat die spätere Alleinarbeit akzeptiert; die Anforderungen bleiben unverändert. Daraus ergibt sich weder eine dokumentierte Sondernote noch eine automatische Zuschreibung sämtlicher Arbeit an eine Person.

Im Gitverlauf existieren ein älterer API-, SQLite- und Konsolenansatz sowie das heutige ASP.NET-/Angular-Produkt. Grundlagen des heutigen Produkts, einschließlich Authentifizierung und Routen, sind bereits in Juli-Commits unter der Autoridentität Dr. Aly belegt. Weitere historische Änderungen sind unter LotAnderson belegt. Eine Gitautoridentität allein beweist jedoch keine exklusive persönliche Autorschaft jeder Datei.

Das Scrumboard wurde nachträglich mit dem Codebestand abgeglichen. Bereits vorhandene Funktionen und neue Vorbereitungsaufgaben müssen unterscheidbar bleiben. Ein Commitdatum belegt einen gespeicherten Stand, keine Arbeitsdauer. Historische Schätzungen und Istzeiten werden nicht erfunden. Der Boardstatus wird vor dem Vortrag aktuell geöffnet, weil ältere Dokumente Momentaufnahmen enthalten.

Bei der aktuellen Vorbereitung wurden KI-Assistenzen, darunter Codex und Copilot, eingesetzt. Diese Unterstützung ist offen zu benennen. Für einen persönlichen Beitrag eignet sich die Formulierung: „Meine Aufgabe war diese konkrete Änderung. Hier ist der Nachweis. So funktioniert sie. Dieses Ergebnis wurde geprüft. Dabei habe ich diese Entscheidung verstanden.“ Zwei fachlich sicher erklärbare Beiträge sind wertvoller als eine unbelegte Behauptung über das gesamte Projekt.

## 12. Eine kurze mündliche Generalprobe

Die Projektidee lässt sich so erklären: „TopImmo zeigt Mietwohnungen in Stuttgart. Ich wähle einen Stadtbezirk über eine Karte und öffne die passenden Angebote. Angular übernimmt die Oberfläche. Das C#-Backend prüft die Anmeldung, lädt externe Daten und verarbeitet die Bezirke. SQLite speichert Benutzer und RefreshTokens. Die Angebote liegen für fünf Minuten im Arbeitsspeicher.“

Anschließend helfen diese Fragen beim freien Erzählen:

- **Warum das eigene Backend?** Es bündelt Authentifizierung, externe API-Anbindung und fachliche Verarbeitung. Der private API-Schlüssel wird dort verwendet.
- **Warum zwei Tokens?** Der AccessToken erlaubt geschützte Zugriffe. Der RefreshToken ermöglicht eine neue Tokenausgabe und wird bei erfolgreicher Erneuerung rotiert.
- **Warum stehen nicht in jedem Bezirk Treffer?** Die Anfrage lädt nur Seite eins mit höchstens 30 Angeboten. Ein Bezirk kann in diesem Ausschnitt leer sein.
- **Wo ist die Datenbankleistung?** In der dauerhaften Speicherung von Konten und RefreshTokens über EF Core und SQLite.
- **Welche Verbesserung würde ich begründen?** Beispielsweise Servervalidierung, automatischer Refresh, weitere API-Seiten oder ein kleineres Produktionsbundle. Die konkrete Auswahl folgt dem aktuellen Fehler und dem Nutzen für den Anwender.
- **Wie erkläre ich einen eigenen Beitrag ehrlich?** Mit bestätigter Aufgabe, konkretem Code oder Commit, verständlicher Begründung, geprüftem Ergebnis und transparenter Unterstützung.

Die Präsentation muss das Projekt verständlich machen und die eigene Verantwortung nachvollziehbar zeigen. Dafür sind ein geprobter Nutzerablauf, klare Begriffe, belegte Ergebnisse und offen benannte Grenzen die Grundlage.

## Lokale Quellen dieses Lerntexts

Die technischen Aussagen wurden mit dem aktuellen Code und den neueren Prüfungsnachträgen abgeglichen. Quellen: README.md; docs/architektur.md; docs/algorithmen.md; docs/pruefungen.md; docs/demoprobe.md; docs/konfigurationsbereinigung.md; docs/backlog.md; docs/retrospektive.md; docs/praesentation.md; docs/KONTEXT-UEBERGABE.md sowie die Auth-, Token-, Immobilien- und Bezirksservices im Backend und die entsprechenden Services, Guards und Komponenten im Frontend. Ältere Aussagen über eine noch ungeprüfte Live-Demo sind durch die dokumentierten späteren Proben überholt. Der ergänzende Kartenauftrag ist in docs/stadtbezirke-pruefung.md und docs/demo/karten-vollpruefung.json mit vollständiger Abdeckung dokumentiert.
