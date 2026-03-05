
namespace OutSystems.Extension.MathNetNumerics.UnitTests;

public class FinancialActionsTests
{
    private readonly FinancialActions _sut = new();

    [Fact]
    public void FutureValue_StandardCase()
    {
        var result = _sut.FutureValue(1000, 0.05, 10);
        Assert.Equal(1628.89, result, 2);
    }

    [Fact]
    public void FutureValue_ZeroRate_ReturnsSameValue()
    {
        var result = _sut.FutureValue(1000, 0, 10);
        Assert.Equal(1000, result, 2);
    }

    [Fact]
    public void FutureValue_ZeroPeriods_ReturnsSameValue()
    {
        var result = _sut.FutureValue(1000, 0.05, 0);
        Assert.Equal(1000, result, 2);
    }

    [Fact]
    public void PresentValue_InverseOfFutureValue()
    {
        double fv = _sut.FutureValue(1000, 0.05, 10);
        double pv = _sut.PresentValue(fv, 0.05, 10);
        Assert.Equal(1000, pv, 4);
    }

    [Fact]
    public void PresentValue_ZeroRate()
    {
        var result = _sut.PresentValue(1000, 0, 5);
        Assert.Equal(1000, result, 2);
    }

    [Fact]
    public void NetPresentValue_TextbookCase()
    {
        var cashFlows = new List<double> { -1000, 300, 420, 680 };
        var result = _sut.NetPresentValue(0.10, cashFlows);
        Assert.Equal(130.73, result, 2);
    }

    [Fact]
    public void NetPresentValue_SingleCashFlow()
    {
        var cashFlows = new List<double> { 500 };
        var result = _sut.NetPresentValue(0.10, cashFlows);
        Assert.Equal(500, result, 2);
    }

    [Fact]
    public void InternalRateOfReturn_SimpleCase()
    {
        var cashFlows = new List<double> { -100, 110 };
        var result = _sut.InternalRateOfReturn(cashFlows);
        Assert.Equal(0.10, result, 4);
    }

    [Fact]
    public void InternalRateOfReturn_MultipleCashFlows()
    {
        var cashFlows = new List<double> { -1000, 300, 420, 680 };
        var result = _sut.InternalRateOfReturn(cashFlows);
        Assert.True(result > 0.15 && result < 0.20);
    }

    [Fact]
    public void PaymentAmount_StandardLoan()
    {
        // $200,000 at 6% annual for 360 months
        var result = _sut.PaymentAmount(200000, 0.06, 360);
        Assert.Equal(1199.10, result, 2);
    }

    [Fact]
    public void PaymentAmount_ZeroRate_DividesEvenly()
    {
        var result = _sut.PaymentAmount(12000, 0, 12);
        Assert.Equal(1000, result, 2);
    }

    [Fact]
    public void CompoundInterest_Monthly()
    {
        // $1000 at 5%, compounded monthly, 10 years
        var result = _sut.CompoundInterest(1000, 0.05, 12, 10);
        Assert.Equal(647.01, result, 2);
    }

    [Fact]
    public void CompoundInterest_AnnuallyMatchesFV()
    {
        double ci = _sut.CompoundInterest(1000, 0.05, 1, 10);
        double fv = _sut.FutureValue(1000, 0.05, 10);
        Assert.Equal(fv - 1000, ci, 4);
    }

    [Fact]
    public void StraightLineDepreciation_StandardCase()
    {
        var result = _sut.StraightLineDepreciation(10000, 1000, 5);
        Assert.Equal(1800, result, 2);
    }

    [Fact]
    public void AmortizationSchedule_CorrectLength()
    {
        var schedule = _sut.AmortizationSchedule(10000, 0.06, 12);
        Assert.Equal(12, schedule.Count);
    }

    [Fact]
    public void AmortizationSchedule_PrincipalSumsToLoan()
    {
        var schedule = _sut.AmortizationSchedule(10000, 0.06, 12);
        decimal totalPrincipal = schedule.Sum(e => e.PrincipalPortion);
        Assert.Equal(10000m, totalPrincipal, 0);
    }

    [Fact]
    public void AmortizationSchedule_FinalBalanceNearZero()
    {
        var schedule = _sut.AmortizationSchedule(10000, 0.06, 12);
        Assert.Equal(0m, schedule.Last().RemainingBalance, 0);
    }

    [Fact]
    public void AmortizationSchedule_FirstPeriodIsOne()
    {
        var schedule = _sut.AmortizationSchedule(10000, 0.06, 12);
        Assert.Equal(1, schedule.First().Period);
        Assert.Equal(12, schedule.Last().Period);
    }
}
