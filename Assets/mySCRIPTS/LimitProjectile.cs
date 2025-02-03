using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimitProjectile : MonoBehaviour
{
    public TMP_Text AmmoText;

    public GameObject projectile;
    public Transform shootPoint;

    //[SerializeField] private KeyCode shootButton;
    [SerializeField] private int roundsToAdd = 20;
    [SerializeField] private int maxRounds = 99;

    public int numProjectiles;

    // Sounds
    public AudioSource cannonFire;
    public AudioSource ammoPickup;
    //public AudioSource impactSound;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
    AudioSource[] audioSources = GetComponents<AudioSource>();
    if (audioSources.Length >= 2)
    {
        cannonFire = audioSources[0];
        ammoPickup = audioSources[1];
    }
    else
    {
        Debug.LogError("Not enough audio sources attached!");
    }
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (projectile == null)
            {
            Debug.Log("No projectile Set");
            }
            else
            {
                if (numProjectiles > 0)
                {
                    cannonFire.Play();
                    // direction of instantiation is fixed to object's transform.rotation
                    Instantiate(projectile, shootPoint.position, transform.rotation);

                    // subtract one from round inventory available
                    numProjectiles--;
                    UpdateAmmoText();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            ammoPickup.Play();
            
            // Increase ammo, but do not exceed maxRounds
            numProjectiles = Mathf.Min(numProjectiles + roundsToAdd, maxRounds);
            Debug.Log("Ammo picked up! Current rounds: " + numProjectiles);
            // Optionally, destroy or deactivate the ammo crate
            UpdateAmmoText();
            Destroy(collision.gameObject);

            
        }
    }

    private void UpdateAmmoText ()
    {
        AmmoText.text = "Rounds: " + numProjectiles.ToString();
    }
}
