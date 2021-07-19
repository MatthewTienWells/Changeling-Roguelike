using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Combatant
{
    [SerializeField] protected Transform playerTransform;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    //Checks if the player is in the range of an attack, and, if so, attacks. Returns true if an attack was executed, false otherwise.
    public bool AttemptAttack()
    {
        List<Attack> possibleAttacks = validAttacks(playerTransform.position.x,transform.position.x,playerTransform.position.y,transform.position.y);
        if (possibleAttacks.Any() &&
            !onCooldown()) //Checks if any attacks can currently be executed and if the enemy is not on cooldown
        {
            Attack chosenAttack;
            int bestDamage = 0;
            chosenAttack = possibleAttacks[0];
            foreach(Attack curAttack in possibleAttacks) //Iterate through available attacks
            {
                if (curAttack.damage_modifier >= bestDamage)
                {
                    chosenAttack = curAttack;
                    bestDamage = curAttack.damage_modifier;
                }
            }
            transform.LookAt(playerTransform);
            chosenAttack.TriggerAttack();
            return true;
        }
        else 
        {
            return false;
        }
    }

    //Moves according to the comabt movement AI
    protected virtual void CombatMovement() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If the player is nearby
        if (checkForPlayer(playerTransform.position.x,transform.position.x,playerTransform.position.y,transform.position.y))
        {
            if (AttemptAttack())
            {
                resetCooldown();
            }
            else
            {
                CombatMovement();
            }
        }
    }
}
