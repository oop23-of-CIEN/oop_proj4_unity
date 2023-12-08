using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInfo
{
    public PointInfo()
    {
        isTurnClockwise = true;
        startAngle = 0f;
        rotationCenter = Vector2.zero;
        startTime = 0f;
        endTime = int.MaxValue;
    }
    public PointInfo(bool rotateDir, float angle, Vector2 center, float sTime)
    {
        isTurnClockwise = rotateDir;
        startAngle = angle;
        rotationCenter = center;
        startTime = sTime;
        endTime = int.MaxValue;
}
    ~PointInfo() { }
    public bool isTurnClockwise;
    public float startAngle;
    public Vector2 rotationCenter;
    public float startTime;
    public float endTime;
}
