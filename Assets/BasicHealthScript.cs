using Mono.Cecil.Cil;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class BasicHealthScript : MonoBehaviour
{
    public float Health;
    public float fullHealth;
        public Sprite healthbarFull;
    public Sprite healthbarHalf;
    public Sprite healthbarEmpty; 
    public SpriteRenderer spriteRender;
    public GameObject coinPrefab;
    ParticleSystem particles;
    public Image heartUI;
    public WorldManager localManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Variables.Object(gameObject).IsDefined("Health"))
        {
        Health = (int)Variables.Object(gameObject).Get("Health");
        }
        fullHealth = Health;
        if (gameObject.tag != "Player")
        {
            particles = GetComponent<ParticleSystem>();
        }
        localRegion = GetComponentInParent<RegionManager>();
    }
    float healthRatio;
    public void onAttacked(int attackValue)
    {
        Debug.Log("Something with tag " + gameObject.tag + " was attacked");
        Health -= attackValue;
        if ( particles != null)
        {
            particles.Clear();
            particles.Play();
        }
        else
        {
            PlayerControl.Health -= attackValue;
            if(Health <= fullHealth/2 )
            {
                spriteRender.sprite = healthbarHalf;
            }
            else if(Health <= fullHealth/4)
            {
                spriteRender.sprite = healthbarEmpty;
            }
            healthRatio = Health/fullHealth;
            heartUI.color = Color.Lerp(Color.black, Color.red, healthRatio);

        }
    }
    public void UpdateHealthUI()
    {
        healthRatio = Health/fullHealth;
        heartUI.color = Color.Lerp(Color.black, Color.red, healthRatio);
    }
    bool dead = false;
    RegionManager localRegion;
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && !dead)
        {
            dead = true;
            if(gameObject.tag == "AttackableCharacter")
            {
                Instantiate(coinPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.Euler(0,0,0));
                localRegion.OnBanditDeath();
                Destroy(gameObject);
            }
            else if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}
