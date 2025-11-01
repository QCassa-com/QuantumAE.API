using System;
using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Zárási adatok. CloseAt: a zárás időpontja az Adóügyi Egység órája szerint.
///   <br />
///   en: Close data. CloseAt: the closing timestamp according to the Tax Unit clock.
/// </summary>
/// <param name="CloseAt">
///   hu: A zárás időpontja (adóügyi egység órája szerint)
///   <br />
///   en: Closing timestamp (per the Tax Unit clock)
/// </param>
[PublicAPI]
public sealed record TDocumentClose(DateTime CloseAt);