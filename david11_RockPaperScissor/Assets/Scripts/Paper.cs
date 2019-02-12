using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating paper
    public GameObject paperPrefab;

    // Speed the paper moves
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, speed * Time.deltaTime);
        transform.Rotate(Vector3.up, -speed * Time.deltaTime);
    }
}
