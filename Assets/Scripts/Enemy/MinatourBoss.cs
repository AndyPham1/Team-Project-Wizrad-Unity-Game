using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinatourBoss : MonoBehaviour
{
    private GameObject Player;
    private SpriteRenderer Sprite;
    public GameObject stageExit;
    private PlayerHealth gamerHealth;
    //Enemy Attributes
    private Animator EnemyAnim;
    private EnemyHealth EnemyHealth;
    private int ChangeLocation = 0;
    private Rigidbody2D Rigidbody2D;
    //counter
    float TimeElasped;
    float ChaseDuration = 1;
    float ChargeDuration = 1f;
    float DodgeDuration = 0.2f;
    float ChargeDistanceDamage = 1;
    int ChargeDamage = 30;
    float currentAttackTime = 0;
    int DodgeDirection;
    private Vector3 Targ;

    public enum BossState
    {
        Chase,
        Charge,
        Dodge,
        Attack
    }

    public BossState currState;
    private float MovementSpeed = 1;
    private float JumpSpeed = 8;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        currState = BossState.Chase;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        if (Player != null) gamerHealth = Player.GetComponent<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();
        EnemyHealth = GetComponent<EnemyHealth>();

    }



    private void Update()
    {
        switch (currState)
        {
            case BossState.Attack: Attack(); break;
            case BossState.Chase: Chase(); break;
            case BossState.Charge: Charge(); break;
            case BossState.Dodge: Dodge(); break;
        }

    }

    void Attack()
    {
        EnemyAnim.SetBool("IsMoving", false);
        EnemyAnim.SetBool("IsAttacking", true);

    }

    void Chase()
    {
        EnemyAnim.SetBool("IsMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MovementSpeed * Time.deltaTime);
        TimeElasped += Time.deltaTime;
        if (TimeElasped >= ChaseDuration)
        {
            TimeElasped = 0;
            Targ = (Player.transform.position - transform.position) * 10;

            int num = Random.Range(1, 3);
            if (num == 1)
            {
                currState = BossState.Charge;
            }
            else
            {
                DodgeDirection = Random.Range(1, 2);
                currState = BossState.Dodge;
            }
        }
    }

    //Charge Damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Rigidbody2D.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Player") && currState == BossState.Charge)
        {
            gamerHealth.takeDamage(ChargeDamage);
        }


    }



    //need raycasting solution
    void Charge()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Targ, 100, LayerMask.GetMask("Walls"));

        if (hit.collider != null)
        {
            float distance = Vector2.Distance(hit.point, transform.position);
            float distance2 = Vector2.Distance(Targ, transform.position);
            Vector2 TargPos = distance < distance2 ? hit.point : new Vector2(Targ.x, Targ.y);
            transform.position = Vector2.MoveTowards(transform.position, TargPos, JumpSpeed * Time.deltaTime); //stop going through the darn wall
        }

        TimeElasped += Time.deltaTime;
        if (TimeElasped >= ChargeDuration)
        {
            TimeElasped = 0;
            currState = BossState.Chase;
        }

        if (Vector2.Distance(transform.position, Player.transform.position) < ChargeDistanceDamage)
        {
            EnemyAnim.Play("Melee");
        }
    }


    void Dodge()
    {

        Targ.z = 0;
        Vector3 dir = Vector3.Cross((Player.transform.position - transform.position).normalized, Vector3.forward);
        dir = dir.normalized*10;
        



        TimeElasped += Time.deltaTime;
        if (DodgeDirection == 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Targ, 100, LayerMask.GetMask("Walls"));
            if (hit.collider != null)
            {
                Vector2 targPos = transform.position + dir;
                float distance = Vector2.Distance(hit.point, transform.position);
                float distance2 = Vector2.Distance(targPos, transform.position);
                Vector2 TargPos = distance < distance2 ? hit.point : new Vector2(targPos.x, targPos.y);
                transform.position = Vector2.MoveTowards(transform.position, targPos, JumpSpeed * Time.deltaTime);
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Targ, 100, LayerMask.GetMask("Walls"));
            if (hit.collider != null)
            {
                Vector2 targPos = transform.position - dir;
                float distance = Vector2.Distance(hit.point, transform.position);
                float distance2 = Vector2.Distance(targPos, transform.position);
                Vector2 TargPos = distance < distance2 ? hit.point : new Vector2(targPos.x, targPos.y);
                transform.position = Vector2.MoveTowards(transform.position, targPos, JumpSpeed * Time.deltaTime); 
            }
        }

        if (TimeElasped >= DodgeDuration)
        {
            TimeElasped = 0;
            currState = BossState.Chase;
        }

    }
}
