using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUDinMVC.Models
{
    public class Category
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

       [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public bool IsSelected { get; set; }

    }
}