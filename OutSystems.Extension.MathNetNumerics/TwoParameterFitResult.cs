using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Result of a two-parameter curve fit (exponential: y = A * e^(B*x), power: y = A * x^B).
/// </summary>
[OSStructure(Description = "Result of a two-parameter curve fit (exponential: y=A*e^(Bx), power: y=A*x^B)")]
public struct TwoParameterFitResult
{
    /// <summary>Coefficient A (multiplier).</summary>
    [OSStructureField(Description = "Coefficient A (multiplier)", IsMandatory = true)]
    public double A { get; set; }

    /// <summary>Exponent B (rate or power).</summary>
    [OSStructureField(Description = "Exponent B (rate or power)", IsMandatory = true)]
    public double B { get; set; }
}
