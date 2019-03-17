﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player S; // singleton

    [Header("Set in inspector")]
    // Controls the movements of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = -30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePreFab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;


    [Header("Set dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    private GameObject lastTriggerGo = null;

    // declare a new delegate type
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field 
    public WeaponFireDelegate fireDelegate;

    private void Start()
    {
        if (S == null)
        {
            S = this; // Singleton pattern
        }
        else
        {
            Debug.LogError("Player.Awake() - Attempted to assing a second player.S!");
        }

        // reset the weapons to start with _Palyer with 1 blaster
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
    }

 
    // Update is called once per frame
    void Update()
    {
        // pull information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change transform.position based on the axis
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // use the fireDelegate to fire Weapons
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        // Make sure it's not the same triggering as last time
        if (go == lastTriggerGo)
        {
            return;
        }

        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else if (go.tag == "PowerUp")
        {
            // if the shield was triggered by a PowerUp
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non-enemy: " + go.name);
        }

    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel++;
                break;
            default:
                if (pu.type == weapons[0].type)
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        w.SetType(pu.type);
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }

                break;
        }

        pu.AbsorbedBy(this.gameObject);

    }


    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // if shielf is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);

                // if you die reset points and level
                ScoreManager.currentGameLevel = ScoreManager.GameLevels.Bronze;
                ScoreManager.scoreToNextLevel = ScoreManager.BronzePointsToWin;

                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
