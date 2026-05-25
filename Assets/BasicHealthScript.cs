using Mono.Cecil.Cil;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.AI;

public class BasicHealthScript : MonoBehaviour
{
    public int Health;
    public int attackValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = (int)Variables.Object(gameObject).Get("Health");
    }
    public void onAttack(int attackValue)
    {
        Health -= attackValue;
        GetComponent<ParticleSystem>().Play();
    }
    public void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
    bool checkPos()
    {
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        if (checkPos())
        {
            FindPlayer();
        }
    }
}
