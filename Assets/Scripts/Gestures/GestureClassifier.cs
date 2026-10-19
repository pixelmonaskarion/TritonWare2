using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ClassifiedGesture
{
    RightAngle,
    Square,
    Triangle,
    Star,
    Carrot
}

class GestureClassifier
{
    public ClassifiedGesture? ClassifyGesture(List<GestureSegment> segments)
    {
        var classifications = new Dictionary<ClassifiedGesture, float>()
        {
            { ClassifiedGesture.RightAngle, RightAngleClassifier(segments) },
            { ClassifiedGesture.Square, SquareClassifier(segments) },
            { ClassifiedGesture.Triangle, TriangleClassifier(segments) },
            { ClassifiedGesture.Carrot, CarrotClassifier(segments) },
        };

        var best = classifications.Aggregate((l, r) => l.Value < r.Value ? l : r);
        Debug.Log($"best: {best.Key}: {best.Value}");
        return best.Key;
    }

    public float RightAngleClassifier(List<GestureSegment> segments)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-2)/8f;
        if (segments.Count > 1) {
            score += Math.Abs(segments[0].angle-segments[1].angle-Mathf.PI/4f);
        }

        return score;
    }

    public float CarrotClassifier(List<GestureSegment> segments)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-2)/8f;
        if (segments.Count > 1) {
            score += Math.Abs(SimplifyAngle(segments[0].angle-segments[1].angle)-Mathf.Deg2Rad*50f);
        } else
        {
            score += 1f;
        }

        return score;
    }

    public float TriangleClassifier(List<GestureSegment> segments)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-3)/5f;
        if (segments.Count > 2) {
            score += Math.Abs(SimplifyAngle(segments[0].angle-segments[1].angle)-Mathf.PI/3f)/3f;
            score += Math.Abs(SimplifyAngle(segments[1].angle-segments[2].angle)-Mathf.PI/3f)/3f;
            score += Math.Abs(SimplifyAngle(segments[2].angle-segments[0].angle)-Mathf.PI/3f)/3f;
        } else
        {
            score += 1f;
        }

        return score;
    }

    public float SquareClassifier(List<GestureSegment> segments)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-4)/3f;
        if (segments.Count > 3) {
            score += Math.Abs(SimplifyAngle(segments[0].angle-segments[1].angle)-Mathf.PI/2f)/4f;
            score += Math.Abs(SimplifyAngle(segments[1].angle-segments[2].angle)-Mathf.PI/2f)/4f;
            score += Math.Abs(SimplifyAngle(segments[2].angle-segments[3].angle)-Mathf.PI/2f)/4f;
            score += Math.Abs(SimplifyAngle(segments[3].angle-segments[0].angle)-Mathf.PI/2f)/4f;
        } else
        {
            score += 1f;
        }

        return score;
    }

    public float SimplifyAngle(float angle)
    {
        return Mathf.Abs(angle) > Mathf.PI ? 2f*Mathf.PI - Math.Abs(angle) : Math.Abs(angle);
    }
}