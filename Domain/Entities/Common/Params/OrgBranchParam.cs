using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Params
{
    public class OrgBranchParam
    {
        public decimal ORG_ID { get; set; }
        public decimal BRANCH_ID { get; set; }
    }


    public class OrgBranchParamEMP
    {
        public decimal ORG_ID { get; set; }
        public decimal BRANCH_ID { get; set; }
        public string OFFICE_CODE { get; set; }
        public string DEPARTMENT_TYPE { get; set; }
    }


}
