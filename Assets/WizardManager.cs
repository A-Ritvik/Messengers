using UnityEngine;

public class WizardManager : MonoBehaviour
{
    public GameObject interactNewMessagePopup;
    public GameObject interactRecieveMessagePopup;
    public static int mailPayout = 1;
    public static bool nearWizard;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!PlayerControl.hasMail)
            {
                interactNewMessagePopup.gameObject.SetActive(true);
            }
            else
            {
                interactRecieveMessagePopup.gameObject.SetActive(true);
            }
            nearWizard= true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!PlayerControl.hasMail)
            {
                interactNewMessagePopup.gameObject.SetActive(false);
            }
            else
            {
                interactRecieveMessagePopup.gameObject.SetActive(false);
            }
            nearWizard = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(PlayerControl.hasMail)
            {
                interactNewMessagePopup.gameObject.SetActive(false);
                interactRecieveMessagePopup.gameObject.SetActive(true);                
            }
            else
            {
                interactRecieveMessagePopup.gameObject.SetActive(false);
                interactNewMessagePopup.gameObject.SetActive(true);
            }
        }
    }
}
