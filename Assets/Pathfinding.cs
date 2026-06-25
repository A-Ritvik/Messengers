using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class Pathfinding : MonoBehaviour
{

    public GameObject target;
    public float speed; 
    public static int startTime = 0;
    public static int NPCAttackCoolDown = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = (float)Variables.Object(gameObject).Get("speed");
    }
    public static bool Attack()
    {
        Debug.Log("attack checking for NPC called");
        if(TimeManager.timeStamp - startTime >= NPCAttackCoolDown)
        {
            TimeManager.isCounting = true;
            startTime = TimeManager.timeStamp;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed* Time.deltaTime);
        if(target.transform.position.x > gameObject.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);        
        }
    }
}
