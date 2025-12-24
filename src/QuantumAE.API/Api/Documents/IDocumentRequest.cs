using JetBrains.Annotations;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylat lekérdezés kérés alap interfész
///   <br />
///   en: Document query request base interface
/// </summary>
[PublicAPI]
public interface IDocumentRequest : IQaeRequest
{
}

/// <summary>
///   hu: Bizonylat lekérdezés válasz alap interfész
///   <br />
///   en: Document query response base interface
/// </summary>
[PublicAPI]
public interface IDocumentResponse : IQaeResponse
{
}
