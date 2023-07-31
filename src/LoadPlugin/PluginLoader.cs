using Interface;
using System.Reflection;

namespace LoadPlugin;

/// <summary>
/// Load alle plugin assemblies and the supported plugin types.
/// </summary>
internal static class PluginLoader
{
    /// <summary>
    /// Search pattern to identify supported files by file extention.
    /// </summary>
    private const string pluginSearchPattern = "*.dll";

    /// <summary>
    /// Identify and load all '.dll' files from the plugin directory.
    /// </summary>
    /// <returns>List of all assemblies from the plugin directory.</returns>
    private static List<Assembly> LoadAssemblies()
    {
        List<Assembly> assemblies = new List<Assembly>();
        foreach (string pluginPath in Directory.GetFiles(SetupHelpers.PluginPath, pluginSearchPattern))
        {
            Assembly assembly = Assembly.LoadFrom(pluginPath);
            assemblies.Add(assembly);
        }
        return assemblies;
    }

    /// <summary>
    /// Get list of all supprted Types from all assemblies in the plugin directory.
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns>List of supported plugins.</returns>
    private static List<Plugin> IdentifyPluginTypes(List<Assembly> assemblies)
    {
        List<Plugin> plugins = new List<Plugin>();
        foreach(Assembly assembly in assemblies)
        {
            Type[] pluginTypes = assembly.GetTypes();
            foreach (Type pluginType in pluginTypes)
            {
                // Add Type when IPlugin was implementet.
                Type? type = pluginType.GetInterface(nameof(IPlugin));
                if(type != null)
                {
                    plugins.Add(new Plugin(pluginType));
                }
            }
        }
        return plugins;
    }

    /// <summary>
    /// Load all supported plugins from the plugin directory.
    /// </summary>
    /// <returns>List of supported plugins.</returns>
    public static List<Plugin> Load()
    {
        List<Assembly> assemblies = LoadAssemblies();
        return IdentifyPluginTypes(assemblies);

    }
}
