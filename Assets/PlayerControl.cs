using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public static int coins;
    public TextMeshProUGUI coinDisplay;
    SpriteRenderer spriteRender;
    private Vector2 moveInput;
    public GameObject attackBox;

    public float speed = 5f;
    public int NumberAttack1;
    int NumberAttack1CoolDown = 6;
    public static bool climbMode = false;
    public static bool controlsEnabled = true;
    public static int Health;
    public static PlayerInput playerInput;
    public static Rigidbody2D playerBody;
    public Animator playerAnimator;
    public static bool hasMail = false;
    public static bool mailFromKing;

    public static void SwitchMode(bool topDownMode)
    {
        if(topDownMode)
        {
            playerInput.SwitchCurrentActionMap("PlayertopDown Mode");
            playerBody.gravityScale = 0;
            playerBody.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Custom Player");
            playerBody.gravityScale = 1;
            playerBody.linearVelocity = new Vector2(0, 0);
        }
    }
    bool exitSide;
    bool ventNearby;
    public void onPopVent(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
        if(ventNearby)
        {
            
            if(exitSide)
            {
                player.transform.position = new Vector3(-39, -1.292f, player.transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(-39.4f, -2.467f, player.transform.position.z);
            }
        }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        BasicCollidorQuery vent = collision.GetComponent<BasicCollidorQuery>();
        if(vent != null)
        {
            ventNearby = true;
            if(collision.gameObject.tag == "Exit")
            {
                exitSide = true;
            }
            else
            {
                exitSide = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        BasicCollidorQuery vent = collision.GetComponent<BasicCollidorQuery>();
        if(vent!= null)
        {
            ventNearby = false;
        }
    }
    public void On4dirMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        if (moveInput.x<0)
            {
                spriteRender.flipX=true;
                attackBox.transform.localPosition = new Vector3(-0.464f,0.918f,0);
                attackBox.transform.localRotation = Quaternion.Euler(0,180,205.757f);

            }
        else if(moveInput.x>0)
            {
                spriteRender.flipX=false;
                attackBox.transform.localPosition = new Vector3(0.467f, 0.77f, 0);
                attackBox.transform.localRotation = Quaternion.Euler(0,0,209.828f);
            }
        move = new Vector2(moveInput.x, moveInput.y);
    }
    public void OnClimb(InputAction.CallbackContext context)
    {
        if(controlsEnabled)
        {
        if(climbMode)
        {
                move.x = 0;
                move.y = context.ReadValue<float>();
        }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(!climbMode)
        {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.x<0)
        {
            spriteRender.flipX=true;
            attackBox.transform.localPosition = new Vector3(-0.464f,0.918f,0);
            attackBox.transform.localRotation = Quaternion.Euler(0,-180,933f);

        }
        else if(moveInput.x>0)
        {
            spriteRender.flipX=false;
            attackBox.transform.localPosition = new Vector3(0.505f, 0.813f, 0);
            attackBox.transform.localRotation = Quaternion.Euler(0,0,28.692f);
        }
        move = new Vector2(moveInput.x, moveInput.y);
        }
    }
    Rigidbody2D rb;
     public void OnJump(InputAction.CallbackContext context)
    {
        if(!climbMode)
        {
        if (context.performed && jumpCount < jumpMax)
        {
            playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("Grounded", false);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);

            rb.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);

            jumpCount++;
        }
        }
    }
    public bool attackStateValid;
    float attackDuration = 0.5f;
    public void onAttack(InputAction.CallbackContext context)
    {
        if(attackStateValid)
        {
            if(!climbMode)
            {
                if (context.performed)
                {
                    StartCoroutine(Attack());
                    timeStamp = secondsPassed;
                    NumberAttack1++;

                }
            }
        }
    }
    IEnumerator Attack()
    {
        attackBox.SetActive(true);
        attackBox.GetComponent<BoxCollider2D>().enabled = true;
        playerAnimator.SetTrigger("Attack1");
        yield return new WaitForSeconds(attackDuration);
        attackBox.GetComponent<BoxCollider2D>().enabled = false;
    }
    public int jumpMultiplier = 2;
    int jumpCount = 0;
    public int jumpMax = 1;
    public int secondsPassed;
    public int timeStamp;
    IEnumerator AttackCoolDown()
    {
        while(true)
        {
            if((secondsPassed - timeStamp)%NumberAttack1CoolDown == 0 && (secondsPassed-timeStamp) != 0)
            {
                NumberAttack1 = 0;
            }
            if(NumberAttack1 >= 1)
                {
                    attackStateValid = false;
                }
                else if (NumberAttack1 == 0)
                {
                    attackStateValid = true;
                }
            secondsPassed++;
            yield return new WaitForSeconds(1);
        }
    }
    public Vector2 move;
    private void Update()
    {
        
        transform.Translate(move * speed * Time.deltaTime);
        coinDisplay.text = coins.ToString();
        if (Mathf.Abs(move.x) > 0.01f)
        {
            playerAnimator.SetInteger("AnimState", 1);
            playerAnimator.SetTrigger("Run");
        }
        else
            playerAnimator.SetInteger("AnimState", 0);
            playerAnimator.SetTrigger("Idle");
        playerAnimator.SetFloat("AirSpeedY", playerBody.linearVelocityY);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnimator.SetBool("Grounded", true);
            playerAnimator.SetTrigger("Idle");
            jumpCount = 0;
        }
    }
    public static GameObject player;
    void Start()
    {
        StartCoroutine(AttackCoolDown());
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        player = gameObject;
        playerInput = GetComponent<PlayerInput>();
        playerBody = GetComponent<Rigidbody2D>();
    }
    //for test purposes
    public void AddCoin()
    {
        coins+=20;
    }
}