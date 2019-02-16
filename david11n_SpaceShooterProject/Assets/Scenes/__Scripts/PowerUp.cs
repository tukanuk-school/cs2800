using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 6f; // sec PowerUp exists
    public float fadeTime = 4f; // Sec to fade

    [Header("Set Dynamically")]
    public WeaponType type;     // type of PowerUp
    public GameObject cube;     // Ref to cube child
    public TextMesh letter;     // Ref to the TextMesh
    public Vector3 rotPerSecond;// Euler rotation speed
    public float birthTime;

    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Renderer cubeRend;

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeRend = cube.GetComponent<Renderer>();

        // Set a random velocity
        Vector3 vel = Random.onUnitSphere; // get a ran XYZ vel
        vel.z = 0; // just XY vel
        vel.Normalize(); // normalize makes the length 1m
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;

        // Set the rot of GameObject to 0
        transform.rotation = Quaternion.identity; // no rotation

        // setup the rotPerSecond for the Cube child using rotMinMAx x &y
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y) );

        birthTime = Time.time;



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        // fade out a powerup. Default PowerUp exists for 10 sec, fade for 4
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        // for lifeTime sec, u <= 0. Then transition to 1 over fadeTime.

        // if u >= 1 destroy
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        // use u to determine alpha of PowerUp
        if (u > 0)
        {
            Color c = cubeRend.material.color;
            c.a = 1f - u;
            cubeRend.material.color = c;
            // fade the letter too but not as much
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;

        }

        if (!bndCheck.isOnScreen)
        {
            // if offscreen, destory
            Destroy(gameObject);
        }

    }

    public void SetType(WeaponType wt) 
    {
        // grad weaponDefiniation from Main
        WeaponDefinition def = Main.GetWeaponDefinition(wt);
        // set the color of the cube
        cubeRend.material.color = def.color;
        letter.text = def.letter;
        type = wt;
    }

    public void AbsorbedBy (GameObject targer)
    {
        Destroy(this.gameObject);
    }



}
