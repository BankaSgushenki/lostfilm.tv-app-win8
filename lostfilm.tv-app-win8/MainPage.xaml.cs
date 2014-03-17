using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.ApplicationModel.Background;

using lostfilm.tv_app_win8.Logic;
using lostfilm.tv_app_win8.Model;

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
       

        public  MainPage()
        {
            BackgroundTasks.RegisterBackgroundTask();
            Timer.setTimer(300);
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
                InterfaceIsVisible();
            }
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Notifications.Start(currenEpisods.First());           
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

         private void Refresh_Click(object sender, RoutedEventArgs e)
         {
             StartClass.start("http://www.lostfilm.tv");
         }

         void InterfaceIsVisible()
         {
             gvMain.Visibility = Visibility.Visible;
             refreshButton.Visibility = Visibility.Visible;
         }
    }
}
