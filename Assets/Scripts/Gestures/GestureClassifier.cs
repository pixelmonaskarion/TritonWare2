using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ClassifiedGestureType
{
    RightAngle,
    Square,
    Triangle,
    Star,
    Carrot
}

class ClassifiedGesture
{
    public ClassifiedGestureType gestureType;
    public Vector2 direction;
    public float confidence;

    public ClassifiedGesture(ClassifiedGestureType gestureType, Vector2 direction, float confidence)
    {
        this.gestureType = gestureType;
        this.direction = direction;
        this.confidence = confidence;
    }
}

class GameContext
{
    public Vector2 playerPosition;
    public Camera camera;

    public GameContext(Vector2 playerPosition, Camera camera)
    {
        this.camera = camera;
        this.playerPosition = playerPosition;
    }
}

class GestureClassifier
{
    public ClassifiedGesture ClassifyGesture(List<GestureSegment> segments, GameContext context)
    {
        var classifications = new Dictionary<ClassifiedGestureType, ClassifiedGesture>()
        {
            // { ClassifiedGestureType.RightAngle, RightAngleClassifier(segments, context) },
            { ClassifiedGestureType.Square, SquareClassifier(segments, context) },
            { ClassifiedGestureType.Triangle, TriangleClassifier(segments, context) },
            { ClassifiedGestureType.Carrot, CarrotClassifier(segments, context) },
        };

        var best = classifications.Aggregate((l, r) => l.Value.confidence < r.Value.confidence ? l : r);
        Debug.Log($"best: {best.Key}: {best.Value}");
        return best.Value;
    }

    public ClassifiedGesture RightAngleClassifier(List<GestureSegment> segments, GameContext context)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-2)/8f;
        if (segments.Count > 1) {
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[0].angle, segments[1].angle), Mathf.PI/4f));
            if (SimplifiedAngleDifference(Angle(context.camera.ScreenToWorldPoint(segments[0].start)-ToVec3(context.playerPosition, 0f)), segments[0].angle) < Mathf.Deg2Rad*20f)
            {
                score -= 5f;
            }
            return new(ClassifiedGestureType.RightAngle, (context.camera.ScreenToWorldPoint(segments[1].start)-ToVec3(context.playerPosition, 0f)).normalized, score);
        } else
        {
            return new(ClassifiedGestureType.RightAngle, Vector2.zero, float.PositiveInfinity);
        }
    }

    public ClassifiedGesture CarrotClassifier(List<GestureSegment> segments, GameContext context)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-2)/8f;
        if (segments.Count > 1) {
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[0].angle, segments[1].angle), Mathf.Deg2Rad*50f));
            return new(ClassifiedGestureType.Carrot, (context.camera.ScreenToWorldPoint(segments[1].start)-ToVec3(context.playerPosition, 0f)).normalized, score);
        } else
        {
            return new(ClassifiedGestureType.Carrot, Vector2.zero, float.PositiveInfinity);
        }
    }

    public ClassifiedGesture TriangleClassifier(List<GestureSegment> segments, GameContext context)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-3)/5f;
        if (segments.Count > 2) {
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[0].angle, segments[1].angle), Mathf.PI/3f))/3f;
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[1].angle, segments[2].angle), Mathf.PI/3f))/3f;
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[2].angle, segments[0].angle), Mathf.PI/3f))/3f;
            return new(ClassifiedGestureType.Triangle, (context.camera.ScreenToWorldPoint(segments[1].start)-ToVec3(context.playerPosition, 0f)).normalized, score);
        } else
        {
            return new(ClassifiedGestureType.Triangle, Vector2.zero, float.PositiveInfinity);
        }
    }

    public ClassifiedGesture SquareClassifier(List<GestureSegment> segments, GameContext context)
    {
        float score = 0f;
        score += Math.Abs(segments.Count-4)/3f;
        if (segments.Count > 3) {
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[0].angle, segments[1].angle), Mathf.PI/2f))/4f;
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[1].angle, segments[2].angle), Mathf.PI/2f))/4f;
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[2].angle, segments[3].angle), Mathf.PI/2f))/4f;
            score += Math.Abs(SimplifiedAngleDifference(SimplifiedAngleDifference(segments[3].angle, segments[0].angle), Mathf.PI/2f))/4f;
            return new(ClassifiedGestureType.Square, Vector2.zero, score);
        } else
        {
            return new(ClassifiedGestureType.Square, Vector2.zero, float.PositiveInfinity);
        }
    }

    float Angle(Vector2 difference)
    {
        if (difference.x == 0)
        {
            return Mathf.PI/2f * Mathf.Sign(difference.y);
        }
        return Mathf.Atan(difference.y/difference.x)+Mathf.PI * (Mathf.Sign(difference.x)+1f)/2f + Mathf.PI;
    }

    public float SimplifyAngle(float angle)
    {
        return Mathf.Abs(angle) > Mathf.PI ? 2f*Mathf.PI - Math.Abs(angle) : Math.Abs(angle);
    }

    public float SimplifiedAngleDifference(float a, float b)
    {
        return SimplifyAngle(SimplifyAngle(a)-SimplifyAngle(b));
    }

    Vector3 ToVec3(Vector2 xy, float z)
    {
        return new Vector3(xy.x, xy.y, z);
    }
}