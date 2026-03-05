using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// A single row of a loan amortization schedule showing payment breakdown and remaining balance.
/// </summary>
[OSStructure(Description = "A single row of a loan amortization schedule")]
public struct AmortizationScheduleEntry
{
    /// <summary>Period number (1-based).</summary>
    [OSStructureField(Description = "Period number", IsMandatory = true)]
    public int Period { get; set; }

    /// <summary>Total payment amount for this period.</summary>
    [OSStructureField(Description = "Total payment amount for this period", IsMandatory = true, Decimals = 2)]
    public decimal Payment { get; set; }

    /// <summary>Principal portion of the payment.</summary>
    [OSStructureField(Description = "Principal portion of the payment", IsMandatory = true, Decimals = 2)]
    public decimal PrincipalPortion { get; set; }

    /// <summary>Interest portion of the payment.</summary>
    [OSStructureField(Description = "Interest portion of the payment", IsMandatory = true, Decimals = 2)]
    public decimal InterestPortion { get; set; }

    /// <summary>Remaining loan balance after this payment.</summary>
    [OSStructureField(Description = "Remaining loan balance after this payment", IsMandatory = true, Decimals = 2)]
    public decimal RemainingBalance { get; set; }
}
