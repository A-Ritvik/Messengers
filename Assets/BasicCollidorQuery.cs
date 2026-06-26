using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCollidorQuery : MonoBehaviour
{
    public TextMeshProUGUI ladderAskPopUp;
    public bool ventNearby;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onLadderPress(InputAction.CallbackContext context)
    {
        if(ventNearby)
        {
            player.transform.position = new Vector3(-35, -2.5f, player.transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        ladderAskPopUp.gameObject.SetActive(true);
        ventNearby = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        ladderAskPopUp.gameObject.SetActive(true);
        ventNearby = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        ladderAskPopUp.gameObject.SetActive(false);
        ventNearby = false;
        }
    }
}
