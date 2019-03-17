using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    //public int score = 100;
    public float showDamageDuration = 0.1f; // seconds to show damage
    public float powerUpDropChance = 1f; // Chance to drop of PowerUp

    [Header("Set dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    // enemy hp and points
    public float health; 
    public int points; 

    protected BoundsCheck bndCheck;

    // explosion AudioSource
    protected AudioSource explosionAS;

    // should I move?
    bool shouldMove = true;

    // old colour, after a colour change
    Color oldColour; // = new Color();

    // This is a property
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    //private GameObject levelPanel;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        // get materials and colors for this GameObject and its children
        materials = Utils.GetMaterials(gameObject);

        //originalColors = new Color[materials.Length];
        //for (int i=0 ; i< materials.Length ; i++)
        //{
        //    originalColors[i] = materials[i].color;
        //}

        GameObject go = GameObject.Find("explosionAS");
        explosionAS = go.GetComponent<AudioSource>();

        // this is a total hack to get the enemies to stop moving in settings screens.
        // see if(shouldMove) Move() in Update()
        if (SceneManager.GetActiveScene().name == "_Scene_bronze" ||
            SceneManager.GetActiveScene().name == "_Scene_silver" ||
            SceneManager.GetActiveScene().name == "_Scene_gold" ||
            SceneManager.GetActiveScene().name == "_Scene_enemies" )
        {
            shouldMove = false;
        }

        //levelPanel = GameObject.Find("levelPanel");

    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the health and points of this enemy type
        health = ScoreManager.E0;
        points = ScoreManager.E0points;

        // set the color of the materials based on the ScoreManager
        SetColour(ScoreManager.E0Color);
    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy should move, move!
        // part two of my hack to prevent enemies from moving in certain scenes.
        if (shouldMove) Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown) 
        {
            Destroy(gameObject);
        }
    }

    // set the color of the materials based on the ScoreManager
    public void SetColour(Color colour)
    {
        //Debug.Log ("oldcolor r: " + oldColour.r);
        //oldColour = materials[0].color;

        foreach (var material in materials)
        {
            material.color = colour;
        }

        // when the colour is change set new original colours
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    // the base movements for each enemy. All enemies use the y axis behaviour
    public virtual void Move() 
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectilePlayer":
                Projectile p = otherGO.GetComponent<Projectile>();
                // if enemy is off screen no damage
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }
                // hurt this enemy
                ShowDamage();
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                //print("HIT " + otherGO.name);
                //Debug.Log("I was hit: " + this.name);


                if (health <= 0)
                {
                    if (!notifiedOfDestruction)
                    {
                        Main.S.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;

                    // give us some points
                    ScoreManager.score += (int)points;

                    // update the kill count
                    UpdateEnemyKillCount(this.name);

                    Destroy(this.gameObject);
                    StartCoroutine(Explosion());
                }
                Destroy(otherGO);

                //EndOfLevelCheck();

                break;
            default:
                //print("Enemy hit by non-ProjectilePlayer " + otherGO.name);
                break;
        }
    }




    private void UpdateEnemyKillCount(string enemyName)
    {
        //Debug.Log("name: " + name);
        //Debug.Log("score: " + score.ToString());

        switch (enemyName)
        {
            case "Enemy_0(Clone)":
                EnemyKillManager.killCounts[EnemyKillManager.E0] += 1;  
                break;
            case "Enemy_1(Clone)":
                EnemyKillManager.killCounts[EnemyKillManager.E1] += 1;
                break;
            case "Enemy_2(Clone)":
                EnemyKillManager.killCounts[EnemyKillManager.E2] += 1;
                break;
            case "Enemy_3(Clone)":
                EnemyKillManager.killCounts[EnemyKillManager.E3] += 1;
                break;
            case "Enemy_4(Clone)":
                EnemyKillManager.killCounts[EnemyKillManager.E4] += 1;
                break;
        }
       
    }

    IEnumerator Explosion()
    {
        explosionAS.Play();
        yield return new WaitForSeconds(explosionAS.clip.length);
    }

    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i=0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;

    }
}
