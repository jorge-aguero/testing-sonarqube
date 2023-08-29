using System.Security.Cryptography;
using JWT.Algorithms;
using Microsoft.Extensions.Options;
using SecurityHelper.Options;

namespace SecurityHelper.Algorithms
{
    public class Es256AlgorithmFactory
    {
        private readonly EcdsaOptions ecdsaOptions;

        public Es256AlgorithmFactory(IOptions<EcdsaOptions> ecdsaOptions)
        {
            this.ecdsaOptions = ecdsaOptions.Value;
        }

        public IAsymmetricAlgorithm Build()
        {
            var publicKeyEcdsa = CreatePublicKeyEcdsa();
            var privateKeyEcdsa = CreatePrivateKeyEcdsa();

            return new ES256Algorithm(publicKeyEcdsa, privateKeyEcdsa);
        }

        private ECDsa CreatePublicKeyEcdsa()
        {
            var publicKeyBytes = Convert.FromBase64String(ecdsaOptions.PublicKey);
            ECDsa publicKeyEcdsa = ECDsa.Create();
            publicKeyEcdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            return publicKeyEcdsa;
        }

        private ECDsa CreatePrivateKeyEcdsa()
        {
            var privateKeyBytes = Convert.FromBase64String(ecdsaOptions.PrivateKey);
            ECDsa privateKeyEcdsa = ECDsa.Create();
            privateKeyEcdsa.ImportECPrivateKey(privateKeyBytes, out _);
            return privateKeyEcdsa;
        }
    }
}
