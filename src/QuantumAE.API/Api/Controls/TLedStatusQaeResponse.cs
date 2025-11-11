namespace QuantumAE.Api.Controls;

/// <summary>
/// hu: Egyedi LED állapot információ
/// <br />
/// en: Individual LED status information
/// </summary>
/// <param name="Index">
/// hu: LED index (0=Zöld, 1=Sárga, 2=Piros)
/// <br />
/// en: LED index (0=Green, 1=Yellow, 2=Red)
/// </param>
/// <param name="State">
/// hu: LED állapot (Off, On, SlowBlink, FastBlink)
/// <br />
/// en: LED state (Off, On, SlowBlink, FastBlink)
/// </param>
/// <param name="IsOn">
/// hu: LED aktuálisan világít-e (villogásnál változik)
/// <br />
/// en: Is LED currently lit (changes during blinking)
/// </param>
/// <param name="Color">
/// hu: LED szín (Green, Yellow, Red)
/// <br />
/// en: LED color (Green, Yellow, Red)
/// </param>
public record TLedStatus(int Index, string State, bool IsOn, string Color);

/// <summary>
/// hu: LED-ek állapotának lekérdezése válasz
/// <br />
/// en: LED status query response
/// </summary>
/// <param name="RequestId">
/// hu: Kérés egyedi azonosítója
/// <br />
/// en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
/// hu: Eredménykód (0 = siker)
/// <br />
/// en: Result code (0 = success)
/// </param>
/// <param name="Leds">
/// hu: LED-ek állapotainak listája
/// <br />
/// en: List of LED states
/// </param>
public record TLedStatusQaeResponse(string RequestId, int ResultCode, TLedStatus[] Leds) : IQaeResponse;
