using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] public Rigidbody2D rb;
    SpriteRenderer sr;

    //Maximum health of combatant
    public int max_health = 50;



    //Current health of combatant
    public int health = 50;

    //Defense reduces damage taken from resisted attacks
    public int defense = 0;

    //Speed at which the entity moves
    public float speed = 1;

    //Base attack damage dealt by this entity
    public int base_attack = 5;

    //Types of damage that are resisted
    public List<string> resistances;

    //Types of attack available to this entity

    public List<Attack> attacks;

    //Minimum time in ticks between attacks
    public int cooldown = 1000;

    //Time since last attack
    private int cooldownTimer = 1000;

    //Distance at which an enemy Combatant notices the player- this should be smaller than rooms if it does not filter by room

    private double detectDistance = 10.0;

    //Distance at which an enemy Combatant ceases following the player

    private double followDistance = 30.0;

    //Bool representing if the Combatant is engaging the player
    private bool inCombat = false;

    //Function takes a location for the player and the location of the comabatant and test if the player is currently detected
    public bool checkForPlayer(float pawnLocationX, float selfLocationX, float pawnLocationY, float selfLocationY)
    {

        if(Mathf.Abs(pawnLocationX - selfLocationX) > followDistance || Mathf.Abs(pawnLocationY - selfLocationY) > followDistance)
        {
            //The player is out of range
            inCombat = false;
        }
        else if (Mathf.Abs(pawnLocationX - selfLocationX) <= detectDistance && Mathf.Abs(pawnLocationY - selfLocationY) <= detectDistance)
        {
            //The player has entered the detection range
            inCombat = true;
        }
        return inCombat;
    }

    //Function returns any attacks which would hit a given location from another given location
    public List<Attack> validAttacks(float pawnLocationX, float selfLocationX, float pawnLocationY, float selfLocationY)
    {
        List<Attack> possibleAttacks = new List<Attack>();
        foreach (Attack curAttack in attacks)
        {
            if (curAttack.canHit(pawnLocationX, selfLocationX, pawnLocationY, selfLocationY))
            {
                possibleAttacks.Add(curAttack);
            }
        }

        return possibleAttacks;
    }

    //Returns true if the entity is cooling down from an attack
    public bool onCooldown()
    {
        return (cooldownTimer < cooldown);
    }

    //Resets cooldown timer
    public void resetCooldown()
    {
        cooldownTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //enemy animation and sprite renderer 
        anim = gameObject.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer < cooldown)
        {
            cooldownTimer ++;
        }
    }

    public void SetAnimations(Vector2 movement)
    {
        anim.SetFloat("Horizontal", movement.x); //pass x to animator horizontal
        anim.SetFloat("Vertical", movement.y); //pass y to animator vertical
        anim.SetFloat("Speed", movement.sqrMagnitude); //pass magnitude to animator
    }

    //Show animation for taking damage, check if combatant has died
    public void TakeDamage(int damage_value)
    {
        if (health <= damage_value)
        {
            //If the combatant's health has been fully drained, remove them from play
            Destroy(this.gameObject);
        }
        else
        {
            //Reduce the combatant's current health
            health -= damage_value;
        }
    }
}
