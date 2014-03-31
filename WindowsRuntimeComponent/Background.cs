using Windows.UI.Notifications;
using Windows.ApplicationModel.Background;
using NotificationsExtensions.ToastContent;
using System;
using NotificationsExtensions.TileContent;



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

        protected static void TitleUpdate(Episod currenEpisods)
        {
            ITileWideImage tileContent = TileContentFactory.CreateTileWideImage();
            tileContent.Image.Src = currenEpisods.posterPath;
            tileContent.RequireSquareContent = false;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
        BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
        await StartClass.start("http://www.lostfilm.tv");
        NotificationSend(EpisodsList.currentEpisod);
        TitleUpdate(EpisodsList.currentEpisod);
        deferral.Complete();
        } 
    }
}
