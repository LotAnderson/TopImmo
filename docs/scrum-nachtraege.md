# Scrum-Nachträge – zum Übertragen vorbereitet

Stand: 04.10.2026. Zielboard: [GitHub Project 2](https://github.com/users/LotAnderson/projects/2).

**Online-Status: noch nicht aktualisiert.** Die erste Issue-Änderung wurde mit HTTP 403 „Resource not accessible by integration“ abgewiesen. Kein Issue und kein Projektfeld wurde verändert. Die folgenden Texte sind fertige Übertragungsentwürfe; die GitHub-Verbindung benötigt ausreichenden Schreibzugriff.

Die vollständigen strukturierten Texte stehen in [scrum-import.json](scrum-import.json). Die Datei ist ein vorbereitetes Datenpaket und kein direktes GitHub-Importformat. Für eine manuelle Übertragung die folgenden Texte verwenden.

## Bestehende Aufgaben mit dem aktuellen Stand abgleichen

Issue schließen und den Projektstatus separat auf Done setzen. Eine Issueschließung allein garantiert ohne Projektworkflow keine Änderung der Projektspalte. Originaltexte und historische Erstellungsdaten erhalten; die Ergänzung darunter anhängen.

| Issue | Vorgesehener Status | Einordnung |
|---|---|---|
| [#8](https://github.com/LotAnderson/TopImmo/issues/8) Back End | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#12](https://github.com/LotAnderson/TopImmo/issues/12) Back End Entity frame work als ORM | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#23](https://github.com/LotAnderson/TopImmo/issues/23) Angular Project Erstellen | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#24](https://github.com/LotAnderson/TopImmo/issues/24) Interfaces erstellen aufgrund JSON responce from Back End | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#25](https://github.com/LotAnderson/TopImmo/issues/25) Service wie Http Client(back end aufzurufen), bridge zwischen dem Map und dem API Responce | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#27](https://github.com/LotAnderson/TopImmo/issues/27) Token und Refresch Token Erstellen | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#28](https://github.com/LotAnderson/TopImmo/issues/28) Pages erstellen und in der routes.ts anmelden, um SPA darzustellen | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#30](https://github.com/LotAnderson/TopImmo/issues/30) Backend-EM: Benutzer anlegen ,  Login und Sessions | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#31](https://github.com/LotAnderson/TopImmo/issues/31) Backend EM: endpoint anlegen | Done / geschlossen | Bestandsabgleich; Grundlage bereits vor dem 17.07. vorhanden |
| [#32](https://github.com/LotAnderson/TopImmo/issues/32) UML-Diagramm ertsllen | Done / geschlossen | Architekturdokumentation jetzt lokal vorhanden |

#19 bleibt offen: vorhandene HTTP-Prüfungen dokumentieren, aber keinen unbelegten vollständigen Postman-/Live-Durchlauf behaupten. #29 bleibt als optionale Erweiterung offen. Historische Done-Aufgaben des älteren Backendansatzes erhalten; #10 als Duplikat von #9 einordnen.

## 1.1 Nachträge 17.07. bis 02.10.2026

Zwei neue Aufgaben erstellen, dem Projekt hinzufügen und nach dem Übertragen auf Done setzen. Dieser Abschnitt enthält echte spätere Commitänderungen; die alten Angular-/Token-/SQLite-Grundlagen werden nicht als neue Arbeit gezählt.

### [H1] Älteres Backend: HTTP-Endpunkte und Datenqualität weiterentwickelt

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: 17.07.–02.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Im älteren Backendansatz wurden HouseUrl-Verarbeitung, weitere Controller-/Loginabläufe und Duplikatbehandlung ergänzt. Diese Umsetzung ist historisch getrennt vom heutigen ASP.NET-/Angular-Ansatz darzustellen.

### Nachweise und Grenzen

d178848 (17.07.), fa3f828 (21.07.), ae9043e (28.09.2026). Commitautor: LotAnderson. Exakte Arbeitstage und Arbeitsdauer folgen daraus nicht.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [H2] Projektstruktur und Frontendintegration geordnet

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: 17.07.–02.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Repositorystruktur, Merge und Verlagerung der bestehenden Backend-/Angular-Dateien bis zum Vergleichsstand vom 02.10.2026. Vorhandene Funktionen wurden dabei überwiegend verschoben, nicht neu implementiert.

### Nachweise und Grenzen

1a5a76a, a2686c5, 7a7d838, a6955bd (28.09.), 65d0d31 und 4af8130 (02.10.). Commitautor: LotAnderson.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

## 1.2 Nachträge nach Stand 02.10. bis aktuell

Die abgeschlossenen Einträge beschreiben vorhandenen lokalen Code oder heute erstellte Unterlagen. Die genaue Entstehungszeit uncommitteter Codeänderungen bleibt unbekannt. Eine erfolgreiche Live-Demo wird nicht behauptet.

### [N1] Backendverarbeitung und Such-/Detailvertrag weiterentwickelt

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Lokaler Stand: Mietfilter, Bezirksnormalisierung, Bezirkszahlen, Sortierung, fünf Minuten Cache; Suchantwort mit listings/districtCounts/mapDistricts; district-Parameter und Detailabruf mit 404.

### Nachweise und Grenzen

Backend/ImmscoutAPI/Service/DistrictDataService.cs; Controllers/RealEstateController.cs; Model/ListingSearchResult.cs. Noch uncommittete Unterschiede zu 4af8130; genauer Zeitpunkt/Einzelautor nicht durch Git belegt.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [N2] Frontend an den aktuellen Backendvertrag angepasst

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Lokaler Stand: Bezirksabfrage beim Backend, Backendzahlen und SVG-Zuordnung für Karte, Detailabruf nach ID sowie Lade-, Leer- und Fehlerzustände.

### Nachweise und Grenzen

Frontend/TomInnoFrondEnd/src/app/services/realestate.ts, component/realestate.component und pages/listing-detail.component. Funktionalität mit Testantworten geprüft; Live-Gesamtablauf ungeprüft.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [N3] Aktuellen Datenfluss mit Tests und Audit geprüft

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Vorhandene Backend-Fixture-/HTTP-Checks und acht Frontendtests bestanden. Backendbuild erfolgreich mit 28 Warnungen; Entwicklungsbuild erfolgreich. Zusätzlicher isolierter Auth-/SQLite-Audit funktioniert. Kein erfolgreicher Produktionsbuild oder Live-API-Nachweis.

### Nachweise und Grenzen

Backend/ImmscoutAPI.Checks/Program.cs; Frontend/TomInnoFrondEnd/src/app/listing-flow.spec.ts; docs/pruefungen.md. Auditprüfung und Vorbereitung erfolgten am 04.10.2026 mit Assistenzunterstützung; daraus keine alleinige Programmierautorschaft ableiten.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [N4] Backlog, Retrospektive, Architektur und Algorithmen vorbereitet

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Beide Rekonstruktionszeiträume dokumentiert; historische Beiträge erhalten; genehmigte Solo-Fortführung und unveränderter Umfang erläutert. Komponenten-/UML-/ER-Darstellung sowie Pseudocode sind lokal vorbereitet.

### Nachweise und Grenzen

docs/backlog.md, docs/retrospektive.md, docs/architektur.md, docs/algorithmen.md, docs/pruefungen.md. Erstellt am 04.10.2026 mit Assistenzunterstützung; noch nicht eingecheckt.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [N5] Präsentation für den 05.10.2026 vorbereitet

Projektstatus: **Done**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Lokale Folien und Sprechnotizen mit Produktablauf, Architektur, Verarbeitung, Testergebnissen, eigener Leistung und offenem Stand. Live-Demo und API-Ausweichablauf beschrieben; Vorbereitung ist noch keine gehaltene Präsentation.

### Nachweise und Grenzen

docs/praesentation.md und docs/praesentation.html. Lokal vorbereitet am 04.10.2026 mit Assistenzunterstützung. Vortrag morgen; Browser-Live-Demo separat offen.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Done. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

### [N6] Live-Demo mit echter API proben und Präsentationsstand sichern

Projektstatus: **Backlog**. Verantwortlich für Nachtrag: LotAnderson.

```markdown
## Nachtrag am 04.10.2026

Zeitraum: nach dem Stand vom 02.10. bis 04.10.2026. Dies ist ein nachträglich erfasster Abschnitt, kein damals protokollierter Sprint.

### Inhalt

Vor der Präsentation frisch anmelden, Übersicht laden, Bezirk auswählen, Details/Bilder ansehen und Rückkehr prüfen. Erfolg oder Ausfall dokumentieren. Bei Erfolg Screenshots/kurze Aufnahme sichern; aktuellen vorgeführten Stand eindeutig benennen.

### Nachweise und Grenzen

Noch offen. Vorhandene Fixture-/Komponententests ersetzen diese Browser-/Live-Probe nicht.

### Aufwand und Verantwortung

- Historische Schätzzeit: nicht überliefert.
- Tatsächlicher Aufwand: nicht protokolliert.
- Verantwortlich für heutigen Nachtrag und Fortführung: LotAnderson. Dies ist keine rückwirkende Zuschreibung sämtlicher ursprünglicher Programmierarbeit.
- Vorgesehener Boardstatus: Backlog. Done bezieht sich auf den beschriebenen lokalen Stand; Veröffentlichen/Einchecken und Live-Probe bleiben getrennte Aufgaben.
```

## Ergänzungen für vorhandene Issues

Diese Abschnitte jeweils an die bestehende Beschreibung anhängen. Die Issue-Erstellung bleibt auf dem ursprünglichen Datum; Erfassungsdatum des Nachtrags ist heute.

### #8 – Back End

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Interfaces, Services und DI bereits in 8a91593 vom 02.07.2026.

Grundaufgabe im aktuellen Backend vorhanden.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #12 – Back End Entity frame work als ORM

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

EF Core, SQLite, DbContext und Migrationen bereits in 8a91593.

Konten-/Tokenpersistenz am 04.10.2026 separat geprüft; Immobilienpersistenz ist keine zusätzliche PDF-Pflicht.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #23 – Angular Project Erstellen

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Angular-Projekt in d59e6b3 vom 02.07.2026; spätere Strukturänderung in 65d0d31.

Grundgerüst vorhanden. Die Strukturänderung ist keine erneute Implementierung von Angular.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #24 – Interfaces erstellen aufgrund JSON responce from Back End

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Listing-/Adress-/Antwortinterfaces in d59e6b3; Authinterfaces in 23ceb19 vom 16.07.2026.

Grundaufgabe vorhanden; der neue ListingSearchResult-Vertrag wird getrennt nachgetragen.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #25 – Service wie Http Client(back end aufzurufen), bridge zwischen dem Map und dem API Responce

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

RealEstateService und DistrictFilterService in d59e6b3; aktueller Backendfilter-/Kartenvertrag lokal weiterentwickelt.

Grundaufgabe vorhanden; Live-Browserablauf bleibt ungeprüft.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #27 – Token und Refresch Token Erstellen

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Tokenmodelle, Generierung und Persistenz in 8a91593; Refreshablauf in 23ceb19.

Backendrotation am 04.10.2026 isoliert geprüft. Automatische Frontenderneuerung bleibt offen.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #28 – Pages erstellen und in der routes.ts anmelden, um SPA darzustellen

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Home, Registrierung, Login, Details und Routen spätestens in 23ceb19.

Grundaufgabe bereits vor dem 17.07.2026 vorhanden.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #30 – Backend-EM: Benutzer anlegen ,  Login und Sessions

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Benutzer-/Loginservices in 8a91593; JWT-/Refreshintegration in 23ceb19; anderer Backendansatz in fa3f828.

Grundablauf geprüft; Backendvalidierung und Sitzungsauslauf bleiben offen.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #31 – Backend EM: endpoint anlegen

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Controller/Listenendpunkt in 8a91593 und 23ceb19; weitere Endpunkte im älteren Ansatz in fa3f828.

Grundaufgabe vorhanden; neue lokale Such-/Detailendpunkte getrennt erfassen.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

### #32 – UML-Diagramm ertsllen

```markdown
## Nachträglicher Bestandsabgleich – 04.10.2026

Am 04.10.2026 wurden docs/architektur.md und docs/algorithmen.md erstellt und gegen den aktuellen Code geprüft.

Komponentenbild, UML-Klassenbild, SQLite-ER-Modell und Pseudocode sind lokal vorhanden. Die Dateien sind noch nicht eingecheckt.

Erfasst am 04.10.2026. Ursprüngliche Erstellungsdaten bleiben erhalten. Historische Schätz-/Istzeit ist nicht überliefert. Die ursprüngliche Autorschaft ist aus den Belegcommits zu erklären; die heutige Statuskorrektur ist keine neue historische Solo-Leistung.

Aktueller lokaler Stand und Belege: docs/backlog.md, docs/architektur.md und docs/pruefungen.md. Der heutige Gesamtstand ist noch nicht vollständig eingecheckt; main enthält einen anderen Stand. Eine erfolgreiche Live-API-Demo wird nicht behauptet.
```

## Abschlusskontrolle nach tatsächlicher Übertragung

- Zehn vorhandene Aufgaben haben den begründeten Status Done: #8, #12, #23–#25, #27, #28, #30, #31 und #32.
- Zwei Nachträge für Zeitraum 1 und sechs für Zeitraum 2 sind im Projekt sichtbar. N6 bleibt offen, bis die echte Probe erfolgt ist.
- Historische Leistungen bleiben sichtbar. Datum der Ergänzung und Zeitraum der dargestellten Arbeit sind unterscheidbar.
- Keine historischen Stundenwerte sind erfunden. Für neue Arbeit die Schätzung vor Beginn und tatsächliche Zeit danach erfassen.
- Dokumentation und Präsentation sind lokal vorbereitet; diese Statusliste behauptet keinen aktualisierten Hauptbranch.

Weiterlesen: [Backlog mit Commitbelegen](backlog.md), [Retrospektive](retrospektive.md), [Präsentation](praesentation.md).
