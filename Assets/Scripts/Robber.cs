using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robber : MonoBehaviour
{
    [SerializeField] public Image EnemyHealthBar;
    public GameObject robber_oj;
    public Animator playerAnim;
    public Hide hide;
    public float health = 100;
    public float currentHealth;

    public void Start()
    {
        currentHealth = health;
    }

    public void TakePlayerDamage(float amount)
    {
        currentHealth -= amount;
        EnemyHealthBar.fillAmount = currentHealth / health;
        if(currentHealth <= 0)
        {
           Debug.Log("Player Die");
           
        }
    }

}
