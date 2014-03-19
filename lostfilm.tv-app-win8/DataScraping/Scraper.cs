using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using lostfilm.tv_app_win8.Model;
using lostfilm.tv_app_win8.DataFetchers;

namespace lostfilm.tv_app_win8.DataScraping
{
    class Scraper
    {

        public async static Task scrap(string html)
        {
            List<int> indexes = new List<int>();
            Episod temp = new Episod();
            indexes = GetElementIndex(html, "text-decoration:none\">");
            foreach (var value in indexes)
            {
                EpisodsList.currentEpisods.Add(await GetEpisodInfo(html, value));
            }
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

        public static List<int> GetElementIndex(string html, string key)            //return all indexes of "key" in html
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

        public async static Task<Episod> GetEpisodInfo(string html, int EpisodLocation)
        {
            Episod currentEpisod = new Episod();
            currentEpisod.showTitle = GetHtmlString("text-decoration:none\">", "</a></span>", html, EpisodLocation);
            currentEpisod.episodTitle = GetHtmlString("span class=\"torrent_title\"><b>", "</b></span>", html, EpisodLocation);
            currentEpisod.imagePath += GetHtmlString("img src=\"", "\" alt=\"", html, EpisodLocation);
            currentEpisod.detailsPath += GetHtmlString("a href=\"", "\"><img src=", html, EpisodLocation);
            currentEpisod.posterPath += GetHtmlString("img src=\"/Static/icons/cat_", "\" alt=\"", html, EpisodLocation);
            string temp = GetHtmlString("ShowAllReleases", "\"></a>", html, EpisodLocation);
            temp = temp.Substring(7, 8);
            currentEpisod.id += temp;
            EpisodFormat(currentEpisod);
            await Scraper.findDescription(currentEpisod);
            return currentEpisod;
        }

        public static void EpisodFormat(Episod currentEpisod)
        {

            currentEpisod.episodTitle = clearFromHtml(currentEpisod.episodTitle);

            string pattern2 = "'";
            Regex rgx2 = new Regex(pattern2);
            currentEpisod.id = rgx2.Replace(currentEpisod.id, "");


            string pattern3 = ",";
            Regex rgx3 = new Regex(pattern3);
            currentEpisod.id = rgx3.Replace(currentEpisod.id, ", серия  ");
        
        }

        public async static Task findDescription(Episod Selected)
        {
            string responce = await Request.getInfo(Selected.detailsPath);
            Selected.description = Scraper.GetHtmlString("font-weight: bold\">", "<div class=\"content\">", responce, 0);
            Selected.description = Scraper.GetHtmlString("<span>", "</span>", Selected.description, 0);
 
            Selected.description = clearFromHtml(Selected.description);

            string temp = Scraper.GetHtmlString("color:gray", "label", responce, 0);
            temp = Scraper.GetHtmlString("<span><b>", "</b>", responce, 0);
            Selected.rating += temp;
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
