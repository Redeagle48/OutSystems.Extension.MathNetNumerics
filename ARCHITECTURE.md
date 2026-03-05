# Architecture

This document explains the design decisions behind the OutSystems.Extension.MathNetNumerics library.

## Overview

The library wraps [MathNet.Numerics](https://numerics.mathdotnet.com/) (58M+ NuGet downloads) as an [OutSystems Developer Cloud (ODC) External Library](https://success.outsystems.com/documentation/outsystems_developer_cloud/building_apps/extend_your_apps_with_external_logic_using_custom_code/external_libraries_sdk/). It exposes 48 server actions through a single ODC module, targeting .NET 8.0.

```
┌─────────────────────────────────────────────────┐
│            OutSystems Application               │
│         (Service Studio / ODC Runtime)           │
└──────────────────────┬──────────────────────────┘
                       │ [OSInterface] / [OSAction]
┌──────────────────────▼──────────────────────────┐
│     IMathNetNumerics.cs (single [OSInterface])   │
│   48 [OSAction] methods across 7 domains         │
│   XML doc comments for IDE + agent indexing      │
└──────────────────────┬──────────────────────────┘
                       │ implements (composition)
┌──────────────────────▼──────────────────────────┐
│  MathNetNumericsActions → 7 domain classes       │
│   Input validation via MathHelper                │
│   Delegates to MathNet.Numerics                  │
└──────────────┬───────────────────┬──────────────┘
               │                   │
┌──────────────▼────┐  ┌──────────▼──────────────┐
│   MathHelper.cs   │  │   MathNet.Numerics      │
│   - ValidateFinite│  │   - Statistics           │
│   - ValidatePos.  │  │   - Distributions        │
│   - ValidateRange │  │   - LinearRegression     │
│   - MaxCollection │  │   - Interpolation        │
│   - EvalPolynomial│  │   - Integration          │
└───────────────────┘  │   - RootFinding          │
                       └──────────────────────────┘
```

## Why a Single Interface with Composition Delegation

ODC requires exactly one `[OSInterface]` per external library. The `IMathNetNumerics` interface defines all 48 `[OSAction]` methods in a single file, organized by `#region` blocks (Financial, Statistics, Distributions, Regression, Interpolation, Integration, Root Finding).

The `MathNetNumericsActions` class implements this interface by delegating to 7 domain action classes via composition:

1. **ODC compatibility** — A single `[OSInterface]` ensures successful upload to the ODC portal. All 48 actions appear under one `MathNetNumerics` module in Service Studio.

2. **Separation of concerns** — Each domain class (`FinancialActions`, `StatisticsActions`, etc.) contains its own logic and validation. The composition class is a thin routing layer.

3. **Testability** — Unit tests instantiate the domain classes directly (`new FinancialActions()`) without needing the ODC runtime. This enables standard xUnit testing with `dotnet test`.

4. **Documentation surface** — XML doc comments on the interface are consumed by IDEs, NuGet package browsers, and AI coding assistants. The `[OSAction]` descriptions are consumed by OutSystems at runtime. Both audiences are served without duplication.

## Why MathHelper Centralizes Validation

All input validation runs through `MathHelper.cs` — a single internal static class with 10 validation methods. This design solves three problems:

### 1. IEEE 754 NaN Bypass

Standard C# comparison operators return `false` when one operand is `NaN`. This means naive validators like:

```csharp
if (value <= 0) throw ...;  // NaN <= 0 is false — NaN passes through
```

...silently accept `NaN` inputs. Every double-accepting validator in `MathHelper` explicitly checks `double.IsNaN()` and `double.IsInfinity()` before any comparison:

```csharp
internal static void ValidatePositive(double value, string paramName)
{
    if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
        throw new ArgumentOutOfRangeException(paramName, value, ...);
}
```

### 2. Resource Exhaustion Prevention

Since this library runs server-side in ODC, unbounded inputs could exhaust memory:

- **Collection size cap**: `MaxCollectionSize = 10,000,000` elements, enforced in `ValidateNotNullOrEmpty`
- **Amortization period cap**: 12,000 periods maximum (1,000 years of monthly payments)
- **Sample count cap**: `NormalSample` limited to 1,000,000 samples

### 3. Consistent Error Messages

All validators include the parameter name and constraint in the exception message, making debugging straightforward for OutSystems developers who may not have access to the C# source code.

## Why Structs Are Used for Return Types

The four return types (`AmortizationScheduleEntry`, `StatisticsSummary`, `LinearRegressionResult`, `TwoParameterFitResult`) are `struct` rather than `class` because:

1. **ODC SDK convention** — The `[OSStructure]` attribute is designed for value types that map to OutSystems structures
2. **Immutable semantics** — Results are computed once and returned. No shared state, no mutation after creation
3. **Allocation efficiency** — Small structs (2-5 fields of primitive types) avoid heap allocation overhead

All struct fields are marked `IsMandatory = true` in their `[OSStructureField]` attributes, ensuring OutSystems treats them as required fields.

## Polynomial Coefficient Convention

All polynomial operations (evaluation, root finding, integration) use the convention:

```
coefficients[0] = a0  (constant term)
coefficients[1] = a1  (linear coefficient)
coefficients[2] = a2  (quadratic coefficient)
...
```

Representing the polynomial: `a0 + a1*x + a2*x^2 + ...`

Evaluation uses Horner's method (`MathHelper.EvaluatePolynomial`) for numerical stability and O(n) performance.

## Test Architecture

Tests are split into two categories per module:

| Category | Naming | Purpose | Count |
|----------|--------|---------|-------|
| Functional | `*ActionsTests.cs` | Verify correct mathematical output with known inputs | 113 |
| Validation | `*ValidationTests.cs` | Verify proper rejection of invalid inputs (null, empty, NaN, Infinity, out of range) | 86 |

Total: 199 tests covering all 48 server actions.

## Security Considerations

- **No network calls** — Purely computational. No HTTP, no sockets, no DNS.
- **No file I/O** — No reading or writing to the filesystem.
- **No serialization** — No JSON/XML/binary deserialization of external data.
- **No dynamic code** — No reflection, no `Expression.Compile()`, no Roslyn scripting.
- **No secrets** — No API keys, connection strings, or credentials anywhere in the codebase.
- **CI/CD hardened** — GitHub Actions pinned to commit SHAs, explicit least-privilege `permissions` blocks, Dependabot enabled.
