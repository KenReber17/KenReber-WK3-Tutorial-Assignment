using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 50f;

    // Store the direction based on the turret's rotation, not the target's position
    private Vector2 firingDirection;

    // Declare the target variable
    private Transform target;

    void Start()
    {
        // Get the direction based on the turret's current rotation
        if (rb != null)
        {
            firingDirection = transform.right; // Assuming 'right' is forward for your turret setup
            rb.linearVelocity = firingDirection * bulletSpeed;
        }

        StartCoroutine(SelfDestruct());
    }

    public void SetTarget(Transform _target)
    {
        // Store the target or perform any setup needed with the target
        target = _target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TAKE Health from Player or handle the impact
        Destroy(gameObject); // Destroy bullet on trigger
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}