using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Combatant
{
    [SerializeField] Transform pawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        pawnTransform = FindObjectOfType<Pawn>().GetComponent<Transform>();
    }

    //Checks if the player is in the range of an attack, and, if so, attacks. Returns true if an attack was executed, false otherwise.
    public bool AttemptAttack()
    {
        List<Attack> possibleAttacks = validAttacks(pawnTransform.position.x,transform.position.x,pawnTransform.position.y,transform.position.y);
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
            transform.LookAt(pawnTransform);
            chosenAttack.TriggerAttack();
            return true;
        }
        else 
        {
            return false;
        }
    }

    //Moves according to the comabt movement AI
    private void CombatMovement() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If the player is nearby
        if (checkForPlayer(pawnTransform.position.x,transform.position.x,pawnTransform.position.y,transform.position.y))
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
        else
        {
            anim.SetBool("Walking", false);
        }

    }
}
