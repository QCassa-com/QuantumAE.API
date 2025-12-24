
using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés zárása üzemanyagkártyaként
///   <br />
///   en: Order close as fuel card
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres)
///   <br />
///   en: Result code (0 = success)
/// </param>
/// <param name="OrderId">
///   hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///   <br />
///   en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="DocumentId">
///   hu: Bizonylat azonosító
///   <br />
///   en: Document identifier
/// </param>
/// <param name="CloseMethod">
///   hu: Zárás módja
///   <br />
///   en: Close method
/// </param>
/// <param name="DocumentGeneral">
///   hu: Dokumentum általános adatai
///   <br />
///   en: Document general info
/// </param>
/// <param name="FuelCardClose">
///   hu: Üzemanyagkártya zárási adatai
///   <br />
///   en: Fuel card close information
/// </param>
/// <param name="Customer">
///   hu: Vevő adatai
///   <br />
///   en: Customer data
/// </param>
/// <param name="Cut">
///   hu: Vágás jelzése
///   <br />
///   en: Indication of cutting
/// </param>
/// <param name="Retraction">
///   hu: Visszahúzás sorok száma
///   <br />
///   en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record OrderCloseToFuelCardRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  int ResultCode,

  [property: Required]
  [property: NotEmptyString]
  string OrderId,

  string DocumentId,
  TCloseMethod CloseMethod,
  TDocumentGeneral DocumentGeneral,

  [property: Required]
  TFuelCardClose FuelCardClose,

  TCustomer Customer,
  bool Cut,

  [property: Range(0, 100)]
  int Retraction
) : IOrderRequest;

/// <summary>
///   hu: Rendelés zárása üzemanyagkártyaként válasz
///   <br />
///   en: Order close as fuel card response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
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
[PublicAPI]
public sealed record OrderCloseToFuelCardResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : IOrderResponse;