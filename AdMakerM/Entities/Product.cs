using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{

    public class Product : IProduct
    {
        public ProductType ProductType { get; set; }
        public string Title { get; set; }
        public string Guid { get; set; }
        public string Template { get; set; }
        public string Description { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public decimal Price { get; set; }
    }
}
