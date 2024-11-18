using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystemSoftware1.ViewModels;
using InventorySystemSoftware1.Views;
using InventorySystemSoftware1.Memory;
using InventorySystemSoftware1.Repos;



namespace InventorySystemSoftware1
{
    public static class InventoryConfig
    {
        public static StockViewModel stockViewModel = null;
        public static OrderViewModel orderViewModel = null;

        static InventoryConfig()
        {
            
                // Initialize ViewModels
                //stockViewModel = new StockViewModel();
                //orderViewModel = new OrderViewModel();
            
               
        }

    }

    }
    

