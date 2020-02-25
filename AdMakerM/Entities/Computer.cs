using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdMakerM
{
    public class Computer : IProduct , INotifyPropertyChanged
    {
        private decimal price;
        string internalTitle;

        public ProductType ProductType { get => ProductType.Computer; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string InternalTitle
        {
            get { return internalTitle; }
            set
            {
                internalTitle = value;
                OnPropertyChanged();
            }
        }
        public string Template { get; }
        public string Description { get; set; }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public string ImgFileName { get; set; }
        public string ImgFileDir { get; set; }
        public string ImgFilePath { get; set; }
        public ObservableCollection<Processor> Processors { get; set; } = new ObservableCollection<Processor>();
        public ObservableCollection<Memory> Memories { get; set; } = new ObservableCollection<Memory>();
        public ObservableCollection<PowerSupply> PowerSupplys { get; set; } = new ObservableCollection<PowerSupply>();
        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<HDD> HDDs { get; set; } = new ObservableCollection<HDD>();
        public ObservableCollection<SSD> SSDs { get; set; } = new ObservableCollection<SSD>();
        public ObservableCollection<Motherboard> Motherboards { get; set; } = new ObservableCollection<Motherboard>();
        public ObservableCollection<Case> Cases { get; set; } = new ObservableCollection<Case>();
        public ObservableCollection<ProcessorCooler> CPUCoolers { get; set; } = new ObservableCollection<ProcessorCooler>();
        public ObservableCollection<AdImage> ImagesPath { get; set; } = new ObservableCollection<AdImage>();
        public ObservableCollection<Ad> Ads { get; set; } = new ObservableCollection<Ad>();




        #region INotifyPropertyChanged code

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
