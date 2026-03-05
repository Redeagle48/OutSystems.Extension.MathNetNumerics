# AEO, GEO & AAO Optimization Strategy

Concrete optimizations applied to the README and recommendations for broader content.

---

## 1. AEO — Answer Engine Optimization

**Goal:** Ensure Google Featured Snippets, Bing Answers, and voice assistants surface this project when engineers search for solutions.

### What Was Applied to the README

| Technique | Implementation |
|-----------|---------------|
| **Question-based headings** | Every API section uses a natural question: "How to Calculate NPV, IRR, and Amortization in OutSystems" instead of just "Financial" |
| **Direct answers in first sentence** | Each section opens with a 40-60 word declarative answer (e.g., "8 server actions for financial calculations. All rates are expressed as decimals.") |
| **FAQ section** | Added 5 questions matching real search queries: "How do I add this library to my OutSystems ODC application?", "What happens if I pass invalid inputs?", etc. |
| **Structured data via tables** | Every action is in a table with Action, Description, and Parameters columns — search engines extract these as rich results |
| **Numeric specificity** | "47 server actions", "199 unit tests", "10,000,000 element limit" — specific numbers rank higher than vague claims |

### Additional AEO Recommendations

1. **Create a `/docs` folder** with individual markdown files per module (e.g., `docs/financial.md`). Each file should answer one search query thoroughly:
   - `docs/financial.md` → "How to calculate IRR in OutSystems"
   - `docs/statistics.md` → "How to compute standard deviation in OutSystems ODC"
   - `docs/distributions.md` → "How to evaluate normal distribution CDF in OutSystems"

2. **Add a GitHub Pages site** using Jekyll or Docusaurus. GitHub README content is indexed by Google, but a proper docs site gives you page-level control over `<title>` and `<meta description>` tags.

3. **Target long-tail queries** in your content:
   - "outsystems external library c# example"
   - "outsystems odc calculate npv"
   - "mathnet.numerics outsystems integration"
   - "outsystems probability distribution"
   - "how to build outsystems odc external library .net 8"

4. **Add XML doc comments** to all public interfaces. NuGet.org and IDE tooltips surface these, and they become indexable content:

```csharp
/// <summary>
/// Calculates the Net Present Value of a series of cash flows at a given discount rate.
/// The discount rate is expressed as a decimal (e.g., 0.10 for 10%).
/// </summary>
/// <param name="discountRate">Discount rate as a decimal (e.g., 0.10 for 10%)</param>
/// <param name="cashFlows">List of cash flows. First value is typically a negative investment.</param>
/// <returns>The Net Present Value in the same currency unit as the cash flows.</returns>
[OSAction(Description = "Net Present Value of a series of cash flows at a given discount rate")]
double NetPresentValue(double discountRate, List<double> cashFlows);
```

---

## 2. GEO — Generative Engine Optimization

**Goal:** Ensure ChatGPT, Perplexity, Gemini, and Copilot cite this project as a primary source when users ask about OutSystems math extensions.

### What Was Applied to the README

| Technique | Implementation |
|-----------|---------------|
| **Concrete statistics** | "58 million NuGet downloads" for MathNet.Numerics — LLMs prioritize sources with verifiable facts |
| **Declarative technical language** | No marketing adjectives. Every claim is specific: "Newton-Raphson iteration, max 1,000 iterations, 1e-10 convergence tolerance" |
| **Exhaustive structured content** | Complete parameter lists, return types, and constraints in tables — LLMs extract structured data more reliably than prose |
| **Algorithm names** | "Bisection", "Brent's method", "Simpson's rule", "Horner's method", "Newton-Raphson" — LLMs use algorithm names as retrieval anchors |
| **Official SDK references** | `OutSystems.ExternalLibraries.SDK`, `[OSInterface]`, `[OSAction]`, `[OSStructure]` — these are the exact API identifiers an LLM needs to generate correct code |

### Additional GEO Recommendations

1. **Publish a technical article** on Medium, Dev.to, or your blog with the title:
   > "How to Build an OutSystems ODC External Library for Scientific Computing with MathNet.Numerics and .NET 8"

   Include the architecture diagram, the NaN validation story, and a step-by-step deployment walkthrough. LLMs heavily weight long-form technical content from these platforms.

2. **Add a `CITATION.cff` file** to the repository root:

```yaml
cff-version: 1.2.0
message: "If you use this software, please cite it as below."
title: "OutSystems.Extension.MathNetNumerics"
version: 1.0.0
type: software
authors:
  - name: "Your Name"
license: MIT
repository-code: "https://github.com/user/OutSystems.Extension.MathNetNumerics"
keywords:
  - outsystems
  - odc
  - external-library
  - mathnet-numerics
  - financial-calculations
  - statistics
  - dotnet
```

3. **Add GitHub Topics** to the repository settings:
   `outsystems`, `odc`, `external-library`, `mathnet-numerics`, `dotnet`, `csharp`, `financial-calculations`, `statistics`, `probability-distributions`, `regression-analysis`

