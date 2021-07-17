using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : Combatant
{

    //Store movement data
    public Vector2 movement;
    //Representation of the player pawn
    public Pawn pawn;
    //Rigidbody for the player's default melee attack
    public GameObject meleeAttack;
    //Rigidbody for the player's default directional attack
    public GameObject bowAttack;
    //Boolean representing if we are loading player stats from a save file
    private bool loadStats = false;
    //Index of the attack the player is using currently
    private int loadedAttack = 0;
    //Copies the characteristics of the enemy provided
    void Copy(Combatant target)
    {
        max_health = target.max_health;
        speed = target.speed;
        defense = target.defense;
        resistances = target.resistances;
        base_attack = target.base_attack;
        cooldown = target.cooldown;
        foreach (Attack curAttack in attacks)
        {
            if (curAttack.special)
            {
                attacks.Remove(curAttack);
            }
        }
        attacks.AddRange(target.attacks);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = target.gameObject.GetComponent<Animator>().runtimeAnimatorController;
    }

    void setStatsFromFile()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Attack melee = new Attack(); //Create a melee attack
        melee.attackCollider = meleeAttack; //Set collider template
        melee.damage_type = "slashing"; //Reflects the damage being a bite
        melee.damage_modifier = 2;
        melee.shape = "melee";
        melee.radius = 0.5;
        Attack ranged = new Attack(); //Create a melee attack
        ranged.attackCollider = bowAttack; //Set collider template
        ranged.damage_type = "piercing"; //Reflects the damage being a bowshot
        ranged.damage_modifier = 1;
        ranged.shape = "directional";
        attacks.Add(melee);
        attacks.Add(ranged); //Add basic melee and ranged attacks to the player
        if (loadStats)
        {
            setStatsFromFile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.Lerp(transform.position, Input.mousePosition, speed); //Set movement towards the mouse cursor
        if (Input.GetMouseButtonDown(0) && !onCooldown()) //If the player is clicking and has an attack ready
        {
            if (loadedAttack > attacks.Count) //MAke sure that the attack index exists
            {
                loadedAttack = 0; //Reset the index to 0 if it doesn't
            }
            attacks[loadedAttack].TriggerAttack(); //Execute the current attack
        }
        if (Input.GetMouseButtonDown(1)) //IF the player is clicking the other mouse button
        {
            loadedAttack += 1; //Increment the active attack to the next known attack
        }
    }

    private void FixedUpdate()
    {
        pawn.Move(movement); //Move towards the given location
    }
}
