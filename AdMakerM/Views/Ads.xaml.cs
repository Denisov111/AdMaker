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
using System.IO;
using System.Collections.Specialized;

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для Ads.xaml
    /// </summary>
    public partial class Ads : Window
    {
        public ObservableCollection<Ad> Ads_ { get; set; } = new ObservableCollection<Ad>();
        ObservableCollection<Ad> CacheAds_ { get; set; } = new ObservableCollection<Ad>();
        Computer Comp { get; set; }
        public decimal MinPriceFilter { get; set; }
        public decimal MaxPriceFilter { get; set; }
        public decimal MinCostPriceFilter { get; set; }
        public decimal MaxCostPriceFilter { get; set; }

        Global global;
        //bool IsPublishedAdsViewMode = false;

        public Ads(Global global, Computer comp_)
        {
            InitializeComponent();
            this.global = global;
            Comp = comp_;
            DataContext = this;

            if (Comp != null)
            {
                List<string> partsTemplates = new List<string>() { "{video}", "{memory}", "{hdd}", "{ssd}", "{processor}",
                "{case}", "{mb}", "{power}", "{cpucooler}" };

                //если объявления ещё не генерились, то генерим
                if (Comp.Ads.Count == 0)
                {
                    Ad ad = new Ad() { Title = Comp.Title, Description = Comp.Description };

                    Ads_.Add(ad);

                    foreach (string temp in partsTemplates)
                    {
                        CacheAds_ = new ObservableCollection<Ad>();
                        foreach (Ad ad_ in Ads_)
                        {
                            ObservableCollection<IProduct> parts = GetParts(temp, Comp);
                            foreach (IProduct pr in parts)
                            {
                                decimal price = ad_.Price + pr.Price;
                                string desc = ad_.Description.Replace(temp, pr.ToString());
                                string title = ad_.Title.Replace(temp, pr.ToString());
                                Ad newAd = new Ad() { Description = desc, Title = title, Price = price };

                                foreach (string oldPart in ad_.Guids)
                                    newAd.Guids.Add(oldPart);
                                newAd.Guids.Add(pr.Guid);

                                CacheAds_.Add(newAd);
                            }
                        }
                        Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;
                    }

                    //добавляем фото
                    if (Comp.ImagesPath.Count > 0)
                    {
                        int imageIndex = 0;
                        for (int i = 0; i < Ads_.Count; i++)
                        {
                            if (imageIndex == Comp.ImagesPath.Count) imageIndex = 0;
                            Ads_[i].ImgFileName = Comp.ImagesPath[imageIndex].Path;
                            Ads_[i].IconFileName = Comp.ImagesPath[imageIndex].IconPath;
                            imageIndex++;
                        }
                    }


                    for (int i = 0; i < Ads_.Count; i++)
                    {

                        Ads_[i].Description = Randomizator.ParseSpintax(Ads_[i].Description);
                        Ads_[i].Title = Randomizator.ParseSpintax(Ads_[i].Title);


                    }

                    Comp.Ads = Ads_;
                }
                Ads_ = Comp.Ads;
                adsDataGrid.ItemsSource = Comp.Ads;
                adsDataGrid.Items.Refresh();

                videoComboBox.ItemsSource = Comp.VideoAdapters;
                memoryComboBox.ItemsSource = Comp.Memories;
                hddComboBox.ItemsSource = Comp.HDDs;
                ssdComboBox.ItemsSource = Comp.SSDs;
                cpuComboBox.ItemsSource = Comp.Processors;

                adsCountLabel.Content = Comp.Ads.Count.ToString();
                filteredAdsCountLabel.Content = Comp.Ads.Count.ToString();
            }
            else
            {
                Ads_ = global.AdsArchive;
                adsDataGrid.ItemsSource = global.AdsArchive;
                adsDataGrid.Items.Refresh();

                videoComboBox.ItemsSource = global.VideoAdapters;
                memoryComboBox.ItemsSource = global.MemoryOptions;
                hddComboBox.ItemsSource = global.HDDOptions;
                ssdComboBox.ItemsSource = global.SSDOptions;
                cpuComboBox.ItemsSource = global.ProcessorOptions;

                adsCountLabel.Content = global.AdsArchive.Count.ToString();
                filteredAdsCountLabel.Content = global.AdsArchive.Count.ToString();
            }
        }

        private ObservableCollection<IProduct> GetParts(string temp, Computer comp)
        {
            ObservableCollection<IProduct> parts = new ObservableCollection<IProduct>();
            switch (temp)
            {
                case "{video}":
                    foreach (IProduct pr in comp.VideoAdapters)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{memory}":
                    foreach (IProduct pr in comp.Memories)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{hdd}":
                    foreach (IProduct pr in comp.HDDs)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{ssd}":
                    foreach (IProduct pr in comp.SSDs)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{processor}":
                    foreach (IProduct pr in comp.Processors)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{case}":
                    foreach (IProduct pr in comp.Cases)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{mb}":
                    foreach (IProduct pr in comp.Motherboards)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{power}":
                    foreach (IProduct pr in comp.PowerSupplys)
                    {
                        parts.Add(pr);
                    }
                    break;
                case "{cpucooler}":
                    foreach (IProduct pr in comp.CPUCoolers)
                    {
                        parts.Add(pr);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return parts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Comp.Ads.Count; i++)
            {
                if (Comp.Ads[i].Articul == 0)
                {
                    Comp.Ads[i].SetArticul();
                    if (Ads_[i].Description.Contains("*art*"))
                        Ads_[i].Description = Ads_[i].Description.Replace("*art*", "#" + Ads_[i].Articul);
                }

            }

            global.SaveAll();
            Close();
        }

        async private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            if (!Directory.Exists("modimages"))
            {
                Directory.CreateDirectory("modimages");
            }
            for (int i = 0; i < Comp.Ads.Count; i++)
            {
                string imagePath = Comp.Ads[i].ImgFileName;
                string newFileName = "modimages\\" + Comp.Ads[i].Articul.ToString() + ".jpg";
                string newPath = System.IO.Path.Combine(currentDir, newFileName);
                if (!String.IsNullOrWhiteSpace(imagePath))
                {
                    try
                    {
                        imageEditorImage.Source = await ImageEditor.EditImage(imagePath, newPath);
                        Comp.Ads[i].ModImgFileName = newPath;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                }

            }
            global.SaveAll();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            Ad ad = Ads_.Where(a => a.Articul.ToString() == tag).FirstOrDefault();
            Views.AdCard f = new AdCard(ad, global);
            try
            {
                f.ShowDialog();
            }
            catch { }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Ads_.Clear();
            global.SaveAll();
            Close();
        }

        private void VideoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringCollection selectedGuids = new StringCollection();

            VideoAdapter selectedVideoAdapter = (VideoAdapter)(videoComboBox).SelectedItem;
            if (selectedVideoAdapter != null) selectedGuids.Add(selectedVideoAdapter.Guid);

            Memory selectedMemory = (Memory)(memoryComboBox).SelectedItem;
            if (selectedMemory != null) selectedGuids.Add(selectedMemory.Guid);

            HDD selectedHDD = (HDD)(hddComboBox).SelectedItem;
            if (selectedHDD != null) selectedGuids.Add(selectedHDD.Guid);

            SSD selectedSSD = (SSD)(ssdComboBox).SelectedItem;
            if (selectedSSD != null) selectedGuids.Add(selectedSSD.Guid);

            Processor selectedCPU = (Processor)(cpuComboBox).SelectedItem;
            if (selectedCPU != null) selectedGuids.Add(selectedCPU.Guid);

            RefreshDataGrid(selectedGuids);
        }

        private void RefreshDataGrid(StringCollection selectedGuids)
        {
            if (selectedGuids.Count == 0)
            {

                ObservableCollection<Ad> tempAds = new ObservableCollection<Ad>(Ads_);
                if (MaxPriceFilter > 0)
                    tempAds = new ObservableCollection<Ad>(tempAds.Where(ad => ad.BuyPrice <= MaxPriceFilter));
                if (MinPriceFilter > 0)
                    tempAds = new ObservableCollection<Ad>(tempAds.Where(ad => ad.BuyPrice >= MinPriceFilter));
                if (MaxCostPriceFilter > 0)
                    tempAds = new ObservableCollection<Ad>(tempAds.Where(ad => ad.Price <= MaxCostPriceFilter));
                if (MinCostPriceFilter > 0)
                    tempAds = new ObservableCollection<Ad>(tempAds.Where(ad => ad.Price >= MinCostPriceFilter));
                adsDataGrid.ItemsSource = tempAds;
            }
            else
            {
                ObservableCollection<Ad> filteredAds;
                if (Comp != null)
                    filteredAds = new ObservableCollection<Ad>(Comp.Ads);
                else
                    filteredAds = new ObservableCollection<Ad>(global.AdsArchive);

                foreach (string guid in selectedGuids)
                {
                    filteredAds = new ObservableCollection<Ad>(filteredAds.Where(ad => ad.Guids.Contains(guid)));
                }
                adsDataGrid.ItemsSource = filteredAds;
            }
            adsDataGrid.Items.Refresh();
            filteredAdsCountLabel.Content = adsDataGrid.Items.Count.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            videoComboBox.SelectedItem = null;
            memoryComboBox.SelectedItem = null;
            hddComboBox.SelectedItem = null;
            ssdComboBox.SelectedItem = null;
            cpuComboBox.SelectedItem = null;
            adsDataGrid.ItemsSource = Comp.Ads;
            filteredAdsCountLabel.Content = adsDataGrid.Items.Count.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            VideoComboBox_SelectionChanged(null, null);
        }
    }
}
