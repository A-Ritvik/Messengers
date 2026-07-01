using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    Vector3 origin = new Vector3(3.62F, -1.255F, -1F);
    public enum personality
    {
        attackPlayer
    };
    public Canvas shopScreen;
    public Canvas pauseScreen;
    public Image mailIcon;
    bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPause()
    {
        if(!paused)
        {
            Time.timeScale = 0f;
            pauseScreen.gameObject.SetActive(true);
            paused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.gameObject.SetActive(false);
            paused = false;
        }
    }
    public void onResetClick()
    {
        player.transform.position = origin;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    public void UnPause()
    {
        PlayerControl.climbMode = false;
        PlayerControl.controlsEnabled = true;
        shopScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnEscape(InputAction.CallbackContext context)
    {
        if(BasicMerchantManager.inShop)
        {
            UnPause();
        }
        else
        {
            OnPause();
        }
    }
    public void BackToStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("interact performed");
            if(PlayerControl.hasMail)
            {
                if(PlayerControl.mailFromKing && WizardManager.nearWizard)
                {
                    PlayerControl.coins += KingManager.mailPayout;
                    PlayerControl.hasMail = false;
                    mailIcon.gameObject.SetActive(false);

                }
                else if (KingManager.nearKing && !PlayerControl.mailFromKing)
                {
                    PlayerControl.coins+= WizardManager.mailPayout;
                    PlayerControl.hasMail = false;
                    mailIcon.gameObject.SetActive(false);

                }
            }
            else
            {
                PlayerControl.hasMail = true;
                mailIcon.gameObject.SetActive(true);
                Debug.Log("Mail Icon Should Show Up");
                if(KingManager.nearKing)
                    PlayerControl.mailFromKing = true;
                else
                    PlayerControl.mailFromKing = false;
            }
        }
    }
    public GameObject horizonsBarrier;
    public GameObject boat;
    public void SetjourneyToHorizon()
    {
        horizonsBarrier.SetActive(false);
        boat.SetActive(true);
    }
}
