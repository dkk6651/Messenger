/// Name: Daniel Kim
///
/// 

using System;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;

namespace Messenger
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        // Key from jsb@cs.rit.edu
        //{"email":"jsb@cs.rit.edu","key":"AAAAAwEAAQAAAQB7w4yJG+kH5BXhL9lgeCxkNqKeIIyC0zzG0FYJu5/WVa7xCdXGSmG3pEEpyEPhe
        //81L9zb1qWpnn9yoiMPPawtDoZ26Um0LA/MAx/n4UdBENyWYd807+ex1h/uJ/GHgeZI/8yZ5LapCTNXaAwXvTfSY4OTG9hEgTJ6uK7cM11hn/qK
        //07EnH1beaGoj/FOATFPqpLkDaz/fOkRQIQr6F41ks0PIJXjzmMeIJdUhBsluJaU/pllHqjTDFk2uBOSQr5g0WFeCVLfss0EYbkbx3BsLtvThDg
        //phBc98KOU2gx3o+Tm5U1oTT/tZdUjrWq8iPWzI+JMrG1RtZEVVeewOFT5sn"}

        static async Task Main(string[] args)
        {
            HttpResponseMessage response = await client.GetAsync("http://kayrun.cs.rit.edu:5000/Key/jsb@cs.rit.edu");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }

        static void keyGen(int bitsize)
        {
            
        }

        static void sendKey(String email)
        {
            
        }

        static void getKey(String email)
        {
            
        }

        static void sendMsg(String email, String text)
        {
            
        }

        static async void getMsg(String email)
        {
            HttpClient httpClient = new HttpClient();
            
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
    }
}