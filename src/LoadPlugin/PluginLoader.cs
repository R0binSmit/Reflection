using Interface;
using System.Reflection;

namespace LoadPlugin;

/// <summary>
/// Load alle plugin assemblies and the supported plugin types.
/// </summary>
internal class PluginLoader
{
    /// <summary>
    /// Search pattern to identify supported files by file extention.
    /// </summary>
    private const string pluginSearchPattern = "*.dll";

    /// <summary>
    /// List of all found assemblies in the plugin directory.
    /// </summary>
    private List<Assembly> pluginAssemblies = new List<Assembly>();

    /// <summary>
    /// List of all supported Plugin Types.
    /// </summary>
    public List<Plugin> Plugins = new List<Plugin>();

    /// <summary>
    /// Identify and load all '.dll' files from the plugin directory.
    /// </summary>
    private void LoadAssemblies()
    {
        foreach (string pluginPath in Directory.GetFiles(SetupHelpers.PluginPath, pluginSearchPattern))
        {
            Assembly assembly = Assembly.LoadFrom(pluginPath);
            pluginAssemblies.Add(assembly);
        }
    }

    /// <summary>
    /// Create list of all supprted Types from all assemblies in the plugin directory.
    /// </summary>
    private void IdentifyPluginTypes()
    {
        foreach(Assembly assembly in pluginAssemblies)
        {
            Type[] pluginTypes = assembly.GetTypes();
            foreach (Type pluginType in pluginTypes)
            {
                // Add Type when IPlugin was implementet.
                Type? type = pluginType.GetInterface(nameof(IPlugin));
                if(type != null)
                {
                    Plugins.Add(new Plugin(pluginType));
                }
            }
        }
    }

    /// <summary>
    /// Create a plugin loader object that automatically loads all plugin applications and lists supported plugin types.
    /// </summary>
    public PluginLoader()
    {
        LoadAssemblies();
        IdentifyPluginTypes();
    }
}
