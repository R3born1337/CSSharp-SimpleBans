using System.Text.Json;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using SimpleBans.Data;
using SimpleBans.Handlers;

namespace SimpleBans;

public class SimpleBansPlugin: BasePlugin
{
    public override string ModuleName => "SimpleBans";
    public override string ModuleVersion => "0.0.1";
    public override string ModuleAuthor => "R3born";

    private const string ConfigFileName = "simplebans.cfg";
    private string _configFilePath { get; set; } = string.Empty;

    private SimpleBansConfig _config { get; set; } = new();
    private IConfigHandler ConfigHandlerHandler { get; } = new ConfigHandler();

    public override async void Load(bool hotReload)
    {
        _configFilePath = ModuleDirectory + ConfigFileName;

        var (success, loadingState, config) = await ConfigHandlerHandler.LoadConfig(_configFilePath);
        if (success)
        {
            _config = config!;
            return;
        }

        if (loadingState == ConfigLoadingError.Error)
        {
            Console.WriteLine("[SimpleBans] There has been an error while parsing the config file. Please correct the file or delete it.");
            Unload(true);
            return;
        }

        var newConfig = new SimpleBansConfig
        {
            DatabaseType = DatabaseType.None
        };

        (success, var error) = await ConfigHandlerHandler.SaveConfig(_configFilePath, newConfig);
        if (!success)
        {
            Console.WriteLine(error);
            Unload(true);
            return;
        }

        _config = newConfig;
    }
}