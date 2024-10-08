// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Security.Cryptography.Tests
{
    internal static class SignatureSupport
    {
        internal static bool CanProduceSha1Signature(AsymmetricAlgorithm algorithm) => CanProduceSignature(algorithm, HashAlgorithmName.SHA1);
        internal static bool CanProduceMd5Signature(AsymmetricAlgorithm algorithm) => CanProduceSignature(algorithm, HashAlgorithmName.MD5);

        private static bool CanProduceSignature(AsymmetricAlgorithm algorithm, HashAlgorithmName hashAlgorithmName)
        {
            using (algorithm)
            {
#if NETFRAMEWORK
                return true;
#else
                // We expect all non-Linux platforms to support any signatures, currently.
                if (!OperatingSystem.IsLinux())
                {
                    return true;
                }

                switch (algorithm)
                {
                    case ECDsa ecdsa:
                        try
                        {
                            ecdsa.SignData(Array.Empty<byte>(), hashAlgorithmName);
                            return true;
                        }
                        catch (CryptographicException)
                        {
                            return false;
                        }
                    case RSA rsa:
                        try
                        {
                            rsa.SignData(Array.Empty<byte>(), hashAlgorithmName, RSASignaturePadding.Pkcs1);
                            return true;
                        }
                        catch (CryptographicException)
                        {
                            return false;
                        }
                    default:
                        throw new NotSupportedException($"Algorithm type {algorithm.GetType()} is not supported.");
                }
#endif
            }
        }
    }
}
