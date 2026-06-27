using System.Collections;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public enum shopItems
    {
        //listed in order of assigned shop#
      heal, strength, speed  
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator tempPopUp(TextMeshProUGUI popUp)
    {
        popUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        popUp.gameObject.SetActive(false);
    }
    public GameObject player;
    public TextMeshProUGUI notEnoughMoney;
    public void BuyHealthPotion() => BuyItem(1, shopItems.heal);
    public void BuyStrengthPotion() => BuyItem(3, shopItems.strength);
    public void BuySpeedPotion() => BuyItem(2, shopItems.speed);
    void BuyItem(int cost, shopItems item)
    {
        if(PlayerControl.coins >= cost)
        {
            PlayerControl.coins -= cost;
            if(item == shopItems.heal)
            {
                player.GetComponent<BasicHealthScript>().Health += 2;
            }
            else if (item == shopItems.strength)
            {
                player.GetComponentInChildren<AttackManager>().attackStrength ++;
            }
            else
            {
                player.GetComponent<PlayerControl>().speed ++;
            }
        }
        else
        {
            StartCoroutine(tempPopUp(notEnoughMoney));
        }
    }

}
