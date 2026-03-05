
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class RegressionActionsTests
{
    private readonly RegressionActions _sut = new();

    [Fact]
    public void LinearRegression_PerfectLine()
    {
        // y = 1 + 2x
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 3, 5, 7, 9, 11 };
        var result = _sut.LinearRegression(x, y);

        Assert.Equal(1.0, result.Intercept, 4);
        Assert.Equal(2.0, result.Slope, 4);
    }

    [Fact]
    public void LinearRegression_HorizontalLine()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 5, 5, 5, 5, 5 };
        var result = _sut.LinearRegression(x, y);

        Assert.Equal(0.0, result.Slope, 4);
        Assert.Equal(5.0, result.Intercept, 4);
    }

    [Fact]
    public void LinearPredict_AtKnownPoint()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 3, 5, 7, 9, 11 };
        var result = _sut.LinearPredict(x, y, 3);
        Assert.Equal(7.0, result, 4);
    }

    [Fact]
    public void LinearPredict_Extrapolation()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 3, 5, 7, 9, 11 };
        var result = _sut.LinearPredict(x, y, 10);
        Assert.Equal(21.0, result, 4);
    }

    [Fact]
    public void PolynomialRegression_ReturnsCorrectCoefficientCount()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 1, 4, 9, 16, 25 };
        var coeffs = _sut.PolynomialRegression(x, y, 2);
        Assert.Equal(3, coeffs.Count); // a0, a1, a2
    }

    [Fact]
    public void PolynomialRegression_Order1_MatchesLinear()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 3, 5, 7, 9, 11 };

        var linear = _sut.LinearRegression(x, y);
        var poly = _sut.PolynomialRegression(x, y, 1);

        Assert.Equal(linear.Intercept, poly[0], 4);
        Assert.Equal(linear.Slope, poly[1], 4);
    }

    [Fact]
    public void PolynomialPredict_Quadratic()
    {
        // y = x^2
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 1, 4, 9, 16, 25 };
        var result = _sut.PolynomialPredict(x, y, 2, 3);
        Assert.Equal(9.0, result, 2);
    }

    [Fact]
    public void RSquared_PerfectFit_IsOne()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 3, 5, 7, 9, 11 };
        var result = _sut.RSquared(x, y);
        Assert.Equal(1.0, result, 6);
    }

    [Fact]
    public void RSquared_ImperfectFit_LessThanOne()
    {
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = new List<double> { 2, 5, 4, 8, 12 };
        var result = _sut.RSquared(x, y);
        Assert.True(result > 0 && result < 1);
    }

    [Fact]
    public void ExponentialFit_RecoverParameters()
    {
        // y = 2 * e^(0.5x)
        var x = new List<double> { 0, 1, 2, 3, 4 };
        var y = x.Select(v => 2.0 * Math.Exp(0.5 * v)).ToList();
        var result = _sut.ExponentialFit(x, y);

        Assert.Equal(2.0, result.A, 2);
        Assert.Equal(0.5, result.B, 2);
    }

    [Fact]
    public void PowerFit_RecoverParameters()
    {
        // y = 3 * x^2
        var x = new List<double> { 1, 2, 3, 4, 5 };
        var y = x.Select(v => 3.0 * Math.Pow(v, 2)).ToList();
        var result = _sut.PowerFit(x, y);

        Assert.Equal(3.0, result.A, 1);
        Assert.Equal(2.0, result.B, 1);
    }
}
