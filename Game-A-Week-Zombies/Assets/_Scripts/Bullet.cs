using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    static private Transform _BULLET_ANCHOR;

	
    //This is the kind of clever shit I never consider.
    static Transform BULLET_ANCHOR
    {
        get
        {
            if (_BULLET_ANCHOR == null)
            {
                GameObject go = new GameObject("BulletAnchor");
				_BULLET_ANCHOR = go.transform;
            }
            return _BULLET_ANCHOR;
        }
    }


    public float bulletSpeed = 20;
    public float lifeTime = 2;


    // Use this for initialization
    void Start()
    {
		
        transform.SetParent(BULLET_ANCHOR);

        Invoke("DestroyMe", lifeTime);

        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }

    void DestroyMe()
    {
       Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
