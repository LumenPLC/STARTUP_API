using Application.IRepository;
using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class SetupService :ISetupService
    {
        private readonly ISetupRepository _setupRepository;
        public SetupService(ISetupRepository setupRepository)
        {
            _setupRepository = setupRepository;
            
        }

      



}



}



