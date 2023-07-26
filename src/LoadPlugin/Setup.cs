namespace LoadPlugin;

/// <summary>
/// Setup class to make sure all needed assembly requirements are given/created.
/// </summary>
internal static class Setup
{
    /// <summary>
    /// Setup plugin directory if not present.
    /// </summary>
    public static void PluginDirectory()
    {
        if(!Path.Exists(SetupHelpers.PluginPath))
        {
            Directory.CreateDirectory(SetupHelpers.PluginPath);
        }
    }
}
