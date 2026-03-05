using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// OutSystems Developer Cloud (ODC) external library providing 48 server actions for
/// financial calculations, statistics, probability distributions, regression, interpolation,
/// integration, and root finding — powered by MathNet.Numerics.
/// </summary>
[OSInterface(
    Name = "MathNetNumerics",
    Description = "Math and scientific computing: financial, statistics, distributions, regression, interpolation, integration, root finding",
    IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png")]
public interface IMathNetNumerics
{
    #region Financial

    /// <summary>
    /// Calculates the future value of a present amount using compound growth.
    /// Formula: FV = presentValue * (1 + annualRate) ^ periods.
    /// </summary>
    /// <param name="presentValue">The current value of the investment or loan. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number.</param>
    /// <param name="periods">Number of compounding periods. Must be non-negative.</param>
    /// <returns>The future value in the same currency unit as <paramref name="presentValue"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates future value given present value, annual rate as decimal (e.g. 0.05 for 5%), and number of periods")]
    double FutureValue(double presentValue, double annualRate, int periods);

    /// <summary>
    /// Calculates the present value of a future amount by discounting.
    /// Formula: PV = futureValue / (1 + annualRate) ^ periods.
    /// </summary>
    /// <param name="futureValue">The future amount to be discounted. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number. Cannot be -1 (100% loss).</param>
    /// <param name="periods">Number of discounting periods. Must be non-negative.</param>
    /// <returns>The present value in the same currency unit as <paramref name="futureValue"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    /// <exception cref="ArgumentException">Thrown when annualRate is -1 with positive periods (zero divisor).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates present value given future value, annual rate as decimal (e.g. 0.05 for 5%), and number of periods")]
    double PresentValue(double futureValue, double annualRate, int periods);

    /// <summary>
    /// Computes the Net Present Value (NPV) of a series of cash flows at a given discount rate.
    /// Each cash flow is discounted by (1 + discountRate) ^ t, where t is the zero-based period index.
    /// </summary>
    /// <param name="discountRate">Discount rate as a decimal (e.g., 0.10 for 10%). Must be a finite number.</param>
    /// <param name="cashFlows">List of cash flows ordered by period. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>The Net Present Value in the same currency unit as the cash flows.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when discountRate is NaN or Infinity.</exception>
    /// <exception cref="ArgumentNullException">Thrown when cashFlows is null.</exception>
    /// <exception cref="ArgumentException">Thrown when cashFlows is empty.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Net Present Value of a series of cash flows at a given discount rate as decimal (e.g. 0.10 for 10%)")]
    double NetPresentValue(double discountRate, List<double> cashFlows);

    /// <summary>
    /// Computes the Internal Rate of Return (IRR) for a series of cash flows using Newton-Raphson iteration.
    /// Converges when |NPV| &lt; 1e-10 or after 1,000 iterations.
    /// The first cash flow is typically a negative investment.
    /// </summary>
    /// <param name="cashFlows">List of cash flows with at least 2 elements. First value is typically negative (investment). Maximum 10,000,000 elements.</param>
    /// <returns>The IRR as a decimal (e.g., 0.12 for 12%).</returns>
    /// <exception cref="ArgumentNullException">Thrown when cashFlows is null.</exception>
    /// <exception cref="ArgumentException">Thrown when cashFlows has fewer than 2 elements.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the algorithm does not converge (no real IRR exists).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Internal Rate of Return for a series of cash flows (first value is typically negative investment). Returns rate as decimal")]
    double InternalRateOfReturn(List<double> cashFlows);

    /// <summary>
    /// Calculates the fixed monthly payment for a fully amortizing loan.
    /// Uses the standard annuity formula. When the monthly rate is near zero (&lt; 1e-12), returns principal / totalPeriods.
    /// </summary>
    /// <param name="principal">Loan principal amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.06 for 6%). Must be a finite number.</param>
    /// <param name="totalPeriods">Total number of monthly payment periods. Must be positive.</param>
    /// <returns>The fixed monthly payment amount in the same currency unit as <paramref name="principal"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Monthly payment for a loan given principal, annual rate as decimal (e.g. 0.06 for 6%), and total number of monthly periods")]
    double PaymentAmount(double principal, double annualRate, int totalPeriods);

    /// <summary>
    /// Calculates the compound interest earned: FV - principal.
    /// Formula: principal * (1 + annualRate / compoundingsPerYear) ^ (compoundingsPerYear * years) - principal.
    /// </summary>
    /// <param name="principal">Initial investment amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.05 for 5%). Must be a finite number.</param>
    /// <param name="compoundingsPerYear">Number of times interest compounds per year (e.g., 12 for monthly). Must be positive.</param>
    /// <param name="years">Number of years. Must be non-negative.</param>
    /// <returns>The compound interest amount (FV - principal) in the same currency unit as <paramref name="principal"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Calculates compound interest amount (FV - PV) given annual rate as decimal (e.g. 0.05 for 5%)")]
    double CompoundInterest(double principal, double annualRate, int compoundingsPerYear, int years);

    /// <summary>
    /// Calculates annual depreciation using the straight-line method.
    /// Formula: (cost - salvageValue) / usefulLifeYears.
    /// </summary>
    /// <param name="cost">Original asset cost. Must be a finite number.</param>
    /// <param name="salvageValue">Estimated residual value at end of useful life. Must be a finite number.</param>
    /// <param name="usefulLifeYears">Useful life in years. Must be positive.</param>
    /// <returns>The annual depreciation amount.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Annual depreciation using straight-line method: (cost - salvageValue) / usefulLifeYears")]
    double StraightLineDepreciation(double cost, double salvageValue, int usefulLifeYears);

    /// <summary>
    /// Generates a complete loan amortization schedule with per-period breakdown of principal, interest, and remaining balance.
    /// The last period is adjusted to eliminate rounding drift, ensuring the balance reaches exactly zero.
    /// </summary>
    /// <param name="principal">Loan principal amount. Must be a finite number.</param>
    /// <param name="annualRate">Annual interest rate as a decimal (e.g., 0.06 for 6%). Must be a finite number.</param>
    /// <param name="totalPeriods">Total number of monthly payment periods. Must be between 1 and 12,000.</param>
    /// <returns>A list of <see cref="AmortizationScheduleEntry"/> with one entry per period.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any parameter is NaN, Infinity, or out of range. Thrown when totalPeriods exceeds 12,000.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Returns amortization schedule for monthly payments given annual rate as decimal (e.g. 0.06 for 6%)")]
    List<AmortizationScheduleEntry> AmortizationSchedule(double principal, double annualRate, int totalPeriods);

    #endregion

    #region Statistics

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

    #endregion

    #region Distributions

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

    #endregion

    #region Root Finding

    /// <summary>
    /// Finds a root of a polynomial within [lowerBound, upperBound] using the bisection method.
    /// The function values at the bounds must have opposite signs (sign change required).
    /// Polynomial is evaluated using Horner's method for numerical stability.
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...] where polynomial = a0 + a1*x + a2*x^2 + .... Must not be null or empty.</param>
    /// <param name="lowerBound">Lower bound of the search interval. Must be a finite number less than upperBound.</param>
    /// <param name="upperBound">Upper bound of the search interval. Must be a finite number greater than lowerBound.</param>
    /// <returns>The x value where the polynomial equals zero (within tolerance).</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or lowerBound &gt;= upperBound.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bounds are NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds root of a polynomial in range [lowerBound, upperBound] using bisection. Coefficients are a0 + a1*x + a2*x^2 + ...")]
    double BisectionRoot(List<double> coefficients, double lowerBound, double upperBound);

    /// <summary>
    /// Finds a root of a polynomial within [lowerBound, upperBound] using Brent's method.
    /// Generally converges faster than bisection. Requires a sign change across the interval.
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...]. Must not be null or empty.</param>
    /// <param name="lowerBound">Lower bound of the search interval. Must be a finite number less than upperBound.</param>
    /// <param name="upperBound">Upper bound of the search interval. Must be a finite number greater than lowerBound.</param>
    /// <returns>The x value where the polynomial equals zero (within tolerance).</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or lowerBound &gt;= upperBound.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bounds are NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds root of a polynomial in range [lowerBound, upperBound] using Brent's method")]
    double BrentRoot(List<double> coefficients, double lowerBound, double upperBound);

    /// <summary>
    /// Calculates the break-even quantity where total revenue equals total cost.
    /// Formula: fixedCost / (pricePerUnit - variableCostPerUnit).
    /// Requires that pricePerUnit &gt; variableCostPerUnit (positive margin).
    /// </summary>
    /// <param name="pricePerUnit">Revenue per unit sold. Must be a finite number.</param>
    /// <param name="fixedCost">Total fixed costs. Must be a non-negative finite number.</param>
    /// <param name="variableCostPerUnit">Variable cost per unit. Must be a finite number less than pricePerUnit.</param>
    /// <returns>The break-even quantity (number of units).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when parameters are NaN/Infinity or fixedCost is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when pricePerUnit &lt;= variableCostPerUnit (zero or negative margin).</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Finds break-even quantity where Revenue = Cost. Given price per unit, fixed cost, and variable cost per unit")]
    double BreakEvenQuantity(double pricePerUnit, double fixedCost, double variableCostPerUnit);

    #endregion

    #region Integration

    /// <summary>
    /// Computes the area under a curve from paired x,y data points using the trapezoidal rule.
    /// Sums the areas of trapezoids formed between consecutive data points.
    /// </summary>
    /// <param name="xValues">X coordinates of the data points. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Y coordinates of the data points. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>The approximate area under the curve.</returns>
    /// <exception cref="ArgumentNullException">Thrown when xValues or yValues is null.</exception>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have fewer than 2 elements, or have different lengths.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes area under curve using trapezoidal rule from x,y data points")]
    double TrapezoidalFromData(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Computes the definite integral of a polynomial over the interval [a, b] using Simpson's composite rule with 100 subdivisions.
    /// Polynomial coefficients follow the convention: a0 + a1*x + a2*x^2 + ... (constant term at index 0).
    /// </summary>
    /// <param name="coefficients">Polynomial coefficients [a0, a1, a2, ...]. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <param name="a">Lower integration bound. Must be a finite number strictly less than b.</param>
    /// <param name="b">Upper integration bound. Must be a finite number strictly greater than a.</param>
    /// <returns>The definite integral value.</returns>
    /// <exception cref="ArgumentException">Thrown when coefficients is empty or a &gt;= b.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a or b is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Integrates a polynomial (a0 + a1*x + a2*x^2 + ...) over [a, b]")]
    double PolynomialIntegral(List<double> coefficients, double a, double b);

    #endregion

    #region Regression

    /// <summary>
    /// Performs ordinary least squares linear regression on paired x,y data.
    /// Fits the model y = intercept + slope * x.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>A <see cref="LinearRegressionResult"/> containing the intercept and slope.</returns>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have different lengths, or have fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear regression y = a + bx. Returns intercept and slope")]
    LinearRegressionResult LinearRegression(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Predicts a y value for a given x using linear regression fitted on the provided data.
    /// Internally fits y = intercept + slope * x, then evaluates at xPredict.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="xPredict">The x value at which to predict y. Must be a finite number.</param>
    /// <returns>The predicted y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when xPredict is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Predicts y for a given x using linear regression on provided data")]
    double LinearPredict(List<double> xValues, List<double> yValues, double xPredict);

    /// <summary>
    /// Performs polynomial regression of a specified order.
    /// Fits the model y = a0 + a1*x + a2*x^2 + ... + an*x^n.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least (order + 1) elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="order">Polynomial order (degree). Must be positive and less than the number of data points.</param>
    /// <returns>A list of coefficients [a0, a1, a2, ...] with (order + 1) elements.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when order is not positive.</exception>
    /// <exception cref="ArgumentException">Thrown when data has fewer than (order + 1) elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Polynomial regression of given order. Returns coefficients list (a0, a1, a2, ...)")]
    List<double> PolynomialRegression(List<double> xValues, List<double> yValues, int order);

    /// <summary>
    /// Predicts a y value for a given x using polynomial regression of the specified order.
    /// Internally fits the polynomial, then evaluates at xPredict using Horner's method.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least (order + 1) elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="order">Polynomial order (degree). Must be positive.</param>
    /// <param name="xPredict">The x value at which to predict y. Must be a finite number.</param>
    /// <returns>The predicted y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when order is not positive or xPredict is NaN/Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Predicts y for a given x using polynomial regression of specified order")]
    double PolynomialPredict(List<double> xValues, List<double> yValues, int order, double xPredict);

    /// <summary>
    /// Computes the R-squared (coefficient of determination) for a linear regression fit.
    /// Values range from 0 (no fit) to 1 (perfect fit).
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>.</param>
    /// <returns>R-squared value in the range [0, 1]. A value of 1 indicates a perfect linear fit.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Computes R-squared (coefficient of determination) for linear regression fit")]
    double RSquared(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Fits an exponential model y = A * e^(B*x) to the provided data.
    /// Uses logarithmic linearization internally.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>. All y values must be positive.</param>
    /// <returns>A <see cref="TwoParameterFitResult"/> where A is the multiplier and B is the rate.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Exponential fit y = A*e^(B*x). Returns parameters A and B")]
    TwoParameterFitResult ExponentialFit(List<double> xValues, List<double> yValues);

    /// <summary>
    /// Fits a power model y = A * x^B to the provided data.
    /// Uses logarithmic linearization internally.
    /// </summary>
    /// <param name="xValues">Independent variable values. Must have at least 2 elements. All x values must be positive. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Dependent variable values. Must have the same length as <paramref name="xValues"/>. All y values must be positive.</param>
    /// <returns>A <see cref="TwoParameterFitResult"/> where A is the coefficient and B is the power exponent.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Power fit y = A*x^B. Returns parameters A and B")]
    TwoParameterFitResult PowerFit(List<double> xValues, List<double> yValues);

    #endregion

    #region Interpolation

    /// <summary>
    /// Estimates y at a given x using piecewise linear interpolation (linear spline).
    /// Connects known data points with straight line segments.
    /// </summary>
    /// <param name="xValues">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    /// <exception cref="ArgumentException">Thrown when arrays are empty, have different lengths, or have fewer than 2 elements.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear interpolation: estimates y at a given x from known data points")]
    double LinearInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y at a given x using natural cubic spline interpolation.
    /// Produces a smooth curve through all known data points with continuous first and second derivatives.
    /// </summary>
    /// <param name="xValues">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Cubic spline interpolation: smooth curve estimation at a given x")]
    double CubicSplineInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y at a given x using Barycentric rational interpolation (Floater-Hormann algorithm).
    /// Suitable for polynomial-like interpolation without Runge's phenomenon.
    /// </summary>
    /// <param name="xValues">Known x coordinates. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yValues">Known y coordinates. Must have the same length as <paramref name="xValues"/>.</param>
    /// <param name="x">The x value at which to interpolate. Must be a finite number.</param>
    /// <returns>The interpolated y value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when x is NaN or Infinity.</exception>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Polynomial (Barycentric) interpolation at a given x")]
    double PolynomialInterpolate(List<double> xValues, List<double> yValues, double x);

    /// <summary>
    /// Estimates y values at multiple x points using piecewise linear interpolation.
    /// More efficient than calling <see cref="LinearInterpolate"/> in a loop because the spline is built once.
    /// </summary>
    /// <param name="xKnown">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yKnown">Known y coordinates. Must have the same length as <paramref name="xKnown"/>.</param>
    /// <param name="xPoints">X values at which to interpolate. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>A list of interpolated y values with the same length as <paramref name="xPoints"/>.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Linear interpolation for multiple x points. Returns list of y values")]
    List<double> BulkLinearInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints);

    /// <summary>
    /// Estimates y values at multiple x points using natural cubic spline interpolation.
    /// More efficient than calling <see cref="CubicSplineInterpolate"/> in a loop because the spline is built once.
    /// </summary>
    /// <param name="xKnown">Known x coordinates, sorted in ascending order. Must have at least 2 elements. Maximum 10,000,000 elements.</param>
    /// <param name="yKnown">Known y coordinates. Must have the same length as <paramref name="xKnown"/>.</param>
    /// <param name="xPoints">X values at which to interpolate. Must not be null or empty. Maximum 10,000,000 elements.</param>
    /// <returns>A list of interpolated y values with the same length as <paramref name="xPoints"/>.</returns>
    [OSAction(IconResourceName = "OutSystems.Extension.MathNetNumerics.resources.MathNetNumerics_icon.png", Description = "Cubic spline interpolation for multiple x points. Returns list of y values")]
    List<double> BulkCubicSplineInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints);

    #endregion
}
