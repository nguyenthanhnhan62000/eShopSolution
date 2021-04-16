using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolutionWebApp.Models
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public String Description { get; set; }

        public String Name { get; set; }

        public String Image { get; set; }
    }

}
