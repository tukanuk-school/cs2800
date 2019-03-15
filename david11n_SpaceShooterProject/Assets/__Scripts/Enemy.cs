using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public int score = 100;
    public float showDamageDuration = 0.1f; // seconds to show damage
    public float powerUpDropChance = 1f; // Chance to drop of PowerUp

    //[SerializeField]
    float health = ScoreManager.E0;

    [Header("Set dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    protected BoundsCheck bndCheck;

    // explosion AudioSource
    AudioSource explosionAS;

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

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        // get materials and colors for this GameObject and its children
        materials = Utils.GetMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i=0; i<materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    void Start()
    {
        GameObject go = GameObject.Find("explosionAS");
        explosionAS = go.GetComponent<AudioSource>();

        Debug.Log(health);
    }



    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown) 
        {
            Destroy(gameObject);
        }
    }



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

                    // destroy this enemy
                    ScoreManager.score += (int) score;

                    // update the kill count
                    UpdateEnemyKillCount(this.name);

                    Destroy(this.gameObject);
                    StartCoroutine(Explosion());
                }
                Destroy(otherGO);
                break;
            default:
                print("Enemy hit by non-ProjectilePlayer " + otherGO.name);
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
        Debug.Log(EnemyKillManager.killCounts);
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
