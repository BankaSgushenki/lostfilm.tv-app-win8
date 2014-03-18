using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.System;

using lostfilm.tv_app_win8.Logic;
using lostfilm.tv_app_win8.Model;

namespace lostfilm.tv_app_win8
{
    
    public sealed partial class MainPage : Page
    {
        Episod currentFirstEpisod;

        private ObservableCollection<Episod> currentEpisods;

        Episod Selected = new Episod();   
       

        public  MainPage()
        {
            BackgroundTasks.RegisterBackgroundTask();
            Timer.setRefreshTimer(300);
            Data.EventHandler = new Data.MyEvent(show);
            this.InitializeComponent();          
            StartClass.start("http://www.lostfilm.tv");
        }


        void show(ObservableCollection<Episod> current)
        {
            if (currentEpisods == null)
            {
                currentEpisods = current;
                currentFirstEpisod = currentEpisods.First();
                Notifications.Start(currentEpisods.First());

                gvMain.ItemsSource = currentEpisods;
                InterfaceIsVisible();
                return;
            }
            if (!Episod.Equals(current.First(), currentFirstEpisod))
            {
                currentEpisods.Add(current.First());
                currentFirstEpisod = currentEpisods.First();
                Notifications.Start(currentEpisods.First());

                gvMain.ItemsSource = currentEpisods;
                InterfaceIsVisible();
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
             StartClass.start("http://www.lostfilm.tv");
         }

         void InterfaceIsVisible()
         {
             gvMain.Visibility = Visibility.Visible;
             refreshButton.Visibility = Visibility.Visible;
         }

         private void exitButton_Click(object sender, RoutedEventArgs e)
         {
             BackgroundTasks.deleteAllBackground();
             Application.Current.Exit();
         }
    }
}
