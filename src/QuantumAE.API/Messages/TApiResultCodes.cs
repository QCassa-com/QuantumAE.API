namespace QuantumAE.Messages;

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
  public const int OrderResult = 0x100;
  public const int ControlResult = 0x200;
}

public enum TControlResult
{
  Success,
  SessionNotFound = TApiResultCodes.ControlResult,
  SessionInvalid
}

public enum TOrderResult
{
  Success,
  OrderNotFound = TApiResultCodes.OrderResult,
  NotOpenOrClosed,
  Empty,
  Closed
} 
