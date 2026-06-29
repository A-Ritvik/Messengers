using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class Pathfinding : MonoBehaviour
{

    public GameObject target;
    public float speed; 
    public static int startTime = 0;
    public static int NPCAttackCoolDown = 2;
    Rigidbody2D NPCBody;
    public Animator NPCAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = (float)Variables.Object(gameObject).Get("speed");
        NPCBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public bool Attack()
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            NPCAnimator.SetInteger("AnimState", 1);
            basedOnTargetPos = false;
        }
    }
        void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            NPCAnimator.SetInteger("AnimState", 1);
            basedOnTargetPos = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        basedOnTargetPos = true;
    }
    //called to tell the animator the NPC is close enough they don't have to run
    public void ifCloseEnough()
    {
        NPCAnimator.SetInteger("AnimState", 1);
        basedOnTargetPos = false;
    }
    public void uponBeingNotCloseEnough()
    {
        basedOnTargetPos = true;
    }
    bool basedOnTargetPos;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed* Time.deltaTime);
        if (gameObject.transform.position.x != target.transform.position.x && basedOnTargetPos)
            NPCAnimator.SetInteger("AnimState", 2);
        else if (basedOnTargetPos)
            NPCAnimator.SetInteger("AnimState", 1);
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
