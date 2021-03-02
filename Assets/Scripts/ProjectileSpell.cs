using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{

    public float fireRate = 0;
    public float spellDamage = 2;
    public LayerMask whatToHit;
    public GameObject fireball;
    public PlayerHealth playerHealth;

    private float timeToFire = 0;
    private float fireballSpeed = 10;
    public RadialCoolDown cooldownUI;
    public bool isFiring = false;
    Transform firePoint;
    public float coolDownTime = 0.0f;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("ProjectileSource");
        cooldownUI = cooldownUI.GetComponent<RadialCoolDown>();
        isFiring = false;
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
        
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1") && !playerHealth.isDead && !isFiring && Time.timeScale != 0) //LMB
            {
                cooldownUI.cooldown = coolDownTime;
                isFiring = true;
                StartCoroutine(OnUpdateRoutine());
                Shoot();
            }
        }

        //If fireRate !=0 then the projectile is automatic
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire && !playerHealth.isDead && !isFiring && Time.timeScale != 0)
            {
                cooldownUI.cooldown = coolDownTime;
                isFiring = true;
                StartCoroutine(OnUpdateRoutine());
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }

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

    void Shoot()
    {
        cooldownUI.isCooldown = true;

        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        Vector2 direction = mousePosition - firePointPosition;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
        GameObject projectile = Instantiate(fireball, firePointPosition, rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (mousePosition - firePointPosition), 100, whatToHit); //b - a == Vector2 direction

        //Debug.DrawLine(firePointPosition, mousePosition);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
        }
    }

}
