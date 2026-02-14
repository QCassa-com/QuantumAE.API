using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Terméktörzs lekérdezési kérés - termék keresése vonalkód/EAN kód alapján a NAV DTSZK adatbázisából.
///   <br />
///   en: Product catalog query request - search product by barcode/EAN code from NAV DTSZK database.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ProductCode">
///   hu: Termékkód (EAN, GS1, vonalkód, stb.) - min. 5 alfanumerikus karakter
///   <br />
///   en: Product code (EAN, GS1, barcode, etc.) - min. 5 alphanumeric characters
/// </param>
[PublicAPI]
public sealed record DownloadProductListRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  [property: StringLength(100, MinimumLength = 5)]
  string ProductCode
) : INavIRequest;

/// <summary>
///   hu: Terméktörzs lekérdezési válasz - a NAV DTSZK adatbázisából kapott termék adatok.
///   <br />
///   en: Product catalog query response - product data from the NAV DTSZK database.
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
/// <param name="Products">
///   hu: Termék lista (ha van találat)
///   <br />
///   en: Product list (if any results)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record DownloadProductListResponse(
  string RequestId,
  int ResultCode,
  List<TProductDto>? Products = null,
  string? ErrorMessage = null
) : INavIResponse;

/// <summary>
///   hu: Termék DTO - a NAV DTSZK adatbázisából származó termékinformáció az API rétegben.
///   <br />
///   en: Product DTO - product information from the NAV DTSZK database in the API layer.
/// </summary>
/// <param name="DtszkId">
///   hu: DTSZK azonosító
///   <br />
///   en: DTSZK identifier
/// </param>
/// <param name="ProductId">
///   hu: Terméken feltüntetett azonosító (EAN vonalkód)
///   <br />
///   en: Product identifier shown on the product (EAN barcode)
/// </param>
/// <param name="ProductName">
///   hu: Termék megnevezése
///   <br />
///   en: Product name
/// </param>
/// <param name="ProductManufacturer">
///   hu: Gyártó megnevezése
///   <br />
///   en: Manufacturer name
/// </param>
/// <param name="UnitOfMeasure">
///   hu: Mennyiségi egység
///   <br />
///   en: Unit of measure
/// </param>
/// <param name="Vtsz">
///   hu: Vámtarifa szám / VTSZ kód
///   <br />
///   en: Customs tariff number / VTSZ code
/// </param>
/// <param name="CategoryCode">
///   hu: Kategória kód
///   <br />
///   en: Category code
/// </param>
/// <param name="CategoryName">
///   hu: Kategória megnevezés
///   <br />
///   en: Category name
/// </param>
/// <param name="State">
///   hu: Termék adat státusza (ACTIVE)
///   <br />
///   en: Product data state (ACTIVE)
/// </param>
[PublicAPI]
public sealed record TProductDto(
  string? DtszkId,
  string? ProductId,
  string? ProductName,
  string? ProductManufacturer = null,
  string? UnitOfMeasure = null,
  string? Vtsz = null,
  string? CategoryCode = null,
  string? CategoryName = null,
  string? State = null
);
