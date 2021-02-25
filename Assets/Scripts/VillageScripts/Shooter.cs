using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //variables
    [SerializeField] private float fireSpeed = 0.4f;
    private float rateOfFire;
    [SerializeField] BulletFire bullet;
    [SerializeField] Transform _firePoint;

    private void Shoot()
    {
        //sets rate of fire from time + firespeed; Instantiates a prefab from a given transfrom position and uses the rotation of the set gameObject
        //fires using forward transfrom of attach gameObject (player)
        rateOfFire = Time.time + fireSpeed;
        BulletFire shot = Instantiate(bullet, _firePoint.transform.position, transform.rotation);
        shot.Fire(transform.forward);
    }

    private bool CanShoot()
    {
        //returns true always as rate of fire is set in shoot as equal
        //to be developed further for mouse input
        return Time.time >= rateOfFire;
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoots
                if (CanShoot())
            Shoot();
    }
}
 