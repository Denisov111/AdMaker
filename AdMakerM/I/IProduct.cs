using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    interface IProduct
    {
        string Title { get; set; }
        string Descriptrion { get; set; }
        decimal Price { get; set; }
        string ImgFileName { get; set; }
        string ImgFileDir { get; set; }
        string ImgFilePath { get; set; }
    }
}
