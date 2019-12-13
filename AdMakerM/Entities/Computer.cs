using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    class Computer : IProduct
    {
        public ProductType ProductType { get => ProductType.Computer; }
        public string Title { get; set; }
        public string Descriptrion { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public List<Processor> Processors { get; set; }
        public List<Memory> Memories { get; set; }
        public List<PowerSupply> PowerSupplys { get; set; }
        public List<VideoAdapter> VideoAdapters { get; set; }
        public List<HDD> HDDs { get; set; }
        public List<SSD> SSDs { get; set; }
        
    }
}
