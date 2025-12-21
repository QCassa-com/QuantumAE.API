using QuantumAE;
using QuantumAE.Api.Orders;
using QuantumAE.Models;
using Spectre.Console;

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
    AnsiConsole.MarkupLine("  qae order open --order <ORDER_ID> [--request <REQ_ID>]");
    AnsiConsole.MarkupLine("  qae order item --order <ORDER_ID> --name <NAME> --article <SKU> --unit <UNIT> --price <PRICE> --qty <QTY> [--cat <CAT>] [--dept <DEPT>]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Commands:");
    var grid = new Grid();
    grid.AddColumn();
    grid.AddColumn();
    grid.AddRow("connect", "Kapcsolódás az API-hoz / Connect to API");
    grid.AddRow("disconnect", "Kapcsolat megszüntetése / Disconnect");
    grid.AddRow("device", "Eszköz műveletek / Device operations");
    grid.AddRow("order", "Rendelés műveletek (open, item) / Order operations");
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

    var baseUrl = url.TrimEnd('/');

    try
    {
        // Statikus API kliens inicializálása és próba hívás
        TApiClientHolder.Configure(baseUrl, ResolveFormat());
        var info = TApiClientHolder.Require().DeviceInfoAsync().GetAwaiter().GetResult();

        ConnectionStore.SaveUrl(baseUrl);
        AnsiConsole.MarkupLine($"[green]Sikeres kapcsolat / Connected:[/] {baseUrl}");

        if (!string.IsNullOrWhiteSpace(info?.ApNumber))
        {
            AnsiConsole.MarkupLine($"[blue]AP szám:[/] {info.ApNumber}");
        }

        return 0;
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
        try
        {
            var info = TApiClientHolder.Require().DeviceInfoAsync().GetAwaiter().GetResult();

            var table = new Table().Border(TableBorder.Rounded).Title("[bold]Device Info[/]");
            table.AddColumn("Field");
            table.AddColumn("Value");
            table.AddRow("ApNumber", info.ApNumber);
            AnsiConsole.Write(table);
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Nem sikerült lekérdezni az eszköz információkat:[/] " + ex.Message);
            return 5;
        }
    }

    return ShowDeviceHelp();
}

static int ShowOrderHelp()
{
    AnsiConsole.MarkupLine("[bold]qae order[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("Usage:");
    AnsiConsole.MarkupLine("  qae order open --order <ORDER_ID> [--request <REQ_ID>]");
    AnsiConsole.MarkupLine("  qae order item --order <ORDER_ID> --name <NAME> --article <SKU> --unit <UNIT> --price <PRICE> --qty <QTY> [--cat <CAT>] [--dept <DEPT>]");
    AnsiConsole.WriteLine();
    return 2;
}

static TApiFormat ResolveFormat()
{
    var env = Environment.GetEnvironmentVariable("QUANTUMAE__API__FORMAT");
    if (string.IsNullOrWhiteSpace(env)) return TApiFormat.Json;
    var v = env.Trim().ToLowerInvariant();
    return v switch
    {
        "json" => TApiFormat.Json,
        "application/json" => TApiFormat.Json,
        "messagepack" => TApiFormat.MessagePack,
        "msgpack" => TApiFormat.MessagePack,
        "application/msgpack" => TApiFormat.MessagePack,
        "application/x-msgpack" => TApiFormat.MessagePack,
        _ => TApiFormat.Json
    };
}

static int HandleOrder(string[] args)
{
    if (args.Length == 0) return ShowOrderHelp();

    var baseUrl = ConnectionStore.LoadUrl();
    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        AnsiConsole.MarkupLine("[red]Nincs beállított kapcsolat. Futtasd:[/] qae connect --url http://127.0.0.1:9090");
        return 2;
    }

    var sub = args[0].ToLowerInvariant();
    var rest = args.Skip(1).ToArray();

    // Biztosítsuk, hogy a statikus kliens a tárolt URL-lel inicializálva legyen
    TApiClientHolder.Configure(baseUrl!, ResolveFormat());
    var client = TApiClientHolder.Require();

    string? GetOpt(string name)
    {
        for (int i = 0; i < rest.Length; i++)
        {
            var a = rest[i];
            if (a.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 < rest.Length) return rest[i + 1];
                return null;
            }
            if (a.StartsWith(name + "=", StringComparison.OrdinalIgnoreCase))
            {
                return a.Substring(name.Length + 1);
            }
        }
        return null;
    }

    try
    {
        switch (sub)
        {
            case "open":
            {
                var orderId = GetOpt("--order");
                if (string.IsNullOrWhiteSpace(orderId))
                {
                    AnsiConsole.MarkupLine("[red]Hiányzó --order paraméter.[/]");
                    return ShowOrderHelp();
                }
                var reqId = GetOpt("--request") ?? Guid.NewGuid().ToString("N");
                var req = new OrderOpenRequest(reqId, orderId!);
                var res = client.OpenAsync(req).GetAwaiter().GetResult();
                AnsiConsole.MarkupLine($"[green]Open OK[/] RequestId={res.RequestId}, ResultCode={res.ResultCode}");
                return 0;
            }
            case "item":
            {
                var orderId = GetOpt("--order");
                var name = GetOpt("--name");
                var unit = GetOpt("--unit") ?? "db";
                var priceStr = GetOpt("--price");
                var qtyStr = GetOpt("--qty") ?? "1";
                var vatCode = GetOpt("--vat") ?? "C";

                if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceStr))
                {
                    AnsiConsole.MarkupLine("[red]Hiányzó kötelező paraméter (--order, --name, --price). Lásd: qae order --help[/]");
                    return ShowOrderHelp();
                }

                if (!decimal.TryParse(priceStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var price))
                {
                    AnsiConsole.MarkupLine("[red]Érvénytelen --price érték (decimális számot vár).[/]");
                    return 2;
                }
                if (!decimal.TryParse(qtyStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var qty))
                {
                    AnsiConsole.MarkupLine("[red]Érvénytelen --qty érték (decimális számot vár).[/]");
                    return 2;
                }

                var unitType = unit.ToLowerInvariant() switch
                {
                    "db" or "darab" or "piece" => TUnitType.Piece,
                    "kg" or "kilogram" => TUnitType.Kilogram,
                    "l" or "liter" => TUnitType.Liter,
                    "m" or "meter" => TUnitType.Meter,
                    "m2" or "nm" => TUnitType.SquareMeter,
                    "m3" => TUnitType.CubicMeter,
                    "h" or "ora" or "hour" => TUnitType.Hour,
                    _ => TUnitType.Other
                };

                var reqId = Guid.NewGuid().ToString("N");
                var item = new TOrderItem()
                {
                    Name = name,
                    Quantity = qty,
                    UnitType = unitType,
                    UnitName = unitType == TUnitType.Other ? unit : null,
                    UnitPrice = price,
                    Total = qty * price,
                    VatCode = vatCode
                };
                var req = new OrderItemsAddRequest(reqId, orderId!, item);
                var res = client.ItemAddAsync(req).GetAwaiter().GetResult();
                AnsiConsole.MarkupLine($"[green]Item added[/] RequestId={res.RequestId}, ResultCode={res.ResultCode}");
                return 0;
            }
            case "--help":
            case "-h":
            case "help":
                return ShowOrderHelp();
            default:
                AnsiConsole.MarkupLine($"[red]Ismeretlen alparancs:[/] {sub}");
                return ShowOrderHelp();
        }
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine("[red]Hiba:[/] " + ex.Message);
        return 10;
    }
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
        case "order":
            exitCode = HandleOrder(rest);
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

namespace QuantumAE
{
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
}
