using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;
using NotificationsExtensions.ToastContent;


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
        NotificationSend(EpisodsList.currentEpisod);
        deferral.Complete();
        }

    }
}
