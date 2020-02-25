using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class HDD : IProduct
    {
        public ProductType ProductType { get => ProductType.HDD; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Template { get; } = "{hdd}";
        public int Volume { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }

        public override string ToString()
        {
            if(Volume>0)
                return Title + " " + Volume + " Гб";
            else
                return Title;
        }

        public HDD Clone()
        {
            return new HDD()
            {
                Guid = Guid,
                Title = Title,
                Description = Description,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath,
                Volume = Volume
            };
        }
    }
}
