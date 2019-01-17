using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;


    // Start is called before the first frame update
    void Start()
    {
        // Find reference to the ScoreCounter GameObject
        GameObject scoreGo = GameObject.Find("ScoreCounter");
        // Get the text component of that GameObject
        scoreGT = scoreGo.GetComponent<Text>();
        // Set the starting points to 0
        scoreGT.text = "0";    
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current screen position of the mouse from input 
        Vector3 mousePos2D = Input.mousePosition;

        // The camera's z position sets how far to push the mouse in to 3d
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into the 3D game world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Baskeet ot the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Find out what hit this basket
        GameObject collidedWith = collision.gameObject;
        if ( collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
        }

        // parse the text of the scoreGT into an int
        int score = int.Parse(scoreGT.text);
        // Add points for catching apples
        score += 100;
        // Convert the score back to a string and display it
        scoreGT.text = score.ToString();

        // Track the high school
        if (score > HighScore.score) {
            HighScore.score = score;
        }
    }
}
