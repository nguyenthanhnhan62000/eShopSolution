using eShopsolution.Viewmodels.Untilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Models
{
    public class NavigationViewModel
    {
        public List<SlideVm> Languages { get; set; }

        public String CurrentLanguageId { get; set; }

        public String ReturnUrl { get; set; }
    }
}
