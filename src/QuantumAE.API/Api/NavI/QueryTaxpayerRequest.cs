using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Adószám lekérdezési kérés - adózó adatainak validálása számla kiállítás előtt.
///   <br />
///   en: Tax number query request - validate taxpayer data before issuing an invoice.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="TaxNumber">
///   hu: A lekérdezendő adószám első 8 karaktere
///   <br />
///   en: The first 8 characters of the tax number to query
/// </param>
[PublicAPI]
public sealed record QueryTaxpayerRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  [property: StringLength(8, MinimumLength = 8)]
  string TaxNumber
) : INavIRequest;

/// <summary>
///   hu: Adószám lekérdezési válasz - az adózó érvényessége, neve és címe.
///   <br />
///   en: Tax number query response - taxpayer validity, name and address.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), különben hiba kód
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="TaxpayerValidity">
///   hu: Az adózó érvényessége (true = érvényes, false = nem érvényes)
///   <br />
///   en: Taxpayer validity (true = valid, false = invalid)
/// </param>
/// <param name="TaxpayerName">
///   hu: Az adózó neve
///   <br />
///   en: Taxpayer name
/// </param>
/// <param name="TaxpayerAddress">
///   hu: Az adózó címe
///   <br />
///   en: Taxpayer address
/// </param>
/// <param name="InfoDate">
///   hu: Az adatok utolsó változásának időpontja
///   <br />
///   en: Last date on which the data was changed
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record QueryTaxpayerResponse(
  string RequestId,
  int ResultCode,
  bool TaxpayerValidity = false,
  string? TaxpayerName = null,
  string? TaxpayerAddress = null,
  string? InfoDate = null,
  string? ErrorMessage = null
) : INavIResponse;
