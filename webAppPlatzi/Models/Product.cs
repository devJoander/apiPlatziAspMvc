using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAppPlatzi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public Category Category { get; set; }

    }
}