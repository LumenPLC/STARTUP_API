using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewEntities.Menu
{
    public class AllMenuViewModel
    {



        public AllMenuViewModel()
        {
            IS_SELECTED = true;
        }
        public decimal MENU_ITEM_ID { get; set; }
        public decimal PARENT_MENU_ITEM_ID { get; set; }
        public string MENU_DESCRIPTION { get; set; }
        public string MENU_URL { get; set; }

        public bool IS_SELECTED { get; set; }


    }
}
