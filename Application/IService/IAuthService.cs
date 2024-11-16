using Domain.Entities.ViewEntities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAuthService
    {
        public AdminUserMstVM GetLoggedData(UserParams user);
    }
}
