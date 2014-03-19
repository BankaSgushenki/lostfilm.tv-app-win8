using System.Collections.ObjectModel;

using lostfilm.tv_app_win8.DataFetchers;
using lostfilm.tv_app_win8.Model;
using lostfilm.tv_app_win8.DataScraping;



namespace lostfilm.tv_app_win8.Logic
{
    static class StartClass
    {
        public static async void start(string URL)
        {
            string responce = await Request.getInfo(URL);
            await Scraper.scrap(responce);
            Data.EventHandler();             
        }

    }

    static class Data                   
    {
        public delegate void MyEvent();
        public static MyEvent EventHandler;
    }
}
