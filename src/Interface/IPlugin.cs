namespace Interface;

public interface IPlugin
{
    Version Version { get; }
    string Name { get; }
    string Author { get; }
    string Description { get; }
    void LoadPlugin();
}
