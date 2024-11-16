using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewEntities.Menu
{
    public class OrgInfoGrid
    {
        public decimal ORG_ID { get; set; }
        public decimal BRANCH_ID { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_IMAGE_URL { get; set; }
        public string ORG_IMAGE_BYTE { get; set; }
        public string BRANCH_NAME { get; set; }
    }
}
