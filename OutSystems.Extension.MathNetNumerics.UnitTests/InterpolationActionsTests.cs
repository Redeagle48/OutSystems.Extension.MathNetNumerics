
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class InterpolationActionsTests
{
    private readonly InterpolationActions _sut = new();

    private static readonly List<double> XKnown = new() { 0, 1, 2, 3, 4 };
    private static readonly List<double> YKnown = new() { 0, 1, 4, 9, 16 };

    [Fact]
    public void LinearInterpolate_AtKnownPoint()
    {
        var result = _sut.LinearInterpolate(XKnown, YKnown, 2);
        Assert.Equal(4.0, result, 4);
    }

    [Fact]
    public void LinearInterpolate_Midpoint()
    {
        var x = new List<double> { 0, 10 };
        var y = new List<double> { 0, 100 };
        var result = _sut.LinearInterpolate(x, y, 5);
        Assert.Equal(50.0, result, 4);
    }

    [Fact]
    public void CubicSplineInterpolate_AtKnownPoint()
    {
        var result = _sut.CubicSplineInterpolate(XKnown, YKnown, 2);
        Assert.Equal(4.0, result, 4);
    }

    [Fact]
    public void CubicSplineInterpolate_BetweenPoints()
    {
        var result = _sut.CubicSplineInterpolate(XKnown, YKnown, 1.5);
        // For y=x^2, expect ~2.25 but cubic spline won't be exact
        Assert.True(result > 1.5 && result < 4.0);
    }

    [Fact]
    public void PolynomialInterpolate_AtKnownPoint()
    {
        var result = _sut.PolynomialInterpolate(XKnown, YKnown, 3);
        Assert.Equal(9.0, result, 2);
    }

    [Fact]
    public void PolynomialInterpolate_ThreePoints_Quadratic()
    {
        // 3 points from y = x^2 -> exact polynomial interpolation
        var x = new List<double> { 0, 1, 2 };
        var y = new List<double> { 0, 1, 4 };
        var result = _sut.PolynomialInterpolate(x, y, 1.5);
        Assert.Equal(2.25, result, 2);
    }

    [Fact]
    public void BulkLinearInterpolate_MultiplePoints()
    {
        var x = new List<double> { 0, 10 };
        var y = new List<double> { 0, 100 };
        var points = new List<double> { 2, 5, 8 };
        var results = _sut.BulkLinearInterpolate(x, y, points);

        Assert.Equal(3, results.Count);
        Assert.Equal(20.0, results[0], 4);
        Assert.Equal(50.0, results[1], 4);
        Assert.Equal(80.0, results[2], 4);
    }

    [Fact]
    public void BulkLinearInterpolate_MatchesSingleCalls()
    {
        var points = new List<double> { 0.5, 1.5, 2.5 };
        var bulk = _sut.BulkLinearInterpolate(XKnown, YKnown, points);

        for (int i = 0; i < points.Count; i++)
        {
            var single = _sut.LinearInterpolate(XKnown, YKnown, points[i]);
            Assert.Equal(single, bulk[i], 10);
        }
    }

    [Fact]
    public void BulkCubicSplineInterpolate_MatchesSingleCalls()
    {
        var points = new List<double> { 0.5, 1.5, 2.5 };
        var bulk = _sut.BulkCubicSplineInterpolate(XKnown, YKnown, points);

        for (int i = 0; i < points.Count; i++)
        {
            var single = _sut.CubicSplineInterpolate(XKnown, YKnown, points[i]);
            Assert.Equal(single, bulk[i], 10);
        }
    }
}
