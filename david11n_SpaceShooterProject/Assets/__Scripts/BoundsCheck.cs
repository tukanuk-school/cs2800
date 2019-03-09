using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a GameObject on the screen
/// Note: this only works for an orthographic Main Camera at [ 0, 0, 0]
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;
    public bool isOnScreen = true;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;


    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offDown = offUp = false;

        // keeping player onscreen in x axis
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }

        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        // keeping player onscreen in y axis
        if (pos.y  > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offUp || offDown || offLeft || offRight);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
        }

    }

    // Draw the bounds in the Scene pane using onDrawGizmos()
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
