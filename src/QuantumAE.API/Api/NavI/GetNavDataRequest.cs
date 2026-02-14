using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: NAV adat lekérdezési kérés - az aktuális ÁFA kulcsok és üzemeltetői adatok lekérdezése.
///   <br />
///   en: NAV data query request - query current VAT rates and operator site data.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record GetNavDataRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: ÁFA csoport DTO az API rétegben.
///   <br />
///   en: VAT group DTO in the API layer.
/// </summary>
/// <param name="CollectorCode">
///   hu: Forgalmi gyűjtő jele (3 karakter, pl. "A  ", "B  ")
///   <br />
///   en: Collector code (3 characters, e.g., "A  ", "B  ")
/// </param>
/// <param name="VatPercentage">
///   hu: ÁFA kulcs százalékban
///   <br />
///   en: VAT rate in percentage
/// </param>
/// <param name="VatContent">
///   hu: ÁFA tartalom százalékban
///   <br />
///   en: VAT content in percentage
/// </param>
/// <param name="VatLabel">
///   hu: ÁFA kulcs megjelenítési neve (pl. "27%")
///   <br />
///   en: VAT rate display label (e.g., "27%")
/// </param>
[PublicAPI]
public sealed record TVatGroupDto(
  string? CollectorCode,
  decimal? VatPercentage,
  decimal? VatContent,
  string? VatLabel
);

/// <summary>
///   hu: NAV adat lekérdezési válasz - aktuális ÁFA kulcsok és üzemeltetői adatok.
///   <br />
///   en: NAV data query response - current VAT rates and operator site data.
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
/// <param name="VatGroups">
///   hu: ÁFA csoportok listája
///   <br />
///   en: List of VAT groups
/// </param>
/// <param name="OperatorName">
///   hu: Üzemeltető neve
///   <br />
///   en: Operator name
/// </param>
/// <param name="OperatingSiteName">
///   hu: Üzlet (működési hely) neve
///   <br />
///   en: Operating site (shop) name
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record GetNavDataResponse(
  string RequestId,
  int ResultCode,
  TVatGroupDto[]? VatGroups = null,
  string? OperatorName = null,
  string? OperatingSiteName = null,
  string? ErrorMessage = null
) : INavIResponse;
