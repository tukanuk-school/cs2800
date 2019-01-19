using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instaintiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree Moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Dropping apples every second
        Invoke("DropApple", 2f);

    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;   //b
        pos.x += speed * Time.deltaTime;
        transform.position = pos;           //d

        // Gradual rotation
        Quaternion rot = transform.rotation;
        if (rot.y < 170f)
        {
            rot.y += 0.5f * Time.deltaTime;
        }
        else { 
            rot.y -= 0.5f * Time.deltaTime;
        }
            transform.rotation = rot;


        // Changing Direction
        if ( pos.x < -leftAndRightEdge )
        {
            // a
            speed = Mathf.Abs(speed);       // Move right

            // b
        } else if ( pos.x > leftAndRightEdge )
        {
            // c
            speed = -Mathf.Abs(speed);      // Move left

        } 
    }

    private void FixedUpdate()
    {
        // Changing direction randomly is now time-based because of FixedUpdate()
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change direction
        }
    }
}
