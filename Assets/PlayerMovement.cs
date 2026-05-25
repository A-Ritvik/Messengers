using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    SpriteRenderer spriteRender;
    private Vector2 moveInput;
    public GameObject attackBox;

    public float speed = 5f;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.x<0)
        {
            spriteRender.flipX=true;
            attackBox.transform.localPosition = new Vector3(-0.1f,0,0);
            attackBox.transform.localRotation = Quaternion.Euler(0,180,-90);

        }
        else if(moveInput.x>0)
        {
            spriteRender.flipX=false;
            attackBox.transform.localPosition = new Vector3(0.101f, -0.011f, 0.0422f);
            attackBox.transform.localRotation = Quaternion.Euler(0,0,-90);
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
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackBox.SetActive(true);
        }
        else
        {
            attackBox.SetActive(false);
        }
    }
    public int jumpMultiplier = 2;
    int jumpCount = 0;
    public int jumpMax;

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
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
}