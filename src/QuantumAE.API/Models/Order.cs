using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace QuantumAE.Models;

// ====================================================================
// Közös interfészek a mintafájl szerint (OpenAPI-kompatibilis XML doc)
// ====================================================================

/// <summary>
/// hu: A kérések közös mezőit leíró interfész (mintában szereplő stílus).
/// <br/>
/// en: Interface describing common request fields (style per sample).
/// </summary>
[PublicAPI]
public interface IRequest
{
    /// <summary>
    /// hu: Kérés egyedi azonosítója
    /// <br/>
    /// en: Unique identifier of the request
    /// </summary>
    string RequestId { get; init; }
}

/// <summary>
/// hu: A válaszok közös mezőit leíró rekord (mintában szereplő stílus).
/// <br/>
/// en: Record describing common response fields (style per sample).
/// </summary>
[PublicAPI]
public interface IResponse
{
    /// <summary>
    /// hu: Kérés egyedi azonosítója
    /// <br/>
    /// en: Unique identifier of the request
    /// </summary>
    string RequestId { get; init; }

    /// <summary>
    /// hu: Eredménykód (0 = siker), különben hiba kód
    /// <br/>
    /// en: Result code (0 = success), otherwise error code
    /// </summary>
    int ResultCode { get; init; }
}

/// <summary>
/// hu: Zárások közös kéréseit leíró interfész (TCloseBase helyett).
/// <br/>
/// en: Interface describing common close requests (replaces TCloseBase).
/// </summary>
[PublicAPI]
public interface ICloseBase : IRequest
{
    /// <summary>
    /// hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
    /// <br/>
    /// en: Unique identifier of the order to be closed in the Tax Unit
    /// </summary>
    int OrderId { get; init; }
}

// ====================================================================
// ---------------------------- Open DTO-k ----------------------------
// ====================================================================

/// <summary>
/// hu: Rendelés nyitási kérés
/// <br/>
/// en: Order opening request
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record TOpenRequest(string RequestId) : IRequest;

/// <summary>
/// hu: Bizonylat nyitási válasz
/// <br/>
/// en: Document opening response
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="OrderId">
///  hu: Rendelés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the order
/// </param>
/// <param name="OpenedAt">
///  hu: A rendelés megnyitásának időpontja
///  <br/>
///  en: Timestamp of the order opening
/// </param>
[PublicAPI]
public sealed record TOpenResponse(string RequestId, int ResultCode, int OrderId, DateTime OpenedAt) : IResponse;

/// <summary>
/// hu: Tétel hozzáadási kérés
/// <br/>
/// en: Item addition request
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: Rendelés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the order
/// </param>
/// <param name="Item">
///  hu: Hozzáadandó tétel adatai
///  <br/>
///  en: Data of the item to be added
/// </param>
[PublicAPI]
public sealed record TItemAddRequest(string RequestId, int OrderId, TOrderItem Item) : IRequest;

/// <summary>
/// hu: Tétel hozzáadási válasz
/// <br/>
/// en: Item addition response
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="OrderId">
///  hu: Rendelés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the order
/// </param>
/// <param name="AddAt">
///  hu: A tétel hozzáadásának időpontja
///  <br/>
///  en: Timestamp of the item addition
/// </param>
[PublicAPI]
public sealed record  TItemAddResponse(string RequestId, int ResultCode, int OrderId, DateTime AddAt): IResponse;

/// <summary>
///  hu: Rendelés tétel adatai
///  <br/>
///  en: Order item data
/// </summary>
/// <param name="Name">
///  hu: Tétel megnevezése
///  <br/>
///  en: Item name
/// </param>
/// <param name="ArticleNo">
///  hu: Tétel cikkszáma
///  <br/>
///  en: Item article number
/// </param>
/// <param name="Unit">
///  hu: Tétel mértékegysége
///  <br/>
///  en: Item unit of measure
/// </param>
/// <param name="UnitPrice">
///  hu: Tétel egységára
///  <br/>
///  en: Item unit price
/// </param>
/// <param name="Quantity">
///  hu: Tétel mennyisége
///  <br/>
///  en: Item quantity
/// </param>
/// <param name="Cat">
///  hu: Tétel kategóriája
///  <br/>
///  en: Item category
/// </param>
/// <param name="Dept">
///  hu: Tétel osztálya
///  <br/>
///  en: Item department
/// </param>
[PublicAPI]
public sealed record TOrderItem(string Name, string ArticleNo, string Unit, decimal UnitPrice, decimal Quantity, string Cat, string Dept);

