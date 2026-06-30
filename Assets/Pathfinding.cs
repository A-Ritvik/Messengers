using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System;

public class Pathfinding : MonoBehaviour
{

    public GameObject target;
    public float speed; 
    public static int startTime = 0;
    public static int NPCAttackCoolDown = 2;
    Rigidbody2D NPCBody;
    public Animator NPCAnimator;
    RegionManager localRegion;
    Vector2 idleTarget;
    Vector2 targetVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = (float)Variables.Object(gameObject).Get("speed");
        NPCBody = gameObject.GetComponent<Rigidbody2D>();
        localRegion = GetComponentInParent<RegionManager>();
        target = GameObject.FindWithTag("Player");
        idleTarget = new Vector2(localRegion.regionCollider.bounds.min.x, localRegion.designatedSpawnY);
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
        basedOnTargetPos = false;
    }
    public void uponBeingNotCloseEnough()
    {
        basedOnTargetPos = true;
    }
    bool basedOnTargetPos = true;
    IEnumerator waitFor3()
    {
        yield return new WaitForSeconds(3);
        if(idleTarget.x == localRegion.regionCollider.bounds.min.x)
        {
            idleTarget.x = localRegion.regionCollider.bounds.max.x;
        }
        else
        {
            idleTarget.x = localRegion.regionCollider.bounds.min.x;
        }
        changeAlreadyCalled = false;
    }
    //makes sure that changing idle isn't called multiple times avoiding a wait longer than 3 seconds
    bool changeAlreadyCalled;
    void ChangingIdle()
    {
        if(!changeAlreadyCalled)
        {
            changeAlreadyCalled = true;
            StartCoroutine(waitFor3());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!localRegion.playerPresent)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, idleTarget, speed* Time.deltaTime);
            if(idleTarget.x > gameObject.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,0,0);        
            }
            if(Math.Abs(gameObject.transform.position.x - idleTarget.x) < 1.5f)
            {
                NPCAnimator.SetInteger("AnimState",1);
                ChangingIdle();
            }
            else
                NPCAnimator.SetInteger("AnimState", 2);
        }
        else 
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
}
