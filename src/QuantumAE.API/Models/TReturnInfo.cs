using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Visszáru adatok
///   <br />
///   en: Return information
/// </summary>
/// <param name="Reason">
///   hu: Visszáru oka (ha van)
///   <br />
///   en: Return reason (if any)
/// </param>
/// <param name="RefDocumentId">
///   hu: Hivatkozott bizonylat azonosítója (ha van)
///   <br />
///   en: Referenced document identifier (if any)
/// </param>
[PublicAPI]
public sealed record TReturnInfo(string? Reason, string? RefDocumentId);