// ====================================================================
// ------------------- Close közös / specifikus DTO-k -----------------
// ====================================================================

/// <summary>
///  hu: Vevő adatai
///  <br/>
///  en: Customer information
/// </summary>
/// <param name="Name">
///  hu: Vevő neve
///  <br/>
///  en: Customer name
/// </param>
/// <param name="TaxNumber">
///  hu: Vevő adószáma
///  <br/>
///  en: Customer tax number
/// </param>
/// <param name="Address">
///  hu: Vevő címe
///  <br/>
///  en: Customer address
/// </param>
[PublicAPI]
public sealed record TCustomer(string Name, string TaxNumber, string Address);

/// <summary>
///  hu: Fizetési adatok
///  <br/>
///  en: Payment information
/// </summary>
/// <param name="Amount">
///  hu: Fizetett összeg
///  <br/>
///  en: Paid amount
/// </param>
/// <param name="Method">
///  hu: Fizetési mód
///  <br/>
///  en: Payment method
/// </param>
[PublicAPI]
public sealed record TPay(decimal Amount, string Method);

/// <summary>
///  hu: Dokumentum általános beállításai
///  <br/>
///  en: Document general settings
/// </summary>
/// <param name="BarCode">
///  hu: Vonalkód (ha van)
///  <br/>
///  en: Barcode (if any)
/// </param>
/// <param name="Copies">
///  hu: Példányszám
///  <br/>
///  en: Number of copies
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TDocumentGeneral(string? BarCode, int? Copies, bool? Cut, int? Retraction);

/// <summary>
///  hu: Visszáru adatok
///  <br/>
///  en: Return information
/// </summary>
/// <param name="Reason">
///  hu: Visszáru oka (ha van)
///  <br/>
///  en: Return reason (if any)
/// </param>
/// <param name="RefDocumentId">
///  hu: Hivatkozott bizonylat azonosítója (ha van)
///  <br/>
///  en: Referenced document identifier (if any)
/// </param>
[PublicAPI]
public sealed record TReturnInfo(string? Reason, string? RefDocumentId);

/// <summary>
///  hu: Sztornó adatok
///  <br/>
///  en: Storno information
/// </summary>
/// <param name="Reason">
///  hu: Sztornó oka (ha van)
///  <br/>
///  en: Void reason (if any)
/// </param>
/// <param name="RefDocumentId">
///  hu: Hivatkozott bizonylat azonosítója
///  <br/>
///  en: Referenced document identifier
/// </param>
[PublicAPI]
public sealed record TStornoInfo(string? Reason, string RefDocumentId);

/// <summary>
///  hu: Üzemanyagkártya zárási adatok
///  <br/>
///  en: Fuel card close information
/// </summary>
/// <param name="CardNumber">
///  hu: Kártyaszám
///  <br/>
///  en: Card number
/// </param>
/// <param name="AuthCode">
///  hu: Engedélyezési kód (ha van)
///  <br/>
///  en: Authorization code (if any)
/// </param>
[PublicAPI]
public sealed record TFuelCardClose(string CardNumber, string? AuthCode);

/// <summary>
///  hu: Nemfiskális sorok
///  <br/>
///  en: Non-fiscal rows
/// </summary>
/// <param name="Rows">
///  hu: Nemfiskális (információs) sorok listája
///  <br/>
///  en: List of non-fiscal (informational) rows
/// </param>
[PublicAPI]
public sealed record TNonFiscalRows(IReadOnlyList<string> Rows);

/// <summary>
///  hu: Zárási mód
///  <br/>
///  en: Close method
/// </summary>
[PublicAPI]
public enum TCloseMethod
{
    /// <summary>
    ///  hu: Ismeretlen zárási mód
    ///  <br/>
    ///  en: Unknown close method
    /// </summary>
    Unknown = 0,

    /// <summary>
    ///  hu: Nyomtatás zárási módként
    ///  <br/>
    ///  en: Print as close method
    /// </summary>
    Print = 1
}

