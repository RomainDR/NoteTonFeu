using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Script.Util
{
    namespace Json
    {
        public class JsonUtil
        {
            public static void SaveAccountJson(Account.Account account)
            {
                string accountData = JsonConvert.SerializeObject(account, Formatting.Indented);
                string filePath = Application.persistentDataPath + "/account.json";
                Debug.Log("Saved in: " + filePath);
                System.IO.File.WriteAllText(filePath, accountData);
                Debug.Log(account.password);
            }

            public static Account.Account LoadAccountJson()
            {
                var filePath = Application.persistentDataPath + "/account.json";
                var json = System.IO.File.ReadAllText(filePath);
                Account.Account accountData = JsonConvert.DeserializeObject<Account.Account>(json);
                return accountData;
            }
        }
    }

    namespace Password
    {
        public class PasswordHasher
        {
            /// <summary>
            /// Hash a password with PBKDF2 and a random salt
            /// </summary>
            public static string HashPassword(string password)
            {
                var salt = GenerateSalt(); // Generate salt
                var hash = HashPasswordWithPbkdf2(password, salt); // Hash password with PBKDF2
                return Convert.ToBase64String(hash) + ":" + Convert.ToBase64String(salt); // Combine hash and salt
            }

            /// <summary>
            /// Verify a password against its stored hash and salt
            /// </summary>
            public static bool VerifyPassword(string password, string storedHash)
            {
                // Split hash and salt from storedHash (expected format: "hash:salt")
                var parts = storedHash.Split(':');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Stored hash is not in the correct format.");
                    return false;
                }

                try
                {
                    var hash = Convert.FromBase64String(parts[0]);
                    var salt = Convert.FromBase64String(parts[1]);

                    // Recompute the hash with the provided password and the extracted salt
                    var computedHash = HashPasswordWithPbkdf2(password, salt);

                    // Securely compare the computed hash with the stored hash
                    return CryptographicEquals(computedHash, hash);
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Error decoding Base64: {e.Message}");
                    return false;
                }
            }

            /// <summary>
            /// Generate a random cryptographic salt
            /// </summary>
            private static byte[] GenerateSalt()
            {
                byte[] salt = new byte[16]; // 128-bit salt
                using var rng = new RNGCryptoServiceProvider();
                rng.GetBytes(salt);
                return salt;
            }

            /// <summary>
            /// Hash a password using PBKDF2 with the given salt
            /// </summary>
            private static byte[] HashPasswordWithPbkdf2(string password, byte[] salt)
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                return pbkdf2.GetBytes(32); // 256-bit hash
            }

            /// <summary>
            /// Perform a constant-time comparison of two byte arrays
            /// </summary>
            private static bool CryptographicEquals(byte[] a, byte[] b)
            {
                if (a.Length != b.Length) return false; // Length mismatch

                int diff = 0;
                for (int i = 0; i < a.Length; i++) // Compare each byte
                {
                    diff |= a[i] ^ b[i];
                }

                return diff == 0; // Return true if all bytes match
            }
        }
    }

    namespace RegexChecker
    {
        public abstract class RegexVerifier
        {
            private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            public static bool IsValidEmail(string email)
            {
                return Regex.IsMatch(email, EmailPattern);
            }
        }
    }
}