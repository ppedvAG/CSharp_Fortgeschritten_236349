using PluginBase;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace PluginClient;

/// <summary>
/// Hier sollen Plugins (.dlls) geladen werden können, und der User soll mithilfe der UI mit den Plugins interagieren können
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		//Pfade sollten dynamisch sein (z.B. Config)
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_11_13\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll";

		Assembly a = Assembly.LoadFrom(pfad);

		Type t = a.DefinedTypes.First(e => e.GetInterface(nameof(IPlugin)) != null); //Suche den ersten Type der das Interface IPlugin hat

		IPlugin plugin = (IPlugin) Activator.CreateInstance(t);

		InfoText.Text = string.Join("\n", t.GetProperties().Select(e => $"{e.Name}: {e.GetValue(plugin)}"));

		foreach (MethodInfo mi in t.GetMethods())
		{
			ReflectionVisible visible = mi.GetCustomAttribute<ReflectionVisible>(); //Hier kann jetzt auf die internen Werte des Attributs zugegriffen werden
			if (visible == null)
				continue;

			MethodenPanel.Children.Add(new TextBlock() { Text = 
				$"{mi.ReturnType.Name} {visible.Name}({mi.GetParameters()
					.Aggregate(string.Empty, (str, par) => str + par.ParameterType.Name + " " + par.Name + ", ")
					.TrimEnd(',', ' ')})" });
		}
	}
}
