
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class DistributionsActionsTests
{
    private readonly DistributionsActions _sut = new();

    [Fact]
    public void NormalPdf_StandardAtZero()
    {
        var result = _sut.NormalPdf(0, 1, 0);
        Assert.Equal(0.3989, result, 4);
    }

    [Fact]
    public void NormalPdf_Symmetry()
    {
        double left = _sut.NormalPdf(0, 1, -1.5);
        double right = _sut.NormalPdf(0, 1, 1.5);
        Assert.Equal(left, right, 10);
    }

    [Fact]
    public void NormalCdf_StandardAtZero()
    {
        var result = _sut.NormalCdf(0, 1, 0);
        Assert.Equal(0.5, result, 6);
    }

    [Fact]
    public void NormalCdf_LargePositive_NearOne()
    {
        var result = _sut.NormalCdf(0, 1, 10);
        Assert.True(result > 0.9999);
    }

    [Fact]
    public void NormalInverseCdf_AtHalf()
    {
        var result = _sut.NormalInverseCdf(0, 1, 0.5);
        Assert.Equal(0.0, result, 6);
    }

    [Fact]
    public void NormalInverseCdf_At975_Approx196()
    {
        var result = _sut.NormalInverseCdf(0, 1, 0.975);
        Assert.Equal(1.96, result, 2);
    }

    [Fact]
    public void NormalSample_CorrectCount()
    {
        var samples = _sut.NormalSample(0, 1, 100);
        Assert.Equal(100, samples.Count);
    }

    [Fact]
    public void NormalSample_MeanApproximatesTarget()
    {
        var samples = _sut.NormalSample(5, 1, 10000);
        double mean = samples.Average();
        Assert.True(Math.Abs(mean - 5) < 0.1);
    }

    [Fact]
    public void ExponentialCdf_AtZero_IsZero()
    {
        var result = _sut.ExponentialCdf(1, 0);
        Assert.Equal(0.0, result, 6);
    }

    [Fact]
    public void ExponentialCdf_LargeX_NearOne()
    {
        var result = _sut.ExponentialCdf(1, 20);
        Assert.True(result > 0.999);
    }

    [Fact]
    public void ExponentialInverseCdf_AtHalf()
    {
        var result = _sut.ExponentialInverseCdf(1, 0.5);
        Assert.Equal(Math.Log(2), result, 4);
    }

    [Fact]
    public void PoissonProbability_ZeroEvents()
    {
        var result = _sut.PoissonProbability(1, 0);
        Assert.Equal(1.0 / Math.E, result, 4);
    }

    [Fact]
    public void PoissonCdf_AtZero_EqualsP0()
    {
        double p0 = _sut.PoissonProbability(1, 0);
        double cdf0 = _sut.PoissonCdf(1, 0);
        Assert.Equal(p0, cdf0, 10);
    }

    [Fact]
    public void BinomialProbability_FairCoin()
    {
        // P(5 heads in 10 flips) with p=0.5
        var result = _sut.BinomialProbability(0.5, 10, 5);
        Assert.Equal(0.2461, result, 4);
    }

    [Fact]
    public void BinomialCdf_AllSuccesses_IsOne()
    {
        var result = _sut.BinomialCdf(0.5, 10, 10);
        Assert.Equal(1.0, result, 6);
    }

    [Fact]
    public void StudentTInverseCdf_LargeDf_ApproachesNormal()
    {
        // With large df, Student-t approaches standard normal
        var result = _sut.StudentTInverseCdf(1000, 0.975);
        Assert.Equal(1.96, result, 1);
    }

    [Fact]
    public void ChiSquaredCdf_AtZero_IsZero()
    {
        var result = _sut.ChiSquaredCdf(5, 0);
        Assert.Equal(0.0, result, 6);
    }
}
