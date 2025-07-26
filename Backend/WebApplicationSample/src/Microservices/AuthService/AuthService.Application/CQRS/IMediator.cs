using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.CQRS
{
    interface IMediator
    {
        T SendQuery<T>(IRequestQuery<T> request);
        void SendCommand (IRequestCommand command);
    }

}
