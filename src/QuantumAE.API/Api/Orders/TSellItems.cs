using System.Collections.Generic;
using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Eladási tételek
///   <br />
///   en: Sell items
/// </summary>
/// <param name="Items">
///   hu: Tételek listája
///   <br />
///   en: List of items
/// </param>
[PublicAPI]
public sealed record TSellItems(IReadOnlyList<TSellItem> Items);