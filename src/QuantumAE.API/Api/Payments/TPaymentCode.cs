using JetBrains.Annotations;

namespace QuantumAE.Api.Payments;

/// <summary>
///   hu: NAV-I fizetőeszköz kódok. Minden TPaymentType pontosan egy kódra képeződik.
///   A NAV XML generáláskor a kód határozza meg, hogy melyik XSD mezőbe kerül az összeg.
///   <br />
///   en: NAV-I payment instrument codes. Each TPaymentType maps to exactly one code.
///   During NAV XML generation, the code determines which XSD field receives the amount.
/// </summary>
[PublicAPI]
public enum TPaymentCode
{
  /// <summary>
  ///   hu: Forint készpénz → cashHufAmount
  ///   <br />
  ///   en: Cash in HUF → cashHufAmount
  /// </summary>
  CASH,

  /// <summary>
  ///   hu: Bankkártya → cardPaymentAmount
  ///   <br />
  ///   en: Card payment → cardPaymentAmount
  /// </summary>
  CARD,

  /// <summary>
  ///   hu: SZÉP kártya → szepCardAmount
  ///   <br />
  ///   en: SZÉP card → szepCardAmount
  /// </summary>
  SZEP,

  /// <summary>
  ///   hu: Azonnali fizetési rendszer → afrAmount
  ///   <br />
  ///   en: Immediate payment system → afrAmount
  /// </summary>
  AFR,

  /// <summary>
  ///   hu: Ajándékutalvány → otherPayment "AJÁND"
  ///   <br />
  ///   en: Gift voucher → otherPayment "AJÁND"
  /// </summary>
  AJAND,

  /// <summary>
  ///   hu: Hűségpont → otherPayment "HŰSÉG"
  ///   <br />
  ///   en: Loyalty points → otherPayment "HŰSÉG"
  /// </summary>
  HUSEG,

  /// <summary>
  ///   hu: Smart fizetés → otherPayment "SMART"
  ///   <br />
  ///   en: Smart payment → otherPayment "SMART"
  /// </summary>
  SMART,

  /// <summary>
  ///   hu: Göngyöleg → otherPayment "GÖNGY"
  ///   <br />
  ///   en: Empties/deposit → otherPayment "GÖNGY"
  /// </summary>
  GONGY,

  /// <summary>
  ///   hu: Kupon → otherPayment "KUPON"
  ///   <br />
  ///   en: Coupon → otherPayment "KUPON"
  /// </summary>
  KUPON
}
