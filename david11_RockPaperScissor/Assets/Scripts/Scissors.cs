using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating scissors
    public GameObject scissorsPrefab;

    // Speed the scissors move
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
        // transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        // transform.Rotate(Vector3.back, -speed * Time.deltaTime);z
    }
}
