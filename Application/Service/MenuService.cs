using Application.IRepository;
using Application.IService;
using Domain.Entities.Common.Params;
using Domain.Entities.ViewEntities.Menu;
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
    }
}
