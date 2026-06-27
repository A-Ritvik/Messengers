using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMerchantManager : MonoBehaviour
{
    public TextMeshProUGUI merchantPopUp;
    public GameObject shopScreen;
    public static bool inShop = false;
    bool nearShop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            merchantPopUp.gameObject.SetActive(true);
            nearShop = true;
        }
    }
    void OnTriggerStay2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            merchantPopUp.gameObject.SetActive(true);
            nearShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            merchantPopUp.gameObject.SetActive(false);
            nearShop = false;
        }
    }
    public void OnShopEnter(InputAction.CallbackContext context)
    {
        if(nearShop)
        {
        shopScreen.gameObject.SetActive(true);
        //call climb mode because it disables all controls except climb when true
        PlayerControl.climbMode = true;
        //enabled control ONLY disables climb
        PlayerControl.controlsEnabled = false;
        Time.timeScale = 0f;
        inShop = true;
        }
    }
}
