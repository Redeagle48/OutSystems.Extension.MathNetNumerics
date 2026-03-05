using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Descriptive statistics and correlation analysis.
/// Exposed as the <c>MathNet_Statistics</c> module in OutSystems Developer Cloud (ODC).
/// All operations delegate to MathNet.Numerics.Statistics.
/// </summary>
[OSInterface(Name = "MathNet_Statistics", Description = "Descriptive statistics and correlation analysis", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IStatistics
{
    /// <summary>
    /// Computes the arithmetic mean (average) of a list of values.
    /// </summary>
    /// <param name="values">List of numeric values. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The arithmetic mean.</returns>
    /// <exception cref="ArgumentNullException">Thrown when values is null.</exception>
    /// <exception cref="ArgumentException">Thrown when values is empty.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the arithmetic mean of a list of values")]
    double Mean(List<double> values);

    /// <summary>
    /// Computes the median (middle value) of a list of values.
    /// For even-length lists, returns the average of the two middle values.
    /// </summary>
    /// <param name="values">List of numeric values. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The median value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when values is null.</exception>
    /// <exception cref="ArgumentException">Thrown when values is empty.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the median of a list of values")]
    double Median(List<double> values);

    /// <summary>
    /// Computes the sample variance (unbiased, using N-1 denominator) of a list of values.
    /// </summary>
    /// <param name="values">List of numeric values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <returns>The sample variance.</returns>
    /// <exception cref="ArgumentException">Thrown when values has fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the sample variance of a list of values")]
    double Variance(List<double> values);

    /// <summary>
    /// Computes the sample standard deviation (square root of sample variance) of a list of values.
    /// </summary>
    /// <param name="values">List of numeric values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <returns>The sample standard deviation.</returns>
    /// <exception cref="ArgumentException">Thrown when values has fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the sample standard deviation of a list of values")]
    double StandardDeviation(List<double> values);

    /// <summary>
    /// Computes the skewness (measure of distribution asymmetry) of a list of values.
    /// Positive skewness indicates a right-skewed distribution; negative indicates left-skewed.
    /// </summary>
    /// <param name="values">List of numeric values. Must have at least 3 elements. Maximum 10,000,000 elements.</param>
    /// <returns>The skewness value. Zero indicates a symmetric distribution.</returns>
    /// <exception cref="ArgumentException">Thrown when values has fewer than 3 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the skewness of a list of values")]
    double Skewness(List<double> values);

    /// <summary>
    /// Computes the kurtosis (measure of tail heaviness) of a list of values.
    /// Higher kurtosis indicates heavier tails relative to a normal distribution.
    /// </summary>
    /// <param name="values">List of numeric values. Must have at least 4 elements. Maximum 10,000,000 elements.</param>
    /// <returns>The excess kurtosis value. A normal distribution has excess kurtosis of 0.</returns>
    /// <exception cref="ArgumentException">Thrown when values has fewer than 4 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes the kurtosis of a list of values")]
    double Kurtosis(List<double> values);

    /// <summary>
    /// Computes a specific percentile of a list of values using linear interpolation.
    /// The 50th percentile equals the median.
    /// </summary>
    /// <param name="values">List of numeric values. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <param name="percentile">Percentile to compute, between 0 and 100 inclusive.</param>
    /// <returns>The value at the specified percentile.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when percentile is outside [0, 100].</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes a specific percentile (0-100) of a list of values")]
    double Percentile(List<double> values, int percentile);

    /// <summary>
    /// Computes the Pearson product-moment correlation coefficient between two datasets.
    /// Returns a value between -1 (perfect negative correlation) and +1 (perfect positive correlation).
    /// </summary>
    /// <param name="valuesA">First dataset. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="valuesB">Second dataset. Must have the same length as <paramref name="valuesA"/>.</param>
    /// <returns>The Pearson correlation coefficient in the range [-1, +1].</returns>
    /// <exception cref="ArgumentException">Thrown when datasets have different lengths or fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes Pearson correlation coefficient between two datasets")]
    double PearsonCorrelation(List<double> valuesA, List<double> valuesB);

    /// <summary>
    /// Returns the minimum value from a list.
    /// </summary>
    /// <param name="values">List of numeric values. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The smallest value in the list.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Returns the minimum value from a list")]
    double Min(List<double> values);

    /// <summary>
    /// Returns the maximum value from a list.
    /// </summary>
    /// <param name="values">List of numeric values. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The largest value in the list.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Returns the maximum value from a list")]
    double Max(List<double> values);

    /// <summary>
    /// Computes a full descriptive statistics summary in a single call: min, max, mean, median,
    /// standard deviation, variance, skewness, and kurtosis.
    /// </summary>
    /// <param name="values">List of numeric values. Must have at least 4 elements. Maximum 10,000,000 elements.</param>
    /// <returns>A <see cref="StatisticsSummary"/> structure containing all 8 statistical measures.</returns>
    /// <exception cref="ArgumentException">Thrown when values has fewer than 4 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Returns min, max, mean, median, stddev, variance, skewness, kurtosis as a structure")]
    StatisticsSummary Summary(List<double> values);
}
