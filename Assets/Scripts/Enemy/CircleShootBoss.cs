using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShootBoss : MonoBehaviour
{
    public Transform[] MoveLocation;
    public GameObject stageExit;
    private GameObject Player;
    private SpriteRenderer Sprite;
    private PlayerHealth HpTarget;
    //Enemy Attributes
    private Animator EnemyAnim;
    private EnemyHealth EnemyHealth;
    private float MovementSpeed = 5;
    private float CircleAttackDuration = 3;
    private float BetweenCircleAttackDelay = 0.5f;
    private float BetweenSquareAttackDelay = 0.3f;
    private float ShootDelay = 0.1f;
    private float AngleAdjust = 0;
    private int ChangeLocation = 0;
    private float cnter;
    private BossState currState;
    public Transform projectile;
    private float waitDuration = 3;
    public Transform ProjectileAttack2;
    public Transform ProjectileAttack3;
    //counter
    float TimeElasped = 0;
    float TimeElasped2 = 0;
    float ChangePositionDelay = 2;

    public enum BossState
    {
        Chase,
        Charge,
        Move,
        Idle,
        CircleAttack,
        Shoot,
        SquareAttack
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null) HpTarget = Player.GetComponent<PlayerHealth>();
        EnemyAnim = GetComponent<Animator>();
        EnemyHealth = GetComponent<EnemyHealth>();
        currState = BossState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case BossState.Idle: Idle(); break;
            case BossState.CircleAttack: CircleAttack(); break;
            case BossState.Shoot: Shoot(); break;
            case BossState.SquareAttack: SquareAttack(); break;
        }
        GoToNextPoint();
    }

    private void Idle()
    {
        TimeElasped += Time.deltaTime;//waitDuration;
        if (TimeElasped >= waitDuration)
        {
            int num = UnityEngine.Random.Range(1, 4);
            TimeElasped = 0;
            if (num==1)
            {
            currState = BossState.SquareAttack;
            }
            else if (num == 2)
            {
                currState = BossState.CircleAttack;
            }
            else if (num == 3)
            {
                currState = BossState.Shoot;
            }
                
            
        }
    }

    private void SquareAttack()
    {

        Vector3 pos = Player.transform.position;
        TimeElasped += Time.deltaTime;
        cnter += Time.deltaTime;
        if ((cnter > BetweenSquareAttackDelay))
        {
            //cnter = 0;
            Transform tmp = Instantiate(ProjectileAttack2, pos, Quaternion.identity);
            StartCoroutine(SquareAttack2(pos));
            Destroy(tmp.gameObject, 2);
        }


        if (TimeElasped > CircleAttackDuration)
        {
            currState = BossState.Idle;
            AngleAdjust = 0;
            TimeElasped = 0;
        }
    }
    IEnumerator SquareAttack2(Vector3 pos)
    {
        yield return new WaitForSeconds(2);
        Transform tmp = Instantiate(ProjectileAttack3, pos, Quaternion.identity);
        Destroy(tmp.gameObject,2);
    }

    private void Shoot()
    {
        TimeElasped += Time.deltaTime;
        cnter += Time.deltaTime;
        if (cnter > ShootDelay)
        {
            cnter = 0;
            Vector3 adjustedPos = transform.position;
            Vector3 dir = (Player.transform.position - adjustedPos).normalized;
            Transform projectile2 = Instantiate(projectile, adjustedPos, Quaternion.identity);
             Destroy(projectile2.gameObject, 3);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile2.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            projectile2.GetComponent<Rigidbody2D>().AddForce(dir * 500);
        }

        if (TimeElasped > CircleAttackDuration)
        {
            currState = BossState.Idle;
            AngleAdjust = 0;
            TimeElasped = 0;
        }


    }

    private void CircleAttack()
    {
        Debug.Log("test");  
        TimeElasped += Time.deltaTime;
        cnter += Time.deltaTime;
        if ((cnter > BetweenCircleAttackDelay))
        {
            cnter = 0;
            ProjectileAttack(AngleAdjust += 10);
        }

        Debug.Log("test");
        if(TimeElasped > CircleAttackDuration)
        {
            currState = BossState.Idle;
            AngleAdjust = 0;
            TimeElasped = 0;
        }

    }


    void ProjectileAttack(float angle)
    {
        float AngleFinner = angle + 360;
        for (float i = angle; i <= AngleFinner; i += 20)
        {
            Transform projectile2 = Instantiate(projectile, transform.position, Quaternion.identity);
            projectile2.transform.rotation = Quaternion.Euler(0,0, i);
            projectile2.GetComponent<Rigidbody2D>().AddForce(-1*projectile2.up * 250);

            Destroy(projectile2.gameObject, 3);

        }
    }

    //Move Boss in pattern on the map
    void GoToNextPoint()
    {

       
        transform.position = Vector2.MoveTowards(transform.position, MoveLocation[ChangeLocation].transform.position, MovementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, MoveLocation[ChangeLocation].position) == 0)
        {
            //EnemyAnim.SetBool("IsMoving", true);
        }

        TimeElasped2 += Time.deltaTime;
        if (TimeElasped2 >= ChangePositionDelay)
        {
            TimeElasped2 = 0;
            ChangeLocation = UnityEngine.Random.Range(0, MoveLocation.Length);
        }
    }

    void ChargeTowardsPlayer()
    {
        TimeElasped2 += Time.deltaTime;
        if (TimeElasped2 >= ChangePositionDelay)
        {
            TimeElasped2 = 0;
        }
    }


}
