using MathNet.Numerics.Integration;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements numerical integration using trapezoidal and Simpson's composite rules.
/// </summary>
public class IntegrationActions : IIntegration
{
    /// <inheritdoc />
    public double TrapezoidalFromData(List<double> xValues, List<double> yValues)
    {
        MathHelper.ValidateNotNullOrEmpty(xValues, nameof(xValues));
        MathHelper.ValidateNotNullOrEmpty(yValues, nameof(yValues));
        MathHelper.ValidateMatchingLengths(xValues, nameof(xValues), yValues, nameof(yValues));
        MathHelper.ValidateMinCount(xValues, 2, nameof(xValues));

        double area = 0;
        for (int i = 1; i < xValues.Count; i++)
        {
            double dx = xValues[i] - xValues[i - 1];
            double avgY = (yValues[i] + yValues[i - 1]) / 2.0;
            area += dx * avgY;
        }
        return area;
    }

    /// <inheritdoc />
    public double PolynomialIntegral(List<double> coefficients, double a, double b)
    {
        MathHelper.ValidateNotNullOrEmpty(coefficients, nameof(coefficients));
        MathHelper.ValidateFinite(a, nameof(a));
        MathHelper.ValidateFinite(b, nameof(b));
        if (a >= b)
            throw new ArgumentException("Lower bound 'a' must be less than upper bound 'b'.");

        return SimpsonRule.IntegrateComposite(
            x => MathHelper.EvaluatePolynomial(coefficients, x), a, b, 100);
    }
}
