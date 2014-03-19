using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;


namespace WindowsRuntimeComponent
{
    public sealed class SampleBackgroundTask : IBackgroundTask 
    {
        public static void SendNotification(string text)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            XmlNodeList elements = toastXml.GetElementsByTagName("text");
            foreach (IXmlNode node in elements)
            {
                node.InnerText = text;
            }

            ToastNotification notification = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
        BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
        await StartClass.start("http://www.lostfilm.tv");
        SendNotification(EpisodsList.currentEpisod.showTitle + " - новая серия уже доступна.");
        deferral.Complete();
        }

    }
}
