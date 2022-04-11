using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDinMVC.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}