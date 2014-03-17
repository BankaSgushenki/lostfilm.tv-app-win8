
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
            rating = "Рейтинг: ";
        }
        public string showTitle { get; set; }
        public string episodTitle { get; set; }
        public string imagePath { get; set; }
        public string detailsPath { get; set; }
        public string posterPath { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string rating { get; set; }

        public static bool Equals(Episod a, Episod b)
        {
            if (b == null)
                return false;
            if (a.episodTitle == b.episodTitle)
                return true;
            else
                return false;


        }
    }
}
