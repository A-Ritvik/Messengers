using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Pathfinding localPathfind;
    public int attackStrength = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackableCharacter"))
        {
            other.gameObject.GetComponent<BasicHealthScript>().onAttacked(attackStrength);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if(localPathfind.Attack())
            {
                localPathfind.ifCloseEnough();
                localPathfind.NPCAnimator.SetTrigger("Attack");
                other.gameObject.GetComponent<BasicHealthScript>().onAttacked(attackStrength);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(gameObject.transform.parent.tag != "Player" && collision.gameObject.tag == "Player")
        {
            localPathfind.uponBeingNotCloseEnough();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.transform.parent.tag != "Player")
        {
            localPathfind = gameObject.GetComponentInParent<Pathfinding>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
