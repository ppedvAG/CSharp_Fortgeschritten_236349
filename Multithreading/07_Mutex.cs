namespace Multithreading;

internal class _07_Mutex
{
	static void Main(string[] args)
	{
		Mutex m = Mutex.OpenExisting("Multithreading"); 
		if (m == null) //Programm wurde noch nicht gestartet
		{
			m = new Mutex(true, "Multithreading");
		}
		else //Programm wurde bereits gestartet
		{
			Environment.Exit(0);
		}
		m.ReleaseMutex(); //Mutex muss am Ende wieder freigegeben werden
	}
}
