using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.IO;

namespace AdMakerM
{
    public class Global
    {
        string storeFile = @"store.xml";

        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();

        //public ObservableCollection<VideoAdapter> ads { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<IProduct> Products { get; set; } = new ObservableCollection<IProduct>();

        public Global()
        {
            InitStore();
            VideoAdapters.CollectionChanged += VideoAdapters_CollectionChanged;
        }

        private void VideoAdapters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveAll();
        }

        

        internal void SaveAll()
        {
            
            XDocument doc = new XDocument();
            XElement st = new XElement("store");
            XElement video = new XElement("videoadapters");
            
            doc.Add(st);

            foreach (VideoAdapter va in VideoAdapters)
            {
                XElement vidAd = new XElement("videoadapter",
                                new XElement("title", va.Title),
                                new XElement("memory", va.Memory),
                                new XElement("tdp", va.TDP),
                                new XElement("price", va.Price));
                video.Add(vidAd);
                
            }
            doc.Root.Add(video);
            doc.Save(storeFile);
        }

        private void InitStore()
        {
            XDocument doc = new XDocument();
            if (!File.Exists(storeFile)) return;

            try
            {
                doc = XDocument.Load(storeFile);

                foreach (XElement el in doc.Root.Element("videoadapters").Elements())
                {
                    string title = el.Element("title").Value;
                    int memory = Int32.Parse(el.Element("memory").Value);
                    int tdp = Int32.Parse( el.Element("tdp").Value);
                    decimal price = (decimal)Double.Parse( el.Element("price").Value);

                    VideoAdapter va = new VideoAdapter()
                    {
                        Title = title,
                        Memory = memory,
                        TDP = tdp,
                        Price = price
                    };
                    VideoAdapters.Add(va);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
