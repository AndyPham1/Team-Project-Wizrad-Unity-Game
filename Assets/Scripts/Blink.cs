using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {

    public GameObject player;
    public GameObject playerBlink;
    public bool isChanneling = false;
    public LayerMask whatToHit;
    public PlayerHealth playerHealth;

    private bool currentlyBlinking = false;
    private bool currentlyInCoolDown = false;
    public RadialCoolDown cooldownUI;
    private GameObject blink;

    // Use this for initialization
    void Awake()
    {
        cooldownUI = cooldownUI.GetComponent<RadialCoolDown>();
    }

    void OnEnable()
    {
        currentlyBlinking = false;
        currentlyInCoolDown = false;
    }

    private void OnDisable()
    {
        currentlyBlinking = false;
        currentlyInCoolDown = false;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Jump") && !playerHealth.isDead && !currentlyBlinking && !currentlyInCoolDown && Time.timeScale != 0) //Space
        {
            cooldownUI.cooldown = 3.0f;
            currentlyInCoolDown = true;
            StartCoroutine(OnUpdateRoutine());
            BlinkStart();
        }
        else if (Input.GetButtonUp("Jump") && !playerHealth.isDead && currentlyBlinking && Time.timeScale != 0) //Space
        {
            BlinkEnd();
        }
    }

    IEnumerator OnUpdateRoutine()
    {
        if (currentlyInCoolDown == true)
        {
            yield return new WaitForSeconds(cooldownUI.cooldown);
            currentlyInCoolDown = false;
        }
    }

    void BlinkStart()
    {
        var playerInitialPosition = player.transform.position; //position at the time of hitting the blink spell
        var playerInitialRotation = player.transform.rotation;

        isChanneling = true; //channeling, so the player should be rooted for the duration of the spell

        if (!currentlyBlinking)
        {
            blink = Instantiate(playerBlink, playerInitialPosition, playerInitialRotation); //the blink ghost is spawning on our player, now we need to lower the alpha
            blink.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f); //lower the alpha of the blink clone
            currentlyBlinking = true; //so we only spawn one blink ghostie
        }
    }

    void BlinkEnd()
    {
        cooldownUI.isCooldown = true;
        isChanneling = false;
        currentlyBlinking = false;

        // This is all new shit im trying

        Vector2 blinkPosition = new Vector2(blink.transform.position.x, blink.transform.position.y); //define vector for the raycast using player and player's blink clone
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        Vector2 direction = blinkPosition - playerPosition;
        direction.Normalize(); //normalize direction vector. This will give us a vector from our player ending at wherever the BLINK SPELL is released

        RaycastHit2D hit = Physics2D.Linecast(playerPosition, blinkPosition, whatToHit);

        if(hit.collider != null && hit.collider.tag == "Wall") //a wall is between blink ghost and player...
        {
            var alteredBlinkPosition = hit.point;
            player.transform.position = hit.point; //place the player by what they ran into, otherwise theyll just phase through walls n stuff
        }
        else
        {
            //Destroy the blink ghost and replace it with our player
            var blinkCurrentPosition = blink.transform.position;
            player.transform.position = blinkCurrentPosition;
        }

        //

        //Destroy the blink ghost and replace it with our player
        //var blinkCurrentPosition = blink.transform.position;
        //player.transform.position = blinkCurrentPosition;

        Destroy(blink);

    }

}
