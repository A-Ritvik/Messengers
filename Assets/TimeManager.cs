using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool isCounting;
    public static int timeStamp; 
    bool turnedOn; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCounting && turnedOn!)
        {
            count();
        }
    }
    IEnumerator count()
    {
        turnedOn = true;
        while(isCounting)
        {
            yield return new WaitForSeconds(1);
            timeStamp ++; 
        }
        if(isCounting!)
        {
            turnedOn = false;
        }
    }

}
