using System.Reflection;

/// <summary>
/// Helper class to organise Setup for assembly requirements.
/// </summary>
internal static class SetupHelpers
{
    /// <summary>
    /// Relative folder name where the plugins should be stored.
    /// </summary>
    private const string _pluginDirectoryName = "Plugins";

    /// <summary>
    /// Identify location for currently running assembly.
    /// </summary>
    internal static string AssemblyPath
    {
        get
        {
            string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return path != null ? path : string.Empty;
        }
    }

    /// <summary>
    /// Return absolute directory where all plugins should be stored.
    /// </summary>
    internal static string PluginPath => Path.Combine(AssemblyPath, _pluginDirectoryName);
}