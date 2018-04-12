using API;
using DMBT.Models;
using Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace API
{
    /// <summary>
    /// BT蚂蚁
    /// </summary>
    public class BTMY
    {
        public  void Serach(int mode,string key,Action<List<BT>,List<Page>, string> action)
        {
            string url = "";
            switch (mode)
            {
                case 1:
                    url = "http://www.btanm.com/q?kw=" + key;
                    break;
                case 2:
                    url = "http://www.btanm.com/search/" + key;
                    break;
                default:
                    url = "http://www.btanm.com/q?kw=" + key;
                    break;
            }
            Thread thread = new Thread(new ThreadStart(() =>
            {
                #region 调用网络
                try
                {
                    using (HttpWebResponse response = HTTP.CreateGetHttpResponse(url))
                    {
                        GZipStream g = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                        System.IO.StreamReader r = new System.IO.StreamReader(g, Encoding.UTF8);
                        string retString = r.ReadToEnd();
                        //后开先关
                        r.Close();
                        r.Dispose();
                        g.Close();
                        g.Dispose();

                        //对返回网页进行处理
                        List<BT> list = new List<BT>();
                        List<Page> pagelist = new List<Page>();
                        //解析可以用 正则，我就谢谢substring 简单一些
                        // 定义正则表达式用来匹配 标签 
                        var matches = Regex.Matches(retString, "<div class[\\s\\S]*?迅雷链接</a>");//取出每个<tr>
                        foreach (Match mc in matches)
                        {
                            string allText = mc.Groups[0].Value;
                            //截取名字
                            var namematches = Regex.Matches(allText, "target=\"_blank\">[\\s\\S]*?</a>");//取出每个<tr>
                            string name = namematches[0].Value.Replace("target=\"_blank\">", "")
                            .Replace("<span class=\"highlight\">", "").Replace("</span>", "").Replace("</a>", "")
                            .Replace("span", "");//</span>


                            //截取热度
                            //<b class=\"cpill yellow-pill\">8.47 GB</b>
                            var pointmatches = Regex.Matches(allText, "<b class=\"cpill yellow-pill\">[\\s\\S]*?</b>");//取出每个<tr>
                            string point = pointmatches[0].Value.Replace("<b class=\"cpill yellow-pill\">", "")
                            .Replace("</b>", "");

                            //截取大小
                            //<b class=\"cpill yellow-pill\">8.47 GB</b>
                            var sizematches = Regex.Matches(allText, "<span>[\\s\\S]*?</span>");//取出每个<tr>
                            string size = sizematches[0].Value.Replace("<span>", "")
                            .Replace("</span>", "");

                            //时间<span>收录时间
                            //<b class=\"cpill yellow-pill\">8.47 GB</b>
                            var timematches = Regex.Matches(allText, "<span>收录时间[\\s\\S]*?</span>");//取出每个<tr>
                            string time = timematches[0].Value.Replace("<span>收录时间： <b class=\"cpill blue-pill\">", "")
                            .Replace("</b> </span>", "");


                            //解析迅雷
                            var amatches = Regex.Matches(allText, "<a[\\s\\S]*?</a>");//取出每个<tr>
                            string xl = "";
                            string cl = "";
                            foreach (Match item in amatches)
                            {
                                Console.WriteLine(item.Value);
                                if (item.Value.Contains("thunder"))//迅雷
                                {
                                    xl = item.Value.Replace("<a href=\"", "").Replace("\"\n          class=\"download\">迅雷链接</a>", "").Split('\\')[0];
                                }
                                if (item.Value.Contains("magnet"))//迅雷
                                {
                                    cl = item.Value.Replace("<a href=\"", "").Replace("\"\n          class=\"download\">磁力链接</a>", "").Split('\\')[0];
                                }
                            }
                           

                            //解析磁力

                            list.Add(new BT() {Name = name,Point = point,Size = size,Time = time ,Type = BTType.BT蚂蚁,Xunlei = xl,Magnet = cl});
                           
                        }

                        //分页
                        //<div class=\"bottom-pager\"> </div>
                        var pagematches = Regex.Matches(retString, "<div class=\"bottom-pager\">[\\s\\S]*?</div>");//取出每个<tr>
                        foreach (Match mc in pagematches)
                        {
                            string allText = mc.Groups[0].Value;


                            var namematches = Regex.Matches(allText, "<a href=[\\s\\S]*?</a>");//取出每个<tr>
                            foreach (Match item in namematches)
                            {
                                string txt = item.Value.Replace("<a href=\"/search/", "").Replace("</a>", "").Replace("\">", "$");//</span>
                                string name = txt.Split('$')[1];
                                string path = txt.Split('$')[0];
                                pagelist.Add(new Page() { Name = name, Path = path, Type = BTType.BT蚂蚁 });
                            }
                            //pagelist.ins
                        }
                        action(list, pagelist,"");
                    }
                }
                catch (Exception ex)//全局错误-网络错误 操作错误
                {
                    action(null,null,ex.Message);
                }
                #endregion
            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
