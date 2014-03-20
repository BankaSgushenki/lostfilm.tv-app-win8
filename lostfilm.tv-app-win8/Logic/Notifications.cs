using Windows.UI.Notifications;
using NotificationsExtensions.ToastContent;
using NotificationsExtensions.TileContent;

using  lostfilm.tv_app_win8.Model;

namespace lostfilm.tv_app_win8.Logic
{
    class Notifications
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

        public static void TitleUpdate(Episod currenEpisods)
        {
            ITileWideImage tileContent = TileContentFactory.CreateTileWideImage();
            tileContent.Image.Src = currenEpisods.posterPath;
            tileContent.RequireSquareContent = false;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }

        public static void Start(Episod currenEpisods)
        {
           NotificationSend(currenEpisods);
           TitleUpdate(currenEpisods);       
        }
    }
}
