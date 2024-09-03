using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Actions;

namespace PasswordFunctions
{
    internal class PassFunctions
    {
        public static void GeneratePassword()
        {
            Console.WriteLine("How long do you want your password to be?");
            string input = Console.ReadLine();
            int passLength = Convert.ToInt32(input);
            if (passLength < 8)
            {
                Console.WriteLine("Password length must be longer than 8");

            }

            List<char> printableChars = new List<char>();
            for (int i = 0x21; i <= 0x7e; i++)
            {
                printableChars.Add(Convert.ToChar(i));
            }

            Random rand = new Random();
            StringBuilder newPassword = new StringBuilder();

            for (int i = 0; i < passLength; i++)
            {
                newPassword.Append(printableChars[rand.Next(printableChars.Count)]);
            }
            Console.WriteLine(newPassword.ToString());

        }
        public static byte[] HashKey(string key, int length)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                return hashedKey.Take(length).ToArray();
            }
        }
        public static void AddNewPassword()
        {

            Console.WriteLine("Give a name for the password file for example the affiliate application the password is used for");
            string filename = Console.ReadLine();
            if (File.Exists(filename))
            {
                Console.WriteLine("File name already exists choose another");
            }
            Console.WriteLine("Enter the password you'd like to encrypt");
            string Password = HidingInput();
            string PasswordToFile = $"Password for {filename}\n{Password}";
            Console.WriteLine("Enter the symmetric key that's going to be used");
            string SymmetricKey = HidingInput();
            string encrypted = EncryptString(SymmetricKey, PasswordToFile);
            File.WriteAllText($"{filename}.enc", encrypted);
            Console.WriteLine($"Passowrd written to {filename}");
        }
        public static void DeletePassword()
        {
            Console.WriteLine("Which password entry would you like to delete? The name of the file is going to end with .enc");
            string file = Console.ReadLine();
            if (File.Exists(file))
            {
                File.Delete(file);
                Console.WriteLine($"{file} has been deleted");
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

        }
        public static void ReadPassword()
        {
            Console.WriteLine("Enter the name of the file you want to decrypt, it's going to end with .enc");
            string filename = Console.ReadLine();
            if (File.Exists(filename))
            {
                string Ciphertext = File.ReadAllText(filename);
                Console.WriteLine("Enter the symmetric key that's used");
                string SymmetricKey = HidingInput();
                try
                {
                    string Plaintext = DecryptString(SymmetricKey, Ciphertext);
                    Console.WriteLine($"Here's the plaitnext\n{Plaintext}");
                }
                catch
                {
                    Exception e;
                    Console.WriteLine("That was not the correct key");
                }
            }
        }

        // encrypt and decrypt from https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = HashKey(key, 32);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }



        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = HashKey(key, 32);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string HidingInput()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
