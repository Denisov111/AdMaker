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
        public ObservableCollection<Memory> MemoryOptions { get; set; } = new ObservableCollection<Memory>();

        //public ObservableCollection<VideoAdapter> ads { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<IProduct> Products { get; set; } = new ObservableCollection<IProduct>();

        public Global()
        {
            InitStore();
            VideoAdapters.CollectionChanged += VideoAdapters_CollectionChanged;
            MemoryOptions.CollectionChanged += VideoAdapters_CollectionChanged;
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
            XElement memoryOptions = new XElement("memory_options");

            doc.Add(st);

            foreach (VideoAdapter va in VideoAdapters)
            {
                XElement vidAd = new XElement("videoadapter",
                                new XElement("title", va.Title),
                                new XElement("memory", va.Memory),
                                new XElement("tdp", va.TDP),
                                new XElement("price", va.Price),
                                new XElement("guid", va.Guid));
                video.Add(vidAd);
                
            }
            doc.Root.Add(video);

            foreach (Memory mo in MemoryOptions)
            {
                XElement memoryOptEl = new XElement("memory_options",
                                new XElement("title", mo.Title),
                                new XElement("volume", mo.Volume),
                                new XElement("memory_type", mo.MemoryType),
                                new XElement("price", mo.Price),
                                new XElement("guid", mo.Guid));
                memoryOptions.Add(memoryOptEl);

            }
            doc.Root.Add(memoryOptions);

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
                    string guid = (el.Element("guid")==null)?"":el.Element("guid").Value;

                    VideoAdapter va = new VideoAdapter()
                    {
                        Title = title,
                        Memory = memory,
                        TDP = tdp,
                        Price = price,
                        Guid= guid
                    };
                    VideoAdapters.Add(va);
                }

                foreach (XElement el in doc.Root.Element("memory_options").Elements())
                {
                    string title = el.Element("title").Value;
                    int volume = Int32.Parse(el.Element("volume").Value);
                    string memoryType = el.Element("memory_type").Value;
                    decimal price = (decimal)Double.Parse(el.Element("price").Value);
                    string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

                    MemoryType mt = 0;

                    if (memoryType == MemoryType.DDR2.ToString())
                        mt = MemoryType.DDR2;
                    if (memoryType == MemoryType.DDR3.ToString())
                        mt = MemoryType.DDR3;
                    if (memoryType == MemoryType.DDR4.ToString())
                        mt = MemoryType.DDR4;

                    Memory memory = new Memory()
                    {
                        Title = title,
                        Volume = volume,
                        MemoryType = mt,
                        Price = price,
                        Guid = guid
                    };
                    MemoryOptions.Add(memory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
