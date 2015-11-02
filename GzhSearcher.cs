using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WeiXinSogou
{
    class GzhSearcher
    {
        public string SearchServerUrl = "http://weixin.sogou.com/weixin?type=1&query={0}&ie=utf8";
        public List<Gzh> Search(string sterm)
        {
            string surl = string.Format(SearchServerUrl, sterm);
            HtmlWeb htmlweb = new HtmlWeb();
            HtmlDocument htmldoc = htmlweb.Load(surl);

            HtmlNodeCollection gzhNodeCol = htmldoc.DocumentNode.SelectNodes(".//div[@class='wx-rb bg-blue wx-rb_v1 _item']");

            List<Gzh> gzhCol = new List<Gzh>();
            foreach (HtmlNode gzhNode in gzhNodeCol)
            {
                Gzh gzh=new Gzh();
                gzh.GzhUrl = gzhNode.GetAttributeValue("href", "").Replace("&amp;", "&");
                gzh.GzhName= gzhNode.SelectSingleNode("./div/h3").InnerText;
                gzh.WxhName= gzhNode.SelectSingleNode("./div/h4").InnerText;
                gzhCol.Add(gzh);
            }

            return gzhCol;
        }
    }

    class Gzh
    {
        public string GzhUrl { get; set; }
        public string GzhName { get; set; }
        public string WxhName { get; set; }

        public string Descrip { get; set; }
        public string AuthCorp { get; set; }
        public string ImageFileName { get; set; }
    }
}
