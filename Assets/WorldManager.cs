using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    Vector3 origin = new Vector3(3.62F, -1.255F, -1F);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onResetClick()
    {
        player.transform.position = origin;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
