using Application.IRepository;
using Application.IService;
using Domain.Entities.Common.Params;
using Domain.Entities.ViewEntities.Menu;
using Domain.Entities.ViewEntities.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class RoleService : IRoleService
    {


        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }




        public List<RoleModel> GetRoleList()
        {
            var response = _roleRepository.GetRoleList();
            return response;
        }













    }
}
