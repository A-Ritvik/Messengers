using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    public static TagHandle playerTag = TagHandle.GetExistingTag("Player");
    Vector3 origin = new Vector3(3.62F, -1.255F, -1F);
    public enum personality
    {
        attackPlayer
    };
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
