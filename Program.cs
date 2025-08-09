
using System.Security.Cryptography;
using System.Text.Json;
using Org.BouncyCastle.Crypto.Generators;


Console.WriteLine("Please enter a path to your backup file: "); // "/home/mu/Documents/Android Backups/Apps/Aegis twofa/aegis-backup-20250723-122400.json"
var backupPath = Console.ReadLine();


if (!File.Exists(backupPath))
    throw new Exception("Backup does not exist");

var backupJson = await File.ReadAllTextAsync(backupPath) ?? throw new Exception("Backup does not exist");

var backup = JsonSerializer.Deserialize<Backup>(backupJson) ?? throw new Exception("Cannot deserialize backupJson"); ;

Console.WriteLine("Please enter your password: ");
var password = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine() ?? throw new Exception("No password provided"));

var slot = backup.Header.Slots[0];

var key = SCrypt.Generate(password, Convert.FromHexString(slot.Salt), slot.N, slot.R, slot.P, 32);

var keyTag = Convert.FromHexString(slot.KeyParams.Tag);
var cipher = new AesGcm(key, keyTag.Length);
var slotKey = Convert.FromHexString(slot.Key);
var master_key = new byte[slotKey.Length];

cipher.Decrypt(Convert.FromHexString(slot.KeyParams.Nonce), slotKey, keyTag, master_key);

var content = Convert.FromBase64String(backup.Db);
var headerTag = Convert.FromHexString(backup.Header.Params.Tag);
cipher = new AesGcm(master_key, headerTag.Length);
var db = new byte[content.Length];
cipher.Decrypt(Convert.FromHexString(backup.Header.Params.Nonce), content, headerTag, db);

string dbJson = System.Text.Encoding.UTF8.GetString(db);
Console.WriteLine("Decrypted DB: " + dbJson);



