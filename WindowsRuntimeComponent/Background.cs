using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;


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
            SendNotification("Sample background task!");       
        }  
        finally        
        {           
            deferral.Complete();        
        }   
        }
    }
}
