using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByConditionAsync(Expression<Func<Role, bool>> predicate);
    }
}
