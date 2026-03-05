# LinkedIn Post — OutSystems MathNet.Numerics Extension

> **Instructions:** Copy the text below into LinkedIn. Paste the GitHub link as the FIRST COMMENT, never in the post body.

---

## POST BODY

OutSystems can't calculate an IRR.
It can't run a Normal distribution. It can't fit a regression line.

And yet, 40% of enterprise low-code apps need financial or statistical logic at some point.

So you build it in C#, wrap it as an ODC External Library, expose it via `[OSInterface]` — and suddenly your OutSystems app can do what Excel's solver does.

Here's how we built it:

🛠️ **The stack**
.NET 8.0 + MathNet.Numerics (58M+ NuGet downloads) + OutSystems.ExternalLibraries.SDK 1.5.0.
47 server actions. 7 ODC modules. Zero network calls. Pure math.

📦 **What it covers**
— Financial: NPV, IRR (Newton-Raphson, 1000 iterations), amortization schedules, compound interest
— Statistics: mean, variance, skewness, kurtosis, Pearson correlation, percentiles
— Distributions: Normal, Poisson, Binomial, Exponential, Student's t, Chi-squared (PDF + CDF + InverseCDF)
— Regression: linear, polynomial, exponential fit, power fit, R-squared
— Interpolation: linear spline, cubic spline, barycentric polynomial
— Integration: trapezoidal rule, Simpson's rule
— Root finding: bisection, Brent's method, break-even analysis

🎯 **The security detail nobody talks about**
`double.NaN` bypasses standard C# validators. `NaN <= 0` evaluates to `false` in IEEE 754.
That means `ValidatePositive(double.NaN)` passes silently.
We had to add explicit `double.IsNaN()` and `double.IsInfinity()` checks across all 47 actions.
Without this, a low-code developer passing bad data gets a silent `NaN` result instead of an error.

💡 **Input validation matters in server-side libraries**
— Every `double` parameter rejects NaN and Infinity
— Lists are capped at 10M elements (prevents OOM in ODC runtime)
— Amortization schedule capped at 12,000 periods
— 199 unit tests: 113 functional + 86 validation edge cases

The whole thing deploys as a single zip to ODC. `dotnet publish` → zip → upload. Done.

If you're building OutSystems extensions and skipping input validation because "it's just math" — reconsider. IEEE 754 edge cases will silently corrupt your financial calculations.

Link to the full project, architecture, and code on GitHub in the comments 👇

---

What's your approach to extending OutSystems with custom .NET logic? Do you unit test your ODC External Libraries, or ship and pray?

#OutSystems #DotNet #LowCode #MathNetNumerics #ODC

---

## VISUAL FORMAT SUGGESTION

**Do NOT use the automatic Link Preview.** LinkedIn's algorithm penalizes external link cards.

**Recommended format: 4-page PDF carousel (1080x1350px)**

- **Page 1 (Hook):** "OutSystems can't calculate IRR. Here's how to fix it." — dark background, bold white text, OutSystems + .NET logos.
- **Page 2 (Architecture):** Clean diagram showing: `OutSystems App → [OSInterface] → C# Actions → MathNet.Numerics`. List the 7 modules as boxes.
- **Page 3 (The NaN trap):** Code block showing `ValidatePositive(double.NaN)` passing silently, then the fix with `double.IsNaN()`. Before/after format.
- **Page 4 (CTA):** "47 actions. 199 tests. 0 network calls. Open source on GitHub." + "Link in the comments."

**Alternative:** A single clean screenshot of the project structure or the `MathHelper.cs` validation code, with a short caption. Avoid stock photos entirely.
