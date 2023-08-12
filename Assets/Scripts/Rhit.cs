using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhit : MonoBehaviour
{
    public EndGame endGame;
    public GameObject _youWin;
    [SerializeField] public Image _enemyBar;
    public float health = 100;
    public float currentHealth;
    public Animator playerAnim;
    public bool _enemyDie = false;

    private void Start()
    {
        currentHealth = health;
        //playerAnim = GetComponent<Animator>();
        playerAnim.SetTrigger("idle");
    }
    

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        _enemyBar.fillAmount = currentHealth / health;
        if(currentHealth <= 10)
        {
            //Destroy(gameObject);
            playerAnim.SetTrigger("die");
            _enemyDie = true;
            _youWin.SetActive(true);
            endGame.EndGameWithDelay();
        }
    }
}
