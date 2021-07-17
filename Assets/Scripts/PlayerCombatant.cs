using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : Combatant
{

    //Store movement data
    public Vector2 movement;
    
    //GameObject for the player's default melee attack
    public GameObject meleeAttack;
    //GameObject for the player's default directional attack
    public GameObject bowAttack;
    //Boolean representing if we are loading player stats from a save file
    private bool loadStats = false;
    //Copies the characteristics of the enemy provided
    new public void Copy(Combatant target)
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
        attacks.Add(new CopyAttack()); //Add the copy ability to known attacks
        attacks.Add(melee);
        attacks.Add(ranged); //Add basic melee and ranged attacks to the player
        if (loadStats)
        {
            setStatsFromFile();
        }
        this.gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
