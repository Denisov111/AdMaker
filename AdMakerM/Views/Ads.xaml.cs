using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для Ads.xaml
    /// </summary>
    public partial class Ads : Window
    {
        public ObservableCollection<Ad> Ads_ { get; set; } = new ObservableCollection<Ad>();
        ObservableCollection<Ad> CacheAds_ { get; set; } = new ObservableCollection<Ad>();
        Global global;

        public Ads(Global global, Computer comp)
        {
            InitializeComponent();
            this.global = global;
            DataContext = this;

            //если объявления ещё не генерились, то генерим
            if (comp.Ads.Count==0)
            {
                Ad ad = new Ad() { Title = comp.Title, Description = comp.Description, Price = comp.Price };
                
                Ads_.Add(ad);

                CacheAds_ = new ObservableCollection<Ad>();
                if (comp.Description.Contains("{video}"))
                {
                    foreach (Ad ad_ in Ads_)
                    {
                        foreach (VideoAdapter va in comp.VideoAdapters)
                        {
                            string desc = ad_.Description.Replace("{video}", va.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = comp.Price };
                            CacheAds_.Add(newAd);
                        }
                    }
                }
                Ads_ = (CacheAds_.Count>0)?CacheAds_:Ads_;

                CacheAds_ = new ObservableCollection<Ad>();
                if (comp.Description.Contains("{memory}"))
                {
                    foreach (Ad ad_ in Ads_)
                    {
                        foreach (Memory mem in comp.Memories)
                        {
                            string desc = ad_.Description.Replace("{memory}", mem.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = comp.Price };
                            CacheAds_.Add(newAd);
                        }
                    }
                }
                Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;

                CacheAds_ = new ObservableCollection<Ad>();
                if (comp.Description.Contains("{hdd}"))
                {
                    foreach (Ad ad_ in Ads_)
                    {
                        foreach (HDD hdd in comp.HDDs)
                        {
                            string desc = ad_.Description.Replace("{hdd}", hdd.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = comp.Price };
                            CacheAds_.Add(newAd);
                        }
                    }
                }
                Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;

                CacheAds_ = new ObservableCollection<Ad>();
                if (comp.Description.Contains("{ssd}"))
                {
                    foreach (Ad ad_ in Ads_)
                    {
                        foreach (SSD ssd in comp.SSDs)
                        {
                            string desc = ad_.Description.Replace("{ssd}", ssd.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = comp.Price };
                            CacheAds_.Add(newAd);
                        }
                    }
                }
                Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;

                CacheAds_ = new ObservableCollection<Ad>();
                if (comp.Description.Contains("{processor}"))
                {
                    foreach (Ad ad_ in Ads_)
                    {
                        foreach (Processor processor in comp.Processors)
                        {
                            string desc = ad_.Description.Replace("{processor}", processor.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = comp.Price };
                            CacheAds_.Add(newAd);
                        }
                    }
                }
                Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;

                comp.Ads = Ads_;
            }
            adsDataGrid.ItemsSource = comp.Ads;
            adsDataGrid.Items.Refresh();
        }
    }
}
