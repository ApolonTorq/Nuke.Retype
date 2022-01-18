namespace Torq.Nuke.Retype
{
    public partial class RetypeBuildSettings
    {
        string GetProcessToolPath()
        {
            return RetypeTasks.GetToolPath();
        }
    }
}