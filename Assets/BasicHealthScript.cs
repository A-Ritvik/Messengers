using Mono.Cecil.Cil;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.AI;
using UnityEditor;

public class BasicHealthScript : MonoBehaviour
{
    public int Health;
    public int fullHealth;
        public Sprite healthbarFull;
    public Sprite healthbarHalf;
    public Sprite healthbarEmpty; 
    public SpriteRenderer spriteRender;
    public GameObject coinPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Variables.Object(gameObject).IsDefined("Health"))
        {
        Health = (int)Variables.Object(gameObject).Get("Health");
        }
        fullHealth = Health;
    }
    public void onAttacked(int attackValue)
    {
        Health -= attackValue;
        if ( GetComponent<ParticleSystem>() != null)
        {
            GetComponent<ParticleSystem>().Play();
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
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if(gameObject.tag == "AttackableCharacter")
            {
                Instantiate(coinPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.Euler(0,0,0));
            }
            Destroy(gameObject);
        }
    }
}
