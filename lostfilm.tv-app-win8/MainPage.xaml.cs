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
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;

using lostfilm.tv_app_win8.DataFetchers;
using lostfilm.tv_app_win8.Logic;
using lostfilm.tv_app_win8.Model;
using lostfilm.tv_app_win8.DataScraping;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace lostfilm.tv_app_win8
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Episod currentFirstEpisod;

        private ObservableCollection<Episod> currenEpisods;

        Episod Selected = new Episod();

        private DispatcherTimer timer = new DispatcherTimer();

        private async void RegisterBackgroundTask()
        {
            try
            {
                BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                if (status == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity || status == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity)
                {
                    bool isRegistered = BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name == "Notification task");
                    if (!isRegistered)
                    {
                        BackgroundTaskBuilder builder = new BackgroundTaskBuilder
                        {
                            Name = "Notification task",
                            TaskEntryPoint =
                                "BackgroundTask.NotificationTask.NotificationTask"
                        };
                        builder.SetTrigger(new TimeTrigger(60, false));
                        builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                        BackgroundTaskRegistration task = builder.Register();
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("The access has already been granted");
            }

        }
        public  MainPage()
        {
            RegisterBackgroundTask();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00, 0, 100);
            timer.Start();

            Data.EventHandler = new Data.MyEvent(show);
            this.InitializeComponent();          
            StartClass.start("http://www.lostfilm.tv");

        }


        void show(ObservableCollection<Episod> current)
        {
            if (!Episod.Equals(current.First(), currentFirstEpisod))
            {
                currenEpisods = current;
                currentFirstEpisod = currenEpisods.First();
                Notifications.Start(currenEpisods.First());

                gvMain.ItemsSource = currenEpisods;
                gvMain.Visibility = Visibility.Visible;
            }
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Notifications.Start(currenEpisods.First());           
        }

         void timer_Tick(object sender, object e)
         {
             StartClass.start("http://www.lostfilm.tv");
         }


         void gvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             Selected = (Episod)gvMain.SelectedItem;
             if (Selected != null)
             {
                 descriptionBox.Text = Selected.description;
             }
         }

         async void Button_Click_2(object sender, RoutedEventArgs e)
         {           
            if (Selected != null)
            {
                Uri url = new Uri(Selected.detailsPath);                 
                var success = await Launcher.LaunchUriAsync(url);
            }
         }
       
    }
}
