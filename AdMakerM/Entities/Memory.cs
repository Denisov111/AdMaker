using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public enum MemoryType
    {
        DDR2,
        DDR3,
        DDR4
    }

    public class Memory : IProduct
    {
        public ProductType ProductType { get => ProductType.Memory; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public int Volume { get; set; } = 1;
        public MemoryType MemoryType { get; set; }

        public override string ToString()
        {
            if(!String.IsNullOrWhiteSpace(Title))
                return Title + " " + Volume + " Гб";
            else
                return Volume + " Гб";

        }

        public Memory Clone()
        {
            return new Memory()
            {
                Guid = Guid,
                Title = Title,
                Description = Description,
                Price = Price,
                ImgFileName = ImgFileName,
                ImgFileDir = ImgFileDir,
                ImgFilePath = ImgFilePath,
                Volume = Volume,
                MemoryType = MemoryType
            };
        }
    }
}
