using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    // Enemy 3 follows a bezier curve which is a linear interpolation b
    // between more than two points
    [Header("Set in Inspector: Enemy_3")]
    public float lifeTime = 5;

    [Header("Set Dynamically: Enemy_3")]
    public Vector3[] vectorPoints;
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        // Set the health and points of this enemy type
        health = ScoreManager.E3;
        points = ScoreManager.E3points;

        vectorPoints = new Vector3[3];

        // Main.SpawnEnemy() sets the start position
        vectorPoints[0] = pos;

        // Set xMin and xMax the smae way that Main.SpawnEnemy() does
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        // pick a random middle position in the bottom half of the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -bndCheck.camHeight + Random.Range(2.75f, 2);
        vectorPoints[1] = v;

        // pick a final position above the screen
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        vectorPoints[2] = v;

        // Set the birthtime to now
        birthTime = Time.time;

        // set the color of the materials based on the ScoreManager
        SetColour(ScoreManager.E3Color);
    }

    public override void Move()
    {
        // bezier cureves work based on a u value between 0 &1
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        // iterpolate the three bezier curve points
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * vectorPoints[0] + u * vectorPoints[1];
        p12 = (1 - u) * vectorPoints[1] + u * vectorPoints[2];
        pos = (1 - u) * p01 + u * p12;
    }
}
