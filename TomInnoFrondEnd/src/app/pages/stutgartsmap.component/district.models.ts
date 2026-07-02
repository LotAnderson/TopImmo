const BEZIRK_RANGES: {min: number; max: number; bezirk: string}[] = [
  {min: 101, max: 110, bezirk: 'Mitte'},
  {min: 121, max: 131, bezirk: 'Nord'},
  {min: 141, max: 151, bezirk: 'Ost'},
  {min: 161, max: 171, bezirk: 'Süd'},
  {min: 181, max: 192, bezirk: 'West'},
  {min: 201, max: 241, bezirk: 'Bad Cannstatt'},
  {min: 261, max: 271, bezirk: 'Birkach'},
  {min: 292, max: 295, bezirk: 'Botnang'},
  {min: 311, max: 321, bezirk: 'Degerloch'},
  {min: 341, max: 348, bezirk: 'Feuerbach'},
  {min: 361, max: 381, bezirk: 'Hedelfingen'},
  {min: 401, max: 421, bezirk: 'Möhringen'},
  {min: 441, max: 481, bezirk: 'Mühlhausen'},
  {min: 501, max: 501, bezirk: 'Münster'},
  {min: 521, max: 531, bezirk: 'Obertürkheim'},
  {min: 551, max: 581, bezirk: 'Plieningen'},
  {min: 601, max: 621, bezirk: 'Sillenbuch'},
  {min: 641, max: 642, bezirk: 'Stammheim'},
  {min: 661, max: 681, bezirk: 'Untertürkheim'},
  {min: 711, max: 741, bezirk: 'Vaihingen'},
  {min: 761, max: 761, bezirk: 'Wangen'},
  {min: 801, max: 841, bezirk: 'Weilimdorf'},
  {min: 861, max: 891, bezirk: 'Zuffenhausen'}
];

export function getBezirkFromStadtteilId(elementId: string): string | null {
  // 1. Как вытащить число "205" из строки "a205_Cannstatt-Mitte"?
  //    Подсказка: regex /a(\d+)_/ или parseInt после slice

  const match = elementId?.match(/a(\d+)_/);
  const elementNumber = match ? parseInt(match[1], 10) : null;

  // 2. Как найти подходящий диапазон в BEZIRK_RANGES?
  //    Подсказка: Array.find()

 const bezirkRange = BEZIRK_RANGES.find(range => (elementNumber !== null && elementNumber >= range.min && elementNumber <= range.max));

  // 3. Вернуть название района или null, если не найдено
  return bezirkRange ? bezirkRange.bezirk : null;


}

