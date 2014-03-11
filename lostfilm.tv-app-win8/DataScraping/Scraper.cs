using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using lostfilm.tv_app_win8.Model;
using lostfilm.tv_app_win8.DataFetchers;

namespace lostfilm.tv_app_win8.DataScraping
{
    class Scraper
    {

        public static ObservableCollection<Episod> scrap(string html)
        {
            List<int> indexes = new List<int>();
            ObservableCollection<Episod> currentEpisods = new ObservableCollection<Episod>();
            indexes = GetElementIndex(html, "text-decoration:none\">");
            foreach (var value in indexes)
            {
                currentEpisods.Add(GetEpisodInfo(html, value));
            }   
            return currentEpisods;
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

        public static Episod GetEpisodInfo(string html, int EpisodLocation)
        {
            Episod currentEpisod = new Episod();
            currentEpisod.showTitle = GetHtmlString("text-decoration:none\">", "</a></span>", html, EpisodLocation);
            currentEpisod.episodTitle = GetHtmlString("span class=\"torrent_title\"><b>", "</b></span>", html, EpisodLocation);
            currentEpisod.imagePath += GetHtmlString("img src=\"", "\" alt=\"", html, EpisodLocation);
            currentEpisod.detailsPath += GetHtmlString("a href=\"", "\"><img src=", html, EpisodLocation);
            currentEpisod.posterPath += GetHtmlString("img src=\"/Static/icons/cat_", "\" alt=\"", html, EpisodLocation);
            currentEpisod.id = GetHtmlString("ShowAllReleases", "\"></a>", html, EpisodLocation);

            EpisodNameFormat(currentEpisod);
            

            return currentEpisod;
        }

        public static void EpisodNameFormat(Episod currentEpisod)
        {
            string pattern = "<wbr>";
            Regex rgx = new Regex(pattern);
            currentEpisod.episodTitle = rgx.Replace(currentEpisod.episodTitle, "");

            string pattern2 = "'";
            Regex rgx2 = new Regex(pattern2);
            currentEpisod.id = rgx.Replace(currentEpisod.id, "");    
        
        }

        public async static Task findDescription(Episod Selected)
        {
            string responce = await Request.getInfo(Selected.detailsPath);
            Selected.description = Scraper.GetHtmlString("font-weight: bold\">", "<div class=\"content\">", responce, 0);
            Selected.description = Scraper.GetHtmlString("<span>", "</span>", Selected.description, 0);

            string pattern = "<br>";
            Regex rgx = new Regex(pattern);
            Selected.description = rgx.Replace(Selected.description, "");

            string temp = Scraper.GetHtmlString("color:gray", "label", responce, 0);
            temp = Scraper.GetHtmlString("<span><b>", "</b>", responce, 0);
            Selected.rating += temp;
        }
    }
}
