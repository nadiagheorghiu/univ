﻿using System;
using System.Security.Cryptography;

namespace Lab4
{
    class Program
    {
        private static void RSAGenerateKey(int size)
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            int count = 100;
            int keySize = 0;
            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
            {
                myRSA = new RSACryptoServiceProvider(size);
                keySize = myRSA.KeySize;
            }
            swatch.Stop();

            String time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("Generate key cost for " + size + ": " + time_consumed);
        }

        private static void RSAEncrypt(int size)
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            int count = 100;
            int keySize = myRSA.KeySize;
            byte[] bytesKey = BitConverter.GetBytes(keySize);
            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
                myRSA.Encrypt(bytesKey, true);
            swatch.Stop();

            String time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("RSA encrypt cost for " + size + ": " + time_consumed);
        }
        private static void RSADecrypt(int size)
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            int keySize = myRSA.KeySize;
            byte[] RSAciphertext;
            int count = 100;
            RSAciphertext = myRSA.Encrypt(BitConverter.GetBytes(keySize), true);

            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
                myRSA.Decrypt(RSAciphertext, true);
            swatch.Stop();

            String time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("RSA cost for " + size + ": " + time_consumed);
        }
        private static void RSASigning(int size)
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            SHA256Managed myHash = new SHA256Managed();
            byte[] signature;
            int count = 100;

            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
                signature = myRSA.SignData(System.Text.Encoding.ASCII.GetBytes("Hello, World!"), myHash);
            swatch.Stop();

            String time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("RSA signing cost for " + size + ": " + time_consumed);
        }
        private static void RSAValidate(int size)
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            myRSA = new RSACryptoServiceProvider(size);
            int keySize = myRSA.KeySize;
            int count = 100;
            byte[] RSAciphertext;
            byte[] signature;
            String time_consumed;
            SHA256Managed myHash = new SHA256Managed();
            
            RSAciphertext = myRSA.Encrypt(BitConverter.GetBytes(keySize), true);
            myRSA.Decrypt(RSAciphertext, true);
            signature = myRSA.SignData(System.Text.Encoding.ASCII.GetBytes("Hello, World"), myHash);

            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
                myRSA.VerifyData(System.Text.Encoding.ASCII.GetBytes("Hello, World"), myHash, signature);
            swatch.Stop();

            time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("RSA validating cost for " + size + ": " + time_consumed);
        }

        private static void RSAComputationalCost(int size)
        {

            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(size);
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            int count = 100;
            byte[] RSAciphertext;
            byte[] signature;
            int keySize;
            String time_consumed;
            SHA256Managed myHash = new SHA256Managed();

            Console.Write("A message: ");
            string firstMessage = Console.ReadLine();
            Console.Write("Anoteher message: ");
            string secondMessage = Console.ReadLine();

            swatch.Start();
            for (int i = count - 1; i >= 0; i--)
            {
                myRSA = new RSACryptoServiceProvider(size);
                keySize = myRSA.KeySize;
                RSAciphertext = myRSA.Encrypt(BitConverter.GetBytes(keySize), true);
                myRSA.Decrypt(RSAciphertext, true);
                signature = myRSA.SignData(System.Text.Encoding.ASCII.GetBytes(firstMessage), myHash);
                myRSA.VerifyData(System.Text.Encoding.ASCII.GetBytes(secondMessage), myHash, signature);
            }
            swatch.Stop();

            time_consumed = (swatch.Elapsed.TotalMilliseconds / count).ToString() + " ms";
            Console.WriteLine("RSA overall cost for " + size + ": " + time_consumed);
        }
        static void Main(string[] args)
        {
            int caseSwitch = 1;

            while (true)
            {
                Console.WriteLine("\nYou have some options: ");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Cost of RSA key generation");
                Console.WriteLine("2. Cost of RSA encryption");
                Console.WriteLine("3. Cost of RSA decryption");
                Console.WriteLine("4. Cost of RSA signing");
                Console.WriteLine("5. Cost of RSA validation");
                Console.WriteLine("6. Total computational cost of RSA cryptosystem");
                Console.Write("Your choice: ");

                caseSwitch = Convert.ToInt16(Console.ReadLine());
                switch (caseSwitch)
                {
                    case 0:
                        System.Environment.Exit(1);
                        break;
                    case 1:
                        Console.WriteLine("Key generation time");
                        Program.RSAGenerateKey(1024);
                        Program.RSAGenerateKey(2048);
                        Program.RSAGenerateKey(3072);
                        Program.RSAGenerateKey(4096);
                        break;
                    case 2:
                        Program.RSAEncrypt(1024);
                        Program.RSAEncrypt(2048);
                        Program.RSAEncrypt(3072);
                        Program.RSAEncrypt(4096);
                        break;
                    case 3:
                        Program.RSADecrypt(1024);
                        Program.RSADecrypt(2048);
                        Program.RSADecrypt(3072);
                        Program.RSADecrypt(4096);
                        break;
                    case 4:
                        Program.RSASigning(1024);
                        Program.RSASigning(2048);
                        Program.RSASigning(3072);
                        Program.RSASigning(4096);
                        break;
                    case 5:
                        Program.RSASigning(1024);
                        Program.RSASigning(2048);
                        Program.RSASigning(3072);
                        Program.RSASigning(4096);
                        break;
                    case 6:
                        Program.RSAComputationalCost(1024);
                        Program.RSAComputationalCost(2048);
                        Program.RSAComputationalCost(3072);
                        Program.RSAComputationalCost(4096);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}