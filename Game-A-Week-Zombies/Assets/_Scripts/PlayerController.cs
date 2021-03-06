﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    //Creating a player singleton! Learn more about singletons Dave.
    //A Design pattern from the Gang of four book
    //This Can only be set privately from inside the class


    static private PlayerController _P;
    static public PlayerController P
    {
        get
        {
            return _P;
        }
        private set
        {
            if (_P != null)
            {
                // Debug.LogWarning("Second attempt to set Player singleton");
            }
            _P = value;
        }
    }

    [Header("Set In Inspector")]
    public float playerSpeed = 10f;

    public float playerHealth = 30f;
    public GameObject bulletPrefab;

    public GameObject playerSplat;
    public GameObject splatter;
    public GameObject muzzleFlash;
    Rigidbody2D rigid;
    AudioSource[] audioSource;
    Object[] audioclips;
    Object[] bulletClips;


    //Store our ship singleton!
    void Start()
    {
        P = this;

        audioSource = GetComponents<AudioSource>();
        audioclips = Resources.LoadAll("_Audio/PlayerHurt", typeof(AudioClip));
        bulletClips = Resources.LoadAll("_Audio/Bullet", typeof(AudioClip));
        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }


        // movement controlls
        float ax = Input.GetAxisRaw("Horizontal");
        float aY = Input.GetAxisRaw("Vertical");

        //normalize and multiply by max speed! (if at max direction)
        Vector3 velocity = new Vector3(ax, aY);
        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }
        rigid.velocity = velocity * playerSpeed;
        if (velocity.magnitude > 0) { transform.up = velocity; }

        // kill player if health falls below zero.
        if (playerHealth <= 0)
        {
            GameObject go = Instantiate<GameObject>(playerSplat, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void Fire()
    {
        //play audio, instantiate bullet and flash
        audioSource[0].PlayOneShot((AudioClip)bulletClips[Random.Range(0, bulletClips.Length)], 0.1f);
        GameObject go = Instantiate<GameObject>(bulletPrefab);
        GameObject flash = Instantiate(muzzleFlash, transform.position + transform.up * .6f, Quaternion.identity);
        flash.transform.SetParent(transform);
        Destroy(flash, 0.02f);

        go.transform.position = transform.position + transform.up;
        go.transform.up = transform.up;
    }

    /*This functin did a bit more until The kill method moved to the update method */
    public static void DecrementHealth(int amount)
    {

        P.playerHealth -= amount;

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //on collision decrement health and move player backward
        if (other.gameObject.tag == "Enemy")
        {
            if (!audioSource[1].isPlaying)
            {
                audioSource[1].PlayOneShot((AudioClip)audioclips[Random.Range(0, audioclips.Length)]);
            }
            GameObject splatterInstance = Instantiate(splatter, other.contacts[0].point, Quaternion.identity);
            Destroy(splatterInstance, 0.2f);
            DecrementHealth(5);
            transform.position = transform.position + (Vector3)(other.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * .1f);
            other.gameObject.transform.position = other.transform.position - (Vector3)(other.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * .1f);
        }
    }

    static public float MAX_SPEED
    {
        get
        {
            return P.playerSpeed;
        }
    }

    static public float PLAYER_HEALTH
    {
        get
        {
            return P.playerHealth;
        }
    }




}
