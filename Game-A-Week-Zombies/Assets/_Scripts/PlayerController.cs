using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    //Creating a ship singleton! Learn more about singletons Dave.
    //A Design pattern from the Gang of four book
    //This Can only be set privately from inside the calss
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
                Debug.LogWarning("Second attempt to set Player singleton");
            }
            _P = value;
        }
    }

    [Header("Set In Inspector")]
    public float playerSpeed = 10f;
    public GameObject bulletPrefab;

    public GameObject muzzleFlash;
    Rigidbody2D rigid;

    //Store our ship singleton!
    void Start()
    {
        P = this;

        rigid = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //using corss platform manager because later we will be using the tilt stuff!
        float ax = Input.GetAxisRaw("Horizontal");
        float aY = Input.GetAxisRaw("Vertical");

        //normalize and multiply by max speed! (if at max direction)
        Vector3 velocity = new Vector3(ax, aY); // has a two var constructor.
        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }
        rigid.velocity = velocity * playerSpeed;
         if (velocity.magnitude > 0) transform.up = velocity;
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

    }

    void Fire()
    {
        Debug.Log("Firing");
        GameObject go = Instantiate<GameObject>(bulletPrefab);
        GameObject flash = Instantiate(muzzleFlash, transform.position + transform.up * .6f, Quaternion.identity);
        flash.transform.SetParent(transform);
        Destroy(flash, 0.02f);

        go.transform.position = transform.position + transform.up;

        go.transform.up = transform.up;

    }

    static public float MAX_SPEED
    {
        get
        {
            return P.playerSpeed;
        }
    }


}
