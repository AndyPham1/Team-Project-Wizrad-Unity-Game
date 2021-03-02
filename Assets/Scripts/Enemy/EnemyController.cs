using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float speed;
    public float attackRange;
    public int damage;
    public float currentAttackTime = 0;
    public float attackDelay = 3;
    public Animator skeletonAnim;
    public SpriteRenderer skeletonSprite;
    public GameObject player;
    public bool isAttacking;
    public bool Boost;
    public int BoostAttack = 1;

    private IEnumerator attack;

    // Use this for initialization
    void Start () 
    {
        //Initialize reference to the player
        //var rootGameObjects = gameObject.scene.GetRootGameObjects();
        var initPlayer = GameObject.FindGameObjectWithTag("Player");
        //player = rootGameObjects[2];
        player = initPlayer;

        //Debug.Log(x.ToString()); //There should probably be a better way of doing this...
    }
	
	// Update is called once per frame
	void Update () 
    {

        Chase();

        // stop moving towards the player at distance 3
        //print(Vector2.Distance(transform.position, player.transform.position));
        //this is real weird
        //transform.LookAt(player.transform.position, transform.up);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player.GetComponent<Collider2D>()) {
            print("Collision");
        }
    }

    private void Chase()
    {
        skeletonSprite.flipX = (player.transform.position.x < transform.position.x);

        if (Vector2.Distance(transform.position, player.transform.position) > attackRange && !isAttacking)
        {
            //isAttacking = false;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            skeletonAnim.SetFloat("speed", speed);
        }
        else
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        var playerHealth = player.GetComponent<PlayerHealth>(); //reference to the player HP script, for damaging the player
        isAttacking = true;
        yield return StartCoroutine(WaitForAnimation());
        int damageModifier = 0;
        if (Boost)
        {
            damageModifier = BoostAttack;
        }
        
        if (playerHealth.currentHealth > 0 && currentAttackTime-Time.time <= 0)
        {
            currentAttackTime = Time.time + attackDelay;
            playerHealth.takeDamage(damage + damageModifier);
        }
        isAttacking = false;
    }

    IEnumerator WaitForAnimation(){
        skeletonAnim.Play("Skeleton_Attack");
        yield return new WaitForSeconds(0.5f);
    }
}
