using System;
using System.Collections;
using UnityEngine;

public interface NPC
{
    void ChasePlayer();

    IEnumerator MeleeAttack();

    IEnumerator RangeAttack();
}

public class BasicNpcObj : MonoBehaviour, NPC
{
    //sprite and gameobjects
    private SpriteRenderer Sprite;
    private GameObject Target;
    private PlayerHealth HpTarget;
    public Transform projectile;

    //Enemy Attribute
    public const float attackDelay = 3;
    public const float rangeAttackDelay = 1;

    public float attackRange = 1;
    public float MeleeAttackDelay = 0;

    public int MeleeDamage = 1;
    public float MovementSpeed = 3.0f;
    public float projectileRange = 1.5f;
    private float nextFire = 0.0F;

    //Misc 
    public bool FlipUpsideDownProjectile;
    public bool hasRangeAttack = false;
    public bool IsFlipSprite = true;
    public Vector3 projectileOffset;
    public bool RotateProjectileABit = true;

    //anim misc
    private Animator EnemyAnim;
    private bool IsAttacking;
    private bool IsMoving;
    public bool IsWalkAround;
    private int walkDuration = 2;
    private float walkTimer = 2f;
    private Vector2 v2;
    int p = 0;

    public void ChasePlayer()
    {
        Sprite.flipX = IsFlipSprite ? (Target.transform.position.x < transform.position.x) : (Target.transform.position.x > transform.position.x);
        float distance = Vector2.Distance(transform.position, Target.transform.position);



        if (distance > projectileRange && distance > attackRange)
        {
            IsAttacking = false;
            EnemyAnim.SetBool("IsMoving", true);
            if (!IsWalkAround)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MovementSpeed * Time.deltaTime);
            }
            else
            {

                walkTimer += Time.deltaTime;
                if (walkTimer >= walkDuration)
                {
                    walkTimer = 0f;
                    v2 = UnityEngine.Random.insideUnitCircle.normalized;
                    p = UnityEngine.Random.Range(0, 2);
                }
                
                if(p == 0)
                    transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MovementSpeed * Time.deltaTime);
                else
                    transform.position += new Vector3(v2.x, v2.y, 0f) * MovementSpeed * Time.deltaTime;
            }
        }
        else if (distance <= projectileRange && distance > attackRange &&  nextFire > rangeAttackDelay)
        {
            if (hasRangeAttack)
            {
                nextFire = 0;
                StartCoroutine(RangeAttack());
            }
            else
            {
                EnemyAnim.SetBool("IsMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MovementSpeed * Time.deltaTime);
            }
        }
        else if (distance < attackRange)
        {
            StartCoroutine(MeleeAttack());
            EnemyAnim.SetBool("IsMoving", false);
        }
    }

    public IEnumerator MeleeAttack()
    {
        PlayerHealth playerHealth = Target.GetComponent<PlayerHealth>(); //reference to the player HP script, for damaging the player
        yield return StartCoroutine(WaitForAnimation());

        if (playerHealth.currentHealth > 0 && MeleeAttackDelay - Time.time <= 0)
        {
            MeleeAttackDelay = Time.time + attackDelay;
            playerHealth.takeDamage(MeleeDamage);
        }
    }

    public IEnumerator RangeAttack()
    {
        yield return WaitForAnimationProjectile();
        Vector3 adjustedPos = transform.position + projectileOffset;
        Vector3 dir = (Target.transform.position - adjustedPos).normalized;
        Transform projectile2 = Instantiate(projectile, adjustedPos, Quaternion.identity);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        projectile2.transform.rotation = Quaternion.Euler(0, 0, angle + (RotateProjectileABit ? 90 : 0));
        projectile2.GetComponent<Rigidbody2D>().AddForce(dir * 500);
    }

    // Start is called before the first frame update
    private void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        Target = GameObject.FindGameObjectWithTag("Player");
        if (Target != null) HpTarget = Target.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void Update()
    {
        nextFire += Time.deltaTime;
        ChasePlayer();
    }

    private IEnumerator WaitForAnimation()
    {
        if (Target.GetComponent<PlayerHealth>().currentHealth != 0) EnemyAnim.Play("Melee");
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator WaitForAnimationProjectile()
    {
        EnemyAnim.Play("Projectile");
        yield return new WaitForSeconds(0.5f);
    }
}