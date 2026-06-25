using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class Pathfinding : MonoBehaviour
{

    public GameObject target;
    public float speed; 
    int startTime;
    public int NPCAttackCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = (float)Variables.Object(gameObject).Get("speed");
    }
    void Attack(Collider2D other)
    {

        if(TimeManager.timeStamp - startTime >= NPCAttackCoolDown)
        if(other.tag == "Player")
        {
            TimeManager.isCounting = true;
            startTime = TimeManager.timeStamp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed* Time.deltaTime);
    }
}
