using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    //public Animator playerAnim;
    public SpriteRenderer playerSprite;
    public bool isMoving = false;
    public bool facingLeft;
    public Rigidbody2D playerRB;
    private float horizontal, vertical;
    private float moveLimiter = 0.7f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

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
        else if (horizontal > 0)
        {
            playerSprite.flipX = false;
            facingLeft = false;
        }

        if (isMoving)
        {
            speed = 7;
            playerRB.MovePosition(new Vector2(player.transform.position.x + moveVector.x * speed * Time.deltaTime, player.transform.position.y + moveVector.y * speed * Time.deltaTime));
        }

        //playerAnim.SetFloat("speed", speed);
    }
}
