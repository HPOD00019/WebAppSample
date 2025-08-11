using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Services
{
    public interface ITokenEncrypter
    {
        public Task<byte[]> Encrypt(byte[] data);
        public string GetEncryptionType();
    }
}
