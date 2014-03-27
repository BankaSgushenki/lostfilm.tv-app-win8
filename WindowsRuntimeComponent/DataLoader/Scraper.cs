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
            indexes = GetElementIndex(html, "text-decoration:none\">");
            GetEpisodeInfo(html, indexes.First());
            return;

        }

        public static string GetHtmlString(string leftBorder, string rightBorder, string html, int location)
        {
            string subString;
            int begin = html.IndexOf(leftBorder, location) + leftBorder.Length;
            int end = html.IndexOf(rightBorder, begin);
            subString = html.Substring(begin, end - begin);
            return subString;
        }

        public static void GetEpisodeInfo(string html, int EpisodeLocation)
        {
            EpisodsList.currentEpisod.showTitle = GetHtmlString("text-decoration:none\">", "</a></span>", html, EpisodeLocation);
            EpisodsList.currentEpisod.episodTitle = GetHtmlString("span class=\"torrent_title\"><b>", "</b></span>", html, EpisodeLocation);
            EpisodsList.currentEpisod.imagePath += GetHtmlString("img src=\"", "\" alt=\"", html, EpisodeLocation);
            EpisodsList.currentEpisod.episodTitle = clearFromHtml(EpisodsList.currentEpisod.episodTitle);
        }

        public static void EpisodFormat(Episod currentEpisode)
        {
            string pattern2 = "'";
            Regex rgx2 = new Regex(pattern2);
            currentEpisode.id = rgx2.Replace(currentEpisode.id, "");


            string pattern3 = ",";
            Regex rgx3 = new Regex(pattern3);
            currentEpisode.id = rgx3.Replace(currentEpisode.id, ", серия  ");
        
        }

        public static List<int> GetElementIndex(string html, string key)      
        {
            int temp;
            int currentIndex = 1;
            List<int> index = new List<int>();

            while (true)
            {
                temp = html.IndexOf(key, currentIndex);
                if (temp > 0)
                {
                    index.Add(temp);
                    currentIndex = index.Last();
                    currentIndex++;
                }
                else
                    break;
            }
            return index;
        }

        public static string clearFromHtml(string data)
        {
            int openTagIndex = 0;
            int closeTagIndex = 0;

            while (true)
            {
                openTagIndex = data.IndexOf('<', closeTagIndex);
                if (openTagIndex == -1)
                    break;
                closeTagIndex = data.IndexOf('>', openTagIndex);
                data = data.Remove(openTagIndex, closeTagIndex - openTagIndex + 1);
                closeTagIndex = openTagIndex;
            }
            return data;
        }
    }
}
