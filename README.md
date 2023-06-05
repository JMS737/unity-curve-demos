# Bernstein Bezier Curve

To derive the polynomial equations for a Cubic Bezier curve, you first start with the equations for calculating the curve using the Lerp implementation.

```
A = lerp ( P0, P1, t )
B = lerp ( P1, P2, t )
C = lerp ( P2, P3, t )
D = lerp (  A,  B, t )
E = lerp (  B,  C, t )
P = lerp (  D,  E, t )
```
Where `P(t)` equals the sum of all of the vectors.
```
P(t) = A + B + C + D + E + P
```

We can write this mathmatically as:

```
A = (1 - t)P0 + tP1
B = (1 - t)P1 + tP2
C = (1 - t)P2 + tP3
D = (1 - t)A  + tB
E = (1 - t)B  + tC
P = (1 - t)D  + tE
```

Expanding out completely and factorising for points P0 thru P3 you get a value of `P(t)`:

```
P(t) = P0 ( -t^3 + 3t^2 - 3t + 1) +
       P1 ( 3t^3 - 6t^2 + 3t    ) +
       P2 (-3t^3 + 3t^2         ) +
       P3 (  t^3                )
```