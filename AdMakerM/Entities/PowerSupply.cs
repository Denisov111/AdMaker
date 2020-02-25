using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class PowerSupply : IProduct
    {
        public ProductType ProductType { get => ProductType.PowerSupply; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Template { get; } = "{power}";
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public PowerSupply Clone()
        {
            return new PowerSupply()
            {
                Guid = Guid,
                Title = Title,
                Description = Description,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath
            };
        }
    }
}
