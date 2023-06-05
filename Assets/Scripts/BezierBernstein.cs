using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierBernstein : BezierCurve
{
    public bool StackVectors = false;

    public bool RenderTangent = false;

    [Range(0f, 5f)]
    public float TangentLength = 2f;

    public bool RenderPath = false;

    protected override void DrawInterpolation()
    {
        if (!StackVectors)
        {
            Handles.color = Color.red;
            Handles.DrawAAPolyLine(5, new Vector3[] { Vector3.zero, EvaluateA(T) });
            Handles.color = Color.blue;
            Handles.DrawAAPolyLine(5, new Vector3[] { Vector3.zero, EvaluateB(T) });
            Handles.color = Color.green;
            Handles.DrawAAPolyLine(5, new Vector3[] { Vector3.zero, EvaluateC(T) });
            Handles.color = Color.yellow;
            Handles.DrawAAPolyLine(5, new Vector3[] { Vector3.zero, EvaluateD(T) });
        }
        else
        {
            var a = EvaluateA(T);
            var b = a + EvaluateB(T);
            var c = b + EvaluateC(T);
            var d = c + EvaluateD(T);

            Handles.color = Color.red;
            Handles.DrawAAPolyLine(5, new Vector3[] { Vector3.zero, a });
            Handles.color = Color.blue;
            Handles.DrawAAPolyLine(5, new Vector3[] { a, b });
            Handles.color = Color.green;
            Handles.DrawAAPolyLine(5, new Vector3[] { b, c });
            Handles.color = Color.yellow;
            Handles.DrawAAPolyLine(5, new Vector3[] { c, d });
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (RenderTangent)
        {
            var p = Evaluate(T);
            var tangent = EvaluateTangent(T).normalized;
            var normal = Vector3.Cross(tangent, Vector3.back).normalized;

            Handles.color = Color.red;
            Handles.DrawAAPolyLine(5, new Vector3[] { p, p + tangent * TangentLength });

            Handles.color = Color.green;
            Handles.DrawAAPolyLine(5, new Vector3[] { p, p + normal * TangentLength });
        }

        if (RenderPath)
        {
            DrawBezierCurve(T, Color.grey, t =>
            {
                var p = Evaluate(t);
                var tangent = EvaluateTangent(t).normalized;
                var normal = Vector3.Cross(tangent, Vector3.back).normalized;

                return p + normal * TangentLength;
            });
            DrawBezierCurve(T, Color.grey, t =>
            {
                var p = Evaluate(t);
                var tangent = EvaluateTangent(t).normalized;
                var normal = Vector3.Cross(tangent, Vector3.back).normalized;

                return p - normal * TangentLength;
            });
        }
    }

    protected override Vector3 Evaluate(float t)
    {
        return EvaluateA(t) + EvaluateB(t) + EvaluateC(t) + EvaluateD(t);
    }

    private Vector3 EvaluateTangent(float t)
    {
        return (PointA * ((-3 * Mathf.Pow(t, 2)) + (6 * t) - 3))
             + (PointB * ((9 * Mathf.Pow(t, 2)) - (12 * t) + 3))
             + (PointC * ((-9 * Mathf.Pow(t, 2)) + (6 * t)))
             + (PointD * (3 * Mathf.Pow(t, 2)));
    }


    private Vector3 EvaluateA(float t)
    {
        return PointA * (-Mathf.Pow(t, 3) + (3 * Mathf.Pow(t, 2)) - (3 * t) + 1);
    }

    private Vector3 EvaluateB(float t)
    {
        return PointB * ((3 * Mathf.Pow(t, 3)) - (6 * Mathf.Pow(t, 2)) + (3 * t));
    }

    private Vector3 EvaluateC(float t)
    {
        return PointC * (-(3 * Mathf.Pow(t, 3)) + (3 * Mathf.Pow(t, 2)));
    }

    private Vector3 EvaluateD(float t)
    {
        return PointD * Mathf.Pow(t, 3);
    }
}
