namespace QuantumAE.Api;

/// <summary>
///  hu: API válaszkódok konstansokat tartalmazó osztály
/// <br />
///  en: API response codes constants class
/// </summary>
public class TApiResultCodes
{
  /// <summary>
  ///  hu: Rendelés műveleti kódok
  ///  <br />
  ///  en: Order operation successful
  /// </summary>
  public const int OrderResult = 0x1000;
  public const int ControlResult = 0x2000;
  public const int DeviceResult = 0x3000;
}

public enum TControlResult
{
  Success,
  SessionNotFound = TApiResultCodes.ControlResult,
  SessionInvalid,
  InternalError
}

public enum TOrderResult
{
  Success,
  OrderNotFound = TApiResultCodes.OrderResult,
  NotOpenOrClosed,
  Empty,
  Closed
} 

public enum TDeviceResult
{
  Success,
  DeviceNotFound = TApiResultCodes.DeviceResult,
  InternalError
}
