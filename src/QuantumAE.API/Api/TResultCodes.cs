using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///   hu: API válaszkódok konstansokat tartalmazó osztály.
///   <br />
///   en: API response codes constants class.
/// </summary>
[PublicAPI]
public class TResultCodes
{
  /// <summary>
  ///   hu: Általános hibakódok.
  ///   <br />
  ///   en: General error codes.
  /// </summary>
  public const int GeneralResult = 0x0000;

  /// <summary>
  ///   hu: Rendeléssel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Order related error codes.
  /// </summary>
  public const int OrderResult = 0x1000;
  
  /// <summary>
  ///   hu: Rendelési tételekkel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Order item related error codes.
  /// </summary>
  public const int OrderItemResult = 0x2000;
  
  /// <summary>
  ///   hu: Vezérléssel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Control related error codes.
  /// </summary>
  public const int ControlResult = 0x3000;
  
  /// <summary>
  ///   hu: Az eszközzel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Device related error codes.
  /// </summary>
  public const int DeviceResult = 0x4000;

  /// <summary>
  ///   hu: NAV-I műveletekkel kapcsolatos hibakódok.
  ///   <br />
  ///   en: NAV-I operation related error codes.
  /// </summary>
  public const int NavIResult = 0x5000;

  /// <summary>
  ///   hu: Riport műveletekkel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Report operation related error codes.
  /// </summary>
  public const int ReportResult = 0x6000;
}