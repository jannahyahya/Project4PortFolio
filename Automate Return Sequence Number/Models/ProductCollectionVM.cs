using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Models
{
    public class ProductCollectionVM
    {
        public IEnumerable<ProductCollection> List_ProductCollection { get; set; }
        public ProductCollection Form_ProductCollection { get; set; }
    }
  

}