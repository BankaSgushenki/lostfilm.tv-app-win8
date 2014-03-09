using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using System.Threading.Tasks;

namespace lostfilm.tv_app_win8.DataFetchers
{
    class Request
    {
        static String Response = "It's still empty";

        public static async Task<string> getInfo(string URL)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            Response = await response.Content.ReadAsStringAsync();
            return Response;
        }
    }
}
