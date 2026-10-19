using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GestureRecognizer
{
    public List<Vector2> samples = new();

    public void AddSample(Vector2 sample)
    {
        samples.Add(sample);
    }

    public List<GestureSegment> CharacterizeSamples(Action<DebugDot> debugPoint)
    {
        List<GestureSegment> fullSegments = SegmentsAtFrequency(1);
        List<GestureSegment> combinedSegments = new();
        // for (int i = 0; i < 10; i++) {
            combinedSegments = MergeSegments(fullSegments);
        // }
        return combinedSegments;
    }

    public List<GestureSegment> CharacterizeSamplesOld(Action<DebugDot> debugPoint)
    {
        List<GestureSegment> forthSegments = SegmentsAtFrequency(4);
        List<float> forthDifferences = AngleDifferences(forthSegments);
        List<GestureSegment> halfSegments = SegmentsAtFrequency(2);
        List<float> halfDifferences = AngleDifferences(halfSegments);
        List<GestureSegment> fullSegments = SegmentsAtFrequency(1);
        List<float> fullDifferences = AngleDifferences(fullSegments);
        List<GestureSegment> combinedSegments = new();

        foreach (GestureSegment s in fullSegments)
        {
            debugPoint(new(s.start, Color.green, s.angle, 1.0f));
        }

        List<int> discontinuities = new();
        for (int i = 0; i < fullSegments.Count-1; i++)
        {
            
            // GestureSegment f1s0 = fullSegments[i];
            // GestureSegment f1s1 = fullSegments[i+1];
            // float diff1 = f1s1.angle-f1s0.angle;
            // float? diff2 = null;
            // float? diff4 = null;
            // if (i/2+1 < halfSegments.Count) {
            //     GestureSegment f2s0 = halfSegments[i/2];
            //     GestureSegment f2s1 = halfSegments[i/2+1];
            //     diff2 = f2s1.angle-f2s0.angle;
            // }
            // if (i/4+1 < forthSegments.Count) {
            //     GestureSegment f4s0 = forthSegments[i/4];
            //     GestureSegment f4s1 = forthSegments[i/4+1];
            //     diff4 = f4s1.angle-f4s0.angle;
            // }
            // float? differenceDifference = null;
            // if (diff4 != null)
            // {
            //     differenceDifference = (differenceDifference ?? 0f) + Mathf.Abs(diff1 - (float)diff4);
            // }
            // if (diff2 != null)
            // {
            //     differenceDifference = (differenceDifference ?? 0f) + Mathf.Abs(diff1 - (float)diff2);
            // }
            // if (diff2 != null && diff4 != null)
            // {
            //     differenceDifference = (differenceDifference ?? 0f) + Mathf.Abs((float)diff2 - (float)diff4);
            // }
            // if (differenceDifference == null)
            // {
            //     differenceDifference = diff1;
            // }

            float fullDifference = fullDifferences[i];
            float halfDifference = halfDifferences[Math.Min(i/2, halfDifferences.Count-1)];
            float forthDifference = forthDifferences[Math.Min(i/4, forthDifferences.Count-1)];
            // float halfDifference = halfDifferences[i/2];
            // float forthDifference = halfDifferences[i/4];
            float differenceDifference = Mathf.Abs(fullDifference-halfDifference)+Mathf.Abs(fullDifference-forthDifference)+Mathf.Abs(halfDifference-forthDifference);


            float tollerance = 3f;
            if (differenceDifference > tollerance)
            {
                discontinuities.Add(i);
            }
        }
        int segmentsCombined = 0;
        float segmentCombinedLength = 0f;
        Vector2? segmentStart = null;
        float averageAngle = 0;
        int nextDiscontinuity = -1;
        if (discontinuities.Count > 0)
        {
            nextDiscontinuity = discontinuities[0];
            discontinuities.RemoveAt(0);
        }
        for (int i = 0; i < fullSegments.Count; i++)
        {
            if (segmentStart == null)
            {
                segmentStart = fullSegments[i].start;
            }
            segmentsCombined += 1;
            segmentCombinedLength += fullSegments[i].length;
            averageAngle += fullSegments[i].angle; 
            if (i == nextDiscontinuity) {
                debugPoint(new(fullSegments[i].start, Color.red, float.NaN, 1.2f));
                combinedSegments.Add(new((Vector2)segmentStart, segmentCombinedLength, averageAngle/segmentsCombined));
                segmentsCombined = 0;
                segmentCombinedLength = 0;
                averageAngle = 0;
                segmentStart = null;
                if (discontinuities.Count > 0)
                {
                    nextDiscontinuity = discontinuities[0];
                    discontinuities.RemoveAt(0);
                }
            }
        }
        if (segmentsCombined > 0) {
            combinedSegments.Add(new((Vector2)segmentStart, segmentCombinedLength, averageAngle/segmentsCombined));
        }

        return combinedSegments;
    }

    public List<GestureSegment> MergeSegments(List<GestureSegment> inputSegments)
    {
        List<GestureSegment> outputSegments = new();
        int i = 0;
        int segmentStart = 0;
        float angle = 0;
        while (i < inputSegments.Count) 
        {
            GestureSegment segmentHere = inputSegments[i];
            int combinedSegmentsSoFar = i-segmentStart;
            if ((combinedSegmentsSoFar == 0 || Math.Abs(angle-segmentHere.angle) < 2.0) && i != inputSegments.Count-1)
            {
                angle = segmentHere.angle;
            } else
            {
                Vector2 start = inputSegments[segmentStart].start;
                Vector2 end = segmentHere.start+new Vector2(Mathf.Cos(segmentHere.angle), Mathf.Sin(segmentHere.angle))*segmentHere.length;
                Vector2 difference = end-start;
                float newAngle = Angle(difference);
                if (difference.magnitude > 20.0) {
                    outputSegments.Add(new GestureSegment(start, difference.magnitude, newAngle));
                }
                segmentStart = i+1;
                angle = 0;
            }
            i += 1;
        }
        // if (segmentStart != i)
        // {
        //     Vector2 start = inputSegments[segmentStart].start;
        //     Vector2 end = segmentHere.start+new Vector2(Mathf.Cos(segmentHere.angle), Mathf.Sin(segmentHere.angle))*segmentHere.length;
        //     Vector2 difference = end-start;
        //     float newAngle = Angle(difference);
        //     outputSegments.Add(new GestureSegment(start, difference.magnitude, newAngle));
        // }
        return outputSegments;
    }

    public List<GestureSegment> SegmentsAtFrequency(int frequency)
    {
        List<GestureSegment> segments = new();
        if (samples.Count == 0)
        {
            return segments;
        }
        segments.Add(SegmentBetween(samples[0], samples[Math.Min(frequency, samples.Count-1)]));
        int i = frequency;
        for (; i < samples.Count-frequency; i += frequency)
        {
            Vector2 sampleHere = samples[i];
            Vector2 nextSample = samples[i+frequency];
            GestureSegment segment = SegmentBetween(sampleHere, nextSample);
            segments.Add(segment);
        }
        if (i < samples.Count-1)
        {
            segments.Add(SegmentBetween(samples[i], samples[^1]));
        }
        return segments;
    }

    public GestureSegment SegmentBetween(Vector2 start, Vector2 end)
    {   
        Vector2 difference = end-start;
        float angle = Angle(difference);
        return new(start, difference.magnitude, angle);
    }

    public List<float> AngleDifferences(List<GestureSegment> segments)
    {
        List<float> differences = new();
        for (int i = 0; i < segments.Count-1; i++)
        {
            differences.Add(segments[i+1].angle-segments[i].angle);
        }
        return differences;
    }

    Vector3 ToVec3(Vector2 xy, float z)
    {
        return new Vector3(xy.x, xy.y, z);
    }

    float Angle(Vector2 difference)
    {
        if (difference.x == 0)
        {
            return Mathf.PI/2f * Mathf.Sign(difference.y);
        }
        return Mathf.Atan(difference.y/difference.x)+Mathf.PI * (Mathf.Sign(difference.x)+1f)/2f + Mathf.PI;
    }
}

public class GestureSegment
{
    public Vector2 start; 
    public float length; 
    public float angle;

    public GestureSegment(Vector2 start, float length, float angle)
    {
        this.start = start;
        this.length = length;
        this.angle = angle;
    }
}

public class DebugDot
{
    public Vector2 start;
    public Color color;
    public float angle;
    public float scale;

    public DebugDot(Vector2 start, Color color, float angle, float scale)
    {
        this.start = start;
        this.color = color;
        this.angle = angle;
        this.scale = scale;
    }
}