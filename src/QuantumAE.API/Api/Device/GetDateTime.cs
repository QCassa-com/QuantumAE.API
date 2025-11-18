using System;

namespace QuantumAE.Api.Device;

/// <summary>
///   hu: Dátum és idő lekérdezése
///   <br />
///   en: Date and time query
/// </summary>
/// <remarks>
///   GET /device/getDateTime?RequestId={ARequestId}
/// </remarks>
public record GetDateTimeRequest(string RequestId) : IDeviceRequest;

/// <summary>
///   hu: Dátum és idő lekérdezése válasz
///   <br />
///   en: Date and time query response
/// </summary>
/// <param name="DateTime"></param>
public record GetDateTimeResponse(string RequestId, int ResultCode, DateTime DateTime): IDeviceResponse;