/// <summary>
/// hu: Zárási válasz. Siker esetén ResultCode = 0 és a DocumentClose ki van töltve; hiba esetén ResultCode != 0 és a DocumentClose null.
/// <br/>
/// en: Close response. On success, ResultCode = 0 and DocumentClose is populated; on error, ResultCode != 0 and DocumentClose is null.
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="DocumentClose">
///  hu: Zárási adatok (ha sikeres a művelet)
///  <br/>
///  en: Close data (if the operation is successful)
/// </param>
[PublicAPI]
public sealed record TCloseResponse(string RequestId, int ResultCode, TDocumentClose? DocumentClose) : IResponse;

/// <summary>
/// hu: Zárási adatok. CloseAt: a zárás időpontja az Adóügyi Egység órája szerint.
/// <br/>
/// en: Close data. CloseAt: the closing timestamp according to the Tax Unit clock.
/// </summary>
/// <param name="CloseAt">
///  hu: A zárás időpontja (adóügyi egység órája szerint)
///  <br/>
///  en: Closing timestamp (per the Tax Unit clock)
/// </param>
[PublicAPI]
public sealed record TDocumentClose(DateTime CloseAt);

// ---- Close kérés rekordok: mind implementálja az ICloseBase-t ----

/// <summary>
/// hu: Zárási kérés – Nyugta
/// <br/>
/// en: Close request – Receipt
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárási módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="Pay">
///  hu: Fizetés adatai
///  <br/>
///  en: Payment info
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
/// <param name="ReceiptType">
///  hu: Nyugta típusa (ha van)
///  <br/>
///  en: Receipt type (if any)
/// </param>
[PublicAPI]
public sealed record TCloseReceiptRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TPay Pay, bool? Cut, int? Retraction, TReceiptType? ReceiptType
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Számla
///  <br/>
///  en: Close request – Invoice
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="Pay">
///  hu: Fizetés adatai
///  <br/>
///  en: Payment info
/// </param>
/// <param name="Customer">
///  hu: Vevő adatai
///  <br/>
///  en: Customer data
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TCloseInvoiceRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TPay Pay, TCustomer Customer, bool Cut, int Retraction
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Visszáru számla
///  <br/>
///  en: Close request – Return invoice
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="Pay">
///  hu: Fizetés adatai
///  <br/>
///  en: Payment info
/// </param>
/// <param name="Customer">
///  hu: Vevő adatai
///  <br/>
///  en: Customer data
/// </param>
/// <param name="ReturnInfo">
///  hu: Visszáru adatai
///  <br/>
///  en: Return information
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TCloseReturnRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TPay Pay, TCustomer Customer, TReturnInfo ReturnInfo, bool Cut, int Retraction
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Sztornó számla
///  <br/>
///  en: Close request – Storno invoice
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="Pay">
///  hu: Fizetés adatai
///  <br/>
///  en: Payment info
/// </param>
/// <param name="Customer">
///  hu: Vevő adatai
///  <br/>
///  en: Customer data
/// </param>
/// <param name="StornoInfo">
///  hu: Sztornó adatai
///  <br/>
///  en: Storno information
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TCloseStornoRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TPay Pay, TCustomer Customer, TStornoInfo StornoInfo, bool Cut, int Retraction
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Göngyöleg
///  <br/>
///  en: Close request – Empties
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="Pay">
///  hu: Fizetés adatai
///  <br/>
///  en: Payment info
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TCloseEmptiesRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TPay Pay, bool Cut, int Retraction
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Összesítő
///  <br/>
///  en: Close request – Summary
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="NonFiscalRows">
///  hu: Nemfiskális (információs) sorok
///  <br/>
///  en: Non-fiscal (informational) rows
/// </param>
[PublicAPI]
public sealed record TCloseSummaryRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral, TNonFiscalRows NonFiscalRows
) : ICloseBase;

