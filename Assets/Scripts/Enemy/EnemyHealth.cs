using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public const int maxHealth = 5;
    public int currentHealth = maxHealth;
    public float flashTime;
    public Color originalColor;
    public SpriteRenderer renderer;
    
    public delegate void triggerFunctions();
    public triggerFunctions trigger;

    private void Start()
    {
        originalColor = renderer.material.color;
    }

    private void FlashRed()
    {
        renderer.material.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        renderer.material.color = originalColor;
    }

    public void takeDamage(int damage)
    {
        
        currentHealth -= damage;
        if (trigger != null) trigger();
        FlashRed();

        

        if (currentHealth <= 0)
        {
            if (gameObject.GetComponent<CircleShootBoss>())
                gameObject.GetComponent<CircleShootBoss>().stageExit.SetActive(true);

            if (gameObject.GetComponent<MinatourBoss>())
                gameObject.GetComponent<MinatourBoss>().stageExit.SetActive(true);

            Destroy(gameObject);
            Debug.Log("ENEMY DIED");
        }

    }
}
