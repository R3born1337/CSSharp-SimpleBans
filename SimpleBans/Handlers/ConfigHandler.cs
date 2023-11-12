using System.Text.Json;
using SimpleBans.Data;

namespace SimpleBans.Handlers;

public class ConfigHandler: IConfigHandler
{
    public async Task<Tuple<bool, string>> SaveConfig(string configPath, SimpleBansConfig config)
    {
        try
        {
            await using var createStream = File.Create(configPath);
            await JsonSerializer.SerializeAsync(createStream, config);
            return Tuple.Create(true, string.Empty);
        }
        catch (Exception ex)
        {
            return Tuple.Create(false, $"[SimpleBans] There has been an error while creating the config file! Exception: {ex.Message}");
        }
    }

    public async Task<Tuple<bool, ConfigLoadingError, SimpleBansConfig?>> LoadConfig(string configPath)
    {
        if (!File.Exists(configPath))
            return Tuple.Create<bool, ConfigLoadingError, SimpleBansConfig?>(false, ConfigLoadingError.NotFound, null);
        
        try
        {
            var fileContent = await File.ReadAllTextAsync(configPath);
            var config = JsonSerializer.Deserialize<SimpleBansConfig>(fileContent);
            return Tuple.Create(true, ConfigLoadingError.Success, config);
        }
        catch (Exception ex)
        {
            //return Tuple.Create<bool, ConfigLoadingError, SimpleBansConfig?>(false, $"[SimpleBans] There has been an error while parsing the config file. Please correct the file or delete it. Exception: {ex.Message}", null);
            return Tuple.Create<bool, ConfigLoadingError, SimpleBansConfig?>(false, ConfigLoadingError.Error, null);
        }
    }
}