using LoadPlugin;
using System.Reflection;

namespace Reflection.LoadPlugin;

public class Program
{
    public static void Main(string[] args)
    {
        // Setup assembly requirements and load all plugins.
        Setup.PluginDirectory();
        List<Plugin> plugins = PluginLoader.Load();


        // Write basic Plugin information in console.
        foreach (Plugin plugin in plugins)
        {
            Console.WriteLine();
            Console.WriteLine($"Plugin was found: {plugin.Name} by {plugin.Author} in version {plugin.Version}");
            Console.WriteLine($"Plugin Description: {plugin.Description}");
            plugin.LoadPlugin();
            Console.WriteLine();
        }
    }
}

