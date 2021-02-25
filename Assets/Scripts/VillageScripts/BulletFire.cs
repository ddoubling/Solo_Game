using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    //Variable for bullet speed
    [SerializeField] float bulletSpeed = 15f;

    
    public void Fire(Vector3 direction)
    {
        //when called, normalise given Vector, use the up transform (bullet was roatated)
        //use rigidbody to move the bullet with the velocity using normalised Vecotr * variable bullet speed

        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }

    //collision to record and destroy bullet on impact
    private void OnCollisionEnter(Collision collision)
    {
     Destroy(gameObject);
    }
    //destory object after 3 seconds, removes them from infinite game space.
    private void Start()
    {
        Destroy(gameObject, 3f);
    }


}
