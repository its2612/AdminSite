using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdminSite.Models
{
    public class ProductModel
    {
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Product Details")]
        public string ProductDetails { get; set; }
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        public HttpPostedFile ImageFile { get; set; }
    }
}