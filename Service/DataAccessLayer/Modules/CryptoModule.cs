using System;
using System.Security.Cryptography;
using System.Text;

namespace DataAccessLayer.Modules
{
    // Author: Andreas Gaarde Boel 

    public class CryptoModule
    {
        public string HashPassword(string password, string salt)
        {
            // Hash password based on password, and salt value of the password
            return GenerateHash(CombinePasswordAndSalt(password, salt), new SHA256CryptoServiceProvider());
        }

        public byte[] GenerateSaltByte(int length)
        {
            // Creating an array of specified length.
            byte[] array = new byte[length];

            // Creating our secure number generator. 
            //The standard number generator is not cryptographically secure, as it is mathmatically predictable.
            RNGCryptoServiceProvider secureRandomGen = new RNGCryptoServiceProvider();

            // Inputting our generated array for the generator to fill.
            secureRandomGen.GetBytes(array);

            return array;
        }

        public string GenerateSaltString(int length)
        {
            return BytesToString(GenerateSaltByte(length));
        }

        public string GenerateSaltString()
        {
            return BytesToString(GenerateSaltByte(256));
        }

//        public byte[] GenerateHash(byte[] input, HashAlgorithm algorithm)
//        {
//            return algorithm.ComputeHash(input);
//        }

        public string GenerateHash(string input, HashAlgorithm algorithm)
        {
            return BytesToString(algorithm.ComputeHash(StringToBytes(input)));
        }

        // The input and salt combined in the following way: [input:salt]
        public byte[] CombinePasswordAndSalt(byte[] input, byte[] salt)
        {
            byte[] combined = new byte[input.Length + salt.Length];
            for(int i = 0; i < input.Length; i++)
            {
                combined[i] = input[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                combined[i + input.Length] = salt[i];
            }
            return combined;
        }

        public string CombinePasswordAndSalt(string input, string salt)
        {
            var combined = BytesToString(CombinePasswordAndSalt(StringToBytes(input), StringToBytes(salt)));
            return combined;
        }

        public string BytesToString(byte[] input)
        {
            var bytetostring = Convert.ToBase64String(input);

//            var bytetostring = Encoding.UTF8.GetString(input);

            return bytetostring;
        }
        public byte[] StringToBytes(string input)
        {
            var stringtobyte = Encoding.UTF8.GetBytes(input);
            return stringtobyte;
        }
    }
}