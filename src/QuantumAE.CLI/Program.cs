using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Spectre.Console;
using QuantumAE.Models;


static int ShowRootHelp()
{
    var current = ConnectionStore.LoadUrl();

    AnsiConsole.MarkupLine("[bold]QuantumAE CLI (qae)[/]");
    if (string.IsNullOrWhiteSpace(current))
    {
        AnsiConsole.MarkupLine("[yellow]Nincs beállított kapcsolat.[/] Tipp: [bold]qae connect --url http://127.0.0.0:9090[/]");
    }
    else
    {
        AnsiConsole.MarkupLine($"[green]Csatlakozva:[/] {current}");
    }

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Usage:");
    AnsiConsole.MarkupLine("  qae connect --url <base-url>");
    AnsiConsole.MarkupLine("  qae disconnect");
    AnsiConsole.MarkupLine("  qae device --info");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Commands:");
    var grid = new Grid();
    grid.AddColumn();
    grid.AddColumn();
    grid.AddRow("connect", "Kapcsolódás az API-hoz / Connect to API");
    grid.AddRow("disconnect", "Kapcsolat megszüntetése / Disconnect");
    grid.AddRow("device", "Eszköz műveletek / Device operations");
    AnsiConsole.Write(grid);
    return 0;
}

static int ShowConnectHelp()
{
    AnsiConsole.MarkupLine("[bold]qae connect[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Usage:");
    AnsiConsole.MarkupLine("  qae connect --url http://127.0.0.0:9090");
    AnsiConsole.WriteLine();
    return 2;
}

static int ShowDeviceHelp()
{
    AnsiConsole.MarkupLine("[bold]qae device[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Usage:");
    AnsiConsole.MarkupLine("  qae device --info");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Options:");
    var grid = new Grid();
    grid.AddColumn();
    grid.AddColumn();
    grid.AddRow("--info", "Megjeleníti az eszköz adatait / Show device info");
    AnsiConsole.Write(grid);
    return 0;
}

static int HandleConnect(string[] args)
{
    // Parse --url
    string? url = null;
    for (int i = 0; i < args.Length; i++)
    {
        var a = args[i];
        if (a.Equals("--url", StringComparison.OrdinalIgnoreCase))
        {
            if (i + 1 < args.Length) url = args[i + 1];
            break;
        }
        else if (a.StartsWith("--url=", StringComparison.OrdinalIgnoreCase))
        {
            url = a.Substring("--url=".Length);
            break;
        }
    }

    if (string.IsNullOrWhiteSpace(url))
    {
        AnsiConsole.MarkupLine("[red]Hiányzó --url paraméter.[/]");
        return ShowConnectHelp();
    }

    if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
    {
        AnsiConsole.MarkupLine("[red]Érvénytelen URL:[/] " + url);
        return 2;
    }

    // Probe /device/
    var baseUrl = url.TrimEnd('/');
    var probeUrl = baseUrl + "/device/";

    try
    {
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        using var req = new HttpRequestMessage(HttpMethod.Get, probeUrl);
        using var res = client.Send(req);

        if ((int)res.StatusCode >= 200 && (int)res.StatusCode < 300)
        {
            ConnectionStore.SaveUrl(baseUrl);
            AnsiConsole.MarkupLine($"[green]Sikeres kapcsolat / Connected:[/] {baseUrl}");

            // Try to read AP number from the device info and show it
            try
            {
                string? apNumber = null;
                var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // attempt parse from the probe response
                var body = res.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(body) && body.TrimStart().StartsWith("{", StringComparison.Ordinal))
                {
                    var info = System.Text.Json.JsonSerializer.Deserialize<TDeviceInfo>(body, opts);
                    apNumber = info?.ApNumber;
                }

                // fallback to /device/info
                if (string.IsNullOrWhiteSpace(apNumber))
                {
                    using var reqInfo = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/device/info");
                    using var resInfo = client.Send(reqInfo);
                    if ((int)resInfo.StatusCode >= 200 && (int)resInfo.StatusCode < 300)
                    {
                        var infoBody = resInfo.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
                        if (!string.IsNullOrWhiteSpace(infoBody))
                        {
                            var info2 = System.Text.Json.JsonSerializer.Deserialize<TDeviceInfo>(infoBody, opts);
                            apNumber = info2?.ApNumber;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(apNumber))
                {
                    AnsiConsole.MarkupLine($"[blue]AP szám:[/] {apNumber}");
                }
            }
            catch
            {
                // ignore, do not fail connect if AP retrieval fails
            }

            return 0;
        }
        AnsiConsole.MarkupLine($"[red]Sikertelen kapcsolat. HTTP { (int)res.StatusCode }[/]");
        return 3;
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine("[red]Nem sikerült kapcsolódni:[/] " + ex.Message);
        return 4;
    }
}

static int HandleDisconnect()
{
    ConnectionStore.Clear();
    AnsiConsole.MarkupLine("[yellow]Kapcsolat törölve / Disconnected.[/]");
    return 0;
}

static int HandleDevice(string[] args)
{
    if (args.Contains("--info", StringComparer.OrdinalIgnoreCase))
    {
        // In a real implementation, these details would be queried from the device/API.
        var info = new TDeviceInfo("AP-0000001");

        var table = new Table().Border(TableBorder.Rounded).Title("[bold]Device Info[/]");
        table.AddColumn("Field");
        table.AddColumn("Value");
        table.AddRow("ApNumber", info.ApNumber);
        AnsiConsole.Write(table);
        return 0;
    }

    return ShowDeviceHelp();
}

int exitCode;
if (args.Length == 0)
{
    exitCode = ShowRootHelp();
}
else
{
    var command = args[0];
    var rest = args.Skip(1).ToArray();
    switch (command.ToLowerInvariant())
    {
        case "connect":
            exitCode = HandleConnect(rest);
            break;
        case "disconnect":
            exitCode = HandleDisconnect();
            break;
        case "device":
            exitCode = HandleDevice(rest);
            break;
        case "-h":
        case "--help":
        case "help":
            exitCode = ShowRootHelp();
            break;
        default:
            AnsiConsole.MarkupLine($"[red]Ismeretlen parancs / Unknown command:[/] {command}");
            exitCode = ShowRootHelp();
            break;
    }
}

return exitCode;

static class ConnectionStore
{
    private static string ConfigDir()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(home, ".qae");
    }

    private static string StorePath() => Path.Combine(ConfigDir(), "connection.txt");

    public static string? LoadUrl()
    {
        var path = StorePath();
        if (File.Exists(path))
        {
            var url = File.ReadAllText(path).Trim();
            return string.IsNullOrWhiteSpace(url) ? null : url;
        }
        return null;
    }

    public static void SaveUrl(string url)
    {
        var dir = ConfigDir();
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(StorePath(), url.Trim());
    }

    public static void Clear()
    {
        var path = StorePath();
        if (File.Exists(path)) File.Delete(path);
    }
}
