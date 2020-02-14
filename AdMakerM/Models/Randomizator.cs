using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdMakerM
{
    public class Randomizator
    {
        public static String ParseSpintax(String s)
        {
            if (s.Contains('{') && s.Contains('}') && s.IndexOf('{') < s.IndexOf('}'))
            {
                int lastOpenPosition = s.LastIndexOf('{');
                string halfOfBlock = s.Substring(lastOpenPosition, s.Length - lastOpenPosition);

                int firstClosePosition = halfOfBlock.IndexOf('}');
                string block = halfOfBlock.Substring(0, firstClosePosition + 1);

                String[] items = block.Substring(1, block.Length - 2).Split('|');
                s = s.Replace(block, items[Global.rnd.Next(items.Length)]);
                return ParseSpintax(s);
            }
            else { return s; }
        }
    }
}
