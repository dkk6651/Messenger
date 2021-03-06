/// Name: Daniel Kim
///
/// 

using System;
using System.IO;
using System.Net.Http;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messenger
{
    class EncryptedFile
    {
        public String email { get; set; }
        public String key { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        // Key from jsb@cs.rit.edu
        //{"email":"jsb@cs.rit.edu","key":"AAAAAwEAAQAAAQB7w4yJG+kH5BXhL9lgeCxkNqKeIIyC0zzG0FYJu5/WVa7xCdXGSmG3pEEpyEPhe
        //81L9zb1qWpnn9yoiMPPawtDoZ26Um0LA/MAx/n4UdBENyWYd807+ex1h/uJ/GHgeZI/8yZ5LapCTNXaAwXvTfSY4OTG9hEgTJ6uK7cM11hn/qK
        //07EnH1beaGoj/FOATFPqpLkDaz/fOkRQIQr6F41ks0PIJXjzmMeIJdUhBsluJaU/pllHqjTDFk2uBOSQr5g0WFeCVLfss0EYbkbx3BsLtvThDg
        //phBc98KOU2gx3o+Tm5U1oTT/tZdUjrWq8iPWzI+JMrG1RtZEVVeewOFT5sn"}

        static void Main(string[] args)
        {
            
        }

        static void keyGen(int bitsize)
        {
            /// Change to implelment email input or put in Main
            var p = bitsize / 2;
            var q = bitsize - p;
            var N = p * q;
            var r = (p - 1) * (q - 1);
            var E = GenerateRandomInt(65536);
            var D = modInverse(E, r);
            var encodedPublicKey = Convert.ToBase64String(E.ToByteArray());
            var encodedPrivateKey = Convert.ToBase64String(D.ToByteArray());
            var publicKeyFile = new EncryptedFile();
        }

        static void sendKey(String email)
        {
            
        }

        static async void getKey(String email)
        {
            var response = await client.GetAsync("http://kayrun.cs.rit.edu:5000/Key/jsb@cs.rit.edu");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
        }

        static void sendMsg(String email, String text)
        {
            
        }

        static async void getMsg(String email)
        {
            
            
        }

        static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
        
        /// <summary>
        /// Method in charge of generating and checking a BigInteger based on the bit inputted.
        /// Loop is started if the generated integer is not a valid Prime number checked by IsProbablePrime.
        /// </summary>
        /// <param name="bits">bit length for the BigInteger created</param>
        /// <returns>Task with result of BigInteger used to print values when found</returns>
        static BigInteger GenerateRandomInt(int bits)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] somebyte = new byte[bits/8];
            rng.GetBytes(somebyte);
            BigInteger result = new BigInteger(somebyte);
            while (!IsProbablePrime(result))
            {
                somebyte = new byte[bits/8];
                rng.GetBytes(somebyte);
                result = new BigInteger(somebyte);
            }

            return result;
        }
        
        /// <summary>
        /// Method responsible for checking if the BigInteger is prime
        /// </summary>
        /// <param name="source">BigInteger to be checked for prime</param>
        /// <param name="certainty">int representing the amount of times to check for the BigInteger (default 10)</param>
        /// <returns>bool representing if source is prime</returns>
        static bool IsProbablePrime(BigInteger source, int certainty=10)
        {
            if(source == 2 || source == 3)
                return true;
            if(source < 2 || source % 2 == 0)
                return false;
 
            BigInteger d = source - 1;
            int s = 0;
 
            while(d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }
                
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;
 
            for(int i = 0; i < certainty; i++)
            {
                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while(a < 2 || a >= source - 2);
 
                BigInteger x = BigInteger.ModPow(a, d, source);
                if(x == 1 || x == source - 1)
                    continue;
 
                for(int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if(x == 1)
                        return false;
                    if(x == source - 1)
                        break;
                }
 
                if(x != source - 1)
                    return false;
            }
 
            return true;
        }
    }
}