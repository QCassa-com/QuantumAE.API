using JetBrains.Annotations;

namespace QuantumAE.Api.Controls;

/// <summary>
///   hu: XSD séma típusok.
///   <br />
///   en: XSD schema types.
/// </summary>
public enum TXsdSchemaTypeApi
{
  /// <summary>
  ///   hu: Bizonylat üzenet séma (documentMessage.xsd).
  ///   <br />
  ///   en: Document message schema (documentMessage.xsd).
  /// </summary>
  DocumentMessage = 0,

  /// <summary>
  ///   hu: Bizonylat adat séma (documentData.xsd).
  ///   <br />
  ///   en: Document data schema (documentData.xsd).
  /// </summary>
  DocumentData = 1,

  /// <summary>
  ///   hu: eReceipt API séma (eReceiptApi.xsd).
  ///   <br />
  ///   en: eReceipt API schema (eReceiptApi.xsd).
  /// </summary>
  EReceiptApi = 2,

  /// <summary>
  ///   hu: eReceipt alap séma (eReceiptBase.xsd).
  ///   <br />
  ///   en: eReceipt base schema (eReceiptBase.xsd).
  /// </summary>
  EReceiptBase = 3,

  /// <summary>
  ///   hu: Kommunikációs adat séma (communicationData.xsd).
  ///   <br />
  ///   en: Communication data schema (communicationData.xsd).
  /// </summary>
  CommunicationData = 4,

  /// <summary>
  ///   hu: Jelentés üzenet séma (reportMessage.xsd).
  ///   <br />
  ///   en: Report message schema (reportMessage.xsd).
  /// </summary>
  ReportMessage = 5
}

/// <summary>
///   hu: XSD séma frissítése.
///   <br />
///   en: Update XSD schema.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="SchemaType">
///   hu: A frissítendő séma típusa.
///   <br />
///   en: The schema type to update.
/// </param>
/// <param name="XsdContent">
///   hu: Az XSD tartalom Base64 kódolva.
///   <br />
///   en: The XSD content Base64 encoded.
/// </param>
[PublicAPI]
public record UpdateXsdSchemaRequest(
  string RequestId,
  TXsdSchemaTypeApi SchemaType,
  string XsdContent) : IControlsRequest;

/// <summary>
///   hu: XSD séma frissítés válasz.
///   <br />
///   en: Update XSD schema response.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres).
///   <br />
///   en: Result code (0 = success).
/// </param>
/// <param name="SchemaVersion">
///   hu: Az új séma verziója.
///   <br />
///   en: The new schema version.
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt).
///   <br />
///   en: Error message (if error occurred).
/// </param>
[PublicAPI]
public record UpdateXsdSchemaResponse(
  string RequestId,
  int ResultCode,
  string? SchemaVersion = null,
  string? ErrorMessage = null) : IControlsResponse;

/// <summary>
///   hu: XSD séma verziók lekérdezése.
///   <br />
///   en: Get XSD schema versions.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
[PublicAPI]
public record GetXsdVersionsRequest(string RequestId) : IControlsRequest;

/// <summary>
///   hu: XSD séma verziók válasz.
///   <br />
///   en: XSD schema versions response.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres).
///   <br />
///   en: Result code (0 = success).
/// </param>
/// <param name="Versions">
///   hu: Séma típus és verzió párosok.
///   <br />
///   en: Schema type and version pairs.
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt).
///   <br />
///   en: Error message (if error occurred).
/// </param>
[PublicAPI]
public record GetXsdVersionsResponse(
  string RequestId,
  int ResultCode,
  IReadOnlyDictionary<TXsdSchemaTypeApi, string>? Versions = null,
  string? ErrorMessage = null) : IControlsResponse;

/// <summary>
///   hu: XML validálása XSD séma alapján.
///   <br />
///   en: Validate XML against XSD schema.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="SchemaType">
///   hu: A validáláshoz használandó séma típusa.
///   <br />
///   en: The schema type to use for validation.
/// </param>
/// <param name="XmlContent">
///   hu: Az XML tartalom Base64 kódolva.
///   <br />
///   en: The XML content Base64 encoded.
/// </param>
[PublicAPI]
public record ValidateXmlRequest(
  string RequestId,
  TXsdSchemaTypeApi SchemaType,
  string XmlContent) : IControlsRequest;

/// <summary>
///   hu: XSD validációs hiba.
///   <br />
///   en: XSD validation error.
/// </summary>
/// <param name="LineNumber">
///   hu: Sor száma, ahol a hiba található.
///   <br />
///   en: Line number where the error is located.
/// </param>
/// <param name="LinePosition">
///   hu: Pozíció a soron belül.
///   <br />
///   en: Position within the line.
/// </param>
/// <param name="Message">
///   hu: Hibaüzenet.
///   <br />
///   en: Error message.
/// </param>
[PublicAPI]
public record TXsdValidationErrorApi(
  int LineNumber,
  int LinePosition,
  string Message);

/// <summary>
///   hu: XML validálás válasz.
///   <br />
///   en: Validate XML response.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres).
///   <br />
///   en: Result code (0 = success).
/// </param>
/// <param name="IsValid">
///   hu: Igaz, ha az XML érvényes.
///   <br />
///   en: True if the XML is valid.
/// </param>
/// <param name="Errors">
///   hu: Validációs hibák listája.
///   <br />
///   en: List of validation errors.
/// </param>
/// <param name="Warnings">
///   hu: Validációs figyelmeztetések listája.
///   <br />
///   en: List of validation warnings.
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha rendszerhiba történt).
///   <br />
///   en: Error message (if system error occurred).
/// </param>
[PublicAPI]
public record ValidateXmlResponse(
  string RequestId,
  int ResultCode,
  bool IsValid = false,
  IReadOnlyList<TXsdValidationErrorApi>? Errors = null,
  IReadOnlyList<TXsdValidationErrorApi>? Warnings = null,
  string? ErrorMessage = null) : IControlsResponse;
