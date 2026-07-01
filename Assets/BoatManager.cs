using UnityEngine;

public class BoatManager : MonoBehaviour
{
    Vector3 move = new Vector3(1,0,0);
    bool playerPresent;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPresent)
        {
            player.transform.parent = gameObject.transform;
            gameObject.transform.Translate(move * Time.deltaTime);
            player.GetComponent<Animator>().SetInteger("AnimState", 0);

        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerPresent = true;
        }
    }
}
