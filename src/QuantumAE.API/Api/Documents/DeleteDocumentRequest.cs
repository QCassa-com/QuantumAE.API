using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylat törlése
///   <br />
///   en: Delete document
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="DocumentId">
///   hu: Törlendő bizonylat azonosítója
///   <br />
///   en: Identifier of the document to delete
/// </param>
[PublicAPI]
public sealed record DeleteDocumentRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string DocumentId
) : IDocumentRequest;

/// <summary>
///   hu: Bizonylat törlésére adott válasz
///   <br />
///   en: Response to delete document request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker, 404 = nem található)
///   <br />
///   en: Result code (0 = success, 404 = not found)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén)
///   <br />
///   en: Error message (on error)
/// </param>
[PublicAPI]
public sealed record DeleteDocumentResponse(
  string RequestId,
  int ResultCode,
  string? ErrorMessage = null
) : IDocumentResponse;
