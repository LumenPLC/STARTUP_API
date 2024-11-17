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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public List<MenuVM> GetMenu(string USER_CODE, int ROLE_ID)
        {
            var response = _menuRepository.GetMenu(USER_CODE,ROLE_ID);
            return response;
        }

       public List<OrgInfoGrid> GetOrganizationInfo(OrgBranchParam orgBranchParam)
        {
            var response = _menuRepository.GetOrganizationInfo(orgBranchParam);
            return response;
        }




        /// role assign 



        public List<AllMenuViewModel> GetAllMenuList()
        {
            var response = _menuRepository.GetAllMenuList();
            return response;
        }


        public List<UserRoleAssign> GetUserRoleAssignList(int roleId)
        {
            var response = _menuRepository.GetUserRoleAssignList(roleId);
            return response;
        }



        public (int status, string[] message) SaveRoleMenu(RoleMenuMapViewModel roleMenuMapViewModel)
        {
            var StatusAndMsg = _menuRepository.SaveRoleMenu(roleMenuMapViewModel);
            return StatusAndMsg;
        }


    


    }
}
