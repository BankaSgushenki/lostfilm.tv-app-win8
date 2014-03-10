using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lostfilm.tv_app_win8.Model
{
    class Episod
    {
        public Episod()
        {
            showTitle = "Empty";
            imagePath = "http://www.lostfilm.tv";
            detailsPath = "http://www.lostfilm.tv/";
            posterPath = "http://www.lostfilm.tv/Static/posters/poster_";
        }
        public string showTitle { get; set; }
        public string episodTitle { get; set; }
        public string imagePath { get; set; }
        public string detailsPath { get; set; }
        public string posterPath { get; set; }
        public string id { get; set; }
        public string description { get; set; }
    }
}
