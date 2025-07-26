using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.CQRS
{
    class Mediator : IMediator
    {
        private readonly IServiceProvider _requestHandlers;
        public Mediator(IServiceProvider requestServices)
        {
            _requestHandlers = requestServices;
        }

        public TResponse SendQuery<TResponse>(IRequestQuery<TResponse> request)
        {
            throw new NotImplementedException();
        }

        public void SendCommand(IRequestCommand command)
        {
            throw new NotImplementedException();
        }

    }
}
