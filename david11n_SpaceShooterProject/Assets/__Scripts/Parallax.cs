using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject poi; // player ship
    public GameObject[] panels; // scrolling panels
    public float scrollSpeed = -30f;
    public float motionMult = 0.25f; // how much panels reach to player movement

    private float panelHt; // heigh of each panel
    private float depth; // depth of panel (pos.z)

    // Start is called before the first frame update
    void Start()
    {
        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.x;

        // Set initial position of panel
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);

    }

    // Update is called once per frame
    void Update()
    {
        float tY, tx = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);

        if (poi != null)
        {
            tx = -poi.transform.position.x * motionMult;
        }

        // Position panels[0]
        panels[0].transform.position = new Vector3(tx, tY, depth);
        // then positon panels[1] where need to make a continuous starfield
        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tx, tY - panelHt, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tx, tY + panelHt, depth);
        }
    }
}
