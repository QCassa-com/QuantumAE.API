using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylat lekérdezése azonosító alapján
///   <br />
///   en: Get document by identifier
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="DocumentId">
///   hu: Bizonylat azonosító
///   <br />
///   en: Document identifier
/// </param>
[PublicAPI]
public sealed record GetDocumentRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string DocumentId
) : IDocumentRequest;

/// <summary>
///   hu: Bizonylat lekérdezésére adott válasz
///   <br />
///   en: Response to get document request
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
/// <param name="Document">
///   hu: Bizonylat adatai (siker esetén)
///   <br />
///   en: Document data (in case of success)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén)
///   <br />
///   en: Error message (on error)
/// </param>
[PublicAPI]
public sealed record GetDocumentResponse(
  string RequestId,
  int ResultCode,
  TDocumentDto? Document = null,
  string? ErrorMessage = null
) : IDocumentResponse;

/// <summary>
///   hu: Bizonylat DTO (Data Transfer Object) a lekérdezésekhez
///   <br />
///   en: Document DTO (Data Transfer Object) for queries
/// </summary>
[PublicAPI]
public sealed record TDocumentDto
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
  ///   hu: Rendelés azonosító
  ///   <br />
  ///   en: Order identifier
  /// </summary>
  public string OrderId { get; init; } = "";

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
  ///   hu: Nettó végösszeg
  ///   <br />
  ///   en: Total net amount
  /// </summary>
  public decimal TotalNet { get; init; }

  /// <summary>
  ///   hu: ÁFA összeg
  ///   <br />
  ///   en: Total VAT amount
  /// </summary>
  public decimal TotalVat { get; init; }

  /// <summary>
  ///   hu: Fizetési mód
  ///   <br />
  ///   en: Payment method
  /// </summary>
  public string PaymentMethod { get; init; } = "";

  /// <summary>
  ///   hu: Pénznem
  ///   <br />
  ///   en: Currency
  /// </summary>
  public string Currency { get; init; } = "HUF";

  /// <summary>
  ///   hu: NAV ellenőrző kód
  ///   <br />
  ///   en: NTCA control code
  /// </summary>
  public string? NtcaControlCode { get; init; }

  /// <summary>
  ///   hu: NAV-nak elküldve
  ///   <br />
  ///   en: Sent to NAV
  /// </summary>
  public bool SentToNav { get; init; }

  /// <summary>
  ///   hu: Offline mentve
  ///   <br />
  ///   en: Saved offline
  /// </summary>
  public bool SavedOffline { get; init; }

  /// <summary>
  ///   hu: Vevő neve
  ///   <br />
  ///   en: Customer name
  /// </summary>
  public string? CustomerName { get; init; }

  /// <summary>
  ///   hu: Vevő adószáma
  ///   <br />
  ///   en: Customer tax number
  /// </summary>
  public string? CustomerTaxNumber { get; init; }

  /// <summary>
  ///   hu: Vevő címe
  ///   <br />
  ///   en: Customer address
  /// </summary>
  public string? CustomerAddress { get; init; }

  /// <summary>
  ///   hu: Üzlet neve
  ///   <br />
  ///   en: Shop name
  /// </summary>
  public string? ShopName { get; init; }

  /// <summary>
  ///   hu: AP szám
  ///   <br />
  ///   en: AP number
  /// </summary>
  public string APNumber { get; init; } = "";

  /// <summary>
  ///   hu: Létrehozás időpontja
  ///   <br />
  ///   en: Created at
  /// </summary>
  public string CreatedAtUtc { get; init; } = "";

  /// <summary>
  ///   hu: Tételek
  ///   <br />
  ///   en: Items
  /// </summary>
  public List<TDocumentItemDto> Items { get; init; } = [];
}

/// <summary>
///   hu: Bizonylat tétel DTO
///   <br />
///   en: Document item DTO
/// </summary>
[PublicAPI]
public sealed record TDocumentItemDto
{
  /// <summary>
  ///   hu: Tétel sorszáma
  ///   <br />
  ///   en: Line number
  /// </summary>
  public int LineNumber { get; init; }

  /// <summary>
  ///   hu: Tétel neve
  ///   <br />
  ///   en: Item name
  /// </summary>
  public string ItemName { get; init; } = "";

  /// <summary>
  ///   hu: Mennyiség
  ///   <br />
  ///   en: Quantity
  /// </summary>
  public decimal Quantity { get; init; }

  /// <summary>
  ///   hu: Mértékegység
  ///   <br />
  ///   en: Unit of measure
  /// </summary>
  public string UnitOfMeasure { get; init; } = "";

  /// <summary>
  ///   hu: Egységár
  ///   <br />
  ///   en: Unit price
  /// </summary>
  public decimal UnitPrice { get; init; }

  /// <summary>
  ///   hu: Nettó összeg
  ///   <br />
  ///   en: Net amount
  /// </summary>
  public decimal NetAmount { get; init; }

  /// <summary>
  ///   hu: ÁFA összeg
  ///   <br />
  ///   en: VAT amount
  /// </summary>
  public decimal VatAmount { get; init; }

  /// <summary>
  ///   hu: Bruttó összeg
  ///   <br />
  ///   en: Gross amount
  /// </summary>
  public decimal GrossAmount { get; init; }

  /// <summary>
  ///   hu: ÁFA kulcs
  ///   <br />
  ///   en: VAT rate
  /// </summary>
  public string VatRate { get; init; } = "";

  /// <summary>
  ///   hu: Tétel jellege
  ///   <br />
  ///   en: Item nature
  /// </summary>
  public string ItemNature { get; init; } = "";
}
