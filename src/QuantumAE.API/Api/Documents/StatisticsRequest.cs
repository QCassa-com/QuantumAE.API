using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylat statisztikák lekérdezése
///   <br />
///   en: Get document statistics
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="FromDate">
///   hu: Kezdő dátum (ISO 8601, kötelező)
///   <br />
///   en: Start date (ISO 8601, required)
/// </param>
/// <param name="ToDate">
///   hu: Záró dátum (ISO 8601, kötelező)
///   <br />
///   en: End date (ISO 8601, required)
/// </param>
[PublicAPI]
public sealed record StatisticsRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string FromDate,

  [property: Required]
  [property: NotEmptyString]
  string ToDate
) : IDocumentRequest;

/// <summary>
///   hu: Bizonylat statisztikák válasz
///   <br />
///   en: Document statistics response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker)
///   <br />
///   en: Result code (0 = success)
/// </param>
/// <param name="Statistics">
///   hu: Statisztikák (siker esetén)
///   <br />
///   en: Statistics (in case of success)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén)
///   <br />
///   en: Error message (on error)
/// </param>
[PublicAPI]
public sealed record StatisticsResponse(
  string RequestId,
  int ResultCode,
  TDocumentStatisticsDto? Statistics = null,
  string? ErrorMessage = null
) : IDocumentResponse;

/// <summary>
///   hu: Bizonylat statisztikák DTO
///   <br />
///   en: Document statistics DTO
/// </summary>
[PublicAPI]
public sealed record TDocumentStatisticsDto
{
  /// <summary>
  ///   hu: Összes bizonylat száma
  ///   <br />
  ///   en: Total document count
  /// </summary>
  public int TotalDocuments { get; init; }

  /// <summary>
  ///   hu: Összes bruttó összeg
  ///   <br />
  ///   en: Total gross amount
  /// </summary>
  public decimal TotalGross { get; init; }

  /// <summary>
  ///   hu: Összes nettó összeg
  ///   <br />
  ///   en: Total net amount
  /// </summary>
  public decimal TotalNet { get; init; }

  /// <summary>
  ///   hu: Összes ÁFA összeg
  ///   <br />
  ///   en: Total VAT amount
  /// </summary>
  public decimal TotalVat { get; init; }

  /// <summary>
  ///   hu: Bizonylatok száma típusonként
  ///   <br />
  ///   en: Document count by type
  /// </summary>
  public Dictionary<string, int> ByType { get; init; } = new();

  /// <summary>
  ///   hu: Bizonylatok száma fizetési módonként
  ///   <br />
  ///   en: Document count by payment method
  /// </summary>
  public Dictionary<string, int> ByPaymentMethod { get; init; } = new();

  /// <summary>
  ///   hu: Bruttó összeg típusonként
  ///   <br />
  ///   en: Gross amount by type
  /// </summary>
  public Dictionary<string, decimal> GrossByType { get; init; } = new();

  /// <summary>
  ///   hu: Bruttó összeg fizetési módonként
  ///   <br />
  ///   en: Gross amount by payment method
  /// </summary>
  public Dictionary<string, decimal> GrossByPaymentMethod { get; init; } = new();
}
