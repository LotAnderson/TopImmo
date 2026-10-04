import { existsSync, readFileSync, writeFileSync } from 'node:fs';
import { randomBytes } from 'node:crypto';
import { fileURLToPath } from 'node:url';

const directory = new URL('../Backend/ImmscoutAPI/', import.meta.url);
const localFile = new URL('appsettings.Local.json', directory);
const sourceFile = existsSync(localFile)
  ? localFile
  : new URL('appsettings.Local.example.json', directory);
const config = JSON.parse(readFileSync(sourceFile, 'utf8'));
config.Jwt ??= {};
config.RapidApi ??= {};
config.RapidApi.ApiKey ??= '';

if (!config.Jwt.Key) {
  config.Jwt.Key = randomBytes(64).toString('base64');
}

writeFileSync(localFile, `${JSON.stringify(config, null, 2)}\n`, { mode: 0o600 });
console.log(`Local configuration ready: ${fileURLToPath(localFile)}`);
console.log('Existing values are preserved; JWT key values are never printed.');
if (!config.RapidApi.ApiKey) {
  console.log('For live listings, enter your RapidAPI key in RapidApi.ApiKey in this local file.');
}
