using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSkin : MonoBehaviour
{

    public GameObject player;
    public bool isStoned = false; //8^)
    public int spellDuration = 4;
    public PlayerHealth playerHealth;

    private bool currentlyInCoolDown = false;
    public RadialCoolDown cooldownUI;
    private Color defaultColor;

    // Use this for initialization
    void Start()
    {
        defaultColor = player.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && !playerHealth.isDead && !currentlyInCoolDown && Time.timeScale != 0) //Left alt idek
        {
            currentlyInCoolDown = true;
            StartCoroutine(Stone());
            StartCoroutine(OnUpdateRoutine());
        }
    }

    IEnumerator OnUpdateRoutine()
    {
        if (currentlyInCoolDown == true)
        {
            cooldownUI.isCooldown = true;
            cooldownUI.cooldown = 8.0f;
            yield return new WaitForSeconds(cooldownUI.cooldown);
            currentlyInCoolDown = false;
        }
    }

    IEnumerator Stone()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(.9f, .8f, .7f, 1f);
        isStoned = true;

        yield return new WaitForSecondsRealtime(spellDuration);

        player.GetComponent<SpriteRenderer>().color = defaultColor;
        isStoned = false;
    }

}
