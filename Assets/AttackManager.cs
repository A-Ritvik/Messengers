using UnityEngine;

public class AttackManager : MonoBehaviour
{
    int attackStrength = 2;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackableCharacter"))
        {
            collision.gameObject.GetComponent<BasicHealthScript>().onAttack(attackStrength);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
