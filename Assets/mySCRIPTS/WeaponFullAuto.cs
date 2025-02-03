using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFullAuto : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 10;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    public Transform flashStart;
    public GameObject flashPoint;
    public float impactForce = 50f;
    //public ParticleSystem muzzleFlash;
    //public ParticleSystem bulletSmoke;
    //public ParticleSystem impactEffect;

    private float fireRate = 9f;
    private float nextTimeToFire = 0f;

    public AudioSource gun_shotLong;
    //public AudioSource gun_shell;
    //public AudioSource engineWind;

    void Start ()
    {
        gun_shotLong = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            gun_shotLong.Play();
            //gun_shell.Play();
            //engineWind.Play();
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }   
    }

    void Shoot ()
    {
        //muzzleFlash.Play();
        //bulletSmoke.Play();
        Instantiate(flashPoint, flashStart.position, flashStart.rotation);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }
            
            //GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            Destroy(impactGO, 2f);
            
        }
    }
}