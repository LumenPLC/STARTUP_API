using Application.IRepository;
using Application.IService;
using Domain.Entities.ViewEntities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        public AdminUserMstVM GetLoggedData(UserParams user)
        {
            var StatusAndMsg = _authRepository.GetLoggedData(user);
            return StatusAndMsg;
        }

    }
}
