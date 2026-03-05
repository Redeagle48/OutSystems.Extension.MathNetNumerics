using MathNet.Numerics.Statistics;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements descriptive statistics, correlation, and summary computations.
/// </summary>
public class StatisticsActions : IStatistics
{
    /// <inheritdoc />
    public double Mean(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        return values.Mean();
    }

    /// <inheritdoc />
    public double Median(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        return values.Median();
    }

    /// <inheritdoc />
    public double Variance(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateMinCount(values, 2, nameof(values));
        return values.Variance();
    }

    /// <inheritdoc />
    public double StandardDeviation(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateMinCount(values, 2, nameof(values));
        return values.StandardDeviation();
    }

    /// <inheritdoc />
    public double Skewness(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateMinCount(values, 3, nameof(values));
        return values.Skewness();
    }

    /// <inheritdoc />
    public double Kurtosis(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateMinCount(values, 4, nameof(values));
        return values.Kurtosis();
    }

    /// <inheritdoc />
    public double Percentile(List<double> values, int percentile)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateRange(percentile, 0, 100, nameof(percentile));
        return values.Percentile(percentile);
    }

    /// <inheritdoc />
    public double PearsonCorrelation(List<double> valuesA, List<double> valuesB)
    {
        MathHelper.ValidateNotNullOrEmpty(valuesA, nameof(valuesA));
        MathHelper.ValidateNotNullOrEmpty(valuesB, nameof(valuesB));
        MathHelper.ValidateMatchingLengths(valuesA, nameof(valuesA), valuesB, nameof(valuesB));
        MathHelper.ValidateMinCount(valuesA, 2, nameof(valuesA));
        return Correlation.Pearson(valuesA, valuesB);
    }

    /// <inheritdoc />
    public double Min(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        return values.Minimum();
    }

    /// <inheritdoc />
    public double Max(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        return values.Maximum();
    }

    /// <inheritdoc />
    public StatisticsSummary Summary(List<double> values)
    {
        MathHelper.ValidateNotNullOrEmpty(values, nameof(values));
        MathHelper.ValidateMinCount(values, 4, nameof(values));
        var stats = new DescriptiveStatistics(values);
        return new StatisticsSummary
        {
            Min = stats.Minimum,
            Max = stats.Maximum,
            Mean = stats.Mean,
            Median = values.Median(),
            StandardDeviation = stats.StandardDeviation,
            Variance = stats.Variance,
            Skewness = stats.Skewness,
            Kurtosis = stats.Kurtosis
        };
    }
}