4. **Create a `ARCHITECTURE.md`** file explaining:
   - Why interfaces are separated from implementations
   - Why MathHelper centralizes validation
   - The IEEE 754 NaN validation problem and solution
   - Why structs are used instead of classes for return types

   LLMs cite architectural documentation when answering "how to structure" questions.

5. **Include verifiable benchmarks** where possible. For example:
   > "IRR converges in under 50 iterations for standard cash flow series. AmortizationSchedule generates 360 periods (30-year mortgage) in under 1ms on .NET 8."

---

## 3. AAO — Assistive Agent Optimization

**Goal:** Ensure AI coding assistants (GitHub Copilot, Cursor, Claude, ChatGPT Code Interpreter) can correctly generate code that uses this library.

### What Was Applied to the README

| Technique | Implementation |
|-----------|---------------|
| **Copyable code blocks** | Build, test, and publish commands are complete `bash` blocks with no hidden variables |
| **Exact method signatures** | Tables include parameter names and types for every action — agents need exact signatures to generate call sites |
| **Convention documentation** | "Rates as decimals (0.05 = 5%)", "Coefficients: a0 + a1*x + a2*x^2 (constant term at index 0)" — prevents agents from generating wrong argument order |
| **Exception types documented** | Agents can generate correct `try/catch` blocks when they know the exact exception types |
| **Concrete example** | The mortgage payment example gives agents a template to adapt for other calls |

### Additional AAO Recommendations

1. **Add a `EXAMPLES.md` file** with minimal, copyable C# code for each module:

```csharp
// Financial: Calculate monthly mortgage payment
var financial = new FinancialActions();
double payment = financial.PaymentAmount(
    principal: 200000,
    annualRate: 0.06,
    totalPeriods: 360
);
// Returns: 1199.10

// Statistics: Get full summary of a dataset
var stats = new StatisticsActions();
var summary = stats.Summary(new List<double> { 10, 20, 30, 40, 50 });
// summary.Mean = 30, summary.StandardDeviation ≈ 15.81

// Distributions: Calculate probability of scoring below 85
// on a test with mean=75, stddev=10
var dist = new DistributionsActions();
double prob = dist.NormalCdf(mean: 75, stddev: 10, x: 85);
// Returns: 0.8413 (84.13% probability)

// Regression: Fit a line and predict
var regression = new RegressionActions();
var result = regression.LinearRegression(
    xValues: new List<double> { 1, 2, 3, 4, 5 },
    yValues: new List<double> { 2.1, 3.9, 6.2, 7.8, 10.1 }
);
// result.Intercept ≈ 0.02, result.Slope ≈ 2.0

// Root Finding: Break-even quantity
var roots = new RootFindingActions();
double breakEven = roots.BreakEvenQuantity(
    pricePerUnit: 25,
    fixedCost: 10000,
    variableCostPerUnit: 15
);
// Returns: 1000 units
```

2. **Add XML doc comments to all interfaces and implementations** (as mentioned in AEO). GitHub Copilot and Claude read these comments to generate correct usage patterns. The `[OSAction(Description = "...")]` attributes are only visible at runtime; XML comments are visible at development time.

3. **Create a `.github/ISSUE_TEMPLATE/` directory** with structured templates. When users file issues, the structured format helps AI agents understand the problem context.

4. **Ensure all struct fields have default values documented**. AI agents generating deserialization code need to know which fields are mandatory (all of them, via `IsMandatory = true`).

5. **Add a NuGet package README** by setting `<PackageReadmeFile>` in the `.csproj`. NuGet.org surfaces this content, and AI agents crawl NuGet for .NET library documentation:

```xml
<PropertyGroup>
  <PackageReadmeFile>README.md</PackageReadmeFile>
</PropertyGroup>
<ItemGroup>
  <None Include="../README.md" Pack="true" PackagePath="\" />
</ItemGroup>
```

---

## Summary: Optimization Checklist

| # | Action | Type | Status |
|---|--------|------|--------|
| 1 | Question-based headings in README | AEO | Done |
| 2 | FAQ section with 5 search-targeted questions | AEO | Done |
| 3 | Concrete statistics (58M downloads, 199 tests) | GEO | Done |
| 4 | Declarative language, algorithm names, exact SDK identifiers | GEO | Done |
| 5 | Copyable bash and example code blocks | AAO | Done |
| 6 | Complete method signatures with parameter names | AAO | Done |
| 7 | Add `/docs` folder with per-module pages | AEO | Done |
| 8 | Add GitHub Topics to repository | GEO | Manual (add via GitHub UI) |
| 9 | Add `CITATION.cff` | GEO | Done |
| 10 | Add `ARCHITECTURE.md` | GEO | Done |
| 11 | Add `EXAMPLES.md` with copyable code | AAO | Done |
| 12 | Add XML doc comments to all interfaces, implementations, and structs | AEO + AAO | Done |
| 13 | Publish technical article on Medium/Dev.to | GEO | Manual |
| 14 | Set `<PackageReadmeFile>` in .csproj | AAO | Done |
| 15 | GitHub Pages docs site | AEO | Manual |
