namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class InterpolationValidationTests
{
    private readonly InterpolationActions _sut = new();

    [Fact]
    public void LinearInterpolate_EmptyLists_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearInterpolate(new List<double>(), new List<double>(), 1));
    }

    [Fact]
    public void LinearInterpolate_SinglePoint_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearInterpolate(new List<double> { 1 }, new List<double> { 2 }, 1));
    }

    [Fact]
    public void LinearInterpolate_MismatchedLengths_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.LinearInterpolate(new List<double> { 1, 2, 3 }, new List<double> { 1 }, 1));
    }

    [Fact]
    public void LinearInterpolate_NullX_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _sut.LinearInterpolate(null!, new List<double> { 1, 2 }, 1));
    }

    [Fact]
    public void CubicSplineInterpolate_SinglePoint_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.CubicSplineInterpolate(new List<double> { 1 }, new List<double> { 2 }, 1));
    }

    [Fact]
    public void PolynomialInterpolate_EmptyLists_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.PolynomialInterpolate(new List<double>(), new List<double>(), 1));
    }

    [Fact]
    public void BulkLinearInterpolate_EmptyXPoints_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BulkLinearInterpolate(
                new List<double> { 1, 2 }, new List<double> { 1, 2 },
                new List<double>()));
    }

    [Fact]
    public void BulkLinearInterpolate_EmptyKnown_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BulkLinearInterpolate(
                new List<double>(), new List<double>(),
                new List<double> { 1 }));
    }

    [Fact]
    public void BulkCubicSplineInterpolate_MismatchedKnown_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            _sut.BulkCubicSplineInterpolate(
                new List<double> { 1, 2, 3 }, new List<double> { 1, 2 },
                new List<double> { 1.5 }));
    }

    [Fact]
    public void LinearInterpolate_NaNX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.LinearInterpolate(new List<double> { 1, 2 }, new List<double> { 1, 2 }, double.NaN));
    }

    [Fact]
    public void CubicSplineInterpolate_InfinityX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.CubicSplineInterpolate(new List<double> { 1, 2 }, new List<double> { 1, 2 }, double.PositiveInfinity));
    }

    [Fact]
    public void PolynomialInterpolate_NaNX_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.PolynomialInterpolate(new List<double> { 1, 2 }, new List<double> { 1, 2 }, double.NaN));
    }
}
