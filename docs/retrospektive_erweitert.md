# Retrospektive – TopImmo

## Rahmen und gemeinsames Ergebnis

Der aktuelle lokale Stand umfasst ein Angular-Frontend und ein C#-/ASP.NET-Core-Backend. Nutzer können sich registrieren und anmelden, Mietwohnungen in Stuttgart nach Bezirk auswählen und Angebotsdetails ansehen. Das Backend verarbeitet externe ImmoScout24-Daten, ordnet Bezirke zu und zählt die geladenen Angebote. Benutzer und RefreshTokens werden mit EF Core in SQLite gespeichert.

Die technischen Grundvorgaben API, SQLite und C# sind damit implementiert. Live-API und vollständiger Browserablauf sind durch die bisherigen automatisierten Prüfungen noch nicht belegt.

## Arbeitsverlauf und Herkunft

| Abschnitt | Nachweisbarer Stand | Zuordnung und Grenze |
|---|---|---|
| Vor dem 17.07.2026 | Angular, EF Core/SQLite sowie Benutzer-/Tokenmodelle in den Juli-Commits; Login, Refresh, Guards und Seiten spätestens in `23ceb19` vom 16.07. | Git-Commitautor `Dr. Aly`; vorhandene Vorarbeit erhalten. Daraus keine neue Solo-Leistung nach dem 17.07. ableiten. |
| 17.07.–02.10.2026 | Älteres Backend: weitere Endpunkte/Login am 21.07. (`fa3f828`), HTTP-Endpunkte und Duplikatbehandlung am 28.09. (`ae9043e`). Struktur-/Integrationsänderungen im aktuellen Branch am 02.10. (`65d0d31`, `4af8130`). | Git-Commitautor `LotAnderson`, zugleich aktuell angemeldeter GitHub-Account. Die verschiedenen Backendansätze sind getrennt zu erklären. |
| Nach dem Stand vom 02.10. bis 04.10.2026 | Lokale Backendverlagerung, Suchantwort mit Listings/Bezirkszahlen/Kartenzuordnung, Detailendpunkt sowie zusätzliche Backend- und Frontendchecks. | Unterschiede zum Commitstand sind nachweisbar; nicht eingecheckte Änderungen belegen keinen genauen Arbeitstag oder Einzelautor. |
| Vorbereitung am 04.10.2026 | Bestandsanalyse, Boardnachträge, Architektur-/Algorithmusdarstellung, diese Retrospektive, Prüfungsnachweis und Präsentationsunterlagen. | Nachträgliche Vorbereitung des aktuellen Stands. Historische fehlende Schätz-/Istzeiten werden dadurch nicht ersetzt. |

## Technische Entscheidungen und Lernpunkte

- **Aufgaben zwischen Frontend und Backend trennen:** Die Bezirksverarbeitung und Zählung liegen im Backend; Angular verarbeitet Auswahl und Darstellung. Der Datenvertrag bündelt Listings, Bezirkszahlen und Kartenzuordnung. Dadurch lässt sich fachliche Verarbeitung zentral prüfen.
- **SQLite für Authdaten nutzen:** Benutzer und RefreshTokens sind persistent, Immobilienangebote kommen aus der externen API und liegen im RAM-Cache. Diese Trennung erfüllt die ausdrücklich verlangte DB-Nutzung; eine Immobilienpersistenz ist keine zusätzliche Pflicht aus den Anhängen.
- **API-Daten fünf Minuten cachen:** Wiederholte Abrufe werden vermieden. Gleichzeitige Cache-Ladevorgänge werden durch eine Semaphore koordiniert. Ein Frontendcache ohne zeitliche Erneuerung bleibt eine offene Konsistenzfrage.
- **Tests mit kontrollierten API-Antworten nutzen:** Fehler, leere Daten und Bezirksfilter lassen sich reproduzierbar prüfen. Diese Tests ersetzen keine Probe mit echter API und Browser.

Diese Entscheidungen beschreiben das aktuelle System. Ihre ursprüngliche Urheberschaft wird hier nur dort zugeordnet, wo ein konkreter Git-Nachweis vorliegt.

## Was gelungen ist

Der Funktionsumfang bildet einen zusammenhängenden Nutzerablauf ab. Backend-Build und vorhandene Fixture-/HTTP-Checks bestehen. Acht Frontendtests bestehen; der Entwicklungsbuild ist erfolgreich. Ein zusätzlicher isolierter HTTP-Test bestätigt Registrierung, Login und RefreshToken-Rotation mit einer eigenen SQLite-Datenbank.

Die Implementierung lässt sich an [Architektur](architektur.md), [Algorithmen](algorithmen.md) und [Prüfungsnachweisen](pruefungen.md) nachvollziehen.

## Was unzureichend dokumentiert oder offen ist

Das Scrumboard wurde seit der letzten gemeinsamen Präsentation kaum gepflegt. Im geprüften öffentlichen Stand waren alle sichtbaren Änderungen vom 17.07.2026; Zuständigkeiten, Schätzzeiten und Istzeiten fehlten. Bereits vorhandene Basisfunktionen standen noch offen. Historische Aufgaben betreffen teilweise einen älteren Backendansatz.

Außerdem fehlen eine erfolgreiche vollständige Live-Demo und ein aktueller Gesamtstand im lokal sichtbaren Hauptbranch. Der Produktionsbuild überschreitet sein Bundlebudget. Das Backend akzeptiert ungültige Registrierungen; das Frontend behandelt abgelaufene Tokens nicht sauber. Bezirkszahlen betreffen nur die erste externe Seite mit maximal 30 Angeboten. Diese Einschränkungen werden bei der Präsentation benannt.

## Verbesserungen und unmittelbare nächste Schritte

1. Boardstatus mit dem tatsächlichen Code abgleichen und Nachträge datieren. Historische Leistungen erhalten; aktuelle Aufgaben nach den zwei Zeiträumen aufführen.
2. Für kommende Arbeit Aufgabe, verantwortliche Person, Schätzung und tatsächlichen Aufwand zeitnah festhalten. Vergangene Zeiten bleiben unbekannt, soweit keine Aufzeichnung existiert.
3. Aktuellen Präsentationsstand sichern und den echten Browserablauf proben. Eine größere Hauptbranch-Zusammenführung darf den funktionierenden Präsentationsstand nicht gefährden.
4. Architektur, wichtige Verarbeitung und zwei bis drei tatsächlich eigene Aufgaben verständlich erklären. Vorarbeit anderer Beteiligter dabei sichtbar zuordnen.
5. Nach der Präsentationsvorbereitung die bekannten Validierungs-, Sitzungs-, Build- und Bedienungsprobleme gezielt beheben und die betroffenen Abläufe prüfen.

Der [Backlog](backlog.md) enthält den konkreten Abgleich mit den Boardaufgaben. Die Retrospektive ersetzt keine historischen Scrumprotokolle und keine Bewertung durch die Lehrkraft.
