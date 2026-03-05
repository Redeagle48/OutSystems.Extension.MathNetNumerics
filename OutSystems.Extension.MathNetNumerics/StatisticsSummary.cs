using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Descriptive statistics summary of a dataset including central tendency, dispersion, and shape measures.
/// </summary>
[OSStructure(Description = "Descriptive statistics summary of a dataset")]
public struct StatisticsSummary
{
    /// <summary>Minimum value in the dataset.</summary>
    [OSStructureField(Description = "Minimum value", IsMandatory = true)]
    public double Min { get; set; }

    /// <summary>Maximum value in the dataset.</summary>
    [OSStructureField(Description = "Maximum value", IsMandatory = true)]
    public double Max { get; set; }

    /// <summary>Arithmetic mean (average).</summary>
    [OSStructureField(Description = "Arithmetic mean", IsMandatory = true)]
    public double Mean { get; set; }

    /// <summary>Median value (50th percentile).</summary>
    [OSStructureField(Description = "Median value", IsMandatory = true)]
    public double Median { get; set; }

    /// <summary>Sample standard deviation.</summary>
    [OSStructureField(Description = "Sample standard deviation", IsMandatory = true)]
    public double StandardDeviation { get; set; }

    /// <summary>Sample variance.</summary>
    [OSStructureField(Description = "Sample variance", IsMandatory = true)]
    public double Variance { get; set; }

    /// <summary>Skewness (measure of asymmetry).</summary>
    [OSStructureField(Description = "Skewness (measure of asymmetry)", IsMandatory = true)]
    public double Skewness { get; set; }

    /// <summary>Kurtosis (measure of tail heaviness).</summary>
    [OSStructureField(Description = "Kurtosis (measure of tail heaviness)", IsMandatory = true)]
    public double Kurtosis { get; set; }
}
