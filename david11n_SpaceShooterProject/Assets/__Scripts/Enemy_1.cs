using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    // seconds for a fill sine wave
    public float waveFrequency = 2;
    // sine wave width in meters
    public float waveWidth = 4;
    public float waveRotY = 45;

    private float xO; // the initial x value of pos
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        xO = pos.x;
        birthTime = Time.time;          
    }

    // overide the move function of Enemy
    public override void Move()
    {
        Vector3 tempPos = pos;
        // theta adjust based on time
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = xO + waveWidth * sin;
        pos = tempPos;

        // rotate a bit about y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        // base Move() still handle the y movement 
        base.Move();

        print("TESST" + bndCheck.isOnScreen);
    }
}
