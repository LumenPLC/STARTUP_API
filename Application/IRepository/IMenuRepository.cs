using Domain.Entities.Common.Params;
using Domain.Entities.ViewEntities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IMenuRepository
    {
        public List<MenuVM> GetMenu(string USER_CODE, int ROLE_ID);
        List<OrgInfoGrid> GetOrganizationInfo(OrgBranchParam orgBranchParam);



        /// role assign 

        List<AllMenuViewModel> GetAllMenuList();


        List<UserRoleAssign> GetUserRoleAssignList(int roleId);


        (int status, string[] message) SaveRoleMenu(RoleMenuMapViewModel roleMenuMapViewModel);





    }
}
