namespace QuantumAE;

static class TApiClientHolder
{
  private static TQuantumAeApiClient? FClient;

  public static void Configure(string baseUrl, TApiFormat format)
  {
    Dispose();
    FClient = new TQuantumAeApiClient(baseUrl.TrimEnd('/'), format);
  }

  public static TQuantumAeApiClient Require()
  {
    if (FClient is not null) return FClient;
    var url = ConnectionStore.LoadUrl();
    if (string.IsNullOrWhiteSpace(url))
      throw new InvalidOperationException("Nincs beállított kapcsolat. Futtasd: qae connect --url http://127.0.0.1:9090");
    var fmt = ResolveFormatEnv();
    FClient = new TQuantumAeApiClient(url!.TrimEnd('/'), fmt);
    return FClient;
  }

  public static void Dispose()
  {
    try { FClient?.Dispose(); } catch { }
    finally { FClient = null; }
  }

  private static TApiFormat ResolveFormatEnv()
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
}