/// <summary>
///  hu: Zárási kérés – Üzemanyagkártya
///  <br/>
///  en: Close request – Fuel card
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///  <br/>
///  en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
/// <param name="CloseMethod">
///  hu: Zárás módja
///  <br/>
///  en: Close method
/// </param>
/// <param name="DocumentGeneral">
///  hu: Dokumentum általános adatai
///  <br/>
///  en: Document general info
/// </param>
/// <param name="FuelCardClose">
///  hu: Üzemanyagkártya zárási adatai
///  <br/>
///  en: Fuel card close information
/// </param>
/// <param name="Customer">
///  hu: Vevő adatai
///  <br/>
///  en: Customer data
/// </param>
/// <param name="Cut">
///  hu: Vágás jelzése
///  <br/>
///  en: Indication of cutting
/// </param>
/// <param name="Retraction">
///  hu: Visszahúzás sorok száma
///  <br/>
///  en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record TCloseFuelCardRequest(
    string RequestId, int OrderId, TCloseMethod CloseMethod, TDocumentGeneral DocumentGeneral,
    TFuelCardClose FuelCardClose, TCustomer Customer, bool Cut, int Retraction
) : ICloseBase;

/// <summary>
///  hu: Nyugta típusa (ha van)
///  <br/>
///  en: Receipt type (if any)
/// </summary>
public enum TReceiptType
{
    /// <summary>
    ///  hu: Normal nyugta
    ///  <br/>
    ///  en: Normal receipt
    /// </summary>
    Default = 0
}

//
// ------------------------ Egyéb parancsok DTO-k ------------------------
//

/// <summary>
/// hu: MultiSelect kérés
/// <br/>
/// en: MultiSelect request
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: Rendelés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the order
/// </param>
[PublicAPI]
public sealed record TMultiSelectRequest(string RequestId, int OrderId) : IRequest;

/// <summary>
///  hu: Egyszerű státusz válasz
///  <br/>
///  en: Simple acknowledgment response
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
[PublicAPI]
public sealed record TSimpleAckResponse(string RequestId, int ResultCode) : IResponse;

/// <summary>
///  hu: GetIds kérés
///  <br/>
///  en: GetIds request
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record TGetIdsRequest(string RequestId) : IRequest;

/// <summary>
///  hu: GetIds válasz
///  <br/>
///  en: GetIds response
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="Count">
///  hu: Azonosítók száma
///  <br/>
///  en: Number of identifiers
/// </param>
/// <param name="Ids">
///  hu: Azonosítók listája
///  <br/>
///  en: List of identifiers
/// </param>
[PublicAPI]
public sealed record TGetIdsResponse(string RequestId, int ResultCode, int Count, IReadOnlyList<int> Ids) : IResponse;

/// <summary>
///  hu: GetItems kérés
///  <br/>
///  en: GetItems request
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///  hu: A rendelés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the order
/// </param>
[PublicAPI]
public sealed record TGetItemsRequest(string RequestId, int OrderId) : IRequest;

/// <summary>
///  hu: GetItems válasz
///  <br/>
///  en: GetItems response
/// </summary>
/// <param name="RequestId">
///  hu: Kérés egyedi azonosítója
///  <br/>
///  en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///  hu: Eredménykód (0 = siker), különben hiba kód
///  <br/>
///  en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="SellItems">
///  hu: Eladási tételek (siker esetén)
///  <br/>
///  en: Sell items (on success)
/// </param>
[PublicAPI]
public sealed record TGetItemsResponse(string RequestId, int ResultCode, TSellItems? SellItems) : IResponse;

/// <summary>
///  hu: Eladási tételek
///  <br/>
///  en: Sell items
/// </summary>
/// <param name="Items">
///  hu: Tételek listája
///  <br/>
///  en: List of items
/// </param>
[PublicAPI]
public sealed record TSellItems(IReadOnlyList<TSellItem> Items);

/// <summary>
///  hu: Tétel
///  <br/>
///  en: Item
/// </summary>
/// <param name="LineNo">
///  hu: Sorszám
///  <br/>
///  en: Line number
/// </param>
/// <param name="Name">
///  hu: Megnevezés
///  <br/>
///  en: Name
/// </param>
/// <param name="Qty">
///  hu: Mennyiség
///  <br/>
///  en: Quantity
/// </param>
/// <param name="Unit">
///  hu: Mértékegység
///  <br/>
///  en: Unit of measure
/// </param>
/// <param name="UnitPrice">
///  hu: Egységár
///  <br/>
///  en: Unit price
/// </param>
/// <param name="LineTotal">
///  hu: Sorösszeg
///  <br/>
///  en: Line total
/// </param>
[PublicAPI]
public sealed record TSellItem(int LineNo, string Name, decimal Qty, string Unit, decimal UnitPrice, decimal LineTotal);