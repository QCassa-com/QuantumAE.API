using JetBrains.Annotations;

namespace QuantumAE.Api.Reports;

/// <summary>
///   hu: Riport kérés interfész.
///   <br />
///   en: Report request interface.
/// </summary>
[PublicAPI]
public interface IReportRequest : IQaeRequest;

/// <summary>
///   hu: Riport válasz interfész.
///   <br />
///   en: Report response interface.
/// </summary>
[PublicAPI]
public interface IReportResponse : IQaeResponse;
