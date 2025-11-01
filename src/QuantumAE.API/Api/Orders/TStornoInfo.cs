using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Sztornó adatok
///   <br />
///   en: Storno information
/// </summary>
/// <param name="Reason">
///   hu: Sztornó oka (ha van)
///   <br />
///   en: Void reason (if any)
/// </param>
/// <param name="RefDocumentId">
///   hu: Hivatkozott bizonylat azonosítója
///   <br />
///   en: Referenced document identifier
/// </param>
[PublicAPI]
public sealed record TStornoInfo(string? Reason, string RefDocumentId);