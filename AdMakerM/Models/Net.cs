using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Threading;
using System.Net;

namespace AdMakerM
{
    class Net
    {
        async public static Task<string> RespAsync(string request_path)    //на вход подаем URL API
        {
            string response = String.Empty;
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(request_path);   //отправление запроса к серверу API
                    WebResponse resp = await Request.GetResponseAsync();
                    HttpWebResponse Response = (HttpWebResponse)resp;      //получение ответа от сервера API
                    StreamReader myStream = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                    string responseX = myStream.ReadToEnd();
                    Response.Close();

                    return responseX;
                }
                catch (Exception ex)
                {
                    await Task.Delay(500);
                    Console.WriteLine(ex);
                }
            }
            return response;
        }
    }
}
