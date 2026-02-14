using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Pénzmozgás irány típus.
///   Meghatározza, hogy a pénzmozgás befizetés, kifizetés vagy fizetőeszköz csere.
///   <br />
///   en: Cash flow direction type.
///   Determines whether the cash flow is a payment in, payment out or instrument exchange.
/// </summary>
public enum TCashFlowDirection
{
  /// <summary>
  ///   hu: Befizetés (jogcím kódok: 01-08).
  ///   <br />
  ///   en: Payment in (title codes: 01-08).
  /// </summary>
  In = 0,

  /// <summary>
  ///   hu: Kifizetés (jogcím kódok: 31-42).
  ///   <br />
  ///   en: Payment out (title codes: 31-42).
  /// </summary>
  Out,

  /// <summary>
  ///   hu: Fizetőeszköz csere (jogcím kód: 60).
  ///   <br />
  ///   en: Instrument exchange (title code: 60).
  /// </summary>
  Exchange
}

/// <summary>
///   hu: Pénztári befizetés/kifizetés jogcím kódok (NAV XSD: CashPaymentTitleType).
///   01-08 befizetések, 31-42 kifizetések, 60 fizetőeszköz csere.
///   <br />
///   en: Cash payment title codes (NAV XSD: CashPaymentTitleType).
///   01-08 payments in, 31-42 payments out, 60 instrument exchange.
/// </summary>
public enum TPaymentTitleCode
{
  #region Befizetések (01-08) / Payments In..

  /// <summary>
  ///   hu: Váltópénz bevitel (01).
  ///   <br />
  ///   en: Cash coin input (01).
  /// </summary>
  CashCoinInput = 1,

  /// <summary>
  ///   hu: Pénztáros pénzfelvétel (02).
  ///   <br />
  ///   en: Withdraw by cashier - in (02).
  /// </summary>
  CashierWithdrawIn = 2,

  /// <summary>
  ///   hu: Díjbeszedés (03).
  ///   <br />
  ///   en: Collection (03).
  /// </summary>
  Collection = 3,

  /// <summary>
  ///   hu: Sorsjegy eladás (04).
  ///   <br />
  ///   en: Sale of lottery ticket (04).
  /// </summary>
  LotteryTicketSale = 4,

  /// <summary>
  ///   hu: Előleg (05).
  ///   <br />
  ///   en: Advance payment (05).
  /// </summary>
  AdvancePayment = 5,

  /// <summary>
  ///   hu: Pénztár hiány (06).
  ///   <br />
  ///   en: Cash deficit (06).
  /// </summary>
  CashDeficit = 6,

  /// <summary>
  ///   hu: Borravaló (07).
  ///   <br />
  ///   en: Tip (07).
  /// </summary>
  Tip = 7,

  /// <summary>
  ///   hu: Egyéb befizetés (08).
  ///   <br />
  ///   en: Other payment in (08).
  /// </summary>
  OtherPaymentIn = 8,

  #endregion

  #region Kifizetések (31-42) / Payments Out..

  /// <summary>
  ///   hu: Fölözés (31).
  ///   <br />
  ///   en: Skimming (31).
  /// </summary>
  Skimming = 31,

  /// <summary>
  ///   hu: Pénztáros levétel (32).
  ///   <br />
  ///   en: Withdraw by cashier - out (32).
  /// </summary>
  CashierWithdrawOut = 32,

  /// <summary>
  ///   hu: Utalvány kivét (33).
  ///   <br />
  ///   en: Voucher take out (33).
  /// </summary>
  VoucherTakeOut = 33,

  /// <summary>
  ///   hu: Ajándékkártya kivét (34).
  ///   <br />
  ///   en: Gift card take out (34).
  /// </summary>
  GiftCardTakeOut = 34,

  /// <summary>
  ///   hu: Bérkifizetés (35).
  ///   <br />
  ///   en: Salary payment (35).
  /// </summary>
  SalaryPayment = 35,

  /// <summary>
  ///   hu: Munkabér előleg (36).
  ///   <br />
  ///   en: Pre-payment of salary (36).
  /// </summary>
  SalaryPrePayment = 36,

  /// <summary>
  ///   hu: Postaköltség (37).
  ///   <br />
  ///   en: Postal cost (37).
  /// </summary>
  PostalCost = 37,

  /// <summary>
  ///   hu: Egyéb rezsi (38).
  ///   <br />
  ///   en: Other costs (38).
  /// </summary>
  OtherCosts = 38,

  /// <summary>
  ///   hu: Áruvásárlás (39).
  ///   <br />
  ///   en: Purchasing of goods (39).
  /// </summary>
  GoodsPurchase = 39,

  /// <summary>
  ///   hu: Záróösszeg levétel (40).
  ///   <br />
  ///   en: Withdraw of closing balance (40).
  /// </summary>
  ClosingBalanceWithdraw = 40,

  /// <summary>
  ///   hu: Egyéb kifizetés (41).
  ///   <br />
  ///   en: Other payment out (41).
  /// </summary>
  OtherPaymentOut = 41,

  /// <summary>
  ///   hu: Készpénzfelvétel / Cash back (42).
  ///   <br />
  ///   en: Cashback (42).
  /// </summary>
  Cashback = 42,

  #endregion

  #region Fizetőeszköz csere (60) / Instrument Exchange..

  /// <summary>
  ///   hu: Fizetőeszköz csere (60).
  ///   <br />
  ///   en: Change of payment instrument (60).
  /// </summary>
  InstrumentExchange = 60

  #endregion
}

/// <summary>
///   hu: Pénzmozgás típus. Egy pénztári befizetés/kifizetés/csere jogcímet ír le,
///   amely a NAV CashPaymentTitleType kódjaihoz kapcsolódik.
///   <br />
///   en: Cash flow type. Describes a cash payment title for payment in/out/exchange,
///   mapped to NAV CashPaymentTitleType codes.
/// </summary>
[PublicAPI]
public class TCashFlowType
{
  #region Properties..

  /// <summary>
  ///   hu: Egyedi azonosító.
  ///   <br />
  ///   en: Unique identifier.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  ///   hu: Pénzmozgás jogcím megnevezése (pl. "Váltópénz bevitel", "Fölözés").
  ///   <br />
  ///   en: Cash flow title name (e.g. "Cash coin input", "Skimming").
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  ///   hu: Pénzmozgás iránya (befizetés, kifizetés, csere).
  ///   <br />
  ///   en: Cash flow direction (in, out, exchange).
  /// </summary>
  public TCashFlowDirection Direction {
    get
    {
      if (PaymentTitleCode < TPaymentTitleCode.Skimming)
        return TCashFlowDirection.In;

      if (PaymentTitleCode < TPaymentTitleCode.InstrumentExchange)
        return TCashFlowDirection.Out;

      return TCashFlowDirection.Exchange;
    }}

  /// <summary>
  ///   hu: NAV pénzmozgás jogcím kódja (01-08 befizetés, 31-42 kifizetés, 60 csere).
  ///   <br />
  ///   en: NAV cash payment title code (01-08 payment in, 31-42 payment out, 60 exchange).
  /// </summary>
  public TPaymentTitleCode PaymentTitleCode { get; set; }

  /// <summary>
  ///   hu: Engedélyezett-e ez a pénzmozgás típus.
  ///   <br />
  ///   en: Whether this cash flow type is enabled.
  /// </summary>
  public bool Enabled { get; set; }

  #endregion

  #region Methods..

  /// <inheritdoc />
  public override string ToString()
  {
    return Name;
  }

  #endregion
}

