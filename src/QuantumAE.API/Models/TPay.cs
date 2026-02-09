using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Fizetési adatok - támogatja az egyszerű és kevert fizetést is.
///   <br />
///   en: Payment information - supports both simple and mixed payments.
/// </summary>
[PublicAPI]
public sealed record TPayment
{
  /// <summary>
  ///   hu: Készpénz összeg (HUF)
  ///   <br />
  ///   en: Cash amount (HUF)
  /// </summary>
  public int? Cash { get; init; }

  /// <summary>
  ///   hu: Bankkártya összeg (HUF)
  ///   <br />
  ///   en: Card payment amount (HUF)
  /// </summary>
  public int? Card { get; init; }

  /// <summary>
  ///   hu: SZÉP kártya összeg (HUF)
  ///   <br />
  ///   en: SZÉP card amount (HUF)
  /// </summary>
  public int? Szep { get; init; }

  /// <summary>
  ///   hu: Azonnali fizetési rendszer összeg (HUF)
  ///   <br />
  ///   en: Instant payment (AFR) amount (HUF)
  /// </summary>
  public int? Afr { get; init; }

  /// <summary>
  ///   hu: Egyszerű fizetési összeg (visszafelé kompatibilis)
  ///   <br />
  ///   en: Simple payment amount (backward compatible)
  /// </summary>
  public decimal? Amount { get; init; }

  /// <summary>
  ///   hu: Egyszerű fizetési mód (visszafelé kompatibilis)
  ///   <br />
  ///   en: Simple payment method (backward compatible)
  /// </summary>
  public string? Method { get; init; }
}
