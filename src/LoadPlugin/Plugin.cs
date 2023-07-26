using Interface;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LoadPlugin;

/// <summary>
/// Mapper class for all identified plugins to call supported methods and getter.
/// </summary>
public class Plugin : IPlugin
{
    private Type pluginType;
    private object? plugin;
    private const string getMethodPräfix = "get_";

    public Version Version
    {
        get
        {
            object? version = InvokeMethod(GetterMethodName());
            return version != null ? (Version)version : new Version(0,0);
        }
    }
    /// <summary>
    /// Invoke name getter from plugin and return value.
    /// </summary>
    public string Name 
    {
        get
        {
            object? name = InvokeMethod(GetterMethodName());
            return name != null ? (string)name : string.Empty;
        }
    }

    /// <summary>
    /// Invoke author getter from plugin and return value.
    /// </summary>
    public string Author
    {
        get
        {
            object? author = InvokeMethod(GetterMethodName());
            return author != null ? (string)author : string.Empty;
        }
    }

    /// <summary>
    /// Invoke description getter from plugin and return value.
    /// </summary>
    public string Description
    {
        get
        {
            object? description = InvokeMethod(GetterMethodName());
            return description != null ? (string)description : string.Empty;
        }
    }

    /// <summary>
    /// Call plugin Method LoadPlugin.
    /// </summary>
    public void LoadPlugin()
    {
        InvokeMethod(nameof(LoadPlugin));
    }

    /// <summary>
    /// Create new Plugin object by assembly type.
    /// </summary>
    /// <param name="pluginType"></param>
    public Plugin(Type pluginType)
    {
        this.pluginType = pluginType;
        this.plugin = Activator.CreateInstance(pluginType);
    }

    /// <summary>
    /// Identify getter method name by context.
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns></returns>
    private string GetterMethodName([CallerMemberName] string memberName = "")
    {
        return $"{getMethodPräfix}{memberName}";
    }


    /// <summary>
    /// Invoke parameterless method from plugin when existing.
    /// </summary>
    /// <param name="methodName"></param>
    private object? InvokeMethod(string methodName)
    {
        MethodInfo? methodInfo = pluginType.GetMethod(methodName);
        if(methodInfo != null)
        {
            return methodInfo?.Invoke(plugin, null);
        }

        return null;
    }
}
