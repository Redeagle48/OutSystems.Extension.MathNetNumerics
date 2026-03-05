using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements regression analysis, curve fitting, and prediction models.
/// </summary>
public class RegressionActions
{
    /// <inheritdoc />
    public LinearRegressionResult LinearRegression(List<double> xValues, List<double> yValues)
    {
        ValidateXY(xValues, yValues, 2);
        var (intercept, slope) = SimpleRegression.Fit(xValues.ToArray(), yValues.ToArray());
        return new LinearRegressionResult { Intercept = intercept, Slope = slope };
    }

    /// <inheritdoc />
    public double LinearPredict(List<double> xValues, List<double> yValues, double xPredict)
    {
        ValidateXY(xValues, yValues, 2);
        MathHelper.ValidateFinite(xPredict, nameof(xPredict));
        var (intercept, slope) = SimpleRegression.Fit(xValues.ToArray(), yValues.ToArray());
        return intercept + slope * xPredict;
    }

    /// <inheritdoc />
    public List<double> PolynomialRegression(List<double> xValues, List<double> yValues, int order)
    {
        MathHelper.ValidatePositive(order, nameof(order));
        ValidateXY(xValues, yValues, order + 1);
        double[] coeffs = Fit.Polynomial(xValues.ToArray(), yValues.ToArray(), order);
        return coeffs.ToList();
    }

    /// <inheritdoc />
    public double PolynomialPredict(List<double> xValues, List<double> yValues, int order, double xPredict)
    {
        MathHelper.ValidatePositive(order, nameof(order));
        ValidateXY(xValues, yValues, order + 1);
        MathHelper.ValidateFinite(xPredict, nameof(xPredict));
        double[] coeffs = Fit.Polynomial(xValues.ToArray(), yValues.ToArray(), order);
        return MathHelper.EvaluatePolynomial(coeffs, xPredict);
    }

    /// <inheritdoc />
    public double RSquared(List<double> xValues, List<double> yValues)
    {
        ValidateXY(xValues, yValues, 2);
        var (intercept, slope) = SimpleRegression.Fit(xValues.ToArray(), yValues.ToArray());
        return GoodnessOfFit.RSquared(
            xValues.Select(x => intercept + slope * x),
            yValues);
    }

    /// <inheritdoc />
    public TwoParameterFitResult ExponentialFit(List<double> xValues, List<double> yValues)
    {
        ValidateXY(xValues, yValues, 2);
        var (a, r) = Fit.Exponential(xValues.ToArray(), yValues.ToArray());
        return new TwoParameterFitResult { A = a, B = r };
    }

    /// <inheritdoc />
    public TwoParameterFitResult PowerFit(List<double> xValues, List<double> yValues)
    {
        ValidateXY(xValues, yValues, 2);
        var (a, b) = Fit.Power(xValues.ToArray(), yValues.ToArray());
        return new TwoParameterFitResult { A = a, B = b };
    }

    private static void ValidateXY(List<double> xValues, List<double> yValues, int minPoints)
    {
        MathHelper.ValidateNotNullOrEmpty(xValues, nameof(xValues));
        MathHelper.ValidateNotNullOrEmpty(yValues, nameof(yValues));
        MathHelper.ValidateMatchingLengths(xValues, nameof(xValues), yValues, nameof(yValues));
        MathHelper.ValidateMinCount(xValues, minPoints, nameof(xValues));
    }
}
