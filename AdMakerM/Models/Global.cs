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

        public ObservableCollection<Computer> Comps { get; set; } = new ObservableCollection<Computer>();

        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<Memory> MemoryOptions { get; set; } = new ObservableCollection<Memory>();
        public ObservableCollection<SSD> SSDOptions { get; set; } = new ObservableCollection<SSD>();
        public ObservableCollection<HDD> HDDOptions { get; set; } = new ObservableCollection<HDD>();

        //public ObservableCollection<VideoAdapter> ads { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<IProduct> Products { get; set; } = new ObservableCollection<IProduct>();

        public Global()
        {
            InitStore();
            VideoAdapters.CollectionChanged += VideoAdapters_CollectionChanged;
            MemoryOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            SSDOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            HDDOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            Comps.CollectionChanged+= VideoAdapters_CollectionChanged;
        }

        internal void Edit(string guid)
        {
            Computer comp = Comps.Where(c=>c.Guid==guid).FirstOrDefault();

            AddComp f = new AddComp(this, comp);
            f.ShowDialog();
        }

        private void VideoAdapters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveAll();
        }

        internal void SaveAll()
        {
            
            XDocument doc = new XDocument();
            XElement st = new XElement("store");
            XElement comps = new XElement("comps");
            XElement video = new XElement("videoadapters");
            XElement memoryOptions = new XElement("memory_options");
            XElement ssdOptions = new XElement("ssd_options");
            XElement hddOptions = new XElement("hdd_options");

            doc.Add(st);

            foreach (Computer comp in Comps)
            {
                XElement compEl = new XElement("comp",
                                new XElement("title", comp.Title),
                                new XElement("desc", comp.Description),
                                new XElement("price", comp.Price),
                                new XElement("guid", comp.Guid));

                if(comp.VideoAdapters!=null)
                {
                    XElement videoEl = new XElement("video_adapters");
                    foreach(VideoAdapter va in comp.VideoAdapters)
                    {
                        string guid = va.Guid;
                        videoEl.Add(new XElement("guid",guid));
                    }
                    compEl.Add(videoEl);
                }

                if (comp.Memories != null)
                {
                    XElement memoryEl = new XElement("memory_options");
                    foreach (Memory mem in comp.Memories)
                    {
                        string guid = mem.Guid;
                        memoryEl.Add(new XElement("guid", guid));
                    }
                    compEl.Add(memoryEl);
                }

                if (comp.SSDs != null)
                {
                    XElement ssdEl = new XElement("ssd_options");
                    foreach (SSD ssd in comp.SSDs)
                    {
                        string guid = ssd.Guid;
                        ssdEl.Add(new XElement("guid", guid));
                    }
                    compEl.Add(ssdEl);
                }

                if (comp.HDDs != null)
                {
                    XElement hddEl = new XElement("hdd_options");
                    foreach (HDD hdd in comp.HDDs)
                    {
                        string guid = hdd.Guid;
                        hddEl.Add(new XElement("guid", guid));
                    }
                    compEl.Add(hddEl);
                }

                comps.Add(compEl);

            }
            doc.Root.Add(comps);

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

            foreach (SSD ssd in SSDOptions)
            {
                XElement ssdOptEl = new XElement("ssd_options",
                                new XElement("title", ssd.Title),
                                new XElement("volume", ssd.Volume),
                                new XElement("price", ssd.Price),
                                new XElement("guid", ssd.Guid));
                ssdOptions.Add(ssdOptEl);

            }
            doc.Root.Add(ssdOptions);

            foreach (HDD hdd in HDDOptions)
            {
                XElement hddOptEl = new XElement("hdd_options",
                                new XElement("title", hdd.Title),
                                new XElement("volume", hdd.Volume),
                                new XElement("price", hdd.Price),
                                new XElement("guid", hdd.Guid));
                hddOptions.Add(hddOptEl);

            }
            doc.Root.Add(hddOptions);

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
                    int tdp = Int32.Parse(el.Element("tdp").Value);
                    decimal price = (decimal)Double.Parse(el.Element("price").Value);
                    string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

                    VideoAdapter va = new VideoAdapter()
                    {
                        Title = title,
                        Memory = memory,
                        TDP = tdp,
                        Price = price,
                        Guid = guid
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

                foreach (XElement el in doc.Root.Element("ssd_options").Elements())
                {
                    string title = el.Element("title").Value;
                    int volume = Int32.Parse(el.Element("volume").Value);
                    decimal price = (decimal)Double.Parse(el.Element("price").Value);
                    string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

                    SSD ssd = new SSD()
                    {
                        Title = title,
                        Volume = volume,
                        Price = price,
                        Guid = guid
                    };
                    SSDOptions.Add(ssd);
                }

                foreach (XElement el in doc.Root.Element("hdd_options").Elements())
                {
                    string title = el.Element("title").Value;
                    int volume = Int32.Parse(el.Element("volume").Value);
                    decimal price = (decimal)Double.Parse(el.Element("price").Value);
                    string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

                    HDD hdd = new HDD()
                    {
                        Title = title,
                        Volume = volume,
                        Price = price,
                        Guid = guid
                    };
                    HDDOptions.Add(hdd);
                }


                //объекты компьютеров собираем в последнюю очередь,
                //чтобы опции сборки были уже проинициализированы
                foreach (XElement el in doc.Root.Element("comps").Elements())
                {
                    string title = el.Element("title").Value;
                    string desc = el.Element("desc").Value;
                    decimal price = (decimal)Double.Parse(el.Element("price").Value);
                    string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

                    Computer comp = new Computer()
                    {
                        Title = title,
                        Description = desc,
                        Price = price,
                        Guid = guid
                    };

                    if(el.Element("video_adapters")!=null)
                    {
                        foreach(XElement vaEl in el.Element("video_adapters").Elements())
                        {
                            string guidVa = vaEl.Value;
                            VideoAdapter va = VideoAdapters.Where(v => v.Guid== guidVa).FirstOrDefault();
                            comp.VideoAdapters.Add(va);
                        }
                    }

                    if (el.Element("memory_options") != null)
                    {
                        foreach (XElement vaEl in el.Element("memory_options").Elements())
                        {
                            string guidVa = vaEl.Value;
                            Memory va = MemoryOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
                            comp.Memories.Add(va);
                        }
                    }

                    if (el.Element("ssd_options") != null)
                    {
                        foreach (XElement vaEl in el.Element("ssd_options").Elements())
                        {
                            string guidVa = vaEl.Value;
                            SSD va = SSDOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
                            comp.SSDs.Add(va);
                        }
                    }

                    if (el.Element("hdd_options") != null)
                    {
                        foreach (XElement vaEl in el.Element("hdd_options").Elements())
                        {
                            string guidVa = vaEl.Value;
                            HDD va = HDDOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
                            comp.HDDs.Add(va);
                        }
                    }

                    Comps.Add(comp);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
