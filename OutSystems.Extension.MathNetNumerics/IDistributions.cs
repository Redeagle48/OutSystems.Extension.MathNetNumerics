using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Probability distribution functions: PDF, CDF, inverse CDF, and random sampling.
/// Exposed as the <c>MathNet_Distributions</c> module in OutSystems Developer Cloud (ODC).
/// Covers Normal, Exponential, Poisson, Binomial, Student's t, and Chi-squared distributions.
/// All operations delegate to MathNet.Numerics.Distributions.
/// </summary>
[OSInterface(Name = "MathNet_Distributions", Description = "Probability distributions: PDF, CDF, inverse CDF, and random sampling", IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IDistributions
{
    /// <summary>
    /// Evaluates the Normal (Gaussian) probability density function at x.
    /// Formula: (1 / (stddev * sqrt(2*pi))) * exp(-0.5 * ((x - mean) / stddev)^2).
    /// </summary>
    /// <param name="mean">Mean of the distribution. Must be a finite number.</param>
    /// <param name="stddev">Standard deviation. Must be a finite number greater than zero.</param>
    /// <param name="x">Point at which to evaluate the PDF. Must be a finite number.</param>
    /// <returns>The probability density at x (always non-negative).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or stddev is not positive.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Normal distribution probability density function at x")]
    double NormalPdf(double mean, double stddev, double x);

    /// <summary>
    /// Evaluates the Normal (Gaussian) cumulative distribution function at x.
    /// Returns P(X &lt;= x) for a normally distributed random variable X.
    /// </summary>
    /// <param name="mean">Mean of the distribution. Must be a finite number.</param>
    /// <param name="stddev">Standard deviation. Must be a finite number greater than zero.</param>
    /// <param name="x">Point at which to evaluate the CDF. Must be a finite number.</param>
    /// <returns>The cumulative probability P(X &lt;= x) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or stddev is not positive.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Normal distribution cumulative distribution function at x")]
    double NormalCdf(double mean, double stddev, double x);

    /// <summary>
    /// Evaluates the Normal (Gaussian) inverse CDF (quantile function).
    /// Returns the value x such that P(X &lt;= x) = p.
    /// </summary>
    /// <param name="mean">Mean of the distribution. Must be a finite number.</param>
    /// <param name="stddev">Standard deviation. Must be a finite number greater than zero.</param>
    /// <param name="p">Probability value. Must be strictly between 0 and 1 (exclusive).</param>
    /// <returns>The quantile value x corresponding to probability p.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when parameters are invalid or p is not in (0, 1).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Normal distribution inverse CDF (quantile function) for probability p")]
    double NormalInverseCdf(double mean, double stddev, double p);

    /// <summary>
    /// Generates a list of random samples drawn from a Normal distribution.
    /// Uses MathNet.Numerics internal random number generator.
    /// </summary>
    /// <param name="mean">Mean of the distribution. Must be a finite number.</param>
    /// <param name="stddev">Standard deviation. Must be a finite number greater than zero.</param>
    /// <param name="count">Number of samples to generate. Must be between 1 and 1,000,000.</param>
    /// <returns>A list of random samples from the specified Normal distribution.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when count is outside [1, 1,000,000] or other parameters are invalid.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Generates a list of random samples from a normal distribution")]
    List<double> NormalSample(double mean, double stddev, int count);

    /// <summary>
    /// Evaluates the Exponential distribution CDF at x.
    /// Returns P(X &lt;= x) = 1 - exp(-rate * x) for x &gt;= 0.
    /// </summary>
    /// <param name="rate">Rate parameter (lambda). Must be a finite number greater than zero.</param>
    /// <param name="x">Point at which to evaluate the CDF. Must be a finite number.</param>
    /// <returns>The cumulative probability P(X &lt;= x) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when rate is not positive or parameters are NaN/Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Exponential distribution CDF at x with given rate")]
    double ExponentialCdf(double rate, double x);

    /// <summary>
    /// Evaluates the Exponential distribution inverse CDF (quantile function).
    /// Returns the value x such that P(X &lt;= x) = p.
    /// </summary>
    /// <param name="rate">Rate parameter (lambda). Must be a finite number greater than zero.</param>
    /// <param name="p">Probability value. Must be strictly between 0 and 1 (exclusive).</param>
    /// <returns>The quantile value x corresponding to probability p.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when rate is not positive or p is not in (0, 1).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Exponential distribution inverse CDF for probability p")]
    double ExponentialInverseCdf(double rate, double p);

    /// <summary>
    /// Computes the probability of exactly k events occurring in a Poisson distribution.
    /// Returns P(X = k) = (mean^k * exp(-mean)) / k!.
    /// </summary>
    /// <param name="mean">Expected number of events (lambda). Must be a finite number greater than zero.</param>
    /// <param name="k">Number of events. Must be non-negative.</param>
    /// <returns>The probability P(X = k) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when mean is not positive or k is negative.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Poisson distribution probability of exactly k events given a mean rate")]
    double PoissonProbability(double mean, int k);

    /// <summary>
    /// Computes the Poisson CDF: probability of k or fewer events.
    /// Returns P(X &lt;= k) = sum of P(X = i) for i = 0 to k.
    /// </summary>
    /// <param name="mean">Expected number of events (lambda). Must be a finite number greater than zero.</param>
    /// <param name="k">Maximum number of events. Must be non-negative.</param>
    /// <returns>The cumulative probability P(X &lt;= k) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when mean is not positive or k is negative.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Poisson distribution CDF: probability of k or fewer events")]
    double PoissonCdf(double mean, int k);

    /// <summary>
    /// Computes the probability of exactly k successes in n independent Bernoulli trials.
    /// Returns P(X = k) = C(n,k) * p^k * (1-p)^(n-k).
    /// </summary>
    /// <param name="p">Probability of success on each trial. Must be in [0, 1].</param>
    /// <param name="n">Number of trials. Must be positive.</param>
    /// <param name="k">Number of successes. Must be in [0, n].</param>
    /// <returns>The probability P(X = k) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when p is outside [0, 1], n is not positive, or k is outside [0, n].</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Binomial distribution probability of exactly k successes in n trials")]
    double BinomialProbability(double p, int n, int k);

    /// <summary>
    /// Computes the Binomial CDF: probability of k or fewer successes in n trials.
    /// Returns P(X &lt;= k).
    /// </summary>
    /// <param name="p">Probability of success on each trial. Must be in [0, 1].</param>
    /// <param name="n">Number of trials. Must be positive.</param>
    /// <param name="k">Maximum number of successes. Must be in [0, n].</param>
    /// <returns>The cumulative probability P(X &lt;= k) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when p is outside [0, 1], n is not positive, or k is outside [0, n].</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Binomial distribution CDF: probability of k or fewer successes")]
    double BinomialCdf(double p, int n, int k);

    /// <summary>
    /// Evaluates the Student's t inverse CDF for hypothesis testing.
    /// Returns the critical t-value for a given probability and degrees of freedom.
    /// </summary>
    /// <param name="degreesOfFreedom">Degrees of freedom. Must be a finite number greater than zero.</param>
    /// <param name="probability">Probability value. Must be strictly between 0 and 1 (exclusive).</param>
    /// <returns>The critical t-value corresponding to the given probability.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when degreesOfFreedom is not positive or probability is not in (0, 1).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Student-t inverse CDF for hypothesis testing. Returns critical value for given probability and degrees of freedom")]
    double StudentTInverseCdf(double degreesOfFreedom, double probability);

    /// <summary>
    /// Evaluates the Chi-squared CDF for goodness-of-fit testing.
    /// Returns P(X &lt;= x) for a Chi-squared distributed random variable.
    /// </summary>
    /// <param name="degreesOfFreedom">Degrees of freedom. Must be a finite number greater than zero.</param>
    /// <param name="x">Point at which to evaluate the CDF. Must be a finite number.</param>
    /// <returns>The cumulative probability P(X &lt;= x) in the range [0, 1].</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when degreesOfFreedom is not positive or parameters are NaN/Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Chi-squared CDF for goodness-of-fit testing")]
    double ChiSquaredCdf(double degreesOfFreedom, double x);
}
