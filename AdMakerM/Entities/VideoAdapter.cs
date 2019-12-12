using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class VideoAdapter : IProduct
    {
        public string Title { get; set; }
        public string Descriptrion { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public int Memory { get; set; }
        public int TDP { get; set; }
    }
}
