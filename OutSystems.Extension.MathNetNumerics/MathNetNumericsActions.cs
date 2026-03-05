namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Single ODC entry point implementing all 48 server actions via composition delegation.
/// </summary>
public class MathNetNumericsActions : IMathNetNumerics
{
    private readonly FinancialActions _financial = new();
    private readonly StatisticsActions _statistics = new();
    private readonly DistributionsActions _distributions = new();
    private readonly RootFindingActions _rootFinding = new();
    private readonly IntegrationActions _integration = new();
    private readonly RegressionActions _regression = new();
    private readonly InterpolationActions _interpolation = new();

    #region Financial

    /// <inheritdoc />
    public double FutureValue(double presentValue, double annualRate, int periods)
        => _financial.FutureValue(presentValue, annualRate, periods);

    /// <inheritdoc />
    public double PresentValue(double futureValue, double annualRate, int periods)
        => _financial.PresentValue(futureValue, annualRate, periods);

    /// <inheritdoc />
    public double NetPresentValue(double discountRate, List<double> cashFlows)
        => _financial.NetPresentValue(discountRate, cashFlows);

    /// <inheritdoc />
    public double InternalRateOfReturn(List<double> cashFlows)
        => _financial.InternalRateOfReturn(cashFlows);

    /// <inheritdoc />
    public double PaymentAmount(double principal, double annualRate, int totalPeriods)
        => _financial.PaymentAmount(principal, annualRate, totalPeriods);

    /// <inheritdoc />
    public double CompoundInterest(double principal, double annualRate, int compoundingsPerYear, int years)
        => _financial.CompoundInterest(principal, annualRate, compoundingsPerYear, years);

    /// <inheritdoc />
    public double StraightLineDepreciation(double cost, double salvageValue, int usefulLifeYears)
        => _financial.StraightLineDepreciation(cost, salvageValue, usefulLifeYears);

    /// <inheritdoc />
    public List<AmortizationScheduleEntry> AmortizationSchedule(double principal, double annualRate, int totalPeriods)
        => _financial.AmortizationSchedule(principal, annualRate, totalPeriods);

    #endregion

    #region Statistics

    /// <inheritdoc />
    public double Mean(List<double> values) => _statistics.Mean(values);

    /// <inheritdoc />
    public double Median(List<double> values) => _statistics.Median(values);

    /// <inheritdoc />
    public double Variance(List<double> values) => _statistics.Variance(values);

    /// <inheritdoc />
    public double StandardDeviation(List<double> values) => _statistics.StandardDeviation(values);

    /// <inheritdoc />
    public double Skewness(List<double> values) => _statistics.Skewness(values);

    /// <inheritdoc />
    public double Kurtosis(List<double> values) => _statistics.Kurtosis(values);

    /// <inheritdoc />
    public double Percentile(List<double> values, int percentile) => _statistics.Percentile(values, percentile);

    /// <inheritdoc />
    public double PearsonCorrelation(List<double> valuesA, List<double> valuesB)
        => _statistics.PearsonCorrelation(valuesA, valuesB);

    /// <inheritdoc />
    public double Min(List<double> values) => _statistics.Min(values);

    /// <inheritdoc />
    public double Max(List<double> values) => _statistics.Max(values);

    /// <inheritdoc />
    public StatisticsSummary Summary(List<double> values) => _statistics.Summary(values);

    #endregion

    #region Distributions

    /// <inheritdoc />
    public double NormalPdf(double mean, double stddev, double x)
        => _distributions.NormalPdf(mean, stddev, x);

    /// <inheritdoc />
    public double NormalCdf(double mean, double stddev, double x)
        => _distributions.NormalCdf(mean, stddev, x);

    /// <inheritdoc />
    public double NormalInverseCdf(double mean, double stddev, double p)
        => _distributions.NormalInverseCdf(mean, stddev, p);

    /// <inheritdoc />
    public List<double> NormalSample(double mean, double stddev, int count)
        => _distributions.NormalSample(mean, stddev, count);

    /// <inheritdoc />
    public double ExponentialCdf(double rate, double x)
        => _distributions.ExponentialCdf(rate, x);

    /// <inheritdoc />
    public double ExponentialInverseCdf(double rate, double p)
        => _distributions.ExponentialInverseCdf(rate, p);

