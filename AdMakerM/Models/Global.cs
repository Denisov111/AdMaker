using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace AdMakerM
{
    public class Global
    {
        public static Random rnd = new Random();
        string storeFile = @"store.xml";

        public ObservableCollection<Computer> Comps { get; set; } = new ObservableCollection<Computer>();

        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<Memory> MemoryOptions { get; set; } = new ObservableCollection<Memory>();
        public ObservableCollection<SSD> SSDOptions { get; set; } = new ObservableCollection<SSD>();
        public ObservableCollection<HDD> HDDOptions { get; set; } = new ObservableCollection<HDD>();
        public ObservableCollection<Processor> ProcessorOptions { get; set; } = new ObservableCollection<Processor>();
        public ObservableCollection<Motherboard> MBOptions { get; set; } = new ObservableCollection<Motherboard>();
        public ObservableCollection<Case> CaseOptions { get; set; } = new ObservableCollection<Case>();
        public ObservableCollection<ProcessorCooler> CPUCoolerOptions { get; set; } = new ObservableCollection<ProcessorCooler>();


        //Для сериализации пустой конструктор
        public Global()
        {
            
        }
        
        public Global(object someObject)
        {
            InitStore();
            VideoAdapters.CollectionChanged += VideoAdapters_CollectionChanged;
            MemoryOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            SSDOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            HDDOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            ProcessorOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            MBOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            CaseOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            CPUCoolerOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            Comps.CollectionChanged+= VideoAdapters_CollectionChanged;
        }

        internal void ShowAds(string guid)
        {
            Computer comp = Comps.Where(c => c.Guid == guid).FirstOrDefault();

            Views.Ads f = new Views.Ads(this, comp);
            f.ShowDialog();
        }

        internal void Edit(string guid)
        {
            Computer comp = Comps.Where(c=>c.Guid==guid).FirstOrDefault();

            AddComp f = new AddComp(this, comp);
            f.ShowDialog();
        }

        public void VideoAdapters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveAll();
        }

        internal void SaveAll()
        {
            
            //XDocument doc = new XDocument();
            //XElement st = new XElement("store");
            //XElement comps = new XElement("comps");
            //XElement video = new XElement("videoadapters");
            //XElement memoryOptions = new XElement("memory_options");
            //XElement ssdOptions = new XElement("ssd_options");
            //XElement hddOptions = new XElement("hdd_options");
            //XElement processorOptions = new XElement("processor_options");
            //XElement imageOptions = new XElement("image_options");

            //doc.Add(st);

            //foreach (Computer comp in Comps)
            //{
            //    //
            //    string objString = Helpers.SerializeObject<Computer>(comp);
            //    XElement Computer = XDocument.Parse(objString).Element("Computer");





            //    XElement compEl = new XElement("comp",
            //                    new XElement("title", comp.Title),
            //                    new XElement("desc", comp.Description),
            //                    new XElement("price", comp.Price),
            //                    new XElement("guid", comp.Guid));

            //    if(comp.VideoAdapters!=null)
            //    {
            //        XElement videoEl = new XElement("video_adapters");
            //        foreach(VideoAdapter va in comp.VideoAdapters)
            //        {
            //            string guid = va.Guid;
            //            videoEl.Add(new XElement("guid",guid));
            //        }
            //        compEl.Add(videoEl);
            //    }

            //    if (comp.Memories != null)
            //    {
            //        XElement memoryEl = new XElement("memory_options");
            //        foreach (Memory mem in comp.Memories)
            //        {
            //            string guid = mem.Guid;
            //            memoryEl.Add(new XElement("guid", guid));
            //        }
            //        compEl.Add(memoryEl);
            //    }

            //    if (comp.SSDs != null)
            //    {
            //        XElement ssdEl = new XElement("ssd_options");
            //        foreach (SSD ssd in comp.SSDs)
            //        {
            //            string guid = ssd.Guid;
            //            ssdEl.Add(new XElement("guid", guid));
            //        }
            //        compEl.Add(ssdEl);
            //    }

            //    if (comp.HDDs != null)
            //    {
            //        XElement hddEl = new XElement("hdd_options");
            //        foreach (HDD hdd in comp.HDDs)
            //        {
            //            string guid = hdd.Guid;
            //            hddEl.Add(new XElement("guid", guid));
            //        }
            //        compEl.Add(hddEl);
            //    }

            //    if (comp.Processors != null)
            //    {
            //        XElement processorEl = new XElement("processor_options");
            //        foreach (Processor processor in comp.Processors)
            //        {
            //            string guid = processor.Guid;
            //            processorEl.Add(new XElement("guid", guid));
            //        }
            //        compEl.Add(processorEl);
            //    }

            //    if (comp.ImagesPath != null)
            //    {
            //        XElement imagesEl = new XElement("image_options");
            //        foreach (AdImage image in comp.ImagesPath)
            //        {
            //            string path = image.Path;
            //            imagesEl.Add(new XElement("path", path));
            //        }
            //        compEl.Add(imagesEl);
            //    }

            //    if (comp.Ads != null)
            //    {
            //        XElement adsEl = new XElement("ads");
            //        foreach (Ad ad in comp.Ads)
            //        {
            //            string articul = ad.Articul.ToString();
            //            string description = ad.Description;
            //            string imgFileName = ad.ImgFileName;
            //            string isPostedOnAu = ad.IsPostedOnAu.ToString();
            //            string isPostedOnAvito = ad.IsPostedOnAvito.ToString();
            //            string isPostedOnUla = ad.IsPostedOnUla.ToString();
            //            string linkOnAu = ad.LinkOnAu;
            //            string linkOnAvito = ad.LinkOnAvito;
            //            string linkOnUla = ad.LinkOnUla;
            //            string price = ad.Price.ToString();
            //            string title = ad.Title;

            //            adsEl.Add(new XElement("ad",
            //                new XElement("articul", articul),
            //                new XElement("title", title),
            //                new XElement("description", description),
            //                new XElement("price", price),
            //                new XElement("img_file_name", imgFileName),
            //                new XElement("is_posted_on_au", isPostedOnAu),
            //                new XElement("is_posted_on_avito", isPostedOnAvito),
            //                new XElement("is_posted_on_ula", isPostedOnUla),
            //                new XElement("link_on_au", linkOnAu),
            //                new XElement("link_on_avito", linkOnAvito),
            //                new XElement("link_on_ula", linkOnUla))); ;
            //        }
            //        compEl.Add(adsEl);
            //    }

            //    comps.Add(compEl);
            //    comps.Add(Computer);

            //}
            //doc.Root.Add(comps);


            //string globalObjString = Helpers.SerializeObject<Global>(this);
            //XDocument Global = XDocument.Parse(globalObjString);


            //foreach (VideoAdapter va in VideoAdapters)
            //{
            //    XElement vidAd = new XElement("videoadapter",
            //                    new XElement("title", va.Title),
            //                    new XElement("memory", va.Memory),
            //                    new XElement("tdp", va.TDP),
            //                    new XElement("price", va.Price),
            //                    new XElement("guid", va.Guid));
            //    video.Add(vidAd);
                
            //}
            //doc.Root.Add(video);

            //foreach (Memory mo in MemoryOptions)
            //{
            //    XElement memoryOptEl = new XElement("memory_options",
            //                    new XElement("title", mo.Title),
            //                    new XElement("volume", mo.Volume),
            //                    new XElement("memory_type", mo.MemoryType),
            //                    new XElement("price", mo.Price),
            //                    new XElement("guid", mo.Guid));
            //    memoryOptions.Add(memoryOptEl);

            //}
            //doc.Root.Add(memoryOptions);

            //foreach (SSD ssd in SSDOptions)
            //{
            //    XElement ssdOptEl = new XElement("ssd_options",
            //                    new XElement("title", ssd.Title),
            //                    new XElement("volume", ssd.Volume),
            //                    new XElement("price", ssd.Price),
            //                    new XElement("guid", ssd.Guid));
            //    ssdOptions.Add(ssdOptEl);

            //}
            //doc.Root.Add(ssdOptions);

            //foreach (HDD hdd in HDDOptions)
            //{
            //    XElement hddOptEl = new XElement("hdd_options",
            //                    new XElement("title", hdd.Title),
            //                    new XElement("volume", hdd.Volume),
            //                    new XElement("price", hdd.Price),
            //                    new XElement("guid", hdd.Guid));
            //    hddOptions.Add(hddOptEl);

            //}
            //doc.Root.Add(hddOptions);

            //foreach (Processor processor in ProcessorOptions)
            //{
            //    XElement processorOptEl = new XElement("processor_options",
            //                    new XElement("title", processor.Title),
            //                    new XElement("socket", processor.Socket.ToString()),
            //                    new XElement("price", processor.Price),
            //                    new XElement("guid", processor.Guid));
            //    processorOptions.Add(processorOptEl);

            //}
            //doc.Root.Add(processorOptions);

            //doc.Save(storeFile);


            /////////////////////////////////
            //XmlSerializer formatterComps = new XmlSerializer(typeof(ObservableCollection<Computer>));
            //using (FileStream fs = new FileStream("comps.xml", FileMode.OpenOrCreate))
            //{
            //    formatterComps.Serialize(fs, Comps);
            //}

            //XmlSerializer formatterVideos = new XmlSerializer(typeof(ObservableCollection<VideoAdapter>));
            //using (FileStream fs = new FileStream("videos.xml", FileMode.OpenOrCreate))
            //{
            //    formatterVideos.Serialize(fs, VideoAdapters);
            //}

            //XmlSerializer formatterMem = new XmlSerializer(typeof(ObservableCollection<Memory>));
            //using (FileStream fs = new FileStream("mems.xml", FileMode.OpenOrCreate))
            //{
            //    formatterMem.Serialize(fs, MemoryOptions);
            //}

            //XmlSerializer formatterSSD = new XmlSerializer(typeof(ObservableCollection<SSD>));
            //using (FileStream fs = new FileStream("ssds.xml", FileMode.OpenOrCreate))
            //{
            //    formatterSSD.Serialize(fs, SSDOptions);
            //}
            //XmlSerializer formatterHDD = new XmlSerializer(typeof(ObservableCollection<HDD>));
            //using (FileStream fs = new FileStream("hdds.xml", FileMode.OpenOrCreate))
            //{
            //    formatterHDD.Serialize(fs, HDDOptions);
            //}

            //XmlSerializer formatterProcs = new XmlSerializer(typeof(ObservableCollection<Processor>));
            //using (FileStream fs = new FileStream("procs.xml", FileMode.OpenOrCreate))
            //{
            //    formatterProcs.Serialize(fs, ProcessorOptions);
            //}

            XmlSerializer formatterGlobal = new XmlSerializer(typeof(Global));
            using (FileStream fs = new FileStream("global.xml", FileMode.Create))
            {
                formatterGlobal.Serialize(fs, this);
            }


            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //if(!Directory.Exists("backup"))
            //{
            //    Directory.CreateDirectory("backup");
            //}
            //string backupFile = "backup/" + unixTimestamp + "_backup";
            //doc.Save(backupFile);
        }

        private void InitStore()
        {
            XmlSerializer formatterGlobal = new XmlSerializer(typeof(Global));
            using (FileStream fs = new FileStream("global.xml", FileMode.OpenOrCreate))
            {
                var gl = (Global)formatterGlobal.Deserialize(fs);
                Comps = gl.Comps;
                VideoAdapters = gl.VideoAdapters;
                MemoryOptions = gl.MemoryOptions;
                SSDOptions = gl.SSDOptions;
                HDDOptions = gl.HDDOptions;
                ProcessorOptions = gl.ProcessorOptions;
            }

            //XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Computer>));
            //using (FileStream fs = new FileStream("comps.xml", FileMode.OpenOrCreate))
            //{
            //    Comps = (ObservableCollection<Computer>)formatter.Deserialize(fs);
            //}

            //XmlSerializer formatterVideos = new XmlSerializer(typeof(ObservableCollection<VideoAdapter>));
            //using (FileStream fs = new FileStream("videos.xml", FileMode.OpenOrCreate))
            //{
            //    VideoAdapters = (ObservableCollection<VideoAdapter>)formatterVideos.Deserialize(fs);
            //}

            //XmlSerializer formatterMem = new XmlSerializer(typeof(ObservableCollection<Memory>));
            //using (FileStream fs = new FileStream("mems.xml", FileMode.OpenOrCreate))
            //{
            //    MemoryOptions = (ObservableCollection<Memory>)formatterMem.Deserialize(fs);
            //}

            //XmlSerializer formatterSSD = new XmlSerializer(typeof(ObservableCollection<SSD>));
            //using (FileStream fs = new FileStream("ssds.xml", FileMode.OpenOrCreate))
            //{
            //    SSDOptions = (ObservableCollection<SSD>)formatterSSD.Deserialize(fs);
            //}

            //XmlSerializer formatterHDD = new XmlSerializer(typeof(ObservableCollection<HDD>));
            //using (FileStream fs = new FileStream("hdds.xml", FileMode.OpenOrCreate))
            //{
            //    HDDOptions = (ObservableCollection<HDD>)formatterHDD.Deserialize(fs);
            //}

            //XmlSerializer formatterProcs = new XmlSerializer(typeof(ObservableCollection<Processor>));
            //using (FileStream fs = new FileStream("procs.xml", FileMode.OpenOrCreate))
            //{
            //    ProcessorOptions = (ObservableCollection<Processor>)formatterProcs.Deserialize(fs);
            //}


            //XDocument doc = new XDocument();
            //if (!File.Exists(storeFile)) return;

            //try
            //{
            //    doc = XDocument.Load(storeFile);



            //    foreach (XElement el in doc.Root.Element("videoadapters").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        int memory = Int32.Parse(el.Element("memory").Value);
            //        int tdp = Int32.Parse(el.Element("tdp").Value);
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        VideoAdapter va = new VideoAdapter()
            //        {
            //            Title = title,
            //            Memory = memory,
            //            TDP = tdp,
            //            Price = price,
            //            Guid = guid
            //        };
            //        VideoAdapters.Add(va);
            //    }

            //    foreach (XElement el in doc.Root.Element("memory_options").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        int volume = Int32.Parse(el.Element("volume").Value);
            //        string memoryType = el.Element("memory_type").Value;
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        MemoryType mt = 0;

            //        if (memoryType == MemoryType.DDR2.ToString())
            //            mt = MemoryType.DDR2;
            //        if (memoryType == MemoryType.DDR3.ToString())
            //            mt = MemoryType.DDR3;
            //        if (memoryType == MemoryType.DDR4.ToString())
            //            mt = MemoryType.DDR4;

            //        Memory memory = new Memory()
            //        {
            //            Title = title,
            //            Volume = volume,
            //            MemoryType = mt,
            //            Price = price,
            //            Guid = guid
            //        };
            //        MemoryOptions.Add(memory);
            //    }

            //    foreach (XElement el in doc.Root.Element("ssd_options").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        int volume = Int32.Parse(el.Element("volume").Value);
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        SSD ssd = new SSD()
            //        {
            //            Title = title,
            //            Volume = volume,
            //            Price = price,
            //            Guid = guid
            //        };
            //        SSDOptions.Add(ssd);
            //    }

            //    foreach (XElement el in doc.Root.Element("hdd_options").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        int volume = Int32.Parse(el.Element("volume").Value);
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        HDD hdd = new HDD()
            //        {
            //            Title = title,
            //            Volume = volume,
            //            Price = price,
            //            Guid = guid
            //        };
            //        HDDOptions.Add(hdd);
            //    }

            //    foreach (XElement el in doc.Root.Element("processor_options").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        string socket = el.Element("socket").Value;
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        ProcessorSocket ps = (ProcessorSocket)Enum.Parse(typeof(ProcessorSocket), socket);

            //        Processor pr = new Processor()
            //        {
            //            Title = title,
            //            Socket = ps,
            //            Price = price,
            //            Guid = guid
            //        };
            //        ProcessorOptions.Add(pr);
            //    }

            //    //объекты компьютеров собираем в последнюю очередь,
            //    //чтобы опции сборки были уже проинициализированы
            //    foreach (XElement el in doc.Root.Element("comps").Elements())
            //    {
            //        string title = el.Element("title").Value;
            //        string desc = el.Element("desc").Value;
            //        decimal price = (decimal)Double.Parse(el.Element("price").Value);
            //        string guid = (el.Element("guid") == null) ? "" : el.Element("guid").Value;

            //        Computer comp = new Computer()
            //        {
            //            Title = title,
            //            Description = desc,
            //            Price = price,
            //            Guid = guid
            //        };

            //        if (el.Element("video_adapters") != null)
            //        {
            //            foreach (XElement vaEl in el.Element("video_adapters").Elements())
            //            {
            //                string guidVa = vaEl.Value;
            //                VideoAdapter va = VideoAdapters.Where(v => v.Guid == guidVa).FirstOrDefault();
            //                if (va != null) comp.VideoAdapters.Add(va);
            //            }
            //        }

            //        if (el.Element("memory_options") != null)
            //        {
            //            foreach (XElement vaEl in el.Element("memory_options").Elements())
            //            {
            //                string guidVa = vaEl.Value;
            //                Memory va = MemoryOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
            //                if (va != null) comp.Memories.Add(va);
            //            }
            //        }

            //        if (el.Element("ssd_options") != null)
            //        {
            //            foreach (XElement vaEl in el.Element("ssd_options").Elements())
            //            {
            //                string guidVa = vaEl.Value;
            //                SSD va = SSDOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
            //                if (va != null) comp.SSDs.Add(va);
            //            }
            //        }

            //        if (el.Element("hdd_options") != null)
            //        {
            //            foreach (XElement vaEl in el.Element("hdd_options").Elements())
            //            {
            //                string guidVa = vaEl.Value;
            //                HDD va = HDDOptions.Where(v => v.Guid == guidVa).FirstOrDefault();
            //                if (va != null) comp.HDDs.Add(va);
            //            }
            //        }

            //        if (el.Element("processor_options") != null)
            //        {
            //            foreach (XElement processorEl in el.Element("processor_options").Elements())
            //            {
            //                string guidPr = processorEl.Value;
            //                Processor pr = ProcessorOptions.Where(v => v.Guid == guidPr).FirstOrDefault();
            //                if (pr != null) comp.Processors.Add(pr);
            //            }
            //        }

            //        if (el.Element("image_options") != null)
            //        {
            //            foreach (XElement imageEl in el.Element("image_options").Elements())
            //            {
            //                string path = imageEl.Value;
            //                AdImage image = new AdImage() { Path = path };
            //                comp.ImagesPath.Add(image);
            //            }
            //        }

            //        if (el.Element("ads") != null)
            //        {
            //            foreach (XElement adEl in el.Element("ads").Elements())
            //            {
            //                int articul = Int32.Parse(adEl.Element("articul").Value);
            //                string description = adEl.Element("description").Value;
            //                string imgFileName = adEl.Element("img_file_name").Value;
            //                bool isPostedOnAu = bool.Parse(adEl.Element("is_posted_on_au").Value);
            //                bool isPostedOnAvito = bool.Parse(adEl.Element("is_posted_on_avito").Value);
            //                bool isPostedOnUla = bool.Parse(adEl.Element("is_posted_on_ula").Value);
            //                string linkOnAu = adEl.Element("link_on_au").Value;
            //                string linkOnAvito = adEl.Element("link_on_avito").Value;
            //                string linkOnUla = adEl.Element("link_on_ula").Value;
            //                decimal price_ = Decimal.Parse(adEl.Element("price").Value);
            //                string title_ = adEl.Element("title").Value;
            //                //string path = imageEl.Value;
            //                //AdImage image = new AdImage() { Path = path };
            //                //comp.ImagesPath.Add(image);

            //                Ad ad = new Ad()
            //                {
            //                    Articul = articul,
            //                    Description = description,
            //                    ImgFileName = imgFileName,
            //                    IsPostedOnAu = isPostedOnAu,
            //                    IsPostedOnAvito = isPostedOnAvito,
            //                    IsPostedOnUla = isPostedOnUla,
            //                    LinkOnAu = linkOnAu,
            //                    LinkOnAvito = linkOnAvito,
            //                    LinkOnUla = linkOnUla,
            //                    Price = price_,
            //                    Title = title_

            //                };

            //                comp.Ads.Add(ad);
            //            }
            //        }

            //        Comps.Add(comp);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }
    }
}
