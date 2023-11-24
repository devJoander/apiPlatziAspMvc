using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPlatzi_.Models
{

    public class ProductsContainer
    {
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string thumbnail { get; set; }
        public double rating { get; set; }
        public double discountPercentage {  get; set; }
        public int stock { get; set; }
        public List<string> images { get; set; }
    }

    //public class Product
    //{
    //    public int id { get; set; }
    //    public string title { get; set; }
    //    public decimal price { get; set; }
    //    public string description { get; set; }
    //    //public List<string> images { get; set; }
    //    //public Category categoryId { get; set; }
    //}
}