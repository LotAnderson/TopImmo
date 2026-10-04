# Schlüssel abschließen und TopImmo selbst testen

Stand: 04.10.2026. Der neue lokal gespeicherte RapidAPI-Schlüssel funktioniert: echte API-Anfrage mit HTTP 200 und 28 Mietangeboten erfolgreich. Ein weiterer neuer Schlüssel ist für den aktuellen Start nicht erforderlich. Der Widerruf des früher öffentlich auf GitHub gespeicherten Schlüssels ist noch nicht bestätigt. Die Assistenz hat keinen Zugriff auf dein RapidAPI-Konto und hat ihn deshalb nicht gelöscht.

## Alten RapidAPI-Schlüssel widerrufen

1. Melde dich auf RapidAPI mit deinem bisherigen Konto an und öffne **Console / Developer Dashboard**.
2. Öffne **Apps**, wähle die verwendete Anwendung aus (auf deinem Screenshot `default-application_11969380`) und gehe zu **Authorization**.
3. Identifiziere dort den alten öffentlich veröffentlichten Schlüssel. Behalte den neuen, der gerade in `Backend/ImmscoutAPI/appsettings.Local.json` bei `RapidApi.ApiKey` steht. Vergleiche die Werte nur privat auf deinem Rechner; sende sie nicht in den Chat.
4. Lösche ausschließlich die alte Authorization / den alten Schlüssel. Die aktuell funktionierende Authorization muss erhalten bleiben. RapidAPI dokumentiert das Löschen der alten Authorization nach dem erfolgreichen Schlüsselwechsel auf der [offiziellen Seite zur Schlüsselrotation](https://docs.rapidapi.com/docs/keys-and-key-rotation).
5. Der aktuelle Projektstand braucht danach keinen erneuten Schlüsselwechsel, sofern der neue Schlüssel erhalten blieb. Die alte Zeichenfolge steht weiterhin in früheren Git-Commits; durch den Widerruf wird sie beim Anbieter unbrauchbar. Die lokale Datei wird von Git ignoriert.

Falls du zukünftig erneut wechselst: in Authorization **Add authorization** wählen, neuen Schlüssel privat in die lokale Datei eintragen, speichern, Backend beenden und neu starten. Die lokale Konfiguration wird beim Start gelesen. Der JWT-Schlüssel muss für diesen RapidAPI-Wechsel nicht geändert werden.

## Jetzt im Browser testen

Die lokale Anwendung lief beim Abschluss auf **http://localhost:4200/login**, das Backend auf **http://localhost:5197**. Öffne die Loginseite. Lade Safari mit **Cmd + R** neu, damit die Kartenkorrektur sichtbar ist.

1. Melde dich mit deinem vorhandenen Projektkonto an. Falls du kein Konto hast, wechsle zur Registrierung, nutze eine gültige E-Mail und ein ausreichend langes Passwort und melde dich danach an.
2. Warte auf Karte, Legende und Angebotsliste. Im geprüften Abruf waren es 28 Angebote; die Livezahl kann sich ändern.
3. Bewege die Maus über verschiedene Stadtteile, auch über die sichtbaren Bezirksbeschriftungen. Innerhalb der Stadtteilfläche muss der Handzeiger erscheinen; der Elternbezirk wird hervorgehoben.
4. Klicke **Obertürkheim** und **Uhlbach**. Beide gehören zum Stadtbezirk Obertürkheim. Beim geprüften Abruf hatte dieser Bezirk 0 Treffer. „Keine Immobilien gefunden“ ist in diesem Fall das richtige Ergebnis; die Karte und Legende müssen trotzdem auswählbar und markiert bleiben.
5. Wähle in der Legende einen Bezirk mit Treffern, zum Beispiel Ost, sofern dort gerade eine Zahl größer als 0 steht. Öffne ein Angebot, wechsle bei mehreren Bildern auf ein zweites Bild und gehe über den Zurücklink zur Übersicht.
6. Prüfe, dass der Bezirk in der Legende und auf der Karte weiterhin markiert ist. Melde dich ab. Der erneute Aufruf der Übersicht muss zum Login führen.

Die Suche lädt nur die erste Seite mit höchstens 30 Angeboten. Ein Bezirk ohne geladene Treffer bedeutet deshalb nicht, dass auf dem gesamten Markt keine Wohnung angeboten wird.

## Falls die Server beendet wurden

Öffne ein Terminal für das Backend:

```bash
cd /Users/edhar_myronchuk/Documents/GitHub/TopImmo/Backend/ImmscoutAPI
dotnet run --launch-profile http
```

Öffne ein zweites Terminal für das Frontend:

```bash
cd /Users/edhar_myronchuk/Documents/GitHub/TopImmo/Frontend/TomInnoFrondEnd
npm start
```

Lass beide Terminals offen und öffne danach die Loginseite. Wenn ein Port bereits belegt ist, läuft dort wahrscheinlich schon die Anwendung; starte keine zweite Instanz auf demselben Port. Installation und Einrichtung auf einem frischen Rechner stehen in der [README](../README.md).

## GitHub und Lerntext

Der geprüfte Stand ist lokal gespeichert. Das Hochladen per Git scheiterte an fehlender GitHub-Anmeldung; die verbundene Integration hatte bei Schreibversuchen HTTP 403. Öffne **GitHub Desktop**, wähle TopImmo und den Branch **LotAnderson-patch-1**, und klicke nach dem lokalen Abschluss **Push origin**. Ein erfolgreicher Push veröffentlicht den Branch; `main` braucht anschließend weiterhin die Integration per Pull Request.

Der fertige [NotebookLM-Lerntext](notebooklm-projekttext.md) enthält die aktuelle Architektur, Algorithmen, Prüfungen und Grenzen sowie Anleitung und Anpassungsprompt für die deutsche Audio-Zusammenfassung.
