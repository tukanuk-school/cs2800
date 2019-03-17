using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Enemy_4 starts offscreen and picks a random point to move to and so on.
/// </summary>

[System.Serializable]
public class Part
{
    // Set in inspector
    public string name;
    public float health;
    public string[] protectedBy;

    // set in Start()
    [HideInInspector]
    public GameObject go;
    [HideInInspector]
    public Material mat;
}

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]
    public Part[] parts;

    private Vector3 p0, p1;
    private float timeStart;
    private float duration = 4;

    // Start is called before the first frame update
    void Start()
    {
        // Set the health and points of this enemy type
        //transform.parent.GetComponent<Enemy>().health = ScoreManager.E4;
        //transform.parent.GetComponent<Enemy>().points = ScoreManager.E4points;
        health = ScoreManager.E4;
        points = ScoreManager.E4points;

        // Main.SpawnEnemy() choose inital point
        p0 = p1 = pos;

        InitMovement();

        // Cache GameObject & Material of each part in parts
        Transform t;
        foreach (Part prt in parts)
        {
            t = transform.Find(prt.name);
            if (t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
                prt.health = ScoreManager.E4;
            }
        }

        // set the color of the materials based on the ScoreManager
        SetColour(ScoreManager.E4Color);
    }

    void InitMovement()
    {
        p0 = p1; // set p0 to the old p1
        // new location for p1
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        // Reset the time
        timeStart = Time.time;
    }

    public override void Move()
    {
        // complete override of Eneny.Move()
        float u = (Time.time - timeStart) / duration;
        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2); // apply ease out to u
        pos = (1 - u) * p0 + u * p1; // linear interlopation

    }

    // find a Part in parts based on name or GameObject
    Part FindPart(string n)
    {
        foreach (Part prt in parts)
        {
            if (prt.name == n)
            {
                return (prt);
            }
        }
        return (null);
    }

    Part FindPart(GameObject go)
    {
        foreach (Part prt in parts)
        {
            if (prt.go == go)
            {
                return (prt);
            }
        }
        return (null);
    }

    // these function return true if the part has been destroyed
    bool Destroyed(GameObject go)
    {
        return (Destroyed(FindPart(go)));
    }
    bool Destroyed(string n)
    {
        return (Destroyed(FindPart(n)));
    }
    bool Destroyed(Part prt)
    {
        if (prt == null)
        {
            return (true);
        }
        return (prt.health <= 0);
    }

    // changes the colour of just one part
    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        damageDoneTime = Time.time + showDamageDuration;
        showingDamage = true;
    }

    // override the OnCollisionEnter that is part of Enemy.cs
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "ProjectilePlayer":
                Projectile p = other.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(other);
                    break;
                }

                // hurt the enemy
                GameObject goHit = collision.contacts[0].thisCollider.gameObject;
                Part prtHit = FindPart(goHit);
                if (prtHit == null)
                {
                    goHit = collision.contacts[0].otherCollider.gameObject;
                    prtHit = FindPart(goHit);
                }

                // check if the part is still protected
                if (prtHit.protectedBy != null)
                {
                    foreach (string s in prtHit.protectedBy)
                    {
                        if (!Destroyed(s))
                        {
                            Destroy(other);
                            return;
                        }
                    }
                }

                // It's not protectd so make it take damage
                prtHit.health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                // show damage on part
                ShowLocalizedDamage(prtHit.mat);
                if (prtHit.health <= 0)
                {
                    // instead of destroying enemny, disable part
                    prtHit.go.SetActive(false);

                    // explosion sound for part
                    explosionAS.Play();
                }

                // check to see if the whole ship is destroyed
                bool allDestroyed = true; // assume destroyed
                foreach (Part prt in parts)
                {
                    if (!Destroyed(prt))
                    {
                        allDestroyed = false;
                        break;
                    }
                }
                if (allDestroyed)
                {
                    Main.S.ShipDestroyed(this);

                    // give us some points
                    ScoreManager.score += (int)points;

                    // up the kill count
                    EnemyKillManager.killCounts[EnemyKillManager.E4] += 1;

                    Destroy(this.gameObject);

                }
                Destroy(other);
                break;


        }
    }
}
