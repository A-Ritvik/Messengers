using UnityEngine;

public class KingManager : MonoBehaviour
{
    public GameObject interactPopup;
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
            interactPopup.gameObject.SetActive(true);
        }
    }
    void OnTriggerExits2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            interactPopup.gameObject.SetActive(false);
        }
    }
}
