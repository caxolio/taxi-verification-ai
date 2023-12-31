﻿using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;


namespace TaxiVerificationIA.Resources
{
    public class Utilities
    {
        public static string EncryptKey(string key) 
        { 
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create()) 
            { 
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(key));

                foreach (byte b in result) 
                    sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
