using System.Security.Cryptography;

namespace JmfJwtHelper;

public interface IEcdsaKeyGenerator
{
    void GenerateEcdsaKeys(out string privateKey, out string publicKey);
}

public class EcdsaKeyGenerator : IEcdsaKeyGenerator
{
    public void GenerateEcdsaKeys(out string privateKey, out string publicKey)
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256)
            ?? throw new NotSupportedException("ECDSA is not supported on this platform.");

        // Generate the private key
        var privateKeyParams = ecdsa.ExportParameters(true);
        privateKey = Convert.ToBase64String(privateKeyParams.D!);

        // Generate the public key
        var publicKeyParams = ecdsa.ExportParameters(false);
        var qX = publicKeyParams.Q.X;
        var qY = publicKeyParams.Q.Y;
        var uncompressedPublicKey = new byte[1 + qX!.Length + qY!.Length];
        uncompressedPublicKey[0] = 0x04;
        Buffer.BlockCopy(qX, 0, uncompressedPublicKey, 1, qX.Length);
        Buffer.BlockCopy(qY, 0, uncompressedPublicKey, 1 + qX.Length, qY.Length);
        publicKey = Convert.ToBase64String(uncompressedPublicKey);
    }
}
