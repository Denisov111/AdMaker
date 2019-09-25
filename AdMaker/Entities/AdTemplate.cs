using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMaker.Entities
{
    class AdTemplate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagesPath { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
