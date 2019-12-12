using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace AdMakerM
{
    public class Global
    {
        private int v;

        public Global()
        {
            VideoAdapters.CollectionChanged += VideoAdapters_CollectionChanged;
        }

        private void VideoAdapters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveAll();
        }

        public ObservableCollection<VideoAdapter> VideoAdapters { get; set; } = new ObservableCollection<VideoAdapter>();

        internal void SaveAll()
        {
            string profilesFile = @"store.xml";
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
            doc.Save(profilesFile);
        }
    }
}
