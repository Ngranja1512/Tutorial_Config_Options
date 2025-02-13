#region Usings

using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

#endregion

// We're using an empty builder that has no pre-configured defaults.

var builder =
    Host.CreateEmptyApplicationBuilder(null);

#region File Configuration Providers
#region JSON {;}

// 📦 Microsoft.Extensions.Configuration.Json

//// For example: appsettings.json
//builder.Configuration.AddJsonFile(
//   path: "appsettings.json",
//   optional: true,
//   reloadOnChange: true);

//// For example: appsettings.Staging.json
//builder.Configuration.AddJsonFile(
//   path: $"appsettings.{builder.Environment.EnvironmentName}.json",
//   optional: true,
//   reloadOnChange: true);

#endregion

#region XML </>

// 📦 Microsoft.Extensions.Configuration.Xml

//// For example: config.xml
//builder.Configuration.AddXmlFile(
//    path: "config.xml",
//    optional: true,
//    reloadOnChange: true);

//// For example: config.Staging.xml
//builder.Configuration.AddXmlFile(
//    path: $"config.{builder.Environment.EnvironmentName}.xml",
//    optional: true,
//    reloadOnChange: true);

#endregion

#region INI [=]

// 📦 Microsoft.Extensions.Configuration.Ini

//// For example: app.ini
//builder.Configuration.AddIniFile(
//    path: "app.ini",
//    optional: true,
//    reloadOnChange: true);

//// For example: app.Development.ini
//builder.Configuration.AddIniFile(
//    path: $"app.{builder.Environment.EnvironmentName}.ini",
//    optional: true,
//    reloadOnChange: true);

#endregion

#region User Secrets (?!)

// 📦 Microsoft.Extensions.Configuration.UserSecrets

// NOTES:
//   Intended for local development environments only!
//   Not usually added manually by user code.

builder.Configuration.AddUserSecrets<Program>(
    optional: true,
    reloadOnChange: true);

// Stored outside of project in the "user profile".
//   Windows:
//     %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
//   Linux/macOS:
//     ~/.microsoft/usersecrets/<user_secrets_id>/secrets.json

#endregion
#endregion

#region Environment Variables ($env)

// 📦 Microsoft.Extensions.Configuration.EnvironmentVariables

// builder.Configuration.AddEnvironmentVariables();

// Many .NET specific env vars are automatically pulled in from
// the Microsoft.Extensions.Hosting meta-package defaults. Such
// as, but not limited to, the "DOTNET_*" prefixed env vars.

// builder.Configuration.AddEnvironmentVariables(prefix: "MY_APP_");

#endregion

#region Command-Line Args (-a)

// 📦 Microsoft.Extensions.Configuration.CommandLine

//var switchMappings =
//    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
//    {
//        // -d="0:0:05" or -d "0:0:05"
//        ["-d"] = "Delay",

//        // --apiOn=true /apiOn=true
//        ["--apiOn"] = "TodoApiOptions:Enabled",

//        // --todoUri <uri> /todoUri <uri>
//        ["--todoUri"] = "TodoApiOptions:BaseAddress"
//    };

//builder.Configuration.AddCommandLine(
//    args, switchMappings);

//We can then add the IConfigurationRoot to an option class like this:

// var host = Host.CreateDefaultBuilder()
//     .ConfigureServices((context, services) =>
//     {
//         services
//             .AddOptions<CommandLineOption>()
//             .Bind(config) 
//             .ValidateOnStart();
//                 
//         services.AddSingleton<IDataGateway,Core.Services.DataGateway>();
//     })
//     .Build();
//         
// var appSettings = host.Services.GetRequiredService<IOptions<CommandLineOption>>().Value;

//Another example of a mapping from short to long command line flags:
// static string[] MapShortFlagsToLong(string[] args)
// {
//     var flagMappings = new Dictionary<string, string>
//     {
//         { "-c", "--config" },
//         { "-h", "--help" },
//         { "-l", "--log" },
//         { "-d", "--debug" },
//     };
//
//     return args.Select(arg => flagMappings.TryGetValue(arg.ToLower(), out var longName) ? $"{longName}=true" : $"{arg}=true").ToArray();
// }

#endregion

#region Key-per file (/key)

// 📦 Microsoft.Extensions.Configuration.KeyPerFile

//builder.Configuration.AddKeyPerFile(
//    directoryPath: @"D:\ExampleApp", 
//    optional: false, 
//    reloadOnChange: true);

#endregion

#region Azure Key Vault (k=v)

// 📦 Azure.Extensions.AspNetCore.Configuration.Secrets
// 📦 Azure.Identity

builder.Configuration.AddAzureKeyVault(
    new Uri($"""
         https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/
         """),
    new DefaultAzureCredential());

#endregion

// Write all the config values to the console.
Console.WriteLine(
    builder.Configuration.GetDebugView());
