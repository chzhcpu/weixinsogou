using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;

namespace WeiXinSogou
{
    //dapper document:https://github.com/StackExchange/dapper-dot-net

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

    public class ArticleSpider
    {
        public string GzhUrl { get; set; }
        private string server = "http://weixin.sogou.com";
        public void GetArticles()
        {
            if (string.IsNullOrEmpty(GzhUrl))
            {
                return;
            }
            GzhUrl = "/gzh?openid=oIWsFtwbhOsda2Ukh1ZcZrbEYmz4&ext=zMKRwhGG3zZRzwC_yqG7u3efGlgYB4_dSpvv3QvE_Z0vJooALwvbKkrm0-UsBgcX";
            GzhUrl = server+GzhUrl + "&cb=sogou.weixin.gzhcb"+"&page=1";
            GzhUrl = "http://weixin.sogou.com/gzhjs?cb=sogou.weixin.gzhcb&openid=oIWsFtwbhOsda2Ukh1ZcZrbEYmz4&ext=zMKRwhGG3zZRzwC_yqG7u3efGlgYB4_dSpvv3QvE_Z0vJooALwvbKkrm0-UsBgcX&gzhArtKeyWord=&page=2&t=1446473734842";
            WebClient webclient = new WebClient();
            string htmldoc = webclient.DownloadString(GzhUrl);

            dynamic root= JsonConvert.DeserializeObject(htmldoc);
            int totalItems = root.totalItems;
            int totalPages = root.totalPages;
            string xmlitems = root.items;

        }
    }

    class Article
    {
        public string WxhName { get; set; }
        public string Title { get; set; }
        public string DetailUrl { get; set; }
        public string Abstract { get; set; }
        public string PubDate { get; set; }
    }
}
