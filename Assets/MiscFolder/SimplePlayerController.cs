using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    private Animator EnemyAnim;
    private SpriteRenderer Sprite;
    private GameObject Target;
    private PlayerHealth HpTarget;
    public float MovementSpeed = 3.0f;
    public int MeleeDamage = 1;
    private float h, v;
    public Transform projectile;
    //anim
    private bool IsAttacking;
    private bool IsMoving;

    //aura
    public bool hasEffectBoost;
    private CircleCollider2D EffectBoost;
    public bool BoostEffect;
    public float radius = 1.5f;


    // Use this for initialization
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindGameObjectWithTag("Player");
        if(Target != null) HpTarget = Target.GetComponent<PlayerHealth>();
        GetComponent<CircleCollider2D>().radius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();   
        HandleAttack();
        HandleProjectile();
    }

    private void HandleProjectile()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            Transform projectile2 = Instantiate(projectile, transform.position, Quaternion.identity);
            projectile2.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1*Vector3.forward);
            projectile2.GetComponent<Rigidbody2D>().AddForce(dir * 500); 
        }
    }

    private void HandleMovement()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //flip sprite
        Sprite.flipX = h < 0;
        //move enemy unit
        transform.Translate(new Vector3(h * MovementSpeed * Time.deltaTime, 0f, 0f));
        transform.Translate(new Vector3(0f, v * MovementSpeed * Time.deltaTime, 0f));
        
        IsWalking(h, v);
    }

    private void IsWalking(float h, float v)
    {
        bool IsMoving = h != 0f || v != 0f;
        EnemyAnim.SetBool("IsMoving", IsMoving);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || hasEffectBoost)
        {
            //collision.transform.parent.gameObject.GetComponent<EnemyController>().Boost = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || hasEffectBoost)
        {
            //collision.transform.parent.gameObject.GetComponent<EnemyController>().Boost = false;

        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
            EnemyAnim.SetBool("IsAttacking", IsAttacking);
            if(HpTarget != null) HpTarget.takeDamage(MeleeDamage);

        }
        else
        {
            IsAttacking = false; 
            EnemyAnim.SetBool("IsAttacking", IsAttacking);
        }
    }
}
