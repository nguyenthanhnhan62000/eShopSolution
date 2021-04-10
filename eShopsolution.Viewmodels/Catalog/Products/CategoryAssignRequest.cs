using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Catalog.Products
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> Catagories { get; set; } = new List<SelectItem>();
    }
}
