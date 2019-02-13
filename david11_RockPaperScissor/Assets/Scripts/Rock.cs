using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating rock
    public GameObject rockPrefab;

    // Speed the rock moves
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // spins the PRS around the middle of screen
        transform.RotateAround(Vector3.zero, Vector3.back, speed * Time.deltaTime);
        transform.Rotate(Vector3.up, -speed * Time.deltaTime);
    }

}

