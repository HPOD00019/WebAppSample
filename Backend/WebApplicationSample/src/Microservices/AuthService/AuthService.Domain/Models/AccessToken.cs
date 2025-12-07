using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Models
{
    public class AccessToken
    {
        public AccessToken() { }
        public string Value;
        public int IssuerId;
    }
}
