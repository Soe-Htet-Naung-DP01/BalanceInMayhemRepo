using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLine : MonoBehaviour
{
    LineRenderer line;
    EdgeCollider2D edgeCol;

    public List<Vector2> linePoints = new List<Vector2>();
    void Start()
    {
        line = GetComponent<LineRenderer>();
        edgeCol = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        linePoints[0] = line.GetPosition(0);
        linePoints[1] = line.GetPosition(1);
        edgeCol.SetPoints(linePoints);
    }
}
