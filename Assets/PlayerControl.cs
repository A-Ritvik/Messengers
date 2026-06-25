using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    SpriteRenderer spriteRender;
    private Vector2 moveInput;
    public GameObject attackBox;

    public float speed = 5f;
    public int NumberAttack1;
    int NumberAttack1CoolDown = 6;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.x<0)
        {
            spriteRender.flipX=true;
            attackBox.transform.localPosition = new Vector3(-0.1f,0,0);
            attackBox.transform.localRotation = Quaternion.Euler(0,180,-50.157f);

        }
        else if(moveInput.x>0)
        {
            spriteRender.flipX=false;
            attackBox.transform.localPosition = new Vector3(0.101f, -0.011f, 0.0422f);
            attackBox.transform.localRotation = Quaternion.Euler(0,0,-50.157f);
        }
    }
    Rigidbody2D rb;
     public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount < jumpMax)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);

            rb.AddForce(Vector2.up * jumpMultiplier, ForceMode2D.Impulse);

            jumpCount++;
        }
    }
    public bool attackStateValid;
    float attackDuration = 0.5f;
    public void onAttack(InputAction.CallbackContext context)
    {
        if(attackStateValid)
        {
        if (context.performed)
        {
            attackBox.GetComponent<Animator>().SetTrigger("Attack");
            StartCoroutine(Attack());
            timeStamp = secondsPassed;
            NumberAttack1++;

        }
        }
    }
    IEnumerator Attack()
    {
        attackBox.SetActive(true);
        attackBox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackBox.GetComponent<BoxCollider2D>().enabled = false;
    }
    public int jumpMultiplier = 2;
    int jumpCount = 0;
    public int jumpMax;
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
                    Debug.Log("attack ready");
                    attackStateValid = true;
                }
            secondsPassed++;
            yield return new WaitForSeconds(1);
        }
    }
    private void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
    void Start()
    {
        StartCoroutine(AttackCoolDown());
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
}