    /// <inheritdoc />
    public double PoissonProbability(double mean, int k)
        => _distributions.PoissonProbability(mean, k);

    /// <inheritdoc />
    public double PoissonCdf(double mean, int k)
        => _distributions.PoissonCdf(mean, k);

    /// <inheritdoc />
    public double BinomialProbability(double p, int n, int k)
        => _distributions.BinomialProbability(p, n, k);

    /// <inheritdoc />
    public double BinomialCdf(double p, int n, int k)
        => _distributions.BinomialCdf(p, n, k);

    /// <inheritdoc />
    public double StudentTInverseCdf(double degreesOfFreedom, double probability)
        => _distributions.StudentTInverseCdf(degreesOfFreedom, probability);

    /// <inheritdoc />
    public double ChiSquaredCdf(double degreesOfFreedom, double x)
        => _distributions.ChiSquaredCdf(degreesOfFreedom, x);

    #endregion

    #region Root Finding

    /// <inheritdoc />
    public double BisectionRoot(List<double> coefficients, double lowerBound, double upperBound)
        => _rootFinding.BisectionRoot(coefficients, lowerBound, upperBound);

    /// <inheritdoc />
    public double BrentRoot(List<double> coefficients, double lowerBound, double upperBound)
        => _rootFinding.BrentRoot(coefficients, lowerBound, upperBound);

    /// <inheritdoc />
    public double BreakEvenQuantity(double pricePerUnit, double fixedCost, double variableCostPerUnit)
        => _rootFinding.BreakEvenQuantity(pricePerUnit, fixedCost, variableCostPerUnit);

    #endregion

    #region Integration

    /// <inheritdoc />
    public double TrapezoidalFromData(List<double> xValues, List<double> yValues)
        => _integration.TrapezoidalFromData(xValues, yValues);

    /// <inheritdoc />
    public double PolynomialIntegral(List<double> coefficients, double a, double b)
        => _integration.PolynomialIntegral(coefficients, a, b);

    #endregion

    #region Regression

    /// <inheritdoc />
    public LinearRegressionResult LinearRegression(List<double> xValues, List<double> yValues)
        => _regression.LinearRegression(xValues, yValues);

    /// <inheritdoc />
    public double LinearPredict(List<double> xValues, List<double> yValues, double xPredict)
        => _regression.LinearPredict(xValues, yValues, xPredict);

    /// <inheritdoc />
    public List<double> PolynomialRegression(List<double> xValues, List<double> yValues, int order)
        => _regression.PolynomialRegression(xValues, yValues, order);

    /// <inheritdoc />
    public double PolynomialPredict(List<double> xValues, List<double> yValues, int order, double xPredict)
        => _regression.PolynomialPredict(xValues, yValues, order, xPredict);

    /// <inheritdoc />
    public double RSquared(List<double> xValues, List<double> yValues)
        => _regression.RSquared(xValues, yValues);

    /// <inheritdoc />
    public TwoParameterFitResult ExponentialFit(List<double> xValues, List<double> yValues)
        => _regression.ExponentialFit(xValues, yValues);

    /// <inheritdoc />
    public TwoParameterFitResult PowerFit(List<double> xValues, List<double> yValues)
        => _regression.PowerFit(xValues, yValues);

    #endregion

    #region Interpolation

    /// <inheritdoc />
    public double LinearInterpolate(List<double> xValues, List<double> yValues, double x)
        => _interpolation.LinearInterpolate(xValues, yValues, x);

    /// <inheritdoc />
    public double CubicSplineInterpolate(List<double> xValues, List<double> yValues, double x)
        => _interpolation.CubicSplineInterpolate(xValues, yValues, x);

    /// <inheritdoc />
    public double PolynomialInterpolate(List<double> xValues, List<double> yValues, double x)
        => _interpolation.PolynomialInterpolate(xValues, yValues, x);

    /// <inheritdoc />
    public List<double> BulkLinearInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints)
        => _interpolation.BulkLinearInterpolate(xKnown, yKnown, xPoints);

    /// <inheritdoc />
    public List<double> BulkCubicSplineInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints)
        => _interpolation.BulkCubicSplineInterpolate(xKnown, yKnown, xPoints);

    #endregion
}
