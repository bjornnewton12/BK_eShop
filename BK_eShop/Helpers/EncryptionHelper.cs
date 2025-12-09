using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using BK_eShop.Models;

namespace BK_eShop.Helpers
{
    public class EncryptionHelper
    {
        private const byte Key = 0x42; // 66 bytes

        // Encrypt
        public static string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            // Convert text to bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ Key);

            }

            // Save result as text
            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return encryptedText;
            }
        
        // Redo text to bytes
        var bytes = Convert.FromBase64String(encryptedText);

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)(bytes[i] ^ Key);
        }

        return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
