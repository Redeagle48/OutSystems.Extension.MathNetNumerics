# How to Find Roots and Break-Even Points in OutSystems

The `MathNet_RootFinding` module provides 3 server actions for equation solving and break-even analysis in OutSystems Developer Cloud (ODC).

## Actions

### BisectionRoot

Finds a root of a polynomial within [lowerBound, upperBound] using the bisection method. The function values at the bounds must have opposite signs (sign change required).

- **Parameters:** `coefficients` (List), `lowerBound` (double), `upperBound` (double)
- **Convergence:** Guaranteed for continuous functions with a sign change

### BrentRoot

Finds a root using Brent's method. Generally converges faster than bisection while maintaining guaranteed convergence.

- **Parameters:** `coefficients` (List), `lowerBound` (double), `upperBound` (double)
- **Recommended over bisection** for most use cases due to faster convergence

### BreakEvenQuantity

Calculates the quantity where total revenue equals total cost.

- **Formula:** `fixedCost / (pricePerUnit - variableCostPerUnit)`
- **Parameters:** `pricePerUnit` (double), `fixedCost` (double, non-negative), `variableCostPerUnit` (double, must be less than pricePerUnit)
- **Returns:** Break-even quantity (number of units)

## Polynomial Coefficient Convention

All polynomial operations use the convention: `coefficients[0]` is the constant term, `coefficients[1]` is the linear coefficient, and so on.

Example: To represent `x^2 - 4`, use `[-4, 0, 1]`.

## Input Validation

Coefficients must not be null or empty. Bounds must be finite and `lowerBound < upperBound`. For BreakEvenQuantity, all parameters must be finite, fixedCost must be non-negative, and pricePerUnit must exceed variableCostPerUnit.
