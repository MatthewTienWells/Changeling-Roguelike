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
    public Rigidbody meleeAttack;
    //Rigidbody for the player's default directional attack
    public Rigidbody bowAttack;
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

    // Start is called before the first frame update
    void Start()
    {
        Attack melee = new Attack(); //Create a melee attack
        melee.attackCollider = meleeAttack; //Set collider template
        melee.damage_type = "slashing"; //Reflects the damage being a bite
        melee.damage_modifier = 2;
        melee.shape = "melee";
        melee.radius = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.Lerp(transform.position, Input.mousePosition, speed);
        
    }

    private void FixedUpdate()
    {
        pawn.Move(movement);
    }
}
