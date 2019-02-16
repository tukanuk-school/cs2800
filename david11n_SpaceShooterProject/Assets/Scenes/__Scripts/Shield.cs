using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set dynamicaally")]
    public int levelShown = 0;

    // not in inspector
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Read the current shield level from the Player singleton
        int currLevel = Mathf.FloorToInt(Player.S.shieldLevel);

        // if different from the level shown
        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            // adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        // rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
