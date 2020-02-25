using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public enum ProcessorSocket
    {
        AM4, AM3PLUS, AM3, LGA1155, LGA1156, LGA1150
    }

    public class Processor : IProduct
    {
        public ProductType ProductType { get => ProductType.Processor; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Template { get; } = "{power}";
        public string Description { get; set; }
        public int TDP { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public ProcessorSocket Socket { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public Processor Clone()
        {
            return new Processor()
            {
                Guid = Guid,
                Title = Title,
                Description = Description,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath,
                Socket = Socket
            };
        }
    }
}
