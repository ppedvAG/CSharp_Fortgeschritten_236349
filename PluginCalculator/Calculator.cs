using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Rechner Plugin";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible]
	public double Add(double x, double y) => x + y;

	[ReflectionVisible("Sub")]
	public double Subtract(double x, double y) => x - y;
}