using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Egy rendelés dokumentumot reprezentáló osztály.
///   <br />
///   en: A class representing an order document.
/// </summary>
/// <param name="OrderNumber">
///   hu: A rendelés száma.
///   <br />
///   en: The order number.
/// </param>
[PublicAPI]
public record TOrder(string OrderNumber);

/// <summary>
///   hu: Egy nyugta dokumentumot reprezentáló osztály.
///   <br />
///   en: A class representing a receipt document.
/// </summary>
/// <param name="ReceiptNumber">
///   hu: A nyugta száma.
///   <br />
///   en: The receipt number.
/// </param>
[PublicAPI]
public record TReceipt(string ReceiptNumber);

/// <summary>
///   hu: Egy számla dokumentumot reprezentáló osztály.
///   <br />
///   en: A class representing an invoice document.
/// </summary>
/// <param name="InvoiceNumber">
///   hu: A számla száma.
///   <br />
///   en: The invoice number.
/// </param>
[PublicAPI]
public record TInvoice(string InvoiceNumber);