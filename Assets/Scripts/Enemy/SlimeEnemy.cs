using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public GameObject newSlime;
    //public SpriteRenderer sprite;
    public int maxMultiply = 5;
    private EnemyHealth EnemyHealth;

    
    void Mutliply()
    {
        if (maxMultiply <= 0)
            return;






        //create second
        if(this.GetComponent<EnemyHealth>().currentHealth >= 2)
        {
            if (this == null || this.transform == null || newSlime == null) return;
            //decrease in size
            this.GetComponent<Transform>().localScale -= new Vector3(0.1f, 0.1f);
            //change color
            newSlime = Instantiate(newSlime, this.transform.position, this.transform.rotation);
            newSlime.GetComponent<SlimeEnemy>().maxMultiply = this.maxMultiply - 1;
            newSlime.GetComponent<EnemyHealth>().currentHealth = this.GetComponent<EnemyHealth>().currentHealth;
            newSlime.GetComponent<Transform>().localScale = this.GetComponent<Transform>().localScale;

            //newSlime.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
            newSlime.GetComponent<SpriteRenderer>().material.color = this.GetComponent<EnemyHealth>().originalColor;

            //decrement maxmultiply
            maxMultiply--;
        }

    }


    // Start is called before the first frame update
    void Awake()
    {
        EnemyHealth = GetComponent<EnemyHealth>();
        if(EnemyHealth.trigger == null) EnemyHealth.trigger = Mutliply;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<EnemyHealth>().currentHealth <= 0) Destroy(this.gameObject);
    }
}
