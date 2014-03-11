using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.UI.Notifications;
using NotificationsExtensions.ToastContent;
using NotificationsExtensions.TileContent;
using Windows.Data.Xml.Dom;
using System.Collections.ObjectModel;
using Windows.System;
using System.Threading;
using System.Threading.Tasks;

using lostfilm.tv_app_win8.DataFetchers;
using lostfilm.tv_app_win8.Logic;
using lostfilm.tv_app_win8.Model;
using lostfilm.tv_app_win8.DataScraping;
//using SDKTemplateCS;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace lostfilm.tv_app_win8
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string currentLastEpisod;

        private ObservableCollection<Episod> currenEpisods;

        Episod Selected = new Episod();

        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00, 0, 10);
            timer.Start();

            Data.EventHandler = new Data.MyEvent(show);
            this.InitializeComponent();          
            StartClass.start("http://www.lostfilm.tv");

        }

        void show(ObservableCollection<Episod> current)
        {
            currenEpisods = current;           
            gvMain.ItemsSource = currenEpisods;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotificationSend();
            TitleUpdate();
        }

         private void Button_Click_1(object sender, RoutedEventArgs e)
         {
             StartClass.start("http://www.lostfilm.tv");
         }

         void timer_Tick(object sender, object e)
         {
             StartClass.start("http://www.lostfilm.tv");

             if (currenEpisods.First().showTitle != currentLastEpisod)
             {
                NotificationSend();
                TitleUpdate();
                currentLastEpisod = currenEpisods.First().showTitle;
             }
         }

         private void NotificationSend()
         {            
                 IToastNotificationContent toastContent = null;
                 IToastImageAndText03 templateContent = ToastContentFactory.CreateToastImageAndText03();
                 templateContent.TextHeadingWrap.Text = currenEpisods.First().showTitle + " - новая серия уже доступна.";
                 templateContent.TextBody.Text = currenEpisods.First().episodTitle;
                 templateContent.Image.Src = currenEpisods.First().imagePath;
                 toastContent = templateContent;
                 ToastNotification toast = toastContent.CreateNotification();
                 ToastNotificationManager.CreateToastNotifier().Show(toast);     
         }

         private void TitleUpdate()
         {
             ITileSquareBlock tileContent = TileContentFactory.CreateTileSquareBlock();
             tileContent.TextBlock.Text = currenEpisods.Last().showTitle;
             tileContent.TextSubBlock.Text = currenEpisods.First().episodTitle; 
             TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
         }


         private async void gvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             /*Episod Selected = (Episod)gvMain.SelectedItem;
             if (Selected != null)
             {
                 Uri url = new Uri(Selected.detailsPath);                 
                 var success = await Launcher.LaunchUriAsync(url);

             }*/
             Selected = (Episod)gvMain.SelectedItem;
             if (Selected != null)
             {
                 descriptionBox.Text = await Scraper.findDescription(Selected);
             }
             


         }

         private async void test(object sender, PointerRoutedEventArgs e)
         {
             /*Episod Selected = (Episod)gvMain.SelectedItem;
             string responce = await Request.getInfo(Selected.detailsPath);               
             Selected.description = Scraper.GetHtmlString("font-weight: bold\">", "<div class=\"content\">", responce, 0);
             Selected.description = Scraper.GetHtmlString("<span>", "</span>", Selected.description, 0);
             descriptionBox.Text = Selected.description;*/
         }

         private async  void Button_Click_2(object sender, RoutedEventArgs e)
         {           
            if (Selected != null)
            {
                Uri url = new Uri(Selected.detailsPath);                 
                var success = await Launcher.LaunchUriAsync(url);

            }
         }

        
        
    }
}
