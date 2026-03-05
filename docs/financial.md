# How to Calculate NPV, IRR, and Amortization in OutSystems

The `MathNetNumerics` module provides 8 Financial server actions for time value of money calculations, loan analysis, and depreciation in OutSystems Developer Cloud (ODC).

All interest rates are expressed as decimals: `0.05` means 5%, `0.10` means 10%.

## Actions

### FutureValue

Calculates the future value of a present amount using compound growth.

- **Formula:** `FV = presentValue * (1 + annualRate) ^ periods`
- **Parameters:** `presentValue` (double), `annualRate` (double), `periods` (int, non-negative)
- **Returns:** Future value in the same currency unit

### PresentValue

Calculates the present value of a future amount by discounting.

- **Formula:** `PV = futureValue / (1 + annualRate) ^ periods`
- **Parameters:** `futureValue` (double), `annualRate` (double), `periods` (int, non-negative)
- **Returns:** Present value in the same currency unit

### NetPresentValue

Computes the Net Present Value (NPV) of a cash flow series at a given discount rate. Each cash flow is discounted by `(1 + discountRate) ^ t`, where `t` is the zero-based period index.

- **Parameters:** `discountRate` (double), `cashFlows` (List of doubles, max 10M elements)
- **Returns:** NPV in the same currency unit as the cash flows

### InternalRateOfReturn

Computes the Internal Rate of Return (IRR) using Newton-Raphson iteration. Converges when |NPV| < 1e-10 or after 1,000 iterations.

- **Parameters:** `cashFlows` (List of doubles, minimum 2 elements, first value typically negative)
- **Returns:** IRR as a decimal (e.g., 0.12 for 12%)
- **Throws:** `InvalidOperationException` if the algorithm does not converge

### PaymentAmount

Calculates the fixed monthly payment for a fully amortizing loan.

- **Parameters:** `principal` (double), `annualRate` (double), `totalPeriods` (int, positive)
- **Returns:** Monthly payment amount
- **Example:** `PaymentAmount(200000, 0.06, 360)` returns approximately `1199.10`

### CompoundInterest

Calculates compound interest earned: FV - principal.

- **Formula:** `principal * (1 + annualRate / n) ^ (n * years) - principal`
- **Parameters:** `principal` (double), `annualRate` (double), `compoundingsPerYear` (int, positive), `years` (int, non-negative)
- **Returns:** Interest amount

### StraightLineDepreciation

Calculates annual depreciation using the straight-line method.

- **Formula:** `(cost - salvageValue) / usefulLifeYears`
- **Parameters:** `cost` (double), `salvageValue` (double), `usefulLifeYears` (int, positive)
- **Returns:** Annual depreciation amount

### AmortizationSchedule

Generates a complete loan amortization schedule. The last period is adjusted to eliminate rounding drift.

- **Parameters:** `principal` (double), `annualRate` (double), `totalPeriods` (int, 1 to 12,000)
- **Returns:** List of `AmortizationScheduleEntry` with Period, Payment, PrincipalPortion, InterestPortion, RemainingBalance

## Input Validation

All double parameters must be finite (no NaN or Infinity). Periods must be non-negative or positive depending on the action. Cash flow lists must not be null or empty and cannot exceed 10,000,000 elements. Amortization periods are capped at 12,000.
