
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class RootFindingActionsTests
{
    private readonly RootFindingActions _sut = new();

    [Fact]
    public void BisectionRoot_Quadratic()
    {
        // x^2 - 4 = 0 -> root at x = 2 (in [0, 3])
        var coeffs = new List<double> { -4, 0, 1 }; // -4 + 0*x + 1*x^2
        var result = _sut.BisectionRoot(coeffs, 0, 3);
        Assert.Equal(2.0, result, 4);
    }

    [Fact]
    public void BisectionRoot_Linear()
    {
        // x - 5 = 0 -> root at x = 5
        var coeffs = new List<double> { -5, 1 }; // -5 + 1*x
        var result = _sut.BisectionRoot(coeffs, 0, 10);
        Assert.Equal(5.0, result, 4);
    }

    [Fact]
    public void BrentRoot_Quadratic()
    {
        // x^2 - 4 = 0 -> root at x = 2 (in [0, 3])
        var coeffs = new List<double> { -4, 0, 1 };
        var result = _sut.BrentRoot(coeffs, 0, 3);
        Assert.Equal(2.0, result, 4);
    }

    [Fact]
    public void BrentRoot_MatchesBisection()
    {
        var coeffs = new List<double> { -4, 0, 1 };
        double bisection = _sut.BisectionRoot(coeffs, 0, 3);
        double brent = _sut.BrentRoot(coeffs, 0, 3);
        Assert.Equal(bisection, brent, 4);
    }

    [Fact]
    public void BrentRoot_Cubic()
    {
        // x^3 - 8 = 0 -> root at x = 2
        var coeffs = new List<double> { -8, 0, 0, 1 };
        var result = _sut.BrentRoot(coeffs, 0, 3);
        Assert.Equal(2.0, result, 4);
    }

    [Fact]
    public void BreakEvenQuantity_StandardCase()
    {
        // price=10, fixed=1000, variable=5 -> breakeven at 200 units
        var result = _sut.BreakEvenQuantity(10, 1000, 5);
        Assert.Equal(200.0, result, 4);
    }

    [Fact]
    public void BreakEvenQuantity_ZeroMargin_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.BreakEvenQuantity(5, 1000, 5));
    }

    [Fact]
    public void BreakEvenQuantity_NegativeMargin_Throws()
    {
        Assert.Throws<ArgumentException>(() => _sut.BreakEvenQuantity(3, 1000, 5));
    }
}
