namespace PluginBase;

/// <summary>
/// Eigenes Attribut wird definiert mithilfe der Attribute Oberklasse
/// Attribute können nur über Reflection verarbeitet werden
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)] //Einzelnes | oder &: Bitweise Verknüpfung
public class ReflectionVisible : Attribute
{
	public string Name { get; set; }

    public ReflectionVisible()
    {
        
    }

	public ReflectionVisible(string Name)
	{
		this.Name = Name;
	}
}