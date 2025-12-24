using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylatok listázása szűrőkkel
///   <br />
///   en: List documents with filters
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="DocumentType">
///   hu: Szűrés bizonylat típusra (opcionális)
///   <br />
///   en: Filter by document type (optional)
/// </param>
/// <param name="FromDate">
///   hu: Kezdő dátum (ISO 8601, opcionális)
///   <br />
///   en: Start date (ISO 8601, optional)
/// </param>
/// <param name="ToDate">
///   hu: Záró dátum (ISO 8601, opcionális)
///   <br />
///   en: End date (ISO 8601, optional)
/// </param>
/// <param name="Limit">
///   hu: Maximum visszaadandó elemek száma (alapértelmezett: 100)
///   <br />
///   en: Maximum number of items to return (default: 100)
/// </param>
/// <param name="Offset">
///   hu: Kihagyandó elemek száma (lapozáshoz)
///   <br />
///   en: Number of items to skip (for pagination)
/// </param>
[PublicAPI]
public sealed record ListDocumentsRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  string? DocumentType = null,
  string? FromDate = null,
  string? ToDate = null,

  [property: Range(1, 1000)]
  int Limit = 100,

  [property: Range(0, int.MaxValue)]
  int Offset = 0
) : IDocumentRequest;

/// <summary>
///   hu: Bizonylatok listázására adott válasz
///   <br />
///   en: Response to list documents request
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
/// <param name="Documents">
///   hu: Bizonylatok listája
///   <br />
///   en: List of documents
/// </param>
/// <param name="TotalCount">
///   hu: Összes találat száma (lapozáshoz)
///   <br />
///   en: Total count of matches (for pagination)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén)
///   <br />
///   en: Error message (on error)
/// </param>
[PublicAPI]
public sealed record ListDocumentsResponse(
  string RequestId,
  int ResultCode,
  IReadOnlyList<TDocumentSummaryDto> Documents,
  int TotalCount,
  string? ErrorMessage = null
) : IDocumentResponse;

/// <summary>
///   hu: Bizonylat összefoglaló DTO (listázáshoz)
///   <br />
///   en: Document summary DTO (for listing)
/// </summary>
[PublicAPI]
public sealed record TDocumentSummaryDto
{
  /// <summary>
  ///   hu: Bizonylat azonosító
  ///   <br />
  ///   en: Document identifier
  /// </summary>
  public string DocumentId { get; init; } = "";

  /// <summary>
  ///   hu: Bizonylat típusa
  ///   <br />
  ///   en: Document type
  /// </summary>
  public string DocumentType { get; init; } = "";

  /// <summary>
  ///   hu: Kiállítás dátuma
  ///   <br />
  ///   en: Issue date
  /// </summary>
  public string IssueDate { get; init; } = "";

  /// <summary>
  ///   hu: Bruttó végösszeg
  ///   <br />
  ///   en: Total gross amount
  /// </summary>
  public decimal TotalGross { get; init; }

  /// <summary>
  ///   hu: Fizetési mód
  ///   <br />
  ///   en: Payment method
  /// </summary>
  public string PaymentMethod { get; init; } = "";

  /// <summary>
  ///   hu: NAV-nak elküldve
  ///   <br />
  ///   en: Sent to NAV
  /// </summary>
  public bool SentToNav { get; init; }
}
