using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class Motherboard : IProduct
    {
        public ProductType ProductType { get => ProductType.Motherboard; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxProcessorTDP { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public ProcessorSocket Socket { get; set; }

        public override string ToString()
        {
            return Title + " " + Socket.ToString();
        }

        public Motherboard Clone()
        {
            return new Motherboard()
            {
                Guid = Guid,
                Title = Title,
                Description = Description,
                MaxProcessorTDP= MaxProcessorTDP,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath,
                Socket = Socket
            };
        }
    }
}
