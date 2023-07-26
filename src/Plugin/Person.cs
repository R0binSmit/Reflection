using Interface;

namespace Plugin;

internal class Person : IPlugin
{
    public Version Version => new Version(1, 0);

    public string Name => "Person";

    public string Author => "Robin Smit";

    public string Description => "This is a Person Plugin";

    public void LoadPlugin()
    {
        Console.WriteLine("Person Plugin");
    }
}
