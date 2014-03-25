using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.System;
using Windows.UI.Xaml.Navigation;

using lostfilm.tv_app_win8.Logic;
using lostfilm.tv_app_win8.Model;

namespace lostfilm.tv_app_win8
{
    
    public sealed partial class MainPage : Page
    {
        Episod Selected = new Episod();
        Episod currentFirstEpisode;
       
        public  MainPage()
        {
            Data.EventHandler = new Data.MyEvent(show);
            BackgroundTasks.RegisterBackgroundTask();
            Timer.setRefreshTimer(300);
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.InitializeComponent();
            progressRing.IsActive = true;
            StartClass.start("http://www.lostfilm.tv");
        }


        void show()
        {
            progressRing.IsActive = false;
            if (!Episod.Equals(EpisodsList.currentEpisods.First(), currentFirstEpisode))
            {
                Notifications.Start(EpisodsList.currentEpisods.First());
                gvMain.ItemsSource = EpisodsList.currentEpisods;
                InterfaceIsVisible();
                currentFirstEpisode = EpisodsList.currentEpisods.First();
            }
          
        }
    
         void gvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             Selected = (Episod)gvMain.SelectedItem;
             if (Selected != null)
             {
                 descriptionBox.Text = Selected.description;
             }
         }

         async void openInBrowser_Click(object sender, RoutedEventArgs e)
         {
            
            if (Selected != null)
            {
                Uri url = new Uri(Selected.detailsPath);                 
                var success = await Launcher.LaunchUriAsync(url);
            }
         }

         private void Refresh_Click(object sender, RoutedEventArgs e)
         {
             progressRing.IsActive = true;
             StartClass.start("http://www.lostfilm.tv");
         }

         void InterfaceIsVisible()
         {
             gvMain.Visibility = Visibility.Visible;
             refreshButton.Visibility = Visibility.Visible;
             settingsButton.Visibility = Visibility.Visible;
         }

         private void settingsButton_Click(object sender, RoutedEventArgs e)
         {
             this.Frame.Navigate(typeof(SettingsPage));

         }
    }
}
