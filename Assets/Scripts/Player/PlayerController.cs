using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public Transform rightStaffOrb, leftStaffOrb;
    public float speed;
    public Animator playerAnim;
    public SpriteRenderer playerSprite;
    public bool isMoving = false;
    public bool isAttacking;
    public ParticleSystem attackParticles;
    public bool facingLeft;
    public Rigidbody2D playerRB;
    private float horizontal, vertical;
    private float moveLimiter = 0.7f;
    // Use this for initialization
    void Start() {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update() {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement() {

        speed = 0;

        horizontal = Input.GetAxisRaw("Horizontal");
        //print(horizontal);
        vertical = Input.GetAxisRaw("Vertical");

        isMoving = (horizontal != 0 || vertical != 0);

        var moveVector = new Vector3(horizontal, vertical, 0);

        //flip sprite x direction based on input
        if (horizontal < 0)
        {
            playerSprite.flipX = true;
            facingLeft = true;
        }
        else if(horizontal > 0)
        {
            playerSprite.flipX = false;
            facingLeft = false;
        }

        if (isMoving && !player.GetComponent<PlayerHealth>().isDead) {
            if (player.GetComponentInChildren<Blink>() != null && !player.GetComponentInChildren<Blink>().isChanneling || player.GetComponentInChildren<Blink>() == null)
            {
                speed = 3;
                playerRB.MovePosition(new Vector2(player.transform.position.x + moveVector.x * speed * Time.deltaTime, player.transform.position.y + moveVector.y * speed * Time.deltaTime));
            }
        }

        playerAnim.SetFloat("speed", speed);
    }

    private void HandleAttack(){
        if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            isAttacking = true;
            playerAnim.SetBool("isAttacking", isAttacking);
            if(facingLeft){
                attackParticles.transform.position = leftStaffOrb.position;
            }
            else{
                attackParticles.transform.position = rightStaffOrb.position;
            }
            attackParticles.Clear();
            attackParticles.Play();
        }
        else{
            isAttacking = false;
            playerAnim.SetBool("isAttacking", isAttacking);
        }
    }
}
