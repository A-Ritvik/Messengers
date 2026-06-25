using Mono.Cecil.Cil;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine.AI;

public class BasicHealthScript : MonoBehaviour
{
    public int Health;
    public int fullHealth;
        public Sprite healthbarFull;
    public Sprite healthbarHalf;
    public Sprite healthbarEmpty; 
    public SpriteRenderer spriteRender;
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
            Destroy(gameObject);
        }
    }
}
