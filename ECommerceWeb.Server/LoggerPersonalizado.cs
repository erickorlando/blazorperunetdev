namespace ECommerceWeb.Server;

public static class LoggerPersonalizado
{
    public static void Log(string message)
    {
        File.AppendAllText("../queries.log", message);
    }
}