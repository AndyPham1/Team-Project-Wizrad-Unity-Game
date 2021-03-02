using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSpell : MonoBehaviour
{
    public GameObject totemPrefab;
    public int totemLifeTime = 2;
    public LayerMask whatToHit;
    public PlayerHealth playerHealth;

    public RadialCoolDown cooldownUI;
    private float timeToFire = 0;
    private bool isFiring = false;
    Transform firePoint;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("ProjectileSource");
        cooldownUI = cooldownUI.GetComponent<RadialCoolDown>();
        if (firePoint == null)
        {
            Debug.Log("No ProjectileSource found under Player/Staff!");
        }
    }

    void OnEnable()
    {
        isFiring = false;
    }

    private void OnDisable()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire2") && !playerHealth.isDead && !isFiring && Time.timeScale != 0) //LMB
        {
            cooldownUI.cooldown = 5.0f;
            isFiring = true;
            StartCoroutine(OnUpdateRoutine());
            SpawnTotem();
        }
        
    }

    IEnumerator OnUpdateRoutine()
    {
        if (isFiring == true)
        {
            yield return new WaitForSeconds(cooldownUI.cooldown);
            isFiring = false;
        }
    }

    void SpawnTotem()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);      
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        GameObject totemClone = Instantiate(totemPrefab, new Vector3(mousePosition.x, mousePosition.y, 0), firePoint.rotation);

        cooldownUI.isCooldown = true; // triggers to do the cooldown filling
        
        Destroy(totemClone, totemLifeTime);
    }

}
