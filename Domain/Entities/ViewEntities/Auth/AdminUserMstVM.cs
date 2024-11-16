using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewEntities.Auth
{
    public class AdminUserMstVM
    {
        public decimal USER_ID { get; set; }

        public string USER_CODE { get; set; }

        public string USER_NAME { get; set; }

        public string EMAIL { get; set; }

        public decimal ROLE_ID { get; set; }

        public decimal USER_TYPE_ID { get; set; }

        public string USER_TYPE_DESC { get; set; }

        public decimal ORG_ID { get; set; }
        public string ORG_NAME { get; set; }

        public string STATUS { get; set; }

        public string Token { get; set; }
        public decimal BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ORG_IMAGE_URL { get; set; }
        public string ORG_IMAGE_BYTE { get; set; }
    }
}
