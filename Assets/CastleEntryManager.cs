using UnityEngine;

public class CastleEntryManager : MonoBehaviour
{
    GameObject castleEntry; 
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
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl.player.transform.position = castleEntry.transform.position;
            PlayerControl.topdownMode = true;
        }
    }
}
