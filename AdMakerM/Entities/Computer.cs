using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AdMakerM
{
    class Computer : IProduct
    {
        public ProductType ProductType { get => ProductType.Computer; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Descriptrion { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public ObservableCollection<Processor> Processors { get; set; }
        public ObservableCollection<Memory> Memories { get; set; }
        public ObservableCollection<PowerSupply> PowerSupplys { get; set; }
        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; }
        public ObservableCollection<HDD> HDDs { get; set; }
        public ObservableCollection<SSD> SSDs { get; set; }

        
    }
}
