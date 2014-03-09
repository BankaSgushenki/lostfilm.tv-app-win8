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
using Windows.Data.Xml.Dom;
using System.Collections.ObjectModel;
using Windows.System;

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

        public MainPage()
        {
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
        }

         private void Button_Click_1(object sender, RoutedEventArgs e)
         {
             StartClass.start("http://www.lostfilm.tv");
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

         private async void gvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             Episod Selected = (Episod)gvMain.SelectedItem;
             Uri url = new Uri(Selected.detailsPath);
             var success = await Launcher.LaunchUriAsync(url);
         }

        
    }
}
