# OutSystems.Extension.MathNetNumerics

> **What is this?** A .NET 8.0 External Library for OutSystems Developer Cloud (ODC) that wraps [MathNet.Numerics](https://numerics.mathdotnet.com/) to provide 47 server actions for financial analysis, statistics, probability distributions, regression, interpolation, integration, and root finding — directly inside OutSystems applications.

[![CI - Run Tests](https://github.com/user/OutSystems.Extension.MathNetNumerics/actions/workflows/test.yml/badge.svg)](https://github.com/user/OutSystems.Extension.MathNetNumerics/actions/workflows/test.yml)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![MathNet.Numerics 5.0.0](https://img.shields.io/badge/MathNet.Numerics-5.0.0-blue)](https://www.nuget.org/packages/MathNet.Numerics/5.0.0)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## Why Use This Extension?

OutSystems ODC does not natively support advanced mathematical operations such as NPV, IRR, probability distributions, or regression analysis. This external library bridges that gap by exposing 47 validated, production-ready server actions built on MathNet.Numerics — one of the most widely used open-source numerical libraries for .NET, with over 58 million NuGet downloads.

**Key capabilities:**

- Calculate NPV, IRR, amortization schedules, and compound interest for financial applications
- Run descriptive statistics (mean, variance, skewness, kurtosis) and Pearson correlation on datasets
- Evaluate Normal, Poisson, Binomial, Exponential, Student's t, and Chi-squared distributions
- Perform linear, polynomial, exponential, and power regression with prediction
- Interpolate data using linear spline, cubic spline, or barycentric polynomial methods
- Compute definite integrals using trapezoidal rule or Simpson's rule
- Find polynomial roots using bisection or Brent's method

---

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- An [OutSystems Developer Cloud (ODC)](https://www.outsystems.com/platform/developer-cloud/) environment for deployment
- Git (for cloning the repository)

---

## How to Build and Deploy

### 1. Clone and Build

```bash
git clone https://github.com/user/OutSystems.Extension.MathNetNumerics.git
cd OutSystems.Extension.MathNetNumerics

dotnet build OutSystems.Extension.MathNetNumerics/OutSystems.Extension.MathNetNumerics.csproj \
  --configuration Release
```

### 2. Run Tests

The project includes 199 unit tests covering all 47 server actions and input validation edge cases.

```bash
dotnet test OutSystems.Extension.MathNetNumerics/OutSystems.Extension.MathNetNumerics.sln \
  --configuration Release
```

### 3. Package for ODC Deployment

```bash
dotnet publish OutSystems.Extension.MathNetNumerics/OutSystems.Extension.MathNetNumerics.csproj \
  --configuration Release \
  --runtime linux-x64 \
  --self-contained false \
  --output ./publish

cd publish && zip -r ../MathNetNumerics.zip .
```

Upload `MathNetNumerics.zip` to your ODC portal under **External Libraries**.

---

## How to Use in OutSystems

After uploading the library to ODC, the following seven service actions modules become available in Service Studio:

| ODC Module | Domain | Actions |
|------------|--------|---------|
| `MathNet_Financial` | Time value of money, loan analysis, depreciation | 8 |
| `MathNet_Statistics` | Descriptive statistics, correlation | 11 |
| `MathNet_Distributions` | PDF, CDF, inverse CDF, random sampling | 12 |
| `MathNet_Regression` | Curve fitting, prediction, R-squared | 7 |
| `MathNet_Interpolation` | Linear, cubic spline, polynomial interpolation | 5 |
| `MathNet_Integration` | Trapezoidal rule, Simpson's rule | 2 |
| `MathNet_RootFinding` | Bisection, Brent's method, break-even | 3 |

---

## API Reference

### How to Calculate NPV, IRR, and Amortization in OutSystems (MathNet_Financial)

8 server actions for financial calculations. All rates are expressed as decimals (e.g., `0.05` = 5%).

| Action | Description | Parameters |
|--------|-------------|------------|
| `FutureValue` | Calculates future value of a present amount | `presentValue`, `annualRate`, `periods` |
| `PresentValue` | Calculates present value of a future amount | `futureValue`, `annualRate`, `periods` |
| `NetPresentValue` | NPV of a cash flow series at a discount rate | `discountRate`, `cashFlows` (List) |
| `InternalRateOfReturn` | IRR using Newton-Raphson iteration (max 1,000 iterations, 1e-10 tolerance) | `cashFlows` (List, first value typically negative) |
| `PaymentAmount` | Fixed monthly payment for a loan | `principal`, `annualRate`, `totalPeriods` |
| `CompoundInterest` | Compound interest earned (FV - PV) | `principal`, `annualRate`, `compoundingsPerYear`, `years` |
| `StraightLineDepreciation` | Annual depreciation: (cost - salvage) / life | `cost`, `salvageValue`, `usefulLifeYears` |
| `AmortizationSchedule` | Full schedule with per-period principal, interest, and remaining balance | `principal`, `annualRate`, `totalPeriods` (max 12,000) |

**Example — Calculate monthly mortgage payment:**

In OutSystems, drag the `PaymentAmount` action and set:
- `principal` = `200000` (loan amount)
- `annualRate` = `0.06` (6% annual rate)
- `totalPeriods` = `360` (30 years x 12 months)
- Returns: `1199.10` (monthly payment in the same currency unit)

### How to Compute Mean, Standard Deviation, and Correlation in OutSystems (MathNet_Statistics)

11 server actions for descriptive statistics and correlation analysis.

| Action | Description | Minimum Elements |
|--------|-------------|-----------------|
| `Mean` | Arithmetic mean | 1 |
| `Median` | Median value | 1 |
| `Variance` | Sample variance | 2 |
| `StandardDeviation` | Sample standard deviation | 2 |
| `Skewness` | Distribution asymmetry measure | 3 |
| `Kurtosis` | Distribution tail heaviness measure | 4 |
| `Percentile` | Value at a given percentile (0-100) | 1 |
| `PearsonCorrelation` | Pearson coefficient between two datasets (-1 to +1) | 2 (matched length) |
| `Min` | Minimum value | 1 |
| `Max` | Maximum value | 1 |
| `Summary` | Returns `StatisticsSummary` with all 8 measures | 4 |

### How to Evaluate Probability Distributions in OutSystems (MathNet_Distributions)

12 server actions covering six probability distributions.

| Action | Distribution | Description |
|--------|-------------|-------------|
| `NormalPdf` | Normal | Probability density function at x |
| `NormalCdf` | Normal | Cumulative probability P(X <= x) |
| `NormalInverseCdf` | Normal | Quantile function: x for a given probability p |
| `NormalSample` | Normal | Generate up to 1,000,000 random samples |
| `ExponentialCdf` | Exponential | Cumulative probability at x |
| `ExponentialInverseCdf` | Exponential | Quantile function for probability p |
| `PoissonProbability` | Poisson | P(X = k) for exactly k events |
| `PoissonCdf` | Poisson | P(X <= k) for k or fewer events |
| `BinomialProbability` | Binomial | P(X = k) for k successes in n trials |
| `BinomialCdf` | Binomial | P(X <= k) for k or fewer successes |
| `StudentTInverseCdf` | Student's t | Critical value for hypothesis testing |
| `ChiSquaredCdf` | Chi-squared | Cumulative probability for goodness-of-fit tests |

### How to Perform Regression and Curve Fitting in OutSystems (MathNet_Regression)

7 server actions for regression analysis and prediction.

| Action | Model | Returns |
|--------|-------|---------|
| `LinearRegression` | y = a + bx | `LinearRegressionResult` (Intercept, Slope) |
| `LinearPredict` | y = a + bx | Predicted y for a given x |
| `PolynomialRegression` | y = a0 + a1x + a2x^2 + ... | List of coefficients |
| `PolynomialPredict` | y = a0 + a1x + a2x^2 + ... | Predicted y for a given x |
| `RSquared` | Linear | Coefficient of determination (0 to 1) |
| `ExponentialFit` | y = A * e^(Bx) | `TwoParameterFitResult` (A, B) |
| `PowerFit` | y = A * x^B | `TwoParameterFitResult` (A, B) |

### How to Interpolate Data Points in OutSystems (MathNet_Interpolation)

5 server actions for estimating values between known data points.

| Action | Method | Description |
|--------|--------|-------------|
| `LinearInterpolate` | Linear spline | Piecewise linear estimate at a single x |
| `CubicSplineInterpolate` | Natural cubic spline | Smooth curve estimate at a single x |
| `PolynomialInterpolate` | Barycentric (Floater-Hormann) | Polynomial estimate at a single x |
| `BulkLinearInterpolate` | Linear spline | Linear estimates for a list of x points |
| `BulkCubicSplineInterpolate` | Natural cubic spline | Cubic spline estimates for a list of x points |

### How to Compute Numerical Integrals in OutSystems (MathNet_Integration)

2 server actions for definite integration.

| Action | Method | Description |
|--------|--------|-------------|
| `TrapezoidalFromData` | Trapezoidal rule | Area under curve from paired x,y data points |
| `PolynomialIntegral` | Simpson's rule (100 subdivisions) | Definite integral of a polynomial over [a, b] |

### How to Find Roots and Break-Even Points in OutSystems (MathNet_RootFinding)

3 server actions for equation solving.

| Action | Method | Description |
|--------|--------|-------------|
| `BisectionRoot` | Bisection | Finds polynomial root in [lowerBound, upperBound] |
| `BrentRoot` | Brent's method | Faster convergence root finding in [lowerBound, upperBound] |
| `BreakEvenQuantity` | Direct | Quantity where Revenue = Total Cost |

Polynomial coefficients use the convention: a0 + a1*x + a2*x^2 + ... (constant term at index 0).

---

## Data Structures

Four structures are returned by specific actions and are automatically available in OutSystems Service Studio:

| Structure | Fields | Returned By |
|-----------|--------|-------------|
| `AmortizationScheduleEntry` | `Period` (int), `Payment`, `PrincipalPortion`, `InterestPortion`, `RemainingBalance` (all decimal, 2dp) | `AmortizationSchedule` |
| `StatisticsSummary` | `Min`, `Max`, `Mean`, `Median`, `StandardDeviation`, `Variance`, `Skewness`, `Kurtosis` (all double) | `Summary` |
| `LinearRegressionResult` | `Intercept`, `Slope` (both double) | `LinearRegression` |
| `TwoParameterFitResult` | `A` (coefficient), `B` (exponent) (both double) | `ExponentialFit`, `PowerFit` |

---

## Input Validation and Security

All 47 server actions validate inputs before execution and throw descriptive exceptions. This prevents silent data corruption and protects against resource exhaustion in a server-side ODC context.

| Validation | Behavior | Exception Type |
|------------|----------|---------------|
| Null or empty list parameters | Rejected | `ArgumentNullException` / `ArgumentException` |
| Collection exceeds 10,000,000 elements | Rejected | `ArgumentOutOfRangeException` |
| `NaN` or `Infinity` on any `double` parameter | Rejected | `ArgumentOutOfRangeException` |
| Negative values where positive required (stddev, rates, periods) | Rejected | `ArgumentOutOfRangeException` |
| Probability outside (0, 1) or [0, 1] | Rejected | `ArgumentOutOfRangeException` |
| Mismatched x/y array lengths | Rejected | `ArgumentException` |
| Insufficient elements for statistical operation | Rejected | `ArgumentException` |
| Amortization periods exceeding 12,000 | Rejected | `ArgumentOutOfRangeException` |

All validators explicitly reject IEEE 754 special values (`NaN`, `+Infinity`, `-Infinity`) to prevent silent bypass of range checks.

---

## Project Structure

```
OutSystems.Extension.MathNetNumerics/
├── Interfaces/
│   ├── IFinancial.cs              # 8 actions — NPV, IRR, amortization, depreciation
│   ├── IStatistics.cs             # 11 actions — mean, variance, correlation
│   ├── IDistributions.cs          # 12 actions — Normal, Poisson, Binomial, etc.
│   ├── IRegression.cs             # 7 actions — linear, polynomial, exponential fit
│   ├── IInterpolation.cs          # 5 actions — linear, cubic spline, polynomial
│   ├── IIntegration.cs            # 2 actions — trapezoidal, Simpson's rule
│   └── IRootFinding.cs            # 3 actions — bisection, Brent, break-even
├── Implementation/
│   ├── FinancialActions.cs
│   ├── StatisticsActions.cs
│   ├── DistributionsActions.cs
│   ├── RegressionActions.cs
│   ├── InterpolationActions.cs
│   ├── IntegrationActions.cs
│   └── RootFindingActions.cs
├── Models/
│   ├── AmortizationScheduleEntry.cs
│   ├── StatisticsSummary.cs
│   ├── LinearRegressionResult.cs
│   └── TwoParameterFitResult.cs
├── MathHelper.cs                  # Centralized input validation (ValidateFinite, ValidatePositive, etc.)
└── OutSystems.Extension.MathNetNumerics.csproj

OutSystems.Extension.MathNetNumerics.UnitTests/
├── *ActionsTests.cs               # Functional correctness tests (113 tests)
└── *ValidationTests.cs            # Input validation + NaN/Infinity edge cases (86 tests)
```

---

## Dependencies

| Package | Version | Downloads | Purpose |
|---------|---------|-----------|---------|
| [MathNet.Numerics](https://www.nuget.org/packages/MathNet.Numerics/5.0.0) | 5.0.0 | 58M+ | Core mathematical algorithms |
| [OutSystems.ExternalLibraries.SDK](https://www.nuget.org/packages/OutSystems.ExternalLibraries.SDK/1.5.0) | 1.5.0 | — | `[OSInterface]`, `[OSAction]`, `[OSStructure]` attributes for ODC |

**Test dependencies:** xUnit 2.7.0, Microsoft.NET.Test.Sdk 17.9.0, Coverlet 6.0.1

---

## CI/CD

| Workflow | Trigger | What It Does |
|----------|---------|-------------|
| [`test.yml`](.github/workflows/test.yml) | Push or PR to `main` | Restores, builds, runs 199 tests with XPlat code coverage, uploads results |
| [`release.yml`](.github/workflows/release.yml) | Git tag `v*.*.*` | Runs tests, publishes for `linux-x64`, creates GitHub Release with deployment zip |

Both workflows pin GitHub Actions to commit SHAs and use explicit `permissions` blocks (least privilege).

Automated dependency updates are managed via [Dependabot](.github/dependabot.yml) for both NuGet packages and GitHub Actions.

---

## Frequently Asked Questions

### How do I add this library to my OutSystems ODC application?

Build the project, create the deployment zip using `dotnet publish`, and upload it to your ODC portal under External Libraries. The seven interface modules will appear automatically in Service Studio.

### What happens if I pass invalid inputs?

Every action validates its inputs and throws a specific .NET exception (`ArgumentNullException`, `ArgumentException`, or `ArgumentOutOfRangeException`) with a message indicating which parameter failed and why. OutSystems surfaces these as errors in the application flow.

### Are NaN and Infinity values handled?

Yes. All `double` parameters are validated for finiteness. Passing `double.NaN`, `double.PositiveInfinity`, or `double.NegativeInfinity` to any action throws `ArgumentOutOfRangeException`.

### What is the maximum dataset size?

List parameters accept up to 10,000,000 elements. The `NormalSample` action generates up to 1,000,000 samples. The `AmortizationSchedule` action supports up to 12,000 periods.

### Does this library make network calls or access the filesystem?

No. All operations are purely computational. There is no file I/O, no network access, no serialization of external data, and no dynamic code generation.

---

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Write tests for new functionality
4. Ensure all 199+ tests pass (`dotnet test`)
5. Submit a pull request

---

## License

See [LICENSE](LICENSE) for details.
