using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Device;

/// <summary>
///   hu: Eszköz információ lekérdezése kérés
///   <br />
///   en: Device information query request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique request identifier
/// </param>
[PublicAPI]
public sealed record GetDeviceInfoRequest(string RequestId): IDeviceRequest
{
  /// <summary>
  ///   hu: Kérés egyedi azonosítója
  ///   <br />
  ///   en: Unique request identifier
  /// </summary>
  [Required]
  [NotEmptyString]
  public string RequestId { get; init; } = RequestId;

  /// <summary>
  ///   hu: Eszköz információ lekérdezése szöveges formában
  ///   <br />
  ///   en: Get device information in text form
  /// </summary>
  public override string ToString()
  {
    return $"GetDeviceInfoRequest(RequestId={RequestId})";
  }
}

/// <summary>
///   hu: Eszköz információ lekérdezése válasz
///   <br />
///   en: Device information query response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique request identifier
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), ha nem, akkor hiba kód
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
/// <param name="DeviceInfo">
///   hu: Eszköz információ lekérdezése szöveges formában
///   <br />
///   en: Get device information in text form
/// </param>
[PublicAPI]
public sealed record GetDeviceInfoResponse(string RequestId, int ResultCode, string? ErrorMessage, TDeviceInfo? DeviceInfo): IDeviceResponse
{
  /// <summary>
  ///   hu: Kérés egyedi azonosítója
  ///   <br />
  ///   en: Unique request identifier
  /// </summary>
  [Required]
  [NotEmptyString]
  public string RequestId { get; init; } = RequestId;

  /// <summary>
  ///   hu: Eredménykód (0 = siker), ha nem, akkor hiba kód
  ///   <br />
  ///   en: Result code (0 = success), otherwise error code
  /// </summary>
  public int ResultCode { get; init; } = ResultCode;

  /// <summary>
  ///   hu: Hibaüzenet (ha hiba történt)
  ///   <br />
  ///   en: Error message (if error occurred)
  /// </summary>
  public string? ErrorMessage { get; init; } = ErrorMessage;

  /// <summary>
  ///   hu: Eszköz információ lekérdezése szöveges formában
  ///   <br />
  ///   en: Get device information in text form
  /// </summary>
  public TDeviceInfo? DeviceInfo { get; init; } = DeviceInfo;

  /// <summary>
  ///   hu: Eszköz információ lekérdezése szöveges formában
  ///   <br />
  ///   en: Get device information in text form
  /// </summary>
  public override string ToString()
  {
    return $"GetDeviceInfoResponse(RequestId={RequestId}, ResultCode={ResultCode}, ErrorMessage={ErrorMessage}, DeviceInfo={DeviceInfo})";
  }
}