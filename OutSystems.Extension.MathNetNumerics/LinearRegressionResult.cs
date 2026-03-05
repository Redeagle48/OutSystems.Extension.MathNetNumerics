using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Result of a linear regression model y = Intercept + Slope * x.
/// </summary>
[OSStructure(Description = "Result of a linear regression y = Intercept + Slope * x")]
public struct LinearRegressionResult
{
    /// <summary>Y-intercept of the regression line.</summary>
    [OSStructureField(Description = "Y-intercept of the regression line", IsMandatory = true)]
    public double Intercept { get; set; }

    /// <summary>Slope of the regression line.</summary>
    [OSStructureField(Description = "Slope of the regression line", IsMandatory = true)]
    public double Slope { get; set; }
}
