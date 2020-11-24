using NUnit.Framework;
using PasswordWallet.Cryptography;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class EncryptionManagerTests
    {
        [Test]
        public void CalculateSHA512_ThrowsException_EmptyStringGiven()
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.Throws<ArgumentException>(() => instance.CalculateSHA512(""));
        }

        private static IEnumerable<TestCaseData> CalculateSHA512_Texts
        {
            get
            {
                yield return new TestCaseData("0697221322", "6A602E85B83A107D012B7BCFD6CB329C2153E83CA9BCA84E42A167AC423A1DD58C8BB5D1E06146E56BD452CD01BCDB451372A89771CCA8B70EE33505E311ADB5");
                yield return new TestCaseData("          ", "3E78A283A8328BAA30429E49981F5AD1F01F8DE77D7E40E377EA2E939D03316A4DB58AFBAD88660C5566C0D5B275B3F6DBC30E5B1B2188E6849790F5C12D94A5");
                yield return new TestCaseData("3E78A283A8328BAA30429E49981F5AD1F01F8DE77D7E40E377EA2E939D03316A4DB58AFBAD88660C5566C0D5B275B3F6DBC30E5B1B2188E6849790F5C12D94A5", "66540EF30D9BEC3139260D76818721C50A18AE1772B2372F2CF78F2483B6131EA031B7548ED692255B80AFEA85E18FF70D650F96ECFE4D35C9C232AB6FE77B0F");
            }
        }

        [Test, TestCaseSource("CalculateSHA512_Texts")]
        public void CalculateSHA512_ReturnsCorrectHash_AnyTextGiven(string text, string expResult)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.AreEqual(expResult, instance.CalculateSHA512(text));
        }

        private static IEnumerable<TestCaseData> CalculateHMAC_EmptyParameters
        {
            get
            {
                yield return new TestCaseData("", "key");
                yield return new TestCaseData("string", "");
            }
        }

        [Test, TestCaseSource("CalculateHMAC_EmptyParameters")]
        public void CalculateHMAC_ThrowsException_EmptyStringOrKeyGiven(string text, string key)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.Throws<ArgumentException>(() => instance.CalculateHMAC(text, key));
        }

        private static IEnumerable<TestCaseData> CalculateHMAC_CorrectParams
        {
            get
            {
                yield return new TestCaseData("0697221322", "9267933099", "C5164E9972D7D12D8D25D1C03DD8D4FCF6B7A87B25151EACF71BF8BB8FB5DC419C018183A60CC12F5A9308F58852F5DBD198281947A71AA33E13A4BDA84A3FC0");
                yield return new TestCaseData(" ", " ", "0B8A72163B925BBB61FFA98E90339E57F0ED5C8956665AF83691AEBBDEBB87E7EB6090A877B62FDCFCA2E29768159D0066E7EF875A87D6A8B2FF9D286A98FF56");
            }
        }

        [Test, TestCaseSource("CalculateHMAC_CorrectParams")]
        public void CalculateHMAC_ReturnsCorrectHash_AnyTextAndKeyGiven(string text, string key, string expResult)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.AreEqual(expResult, instance.CalculateHMAC(text, key));
        }

        private static IEnumerable<TestCaseData> AESEncrypt_CorrectData
        {
            get
            {
                yield return new TestCaseData("1234567890", "xTbxmyAtCXMJ6HSVSIXB+A==", "XSKkbtZBiZlfbigi8rbb4w==");
                yield return new TestCaseData("password", "+24BNTOehGl0AydSrIX1qg==", "Q27CS6l1lVRcJYT1tFSgSQ==");
            }
        }

        [Test, TestCaseSource("AESEncrypt_CorrectData")]
        public void AESEncrypt_ReturnsCorrectCipher_AnyTextAndKeyGiven(string plainText, string key, string expResult)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.AreEqual(expResult, instance.AESEncrypt(key, plainText));
        }

        private static IEnumerable<TestCaseData> AESDecrypt_CorrectData
        {
            get
            {
                yield return new TestCaseData("XSKkbtZBiZlfbigi8rbb4w==", "xTbxmyAtCXMJ6HSVSIXB+A==", "1234567890");
                yield return new TestCaseData("Q27CS6l1lVRcJYT1tFSgSQ==", "+24BNTOehGl0AydSrIX1qg==", "password");
            }
        }

        [Test, TestCaseSource("AESDecrypt_CorrectData")]
        public void AESDecrypt_ReturnsCorrectCipher_AnyTextAndKeyGiven(string cipherText, string key, string expResult)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.AreEqual(expResult, instance.AESDecrypt(key, cipherText));
        }

        private static IEnumerable<TestCaseData> AESDecrypt_ToShortCipher
        {
            get
            {
                yield return new TestCaseData("XSKkbtZBiZ", "xTbxmyAtCXMJ6HSVSIXB+A==");
            }
        }

        [Test, TestCaseSource("AESDecrypt_ToShortCipher")]
        public void AESDecrypt_ThrowsException_IncorrectCipherLength(string cipherText, string key)
        {
            EncryptionManager instance = new EncryptionManager();

            Assert.Throws<ArgumentException>(() => instance.AESDecrypt(key, cipherText));
        }
    }
}