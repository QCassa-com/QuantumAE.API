using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Models;

/// <summary>
///   hu: Vevő adatai - támogatja a NAV XSD CustomerInfoType teljes struktúráját.
///   <br />
///   en: Customer information - supports the full NAV XSD CustomerInfoType structure.
/// </summary>
[PublicAPI]
public sealed record TCustomer
{
  /// <summary>
  ///   hu: Vevő ÁFA státusza (kötelező számlánál)
  ///   <br />
  ///   en: Customer VAT status (required for invoices)
  /// </summary>
  [NotEmptyString]
  public string? VatStatus { get; init; }

  /// <summary>
  ///   hu: Vevő neve
  ///   <br />
  ///   en: Customer name
  /// </summary>
  [StringLength(512)]
  public string? Name { get; init; }

  /// <summary>
  ///   hu: Vevő adószáma (8 jegyű vagy "12345678-2-42" formátum)
  ///   <br />
  ///   en: Customer tax number (8 digits or "12345678-2-42" format)
  /// </summary>
  [StringLength(13)]
  public string? TaxNumber { get; init; }

  /// <summary>
  ///   hu: Vevő címe (egyszerű, egysoros)
  ///   <br />
  ///   en: Customer address (simple, single line)
  /// </summary>
  [StringLength(512)]
  public string? Address { get; init; }

  /// <summary>
  ///   hu: Irányítószám (strukturált cím)
  ///   <br />
  ///   en: Postal code (structured address)
  /// </summary>
  [StringLength(10)]
  public string? PostalCode { get; init; }

  /// <summary>
  ///   hu: Település (strukturált cím)
  ///   <br />
  ///   en: City (structured address)
  /// </summary>
  [StringLength(255)]
  public string? City { get; init; }

  /// <summary>
  ///   hu: Utca, házszám (strukturált cím)
  ///   <br />
  ///   en: Street, house number (structured address)
  /// </summary>
  [StringLength(512)]
  public string? Street { get; init; }

  /// <summary>
  ///   hu: Bankszámlaszám
  ///   <br />
  ///   en: Bank account number
  /// </summary>
  [StringLength(34)]
  public string? BankAccount { get; init; }

  /// <summary>
  ///   hu: Közösségi adószám (EU)
  ///   <br />
  ///   en: Community VAT number (EU)
  /// </summary>
  [StringLength(18)]
  public string? CommunityVatNumber { get; init; }

  /// <summary>
  ///   hu: Harmadik ország adóazonosítója
  ///   <br />
  ///   en: Third state tax identification number
  /// </summary>
  [StringLength(50)]
  public string? ThirdStateTaxId { get; init; }
}
