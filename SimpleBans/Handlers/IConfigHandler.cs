using SimpleBans.Data;

namespace SimpleBans.Handlers;

public interface IConfigHandler
{
    public Task<Tuple<bool, string>> SaveConfig(string configPath, SimpleBansConfig config);
    public Task<Tuple<bool, ConfigLoadingError, SimpleBansConfig?>> LoadConfig(string configPath);
}