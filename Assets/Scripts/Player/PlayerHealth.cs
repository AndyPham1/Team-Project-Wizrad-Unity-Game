using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public const int maximumHealth = 100;
    public int currentHealth = maximumHealth;
    //public RectTransform healthBar;
    public Animator playerAnim;
    public bool isDead = false;
    public Slider healthBar;
    public GameOver gameOverObject;

    public void takeDamage(int damage)
    {
        if (this.gameObject.GetComponentInChildren<StoneSkin>() != null && this.gameObject.GetComponentInChildren<StoneSkin>().isStoned)
            damage = 0;

        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            playerAnim.SetBool("isDead", isDead);
            Debug.Log("YOU DIED");
            gameOverObject.triggerGameOverMenu();
        }

        // Update health bar UI
        healthBar.value = currentHealth;
    }

    public void receiveHealing(int healing)
    {
        currentHealth += healing;
        if(currentHealth >= 100)
        {
            currentHealth = 100;
        }

        // Update health bar UI
        healthBar.value = currentHealth;
    }

}
