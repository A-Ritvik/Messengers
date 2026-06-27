using UnityEngine;

public class CastleEntryManager : MonoBehaviour
{
    public GameObject castleEntry; 
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
            PlayerControl.player.transform.position = new Vector3(castleEntry.transform.position.x, castleEntry.transform.position.y, -1);
            PlayerControl.SwitchMode(true);
        }
    }
}
