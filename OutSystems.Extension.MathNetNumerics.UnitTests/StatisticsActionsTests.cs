
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class StatisticsActionsTests
{
    private readonly StatisticsActions _sut = new();

    [Fact]
    public void Mean_SimpleList()
    {
        var result = _sut.Mean(new List<double> { 1, 2, 3, 4, 5 });
        Assert.Equal(3.0, result, 6);
    }

    [Fact]
    public void Mean_SingleElement()
    {
        var result = _sut.Mean(new List<double> { 42 });
        Assert.Equal(42.0, result, 6);
    }

    [Fact]
    public void Median_OddCount()
    {
        var result = _sut.Median(new List<double> { 1, 2, 3, 4, 5 });
        Assert.Equal(3.0, result, 6);
    }

    [Fact]
    public void Median_EvenCount()
    {
        var result = _sut.Median(new List<double> { 1, 2, 3, 4 });
        Assert.Equal(2.5, result, 6);
    }

    [Fact]
    public void Variance_IdenticalValues_IsZero()
    {
        var result = _sut.Variance(new List<double> { 5, 5, 5, 5 });
        Assert.Equal(0.0, result, 6);
    }

    [Fact]
    public void Variance_KnownData()
    {
        var result = _sut.Variance(new List<double> { 2, 4, 4, 4, 5, 5, 7, 9 });
        Assert.True(result > 0);
    }

    [Fact]
    public void StandardDeviation_IsSqrtOfVariance()
    {
        var data = new List<double> { 2, 4, 4, 4, 5, 5, 7, 9 };
        double variance = _sut.Variance(data);
        double stddev = _sut.StandardDeviation(data);
        Assert.Equal(Math.Sqrt(variance), stddev, 6);
    }

    [Fact]
    public void Skewness_SymmetricData_NearZero()
    {
        var data = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var result = _sut.Skewness(data);
        Assert.True(Math.Abs(result) < 0.1);
    }

    [Fact]
    public void Percentile_50th_EqualsMedian()
    {
        var data = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        double median = _sut.Median(data);
        double p50 = _sut.Percentile(data, 50);
        Assert.Equal(median, p50, 4);
    }

    [Fact]
    public void PearsonCorrelation_PerfectPositive()
    {
        var a = new List<double> { 1, 2, 3, 4, 5 };
        var b = new List<double> { 2, 4, 6, 8, 10 };
        var result = _sut.PearsonCorrelation(a, b);
        Assert.Equal(1.0, result, 6);
    }

    [Fact]
    public void PearsonCorrelation_PerfectNegative()
    {
        var a = new List<double> { 1, 2, 3, 4, 5 };
        var b = new List<double> { 10, 8, 6, 4, 2 };
        var result = _sut.PearsonCorrelation(a, b);
        Assert.Equal(-1.0, result, 6);
    }

    [Fact]
    public void Min_ReturnsSmallest()
    {
        var result = _sut.Min(new List<double> { 5, -3, 10, 0, 7 });
        Assert.Equal(-3.0, result, 6);
    }

    [Fact]
    public void Max_ReturnsLargest()
    {
        var result = _sut.Max(new List<double> { 5, -3, 10, 0, 7 });
        Assert.Equal(10.0, result, 6);
    }

    [Fact]
    public void Summary_FieldsMatchIndividualMethods()
    {
        var data = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var summary = _sut.Summary(data);

        Assert.Equal(_sut.Min(data), summary.Min, 6);
        Assert.Equal(_sut.Max(data), summary.Max, 6);
        Assert.Equal(_sut.Mean(data), summary.Mean, 6);
        Assert.Equal(_sut.Median(data), summary.Median, 6);
        Assert.Equal(_sut.StandardDeviation(data), summary.StandardDeviation, 6);
        Assert.Equal(_sut.Variance(data), summary.Variance, 6);
    }
}
