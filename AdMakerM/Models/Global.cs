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
        public ObservableCollection<PowerSupply> PowerOptions { get; set; } = new ObservableCollection<PowerSupply>();

        


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
            PowerOptions.CollectionChanged += VideoAdapters_CollectionChanged;
            Comps.CollectionChanged+= VideoAdapters_CollectionChanged;
        }

        internal void FindArt(int art)
        {
            foreach (Computer comp in Comps)
            {
                Ad ad = comp.Ads.Where(a=>a.Articul == art).FirstOrDefault();
                if(ad!=null)
                {
                    Views.AdCard f = new Views.AdCard(ad, this);
                    try
                    {
                        f.ShowDialog();
                    }
                    catch { }
                }
            }
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
            XmlSerializer formatterGlobal = new XmlSerializer(typeof(Global));
            using (FileStream fs = new FileStream("global.xml", FileMode.Create))
            {
                formatterGlobal.Serialize(fs, this);
            }
        }

        private void InitStore()
        {
            if(File.Exists("global.xml"))
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
                    MBOptions = gl.MBOptions;
                    CaseOptions = gl.CaseOptions;
                    CPUCoolerOptions = gl.CPUCoolerOptions;
                    PowerOptions = gl.PowerOptions;
                }
            }
        }
    }
}
