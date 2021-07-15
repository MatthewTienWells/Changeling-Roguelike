using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Combatant player;
    //store movement data
    public Vector2 movement;

    //Rigidbody for the player's melee attack
    public Rigidbody meleeAttack;
    //Rigidbody for the player's default directional attack
    public Rigidbody bowAttack;
    //Boolean representing if we are loading player stats from a save file
    private bool loadStats = false;
    //Index of the attack the player is using currently
    private int loadedAttack = 0;
    //Index of secondary attack to swap to
    private int secondAttack = 1;
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
        if (attacks.Count > loadedAttack)
        {
            loadedAttack = attacks.Count;
        }
        if (attacks.Count > secondAttack)
        {
            secondAttack = attacks.Count;
        }
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
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        player.SetAnimations(movement);

        if (Input.GetKeyDown("Fire1"))
        {
            attacks[loadedAttack].TriggerAttack();
        }

        if (Input.GetKeyDown("Fire2"))
        {
            int swap = loadedAttack;
            loadedAttack = secondAttack;
            secondAttack = swap;
        }

        if (Input.mouseScrollDelta.y > 0 && loadedAttack < attacks.Count)
        {
            loadedAttack += 1;
        }

        if (Input.mouseScrollDelta.y < 0 && loadedAttack > 0)
        {
            loadedAttack -= 1;
        }
    }

    private void FixedUpdate()
    {
        player.CombatMovement(movement);
    }
}
