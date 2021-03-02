using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSpellMenu : MonoBehaviour
{
    private int counter = 0;

    public GameObject portal;
    public GameObject spellCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnUpdateRoutine());
    }

    IEnumerator OnUpdateRoutine()
    {
        // wait for 5 seconds
        yield return new WaitForSeconds(5);
        portal = GameObject.FindGameObjectsWithTag("SpellUI")[0];
    }

    // Update is called once per frame
    void Update()
    {
        var bPortal = portal.GetComponent<BossPortal>();

        if (bPortal.isInLecternRoom == true)
        {
            if (counter == 0)
            {
                spellCanvas.SetActive(true);
                counter++;
            }
        }

    }
}
