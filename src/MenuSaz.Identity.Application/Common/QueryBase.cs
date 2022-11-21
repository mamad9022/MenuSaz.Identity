using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuSaz.Identity.Application.Common
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}
