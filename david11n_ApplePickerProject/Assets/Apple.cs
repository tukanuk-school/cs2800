using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Set in Inspector")]
    public static float bottomY = -30f;

    // Start is called before the first frame update
    void Start()
    {
        //test
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);

            // Get a reference to the ApplePicker component of MainCamera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            // Call the public AppleDestroyed() method of apScript
            apScript.AppleDestroyed();
        }

    }
}
