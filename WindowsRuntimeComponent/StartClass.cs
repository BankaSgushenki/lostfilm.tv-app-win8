using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace WindowsRuntimeComponent
{
    static class StartClass
    {
        public static async Task start(string URL)
        {               
            string responce = await Request.getInfo(URL);
            await Scraper.scrap(responce);
            return;           
        }

    }

}
