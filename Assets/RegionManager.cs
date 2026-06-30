using System.Collections;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    bool isAccessible;
    public bool smallArea;
    public GameObject npcEnemy;
    public int npcCount;
    int targetCount;
    public bool playerPresent;
    public float designatedSpawnY;
    public BoxCollider2D regionCollider;
    Vector3 designatedSpawn;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (gameObject.tag == "Accessible")
        {
            isAccessible = true;
        }
        else
        {
            isAccessible = false;
        }
        if(isAccessible)
        {
            if(smallArea)
                targetCount = 1;
            else   
                targetCount = 2;
            regionCollider = GetComponent<BoxCollider2D>();
            designatedSpawn.y = designatedSpawnY;
            designatedSpawn.z = -1;
            for (int number = 1; number<=targetCount; number++)
            {
                if(number%2 != 0)
                {
                    designatedSpawn.x = regionCollider.bounds.max.x;
                    Instantiate(npcEnemy, designatedSpawn, Quaternion.identity, transform);
                }
                else
                {
                    designatedSpawn.x = regionCollider.bounds.min.x;
                    Instantiate(npcEnemy, designatedSpawn, Quaternion.identity, transform);
                }
            }
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerPresent = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerPresent = false;
        }
    }
    IEnumerator banditRespawnCoolDown()
    {
        yield return new WaitForSeconds(5);
        Instantiate(npcEnemy, designatedSpawn, Quaternion.identity, transform);
    }
    public void OnBanditDeath()
    {
        StartCoroutine(banditRespawnCoolDown());
    }
}
