using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public EndGame endGame;
    public GameObject _gameOver;
    [SerializeField] public Image _playerBar;
    public float maxHealth = 200;
    public float currentHealth;
    
    private void Start()
    {
        currentHealth  = maxHealth;
    }

    public void TakePlayerDamage(float damage)
    {
        Debug.Log("Gotcha from PH");
        currentHealth -= damage;
        _playerBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Player Health: " + currentHealth);
        }
    }

    private void Die()
    {
        // Perform actions when the player dies, e.g., destroy the player GameObject
        Debug.Log("Player is dead!");
        _gameOver.SetActive(true);
        Destroy(gameObject);
        endGame.EndGameWithDelay();
    }
}
