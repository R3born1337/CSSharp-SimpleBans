namespace SimpleBans.Data;

public class SimpleBansConfig
{
    public DatabaseType DatabaseType { get; set; }
}

public enum DatabaseType
{
    None = 0,
    MySql = 1,
    MongoDb = 2
}