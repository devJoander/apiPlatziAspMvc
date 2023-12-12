using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAppPlatzi.Models
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public List<string> images { get; set; }
        public Category categoryId { get; set; } = new Category { Id = 1 , Name = "Clothes", Image = "https://i.imgur.com/QkIa5tT.jpeg" };

    }

}