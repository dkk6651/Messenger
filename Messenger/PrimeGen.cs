using System.Numerics;
using System.Security.Cryptography;

namespace Messenger
{
    public class PrimeGen
    {
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