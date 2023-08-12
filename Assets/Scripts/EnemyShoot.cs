using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public float shootInterval = 1f;
    public float damagePerShot = 10;
    public float shootDistance = 100f;
    public Hide hide;
    public PlayerHealth playerHealth;
    public GameObject tuto;
    public AudioSource gunshot;
    public Rhit rhit;

    private float shootTimer = 0f;

    void Update()
    {
        // Check if the player is not null and within shooting distance
        if (player != null && Vector3.Distance(transform.position, player.position) <= shootDistance && rhit._enemyDie == false)
        {
            // Update the shoot timer
            shootTimer += Time.deltaTime;

            Debug.Log("First");

            // Check if enough time has passed since the last shot
            if (shootTimer >= shootInterval)
            {
                if (hide.isDown == false && tuto.activeSelf == false)
                {
                    Debug.Log("fifth");
                    playerHealth.TakePlayerDamage(damagePerShot);
                    gunshot.Play();
                }
                Debug.Log("Second");
                // Reset the shoot timer
                shootTimer = 0f;

                // Call the ShootAtPlayer function to shoot at the player
                // Player is hit, apply damage to the player's health
            }
        }
    }
}
