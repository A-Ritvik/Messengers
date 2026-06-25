using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public int attackStrength = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision detected");
        if (other.gameObject.CompareTag("AttackableCharacter"))
        {
            other.gameObject.GetComponent<BasicHealthScript>().onAttacked(attackStrength);
            Debug.Log("collision with attackable charachter detected");
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
