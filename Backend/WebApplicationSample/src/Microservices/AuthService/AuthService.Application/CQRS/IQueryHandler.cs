using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.CQRS
{
    interface IQueryHandler<in TRequest, TResponse> where TRequest : IRequestQuery<TResponse>
    {

    }
}
