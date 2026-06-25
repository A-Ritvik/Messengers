using Mono.Cecil.Cil;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.AI;

public class BasicHealthScript : MonoBehaviour
{
    public int Health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Variables.Object(gameObject).IsDefined("Health"))
        {
        Health = (int)Variables.Object(gameObject).Get("Health");
        }
    }
    public void onAttacked(int attackValue)
    {
        Health -= attackValue;
        GetComponent<ParticleSystem>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
