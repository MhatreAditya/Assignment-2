using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CRUDinMVC.Models
{
    public class ProductCatelogModel
    {
        public Product Product { get;set; }

        public Category Category { get; set; }
    }
}