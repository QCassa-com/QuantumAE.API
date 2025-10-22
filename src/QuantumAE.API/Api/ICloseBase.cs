using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///   hu: Zárások közös kéréseit leíró interfész (TCloseBase helyett).
///   <br />
///   en: Interface describing common close requests (replaces TCloseBase).
/// </summary>
[PublicAPI]
public interface ICloseBase : IRequest
{
  /// <summary>
  ///   hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
  ///   <br />
  ///   en: Unique identifier of the order to be closed in the Tax Unit
  /// </summary>
  string OrderId { get; init; }

  /// <summary>
  ///   hu: Az lezáráskor keletkező dokumentum egyedi azonosítója (ReceipId, InvoiceId, ReturnInvoiceId, StornoId, stb.)
  ///   <br />
  ///   en: Unique identifier of the document generated after the close (ReceipId, InvoiceId, ReturnInvoiceId, StornoId,
  ///   stb.)
  /// </summary>
  string DocumentId { get; init; }
}