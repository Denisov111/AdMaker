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
        Computer comp;

        public Ads(Global global, Computer comp)
        {
            InitializeComponent();
            this.global = global;
            this.comp = comp;
            DataContext = this;

            List<string> partsTemplates = new List<string>() { "{video}", "{memory}", "{hdd}", "{ssd}", "{processor}",
                "{case}", "{mb}", "{power}", "{cpucooler}" };

            //если объявления ещё не генерились, то генерим
            if (comp.Ads.Count==0)
            {
                Ad ad = new Ad() { Title = comp.Title, Description = comp.Description };
                
                Ads_.Add(ad);

                foreach(string temp in partsTemplates)
                {
                    CacheAds_ = new ObservableCollection<Ad>();
                    foreach (Ad ad_ in Ads_)
                    {
                        ObservableCollection<IProduct> parts = GetParts(temp, comp);
                        foreach (IProduct pr in parts)
                        {
                            decimal price = ad_.Price + pr.Price;
                            string desc = ad_.Description.Replace(temp, pr.ToString());
                            Ad newAd = new Ad() { Description = desc, Title = comp.Title, Price = price };

                            foreach (string oldPart in ad_.Guids)
                                newAd.Guids.Add(oldPart);
                            newAd.Guids.Add(pr.Guid);

                            CacheAds_.Add(newAd);
                        }
                    }
                    Ads_ = (CacheAds_.Count > 0) ? CacheAds_ : Ads_;
                }

                

                //добавляем фото
                for(int i=0;i<comp.ImagesPath.Count && i< Ads_.Count; i++)
                {
                    Ads_[i].ImgFileName = comp.ImagesPath[i].Path;
                    Ads_[i].IconFileName = comp.ImagesPath[i].IconPath;
                }

                for (int i = 0; i <  Ads_.Count; i++)
                {
                    
                    Ads_[i].Description = Randomizator.ParseSpintax(Ads_[i].Description);
                    Ads_[i].Title = Randomizator.ParseSpintax(Ads_[i].Title);

                    
                }


                comp.Ads = Ads_;
            }
            Ads_ = comp.Ads;
            adsDataGrid.ItemsSource = comp.Ads;
            adsDataGrid.Items.Refresh();
        }

        private ObservableCollection<IProduct> GetParts(string temp, Computer comp)
        {
            ObservableCollection<IProduct> parts = new ObservableCollection<IProduct>();
            switch(temp)
            {
                case "{video}":
                    foreach(IProduct pr in comp.VideoAdapters)
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
            for(int i=0;i<comp.Ads.Count;i++)
            {
                if(comp.Ads[i].Articul==0)
                {
                    comp.Ads[i].SetArticul();
                    if (Ads_[i].Description.Contains("*art*"))
                        Ads_[i].Description = Ads_[i].Description.Replace("*art*", "#"+ Ads_[i].Articul);
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
            for (int i = 0; i < comp.Ads.Count; i++)
            {
                string imagePath = comp.Ads[i].ImgFileName;
                string newFileName = "modimages\\" + comp.Ads[i].Articul.ToString() + ".jpg";
                string newPath = System.IO.Path.Combine(currentDir, newFileName);
                if(!String.IsNullOrWhiteSpace(imagePath))
                {
                    try
                    {
                        imageEditorImage.Source = await ImageEditor.EditImage(imagePath, newPath);
                        comp.Ads[i].ModImgFileName = newPath;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                }
                
                //if (comp.Ads[i].Articul == 0)
                //{
                //    comp.Ads[i].SetArticul();
                //}

            }
            global.SaveAll();
        }

        //async private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    string currentDir = Directory.GetCurrentDirectory();
        //    if (!Directory.Exists("icons"))
        //    {
        //        Directory.CreateDirectory("icons");
        //    }

        //    for (int i = 0; i < comp.Ads.Count; i++)
        //    {
        //        if(String.IsNullOrWhiteSpace(comp.Ads[i].IconFileName))
        //        {
                    
        //            string fileName_ = System.IO.Path.GetFileName(comp.Ads[i].ImgFileName);
        //            string newFileName = "icons\\icon_" + fileName_;
        //            string newPath = System.IO.Path.Combine(currentDir, newFileName);
        //            await ImageEditor.EditImage(comp.Ads[i].ImgFileName, newPath, 100);
        //            comp.Ads[i].IconFileName = newPath;
        //        }
        //    }

        //}

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            Ad ad = Ads_.Where(a=>a.Articul.ToString()==tag).FirstOrDefault();
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
    }
}
