using UnityEngine;
using System.Collections.Generic;

public class Shape {
    public List<Vector3> Points;
    Vector3 startPoint;
    Vector3 lastPoint;
    float lineAngle;
    const int distanceLimbetweenPoints = 5;
    const int distanceLimClosePoint = 10;
    const float angleLimDifferVertices = 12f;
    const int coordLim = 10; // погрешность на определение левой верхней вершины
    const float minLimSideLength = 0.5f; // погрешность при определении соотношения сторон
    const float maxLimSideLength = 1.5f; //
    const int angleLimDiffer = 25; // допустимая разница между углами примера и угла нарисованой фигуры


    public Shape(Vector3 point) {
        Points = new List<Vector3>();
        setVertices(point);
    }

    public Shape(List<Vector3> Points) {
        this.Points = Points;
        CloseShape(Points[0]);
    }

    public void AddPoint(Vector3 point) {
        Points.Add(point);
    }

    public void newPoint(Vector3 point) {
        if(Vector3.Distance(point, lastPoint) > distanceLimbetweenPoints) {
            float angle = GetAngleFrom2Roint(startPoint, point);
            if (lineAngle == 0) {
                lineAngle = angle;
                lastPoint = point;
                return;
            }
            if (IsVertices(lineAngle, angle)) {
                setVertices(lastPoint);
            }
            lastPoint = point;
        }
    }

    public void setVertices(Vector3 point) {
        startPoint = point;
        lastPoint = point;
        Points.Add(point);
        lineAngle = 0;
    }

    public void CloseShape(Vector3 curPoint) {
        if (Points.Count > 2) {
            Vector3 firstPoint = Points[0];
            if (Vector3.Distance(firstPoint, curPoint) < distanceLimClosePoint) {
                float angle = GetAngleFrom2Roint(Points[Points.Count - 1], firstPoint);
                float angle2 = GetAngleFrom2Roint(firstPoint, Points[1]);
                if (!IsVertices(angle, angle2)) {
                    Points[0] = Points[Points.Count - 1];
                    Points.RemoveAt(0);
                }
            }
        }
        Vector3 leftTopPoint = Points[0];
        int leftTopPointIndex = 0;
        for (int i = 0; i< Points.Count; i++) {
            if (Points[i].y > leftTopPoint.y + coordLim || (Points[i].y > leftTopPoint.y - coordLim && Points[i].x < leftTopPoint.x - coordLim)) {
                leftTopPoint = Points[i];
                leftTopPointIndex = i;
            }
        }
        List<Vector3> subPoints = Points.GetRange(leftTopPointIndex, Points.Count - leftTopPointIndex);
        subPoints.AddRange(Points.GetRange(0, leftTopPointIndex));
        Points = subPoints;
    }

    bool IsVertices(float lineAngle, float curAngle) {
        return Difference(lineAngle, curAngle) > angleLimDifferVertices;
    }

    float Difference(float firstAngle, float secondAngle) {
        float result1, result2;
        if (firstAngle > secondAngle) {
            result1 = firstAngle - secondAngle;
            result2 = secondAngle - firstAngle + 360;
        }
        else {
            result1 = secondAngle - firstAngle;
            result2 = firstAngle - secondAngle + 360;
        }
        return (result1 > result2) ? result2 : result1;

    }

    float GetAngleFrom2Roint(Vector3 point1, Vector3 point2) {
        return (Mathf.Atan2(point1.y - point2.y, point1.x - point2.x) / Mathf.PI * 180) + 180;
    }

    public bool EqualsShape(Shape shape) {
        if (Points.Count != shape.Points.Count) {
            return false;
        }
        return DirectedEquals(shape, true) || DirectedEquals(shape, false);
    }

    private bool DirectedEquals(Shape shape, bool direct) {
        float sidesRatio = 0;
        for (int i = 0; i < Points.Count; i++) {
            Vector3 point1;
            Vector3 point2;
            if (direct) {
                point1 = Points[i];
                point2 = Points[(i + 1) % Points.Count];
            }
            else {
                point1 = Points[i == 0 ? 0 : Points.Count - i];
                point2 = Points[Points.Count - 1 - i];
            }
            float angle1 = GetAngleFrom2Roint(point1, point2);
            float distance1 = Vector3.Distance(point1, point2);
            float angle2 = GetAngleFrom2Roint(shape.Points[i], shape.Points[(i + 1) % shape.Points.Count]);
            float distance2 = Vector3.Distance(shape.Points[i], shape.Points[(i + 1) % shape.Points.Count]);
            if (sidesRatio == 0) {
                sidesRatio = distance1 / distance2;
            }
            else {
                float x = sidesRatio / (distance1 / distance2);
                if (x < minLimSideLength || x > maxLimSideLength) {
                    return false;
                }
            }
            if (Difference(angle1, angle2) > angleLimDiffer) {
                return false;
            }
        }
        return true;
    }
}
