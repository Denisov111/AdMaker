using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class VideoAdapter : IProduct
    {
        public ProductType ProductType { get => ProductType.Computer; }
        public string Title { get; set; }
        public string Descriptrion { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public int Memory { get; set; }
        public int TDP { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public VideoAdapter Clone()
        {
            return new VideoAdapter()
            {
                Title = Title,
                Descriptrion = Descriptrion,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath,
                Memory = Memory,
                TDP = TDP
            };
        }
    }
}
