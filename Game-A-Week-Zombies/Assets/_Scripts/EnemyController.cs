﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{

    //Creating a ship singleton! Learn more about singletons Dave.
    //A Design pattern from the Gang of four book
    //This Can only be set privately from inside the calss


    [Header("Set In Inspector")]
    public float playerSpeed = 1f;
    Rigidbody2D rigid;

    GameObject player;
    public GameObject splatter;
    public float health;

    public GameObject deathTile;


    public GameObject leaves;
    AudioSource audioSource;
    Object[] audioclips;


    Object[] bloodSplatterClips;
    //Store our ship singleton!
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        audioSource = GetComponent<AudioSource>();

        audioclips = Resources.LoadAll("_Audio/Zombie", typeof(AudioClip));
        bloodSplatterClips = Resources.LoadAll("_Audio/BloodSplatter", typeof(AudioClip));
        StartCoroutine(SoundOut());
    }


    void Update()
    {
        float ax = player.transform.position.x - transform.position.x;
        float aY = player.transform.position.y - transform.position.y;

        //normalize and multiply by max speed! (if at max direction)
        Vector3 velocity = new Vector3(ax, aY); // has a two var constructor.
        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }


        rigid.velocity = velocity * playerSpeed;
        if (velocity.magnitude > 0) transform.up = velocity;




    }

    void depleteHealth(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            ScoreManager.IncrementKills();
            Instantiate(deathTile, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("ZombieHit hit");
        if (other.gameObject.tag == "Bullet")
        {
            audioSource.PlayOneShot((AudioClip)bloodSplatterClips[Random.Range(0, bloodSplatterClips.Length)], 20f);
            StartCoroutine(PauseForSeconds(0.07f));
            GameObject splatterInstance = Instantiate(splatter, other.transform.position, Quaternion.identity);
            Destroy(splatterInstance, 0.5f);

            Destroy(other.gameObject);
            transform.position = transform.position + (Vector3)(other.GetComponent<Rigidbody2D>().velocity.normalized * .1f);
        }
    }


    private IEnumerator PauseForSeconds(float pauseDuration)
    {
        float originalTimeScale = 1; // store original time scale in case it was not 1
        Time.timeScale = 0; // pause
        float t = 0;
        while (t < pauseDuration)
        {
            yield return null; // don't use WaitForSeconds() if Time.timeScale is 0!
            t += Time.unscaledDeltaTime; // returns deltaTime without being multiplied by Time.timeScale
        }
        Time.timeScale = originalTimeScale; // restore time scale from before pause
        depleteHealth(1);
    }

    /* play groan souond on loop every couple of seconds */
    IEnumerator SoundOut()
    {
        while (true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot((AudioClip)audioclips[Random.Range(0, audioclips.Length)]);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
