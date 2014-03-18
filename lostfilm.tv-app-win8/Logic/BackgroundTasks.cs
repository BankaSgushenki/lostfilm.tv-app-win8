using Windows.ApplicationModel.Background;

namespace lostfilm.tv_app_win8.Logic
{
    class BackgroundTasks
    {

        static string taskName;
        static string entryPoint;
        static SystemCondition userCondition;
        static TimeTrigger Trigger;

        public static BackgroundTaskRegistration RegisterBackgroundTask()
        {
            BacgroundTaskSetup();

            deleteAllBackground();
            var builder = new BackgroundTaskBuilder();
            builder.Name = taskName;
            builder.TaskEntryPoint = entryPoint;
            builder.SetTrigger(Trigger);
            if (userCondition != null)
            {
                builder.AddCondition(userCondition);
            }
            BackgroundTaskRegistration task = builder.Register();
            return task;
        }

        public static void BacgroundTaskSetup()
        {
            Trigger = new TimeTrigger(15, false);
            SystemCondition userCondition = new SystemCondition(SystemConditionType.UserPresent);
            taskName = "SampleBackgroundTask";
            entryPoint = "WindowsRuntimeComponent.SampleBackgroundTask";
        }

        public static void deleteAllBackground()
        {
            foreach (var value in BackgroundTaskRegistration.AllTasks)
                value.Value.Unregister(true);
        }
    }
}
