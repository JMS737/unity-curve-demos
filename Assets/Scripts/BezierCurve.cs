using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    public abstract class BezierCurve : MonoBehaviour
    {
        public Transform PointATransform;
        public Transform PointBTransform;
        public Transform PointCTransform;
        public Transform PointDTransform;

        public Vector3 PointA => PointATransform ? PointATransform.position : Vector3.zero;
        public Vector3 PointB => PointBTransform ? PointBTransform.position : Vector3.zero;
        public Vector3 PointC => PointCTransform ? PointCTransform.position : Vector3.zero;
        public Vector3 PointD => PointDTransform ? PointDTransform.position : Vector3.zero;
     
        [Range(0f, 1f)]
        public float T = 0f;

        [Range(0f, 1f)]
        public float PointRadius = 0.3f;

        [Range(1, 50)]
        public int CurveResolution = 50;

        public bool RenderCurve = true;
        public bool RenderPoints = true;
        public bool RenderHandles = true;
        public bool RenderInterpolation = false;

        protected virtual void OnDrawGizmos()
        {
            if (RenderInterpolation)
            {
                DrawInterpolation();
            }

            if (RenderCurve)
            {
                DrawBezierCurve(T, Color.white, Evaluate);
            }

            if (RenderHandles && RenderPoints)
            {
                Handles.color = Color.white;
                Handles.DrawDottedLine(PointA, PointB, 5);
                Handles.DrawDottedLine(PointD, PointC, 5);
                Handles.DrawWireDisc(PointB, Vector3.forward, PointRadius);
                Handles.DrawWireDisc(PointC, Vector3.forward, PointRadius);
            }

            if (RenderPoints)
            {
                Handles.color = Color.white;

                //Handles.DrawBezier(PointA, PointD, PointB, PointC, Color.magenta, null, 5);
                Handles.DrawSolidDisc(PointA, Vector3.forward, PointRadius);
                Handles.DrawSolidDisc(PointD, Vector3.forward, PointRadius);
            }

            //Debug.Log(Vector2.Lerp(new Vector2(1, 1), new Vector2(3, 2), 0.25f));
        }

        protected abstract void DrawInterpolation();

        protected abstract Vector3 Evaluate(float t);

        protected void DrawBezierCurve(float t, Color color, Func<float, Vector3> Evaluate)
        {
            Handles.color = color;
            var segmentSize = t / CurveResolution;

            Vector3[] points = new Vector3[CurveResolution + 1];

            points[0] = Evaluate(0);
            for (var i = 1; i <= CurveResolution; i++)
            {
                points[i] = Evaluate(i * segmentSize);
                //Handles.color = Color.magenta;
                //Handles.DrawLine(InterpolateBezier(segmentSize * i), InterpolateBezier((i + 1) * segmentSize), 5);
            }

            Handles.DrawAAPolyLine(5f, points);
        }
    }
}