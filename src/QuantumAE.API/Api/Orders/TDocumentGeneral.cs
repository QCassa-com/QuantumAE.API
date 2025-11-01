using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Dokumentum általános beállításai
///   <br />
///   en: Document general settings
/// </summary>
/// <param name="BarCode">
///   hu: Vonalkód (ha van)
///   <br />
///   en: Barcode (if any)
/// </param>
/// <param name="Copies">
///   hu: Példányszám
///   <br />
///   en: Number of copies
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
public sealed record TDocumentGeneral(string? BarCode, int? Copies, bool? Cut, int? Retraction);