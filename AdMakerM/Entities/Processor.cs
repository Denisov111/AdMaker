using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    class Processor : IProduct
    {
        public ProductType ProductType { get => ProductType.Computer; }
        public string Title { get; set; }
        public string Descriptrion { get; set; }
        public int TDP { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
    }
}
