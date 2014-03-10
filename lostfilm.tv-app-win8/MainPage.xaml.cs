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
//using SDKTemplateCS;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace lostfilm.tv_app_win8
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Episod> currenEpisods;

        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00, 0, 100);
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
             NotificationSend();
         }

         private void NotificationSend()
         {
             IToastNotificationContent toastContent = null;
             IToastImageAndText03 templateContent = ToastContentFactory.CreateToastImageAndText03();
             templateContent.TextHeadingWrap.Text = currenEpisods.First().showTitle + " (new episod on lostfilm.tv)";
             templateContent.TextBody.Text = currenEpisods.First().episodTitle;
             templateContent.Image.Src = currenEpisods.First().imagePath;
             toastContent = templateContent;
             ToastNotification toast = toastContent.CreateNotification();
             ToastNotificationManager.CreateToastNotifier().Show(toast);          
         }

         private void TitleUpdate()
         {
             /*var xmlDocument = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideBlockAndText01);
             var nodes = xmlDocument.GetElementsByTagName("binding").First().ChildNodes;

             nodes[0].InnerText = currenEpisods.Last().showTitle;
             nodes[1].InnerText = currenEpisods.First().episodTitle; 

             var notification = new TileNotification(xmlDocument);
             var updateManager = TileUpdateManager.CreateTileUpdaterForApplication(); 
             updateManager.Update(notification); */

             ITileWideBlockAndText01 tileContent = TileContentFactory.CreateTileWideBlockAndText01();
             tileContent.TextBody1.Text = currenEpisods.Last().showTitle; ;
             tileContent.TextBody2.Text = currenEpisods.First().episodTitle; 
             //TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
         }


         private async void gvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             Episod Selected = (Episod)gvMain.SelectedItem;
             Uri url = new Uri(Selected.detailsPath);
             var success = await Launcher.LaunchUriAsync(url);
         }
        
        
    }
}
