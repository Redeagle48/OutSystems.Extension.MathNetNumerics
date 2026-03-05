using MathNet.Numerics.Interpolation;

namespace OutSystems.Extension.MathNetNumerics;

/// <summary>
/// Implements interpolation methods including linear spline, cubic spline, and polynomial (Floater-Hormann).
/// </summary>
public class InterpolationActions : OutSystems.Extension.MathNetNumerics.IInterpolation
{
    /// <inheritdoc />
    public double LinearInterpolate(List<double> xValues, List<double> yValues, double x)
    {
        ValidateXY(xValues, yValues, 2);
        MathHelper.ValidateFinite(x, nameof(x));
        var interpolation = LinearSpline.Interpolate(xValues.ToArray(), yValues.ToArray());
        return interpolation.Interpolate(x);
    }

    /// <inheritdoc />
    public double CubicSplineInterpolate(List<double> xValues, List<double> yValues, double x)
    {
        ValidateXY(xValues, yValues, 2);
        MathHelper.ValidateFinite(x, nameof(x));
        var interpolation = CubicSpline.InterpolateNatural(xValues.ToArray(), yValues.ToArray());
        return interpolation.Interpolate(x);
    }

    /// <inheritdoc />
    public double PolynomialInterpolate(List<double> xValues, List<double> yValues, double x)
    {
        ValidateXY(xValues, yValues, 2);
        MathHelper.ValidateFinite(x, nameof(x));
        var interpolation = Barycentric.InterpolateRationalFloaterHormann(xValues.ToArray(), yValues.ToArray());
        return interpolation.Interpolate(x);
    }

    /// <inheritdoc />
    public List<double> BulkLinearInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints)
    {
        ValidateXY(xKnown, yKnown, 2);
        MathHelper.ValidateNotNullOrEmpty(xPoints, nameof(xPoints));
        var interpolation = LinearSpline.Interpolate(xKnown.ToArray(), yKnown.ToArray());
        return xPoints.Select(x => interpolation.Interpolate(x)).ToList();
    }

    /// <inheritdoc />
    public List<double> BulkCubicSplineInterpolate(List<double> xKnown, List<double> yKnown, List<double> xPoints)
    {
        ValidateXY(xKnown, yKnown, 2);
        MathHelper.ValidateNotNullOrEmpty(xPoints, nameof(xPoints));
        var interpolation = CubicSpline.InterpolateNatural(xKnown.ToArray(), yKnown.ToArray());
        return xPoints.Select(x => interpolation.Interpolate(x)).ToList();
    }

    private static void ValidateXY(List<double> xValues, List<double> yValues, int minPoints)
    {
        MathHelper.ValidateNotNullOrEmpty(xValues, nameof(xValues));
        MathHelper.ValidateNotNullOrEmpty(yValues, nameof(yValues));
        MathHelper.ValidateMatchingLengths(xValues, nameof(xValues), yValues, nameof(yValues));
        MathHelper.ValidateMinCount(xValues, minPoints, nameof(xValues));
    }
}
