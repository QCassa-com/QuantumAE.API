using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Technikai tájékoztatás lekérdezési kérés - a NAV-tól kapott legutóbbi technikai üzenet lekérdezése.
///   <br />
///   en: Technical info query request - query the latest technical message received from NAV.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record GetTechnicalInfoRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Technikai tájékoztatás lekérdezési válasz - a NAV-tól kapott legutóbbi PrintMessage szöveg.
///   A szövegben szereplő "\n" karaktersorozatot sortörésként kell értelmezni.
///   <br />
///   en: Technical info query response - the latest PrintMessage text received from NAV.
///   The "\n" character sequence in the text should be interpreted as a newline.
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
/// <param name="PrintMessage">
///   hu: A NAV-tól kapott technikai tájékoztatás szövege (a "\n" sortörést jelent)
///   <br />
///   en: Technical info text received from NAV ("\n" means newline)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record GetTechnicalInfoResponse(
  string RequestId,
  int ResultCode,
  string? PrintMessage = null,
  string? ErrorMessage = null
) : INavIResponse;
