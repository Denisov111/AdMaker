using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public enum ProductType
    {
        Computer,
        HDD,
        Memory,
        Monitor,
        PowerSupply,
        Processor,
        SSD,
        VideoAdapter,
        Motherboard,
        Case,
        ProcessorCooler,
        CaseCooler
    }

    public interface IProduct
    {
        ProductType ProductType { get; }
        string Guid { get; }
        string Title { get; set; }
        string Template { get; }
        string Description { get; set; }
        decimal Price { get; set; }
        string ImgFileName { get; set; }
        string ImgFileDir { get; set; }
        string ImgFilePath { get; set; }
    }
}
