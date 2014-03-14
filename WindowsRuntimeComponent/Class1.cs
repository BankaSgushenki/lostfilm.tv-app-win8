using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;


namespace WindowsRuntimeComponent
{
    public sealed class SampleBackgroundTask : IBackgroundTask 
    {
        private void SendNotification(string text)
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

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral(); 
        try        
        {
            SendNotification("asdasdasdasdasdsad");
        }  
        finally        
        {           
            deferral.Complete();        
        }   
        }
    }
}
