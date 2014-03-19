using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WindowsRuntimeComponent
{
    class Scraper
    {

        public async static Task scrap(string html)
        {           
            List<int> indexes = new List<int>();
            EpisodsList.currentEpisod = GetEpisodInfo(html, 0);
            return;

        }

        public static string GetHtmlString(string leftBorder, string rightBorder, string html, int location)  //return substring, which is between "leftBorder" and "rightBorder"
        {
            string subString;
            int begin = html.IndexOf(leftBorder, location) + leftBorder.Length;
            int end = html.IndexOf(rightBorder, begin);
            subString = html.Substring(begin, end - begin);
            return subString;
        }

        public static Episod GetEpisodInfo(string html, int EpisodLocation)
        {
            Episod currentEpisod = new Episod();
            currentEpisod.showTitle = GetHtmlString("text-decoration:none\">", "</a></span>", html, EpisodLocation);
            currentEpisod.episodTitle = GetHtmlString("span class=\"torrent_title\"><b>", "</b></span>", html, EpisodLocation);
            currentEpisod.imagePath += GetHtmlString("img src=\"", "\" alt=\"", html, EpisodLocation);
            return currentEpisod;
        }

        public static void EpisodFormat(Episod currentEpisod)
        {
            string pattern2 = "'";
            Regex rgx2 = new Regex(pattern2);
            currentEpisod.id = rgx2.Replace(currentEpisod.id, "");


            string pattern3 = ",";
            Regex rgx3 = new Regex(pattern3);
            currentEpisod.id = rgx3.Replace(currentEpisod.id, ", серия  ");
        
        }          
    }
}
