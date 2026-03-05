using MathNet.Numerics.Distributions;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements probability distribution functions for Normal, Exponential, Poisson, Binomial, Student's t, and Chi-squared distributions.
/// </summary>
public class DistributionsActions : IDistributions
{
    /// <inheritdoc />
    public double NormalPdf(double mean, double stddev, double x)
    {
        MathHelper.ValidateFinite(mean, nameof(mean));
        MathHelper.ValidatePositive(stddev, nameof(stddev));
        MathHelper.ValidateFinite(x, nameof(x));
        return Normal.PDF(mean, stddev, x);
    }

    /// <inheritdoc />
    public double NormalCdf(double mean, double stddev, double x)
    {
        MathHelper.ValidateFinite(mean, nameof(mean));
        MathHelper.ValidatePositive(stddev, nameof(stddev));
        MathHelper.ValidateFinite(x, nameof(x));
        return Normal.CDF(mean, stddev, x);
    }

    /// <inheritdoc />
    public double NormalInverseCdf(double mean, double stddev, double p)
    {
        MathHelper.ValidateFinite(mean, nameof(mean));
        MathHelper.ValidatePositive(stddev, nameof(stddev));
        MathHelper.ValidateExclusiveRange(p, 0, 1, nameof(p));
        return Normal.InvCDF(mean, stddev, p);
    }

    /// <inheritdoc />
    public List<double> NormalSample(double mean, double stddev, int count)
    {
        MathHelper.ValidateFinite(mean, nameof(mean));
        MathHelper.ValidatePositive(stddev, nameof(stddev));
        if (count <= 0 || count > 1_000_000)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be between 1 and 1,000,000.");
        var dist = new Normal(mean, stddev);
        var samples = new double[count];
        dist.Samples(samples);
        return samples.ToList();
    }

    /// <inheritdoc />
    public double ExponentialCdf(double rate, double x)
    {
        MathHelper.ValidatePositive(rate, nameof(rate));
        MathHelper.ValidateFinite(x, nameof(x));
        return Exponential.CDF(rate, x);
    }

    /// <inheritdoc />
    public double ExponentialInverseCdf(double rate, double p)
    {
        MathHelper.ValidatePositive(rate, nameof(rate));
        MathHelper.ValidateExclusiveRange(p, 0, 1, nameof(p));
        return Exponential.InvCDF(rate, p);
    }

    /// <inheritdoc />
    public double PoissonProbability(double mean, int k)
    {
        MathHelper.ValidatePositive(mean, nameof(mean));
        MathHelper.ValidateNonNegative(k, nameof(k));
        return Poisson.PMF(mean, k);
    }

    /// <inheritdoc />
    public double PoissonCdf(double mean, int k)
    {
        MathHelper.ValidatePositive(mean, nameof(mean));
        MathHelper.ValidateNonNegative(k, nameof(k));
        return Poisson.CDF(mean, k);
    }

    /// <inheritdoc />
    public double BinomialProbability(double p, int n, int k)
    {
        MathHelper.ValidateRange(p, 0, 1, nameof(p));
        MathHelper.ValidatePositive(n, nameof(n));
        if (k < 0 || k > n)
            throw new ArgumentOutOfRangeException(nameof(k), k, $"k must be between 0 and n ({n}).");
        return Binomial.PMF(p, n, k);
    }

    /// <inheritdoc />
    public double BinomialCdf(double p, int n, int k)
    {
        MathHelper.ValidateRange(p, 0, 1, nameof(p));
        MathHelper.ValidatePositive(n, nameof(n));
        if (k < 0 || k > n)
            throw new ArgumentOutOfRangeException(nameof(k), k, $"k must be between 0 and n ({n}).");
        return Binomial.CDF(p, n, k);
    }

    /// <inheritdoc />
    public double StudentTInverseCdf(double degreesOfFreedom, double probability)
    {
        MathHelper.ValidatePositive(degreesOfFreedom, nameof(degreesOfFreedom));
        MathHelper.ValidateExclusiveRange(probability, 0, 1, nameof(probability));
        return StudentT.InvCDF(0, 1, degreesOfFreedom, probability);
    }

    /// <inheritdoc />
    public double ChiSquaredCdf(double degreesOfFreedom, double x)
    {
        MathHelper.ValidatePositive(degreesOfFreedom, nameof(degreesOfFreedom));
        MathHelper.ValidateFinite(x, nameof(x));
        return ChiSquared.CDF(degreesOfFreedom, x);
    }
}
