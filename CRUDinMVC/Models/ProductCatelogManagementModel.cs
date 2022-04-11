using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDinMVC.Models
{
    public class ProductCatelogManagementModel
    {
        public List<ProductCatelogModel> Products { get; set; }

        public ProductCatelogModel Product { get; set; }

        public List<Category> Categories { get; set; }
    }
}