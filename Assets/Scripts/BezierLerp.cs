using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierLerp : BezierCurve
{

    protected override void DrawInterpolation()
    {
        Handles.color = Color.red;

        Vector3[] primaryLines = new Vector3[]
        {
            PointA, PointB, PointC, PointD
        };
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.magenta };

        Handles.DrawAAPolyLine(5, primaryLines);

        var aT = Vector3.Lerp(PointA, PointB, T);
        var bT = Vector3.Lerp(PointB, PointC, T);
        var cT = Vector3.Lerp(PointC, PointD, T);

        Handles.color = Color.green;

        Handles.DrawSolidDisc(aT, Vector3.forward, PointRadius * 0.5f);
        Handles.DrawSolidDisc(bT, Vector3.forward, PointRadius * 0.5f);
        Handles.DrawSolidDisc(cT, Vector3.forward, PointRadius * 0.5f);

        Handles.DrawAAPolyLine(5, new Vector3[] { aT, bT, cT});

        var dT = Vector3.Lerp(aT, bT, T);
        var eT = Vector3.Lerp(bT, cT, T);

        Handles.color = Color.blue;
        Handles.DrawSolidDisc(dT, Vector3.forward, PointRadius * 0.5f);
        Handles.DrawSolidDisc(eT, Vector3.forward, PointRadius * 0.5f);

        Handles.DrawAAPolyLine(5, new Vector3[] { dT, eT });

        var fT = Vector3.Lerp(dT, eT, T);

        Handles.color = Color.magenta;

        Handles.DrawSolidDisc(fT, Vector3.forward, PointRadius * 0.5f);
    }

    protected override Vector3 Evaluate(float t)
    {
        var aT = Vector3.Lerp(PointA, PointB, t);
        var bT = Vector3.Lerp(PointB, PointC, t);
        var cT = Vector3.Lerp(PointC, PointD, t);
        var dT = Vector3.Lerp(aT, bT, t);
        var eT = Vector3.Lerp(bT, cT, t);
        var fT = Vector3.Lerp(dT, eT, t);

        return fT;
    }
}
