using AuthService.Domain.Services;
using System.Security.Cryptography;

namespace AuthService.Application.Services
{
    public class RsaTokenEncryptionService : ITokenEncrypter
    {
        private readonly string _rsaPrivateKey;
        private readonly string _rsaPublicKey;
        private RSA _encrypter;

        public RsaTokenEncryptionService(string privateKey, string publicKey)
        {
            _rsaPrivateKey = privateKey;
            _rsaPublicKey = publicKey;

            _encrypter = RSA.Create();

            _encrypter.ImportFromPem(publicKey);
            _encrypter.ImportFromPem(privateKey);

        }

        public Task<byte[]> Encrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public string GetEncryptionType()
        {

            throw new NotImplementedException();
        }
    }
}
