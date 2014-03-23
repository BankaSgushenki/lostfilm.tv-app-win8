using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;
using NotificationsExtensions.ToastContent;
using Windows.UI.Popups;
using System;



namespace WindowsRuntimeComponent
{
    public sealed class SampleBackgroundTask : IBackgroundTask 
    {
        public static void NotificationSend(Episod currenEpisods)
        {
            IToastNotificationContent toastContent = null;
            IToastImageAndText03 templateContent = ToastContentFactory.CreateToastImageAndText03();
            templateContent.TextHeadingWrap.Text = currenEpisods.showTitle + " - новая серия уже доступна.";
            templateContent.TextBody.Text = currenEpisods.episodTitle;
            templateContent.Image.Src = currenEpisods.imagePath;    
            toastContent = templateContent;
            ToastNotification toast = toastContent.CreateNotification();

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
        BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
        await StartClass.start("http://www.lostfilm.tv");
        //ReadFile();
        NotificationSend(EpisodsList.currentEpisod);
        deferral.Complete();
        }

        public async void ReadFile()
        {
            // settings
            var path = @"lostfilm.tv-app-win8\MyFile.txt";
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Episod lol = new Episod();

            // acquire file
            var file = await folder.GetFileAsync(path);
            var readFile = await Windows.Storage.FileIO.ReadLinesAsync(file);
            foreach (var line in readFile)
            {
                lol.showTitle = line;
                NotificationSend(lol);
            }
        }

    }
}
