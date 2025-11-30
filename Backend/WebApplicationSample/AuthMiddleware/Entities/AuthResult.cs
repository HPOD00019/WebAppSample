using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMiddleware.Entities
{
    public class AuthResult
    {
        public bool IsValid { get; set; }
        public int? UserId { get; set; }

    }
}
