using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCoolDown : MonoBehaviour
{
    //Variables
    public Image filled;
    public float cooldown = 5;
    public bool isCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (isCooldown == true)
        {
            filled.fillAmount += 1 / cooldown * Time.deltaTime;
            if (filled.fillAmount >= 1)
            {
                isCooldown = false;
            }
        }
        else
        {
            filled.fillAmount = 0;
        }
    }
}
