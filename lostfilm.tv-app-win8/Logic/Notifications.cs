using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using NotificationsExtensions.ToastContent;
using NotificationsExtensions.TileContent;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

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
            ITileSquareBlock tileContent = TileContentFactory.CreateTileSquareBlock();
            tileContent.TextBlock.Text = currenEpisods.showTitle;
            tileContent.TextSubBlock.Text = currenEpisods.episodTitle;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());
        }

        public static void Start(Episod currenEpisods)
        {
           NotificationSend(currenEpisods);
           TitleUpdate(currenEpisods);       
        }
    }
}
