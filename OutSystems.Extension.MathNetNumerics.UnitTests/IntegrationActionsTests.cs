
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class IntegrationActionsTests
{
    private readonly IntegrationActions _sut = new();

    [Fact]
    public void TrapezoidalFromData_Constant()
    {
        // y = 5 on [0, 10] => area = 50
        var x = new List<double> { 0, 2, 4, 6, 8, 10 };
        var y = new List<double> { 5, 5, 5, 5, 5, 5 };
        var result = _sut.TrapezoidalFromData(x, y);
        Assert.Equal(50.0, result, 4);
    }

    [Fact]
    public void TrapezoidalFromData_Triangle()
    {
        // y = x on [0, 4] => area = 8
        var x = Enumerable.Range(0, 41).Select(i => i * 0.1).ToList();
        var y = x.ToList();
        var result = _sut.TrapezoidalFromData(x, y);
        Assert.Equal(8.0, result, 2);
    }

    [Fact]
    public void TrapezoidalFromData_SingleInterval()
    {
        var x = new List<double> { 0, 1 };
        var y = new List<double> { 0, 2 };
        var result = _sut.TrapezoidalFromData(x, y);
        Assert.Equal(1.0, result, 4); // trapezoid: (0+2)/2 * 1 = 1
    }

    [Fact]
    public void PolynomialIntegral_Constant()
    {
        // integral of 1 on [0, 5] = 5
        var coeffs = new List<double> { 1 };
        var result = _sut.PolynomialIntegral(coeffs, 0, 5);
        Assert.Equal(5.0, result, 4);
    }

    [Fact]
    public void PolynomialIntegral_Linear()
    {
        // integral of x on [0, 2] = 2
        var coeffs = new List<double> { 0, 1 };
        var result = _sut.PolynomialIntegral(coeffs, 0, 2);
        Assert.Equal(2.0, result, 4);
    }

    [Fact]
    public void PolynomialIntegral_Quadratic()
    {
        // integral of x^2 on [0, 3] = 9
        var coeffs = new List<double> { 0, 0, 1 };
        var result = _sut.PolynomialIntegral(coeffs, 0, 3);
        Assert.Equal(9.0, result, 4);
    }

    [Fact]
    public void PolynomialIntegral_Cubic()
    {
        // integral of x^3 on [0, 2] = 4
        var coeffs = new List<double> { 0, 0, 0, 1 };
        var result = _sut.PolynomialIntegral(coeffs, 0, 2);
        Assert.Equal(4.0, result, 4);
    }
}
