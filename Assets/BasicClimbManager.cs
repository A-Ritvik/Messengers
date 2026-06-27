using UnityEngine;
using UnityEngine.InputSystem;

public class BasicClimbManager : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerControl.climbMode = true;
            PlayerControl.playerBody.linearVelocity = new Vector2(0,0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerControl.climbMode = false;
        }
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

    }
}
