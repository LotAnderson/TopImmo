# Vollständige Prüfung der Stuttgarter Bezirke und Stadtteile

Prüftag: 4. Oktober 2026. Die amtliche Referenz wurde unabhängig vom vorhandenen Kartenmapping geladen und mit jedem Eintrag verglichen.

## Amtliche Referenz

Stuttgart gliedert sich in **23 Stadtbezirke und 152 Stadtteile**. Die aktuelle [Übersicht des Statistischen Amts](https://www.stuttgart.de/service/statistik-und-wahlen/daten-zu-stadtbezirken) hat den Stand 2. Juni 2026. Als zweite vollständige Referenz dient das [amtliche Straßenverzeichnis, Stand Juni 2023, gedruckte Seiten 7–8](https://www.domino1.stuttgart.de/web/komunis/komunissde.nsf/49ec24bbdf344054c1257ca900367f10/4178526f69a313fac1258a320022df78/%24FILE/c9z01_.PDF#page=7). Sämtliche 152 Nummern, Namen und Zuordnungen wurden auch gegen dieses Verzeichnis verglichen.

Die Webseite enthält zwei Zahlentippfehler: Killesberg steht dort zusätzlich unter **123**, Gehrenwald unter **641**. Das Straßenverzeichnis bestätigt **124 Killesberg** und **661 Gehrenwald**; diese eindeutigen Nummern sind bereits in der Projektkarte vorhanden. Die Backendreferenz verwendet die bestätigten Nummern.

Die Anwendung verwendet für die fünf inneren Stadtbezirke die Kurzformen **Mitte, Nord, Ost, Süd, West**. Auf der Stadtwebseite heißen sie Stuttgart-Mitte, Stuttgart-Nord, Stuttgart-Ost, Stuttgart-Süd und Stuttgart-West. Die Kurzformen entsprechen dem amtlichen Straßenverzeichnis und dem bestehenden Frontendvertrag.

## Vollvergleich des vorhandenen Projektstands

| Prüfung | Ergebnis |
| --- | --- |
| Amtliche Stadtbezirke | 23 von 23 vorhanden |
| Amtliche Stadtteile | 152 von 152 vorhanden |
| Stadtteilnummern | 152 eindeutig, keine fehlende oder zusätzliche Nummer |
| Sichtbare Kartenflächen in Layer_15 | 152, jeweils einem amtlichen Stadtteil zugeordnet |
| Karten-IDs im SVG | 458 mit Stadtteilpräfix, alle im Backendmapping vorhanden |
| Backendmapping-IDs | 458, keine ohne SVG-Gegenstück |
| Vergleich jedes Map-ID-Namens | 458 von 458 stimmen nach Dekodierung von SVG-Suffixen und Illustrator-Escapes |
| Vergleich jeder Elternbezirkszuordnung | 458 von 458 korrekt |

Die zusätzlichen Map-IDs sind weitere Darstellungs-/Konturlayer derselben Stadtteile. Beispielsweise werden `a345_x5F_Lemberg_x2F_Föhrich` und seine Suffixvarianten als **Lemberg/Föhrich** dekodiert. Die vorhandene `map-districts.json` benötigt deshalb keine Änderung.

Der Pointerfehler entstand durch überlagernde Dekolayer, die Mausereignisse abfingen. Das Frontend setzt für alle dekorativen Ebenen einschließlich ihrer Kinder `pointer-events: none`. Die 152 sichtbaren Flächen in `Layer_15` bleiben interaktiv und zeigen den Handzeiger. Ereignisdelegation akzeptiert ausschließlich sichtbare Flächen. Die Markierung wird bei Filteränderung und nach der Mappingantwort wiederhergestellt. Die Legende enthält alle 23 Bezirke, auch mit 0 Angeboten, sowie gegebenenfalls „Ohne Bezirksangabe“; ihre Buttons sind per Tastatur bedienbar.

## Vollständige echte Browserprüfung

[Maschinenlesbarer Bericht](demo/karten-vollpruefung.json), 04.10.2026, reguläres Demokonto, laufendes Angular-/ASP.NET-Produkt, echte API-Angebote; keine HTTP-Mocks.

| Prüfung | Google Chrome | WebKit (Safari-Engine) |
| --- | ---: | ---: |
| Echte Mausbewegung: Innenpunkt, Handzeiger und Hover des Elternbezirks | 152/152 | 152/152 |
| Echte Stadtteilklicks: korrekte Bezirksanfrage, HTTP 200, passende Angebote, vollständige Markierung | 152/152 | 152/152 |
| Echte Legendenklicks einschließlich Bezirken mit 0 Treffern | 23/23 | 23/23 |
| Dekorative Ebenen einschließlich Beschriftungen, Flüssen und Grenzen lassen Ereignisse durch | Bestanden | Bestanden |
| Obertürkheim und Uhlbach | Beide bestanden | Beide bestanden |

Innenpunkte wurden mit `isPointInFill` und der SVG-Transformation ermittelt. `elementFromPoint` bestätigte anschließend die tatsächlich getroffene Stadtteilfläche; es wurden reale Mausereignisse gesendet. Der erste externe Abruf des neugestarteten Backends erhielt RapidAPI HTTP 200. Die 28 geladenen Mietangebote wurden für die wiederholten lokalen Bezirksanfragen aus dem fünfminütigen RAMcache verwendet. 13 der 23 Bezirke hatten in diesem konkreten Ausschnitt keine Treffer und blieben voll auswählbar.

Die [anschließende echte Detailprobe](demo/demoprobe-kartenkorrektur.json) bestätigte Ost (1 Angebot), ein Detail mit 11 Bildern, geladenen Bildwechsel, Rückkehr mit allen 8 Ost-Flächen weiterhin markiert und Logout/Guard. Das ist eine zusätzliche Chrome-Liveprobe. Der WebKit-Lauf prüfte die Kartenbedienung, nicht die komplette Detailnavigation und nicht die native Safari-App.

## Korrektur der Adresszuordnung im Backend

Vorher konnte eine Adresse mit Stadtteil, etwa **Uhlbach**, als eigener Bezirk ausgegeben werden. Adressen ohne auswertbare Bezirksangabe erhielten den Sammelwert **Stuttgart**. Stuttgart ist die Stadt und kein Stadtbezirk.

Die neue Referenz `Backend/ImmscoutAPI/Data/stuttgart-subdistricts.json` enthält für jeden Stadtteil `number`, `name` und `district`. Der Suchdienst erkennt die vollständigen amtlichen Stadtteil- oder Bezirksnamen in den Ortskomponenten einer Adresse. **Uhlbach → Obertürkheim**, **Gehrenwald → Untertürkheim**, **Lemberg/Föhrich → Feuerbach**. Groß-/Kleinschreibung, Unicode-Zusammensetzung, Stuttgart-Präfix, übliche Gedankenstriche und ein abschließendes „(Stuttgart)“ werden normalisiert.

Die erste Komponente einer Adresse mit mehreren Kommas ist die Straßenkomponente und wird nicht als Ortsangabe interpretiert. Es gibt keine Zuordnung anhand einer Straße, eines Titels oder einer PLZ. Fehlende, allgemeine und widersprüchliche Ortsangaben bleiben **Unknown**. Beispiel: „Beispielstraße 1, 70329 Stuttgart“ bleibt unbekannt; „Beispielstraße 1, 70173 Stuttgart, Stuttgart-Mitte, Uhlbach“ nennt zwei verschiedene Bezirke und bleibt ebenfalls unbekannt.

Der bestehende Antwortvertrag **listings, districtCounts, mapDistricts** und der **fünfminütige Cache** bleiben erhalten. Stadtteiladressen werden in den vorhandenen Bezirksfilter und die stadtweiten Zählungen einbezogen.

## Automatische Prüfung

Ausgeführt:

```bash
dotnet run --project Backend/ImmscoutAPI.Checks/ImmscoutAPI.Checks.csproj
```

Ergebnis: **erfolgreich, Exitcode 0**.

- Alle **152 Stadtteilnamen** laufen als künstliche Upstream-Adressen durch den tatsächlichen Suchdienst.
- Alle **23 Stadtbezirke** laufen mit Stuttgart-Präfix durch denselben Dienst.
- **14 zusätzliche Fälle** prüfen unter anderem Uhlbach/Untertürkheim, Groß-/Kleinschreibung, Unicode, PLZ plus expliziten Bezirk, fehlende Angabe, den Sammelwert Stuttgart, unbekannten Ortsnamen, Straßenverwechslung und widersprüchliche Bezirke.
- Somit wurden **189 Adressfälle** geprüft.
- Alle **458 Map-IDs** werden gegen die kanonischen Stadtteilnamen und Elternbezirke geprüft.
- Alle **23 Bezirksfilter** prüfen die Zahl zugeordneter Stadtteile sowie unveränderte stadtweite Zählungen.
- Die vorhandenen Prüfungen für Mietwohnungsfilter, gemeinsam genutzten Cache, Wiederholung nach Upstreamfehler, Authentifizierung, HTTP-Querybindung, Antwortformat, Detailansicht und 404 laufen weiterhin erfolgreich.

Der Build zeigt bereits vorhandene Nullable-Warnungen in den API-Datenmodellen. Die neuen Änderungen führten zu keinen Compilerfehlern.

## Prüfgrenzen

Der amtliche Abgleich betrifft alle Namen, Nummern und Elternbezirke; die automatischen Backendprüfungen verwenden deterministische künstliche API-Antworten. **Diese Prüfung hat keine Live-RapidAPI-Anfrage ausgelöst.** Sie beweist nicht, dass die externe API aktuell in jedem Bezirk Angebote liefert oder dass jede externe Schreibvariante bekannt ist. Geometrische Grenzen der SVG-Karte wurden nicht anhand amtlicher GIS-Polygone vermessen. Die vollständige Browserprüfung ist oben separat dokumentiert.

## Vollständige Referenzliste

Die folgenden 152 Zuordnungen wurden vollständig geprüft; die Ziffer vor jedem Stadtteil ist seine amtliche Nummer. Fachliche Grundlage sind die beiden oben verlinkten Quellen des Statistischen Amts.

| Stadtbezirk (Projektname) | Stadtteile | Vollständige Liste |
| --- | ---: | --- |
| Mitte | 10 | 101 Oberer Schlossgarten; 102 Rathaus; 103 Neue Vorstadt; 104 Universität; 105 Europaviertel; 106 Hauptbahnhof; 107 Kernerviertel; 108 Diemershalde; 109 Dobel; 110 Heusteigviertel |
| Nord | 11 | 121 Relenberg; 122 Lenzhalde; 123 Am Bismarckturm; 124 Killesberg; 125 Weißenhof; 126 Nordbahnhof; 127 Am Pragfriedhof; 128 Am Rosensteinpark; 129 Auf der Prag; 130 Mönchhalde; 131 Heilbronner Straße |
| Ost | 8 | 141 Gänsheide; 142 Uhlandshöhe; 143 Stöckach; 144 Berg; 145 Ostheim; 146 Gaisburg; 147 Gablenberg; 151 Frauenkopf |
| Süd | 7 | 161 Bopser; 162 Lehen; 163 Weinsteige; 164 Karlshöhe; 165 Heslach; 166 Südheim; 171 Kaltental |
| West | 9 | 181 Kräherwald; 182 Hölderlinplatz; 183 Rosenberg; 184 Feuersee; 185 Rotebühl; 186 Vogelsang; 187 Hasenberg; 191 Wildpark; 192 Solitude |
| Bad Cannstatt | 18 | 201 Muckensturm; 202 Schmidener Vorstadt; 203 Espan; 204 Kurpark; 205 Cannstatt-Mitte; 206 Seelberg; 207 Winterhalde; 208 Wasen; 209 Veielbrunnen; 210 Im Geiger; 211 Neckarvorstadt; 212 Pragstraße; 213 Altenburg; 214 Hallschlag; 215 Birkenäcker; 221 Burgholzhof; 231 Sommerrain; 241 Steinhaldenfeld |
| Birkach | 3 | 261 Birkach-Nord; 262 Birkach-Süd; 271 Schönberg |
| Botnang | 4 | 292 Botnang-Nord; 293 Botnang-Ost; 294 Botnang-Süd; 295 Botnang-West |
| Degerloch | 5 | 311 Degerloch; 312 Waldau; 313 Tränke; 314 Haigst; 321 Hoffeld |
| Feuerbach | 8 | 341 Feuerbach-Ost; 342 Siegelberg; 343 Bahnhof Feuerbach; 344 Feuerbach-Mitte; 345 Lemberg/Föhrich; 346 Hohe Warte; 347 Feuerbacher Tal; 348 An der Burg |
| Hedelfingen | 4 | 361 Hedelfingen; 362 Hafen; 371 Lederberg; 381 Rohracker |
| Möhringen | 9 | 401 Möhringen-Nord; 402 Möhringen-Mitte; 403 Wallgraben-Ost; 404 Möhringen-Süd; 405 Möhringen-Ost; 406 Sternhäule; 407 Fasanenhof-Ost; 411 Fasanenhof; 421 Sonnenberg |
| Mühlhausen | 5 | 441 Mühlhausen; 451 Freiberg; 461 Mönchfeld; 471 Hofen; 481 Neugereut |
| Münster | 1 | 501 Münster |
| Obertürkheim | 2 | 521 Obertürkheim; 531 Uhlbach |
| Plieningen | 5 | 551 Plieningen; 552 Chausseefeld; 561 Steckfeld; 571 Asemwald; 581 Hohenheim |
| Sillenbuch | 3 | 601 Sillenbuch; 611 Heumaden; 621 Riedenberg |
| Stammheim | 2 | 641 Stammheim-Süd; 642 Stammheim-Mitte |
| Untertürkheim | 8 | 661 Gehrenwald; 662 Flohberg; 663 Untertürkheim; 664 Benzviertel; 665 Lindenschulviertel; 666 Bruckwiesen; 671 Luginsland; 681 Rotenberg |
| Vaihingen | 12 | 711 Vaihingen-Mitte; 712 Österfeld; 713 Höhenrand; 714 Wallgraben-West; 715 Rosental; 716 Heerstraße; 717 Lauchäcker; 718 Dachswald; 719 Pfaffenwald; 721 Büsnau; 731 Rohr; 741 Dürrlewang |
| Wangen | 1 | 761 Wangen |
| Weilimdorf | 6 | 801 Weilimdorf; 802 Weilimdorf-Nord; 811 Bergheim; 821 Giebel; 831 Hausen; 841 Wolfbusch |
| Zuffenhausen | 11 | 861 Zuffenhausen-Am Stadtpark; 862 Zuffenhausen-Schützenbühl; 863 Zuffenhausen-Elbelen; 864 Zuffenhausen-Frauensteg; 865 Zuffenhausen-Mitte; 866 Zuffenhausen-Hohenstein; 867 Zuffenhausen-Mönchsberg; 868 Zuffenhausen-Im Raiser; 871 Neuwirtshaus; 881 Rot; 891 Zazenhausen |
| **Gesamt** | **152** | **23 Stadtbezirke** |
