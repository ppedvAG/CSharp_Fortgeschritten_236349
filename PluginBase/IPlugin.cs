namespace PluginBase;

/// <summary>
/// Die PluginBase stellt die "Verbindung" zwischen den Plugins und dem Client
/// Die PluginBase ist eine Dependency von den Plugins und vom Client
/// </summary>
public interface IPlugin
{
	public string Name { get; }

	public string Description { get; }

	public string Version { get; }

	public string Author { get; }
}