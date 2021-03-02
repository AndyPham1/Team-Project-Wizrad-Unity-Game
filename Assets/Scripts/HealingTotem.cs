using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTotem : MonoBehaviour {

    public float currentEffectTime = 0;
    public float effectDelay = 3;
    public float effectRange = 5;
    public int effectPower = 25;
    public Animator totemAnim;
    public SpriteRenderer totemSprite;
    public GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        
        var playerHealth = player.GetComponent<PlayerHealth>();

        if (Vector2.Distance(transform.position, player.transform.position) <= effectRange && currentEffectTime - Time.time <= 0 && !playerHealth.isDead) //if the player is within range of the totem...
        {
            currentEffectTime = Time.time + effectDelay;
            playerHealth.receiveHealing(effectPower);
        }
    }
}