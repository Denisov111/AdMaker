using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AdMakerM
{
    public class Computer : IProduct
    {
        public ProductType ProductType { get => ProductType.Computer; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public ObservableCollection<Processor> Processors { get; set; } = new ObservableCollection<Processor>();
        public ObservableCollection<Memory> Memories { get; set; } = new ObservableCollection<Memory>();
        public ObservableCollection<PowerSupply> PowerSupplys { get; set; } = new ObservableCollection<PowerSupply>();
        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<HDD> HDDs { get; set; } = new ObservableCollection<HDD>();
        public ObservableCollection<SSD> SSDs { get; set; } = new ObservableCollection<SSD>();


    }
}
