using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Enemy
{
    private Transform target;
    public float runDistance = 4f;
    //GameObject for the troll's melee attack
    public GameObject meleeAttack;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //Find the target (the player)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Attack melee = new Attack(); //Create a melee attack
        melee.attackCollider = meleeAttack; //Set collider template
        melee.damage_type = "bludgeoning"; //Reflects the damage being a smash
        melee.damage_modifier = 4;
        melee.shape = "melee";
        melee.special = true;
        speed = 2;
        resistances.Add("bludgeoning");
        defense = 5;
        melee.radius = 2;
    }

    // Update is called once per frame
    protected override void CombatMovement()
    {

        //find distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        Vector2 moveDirection = transform.position - target.transform.position;

        //if player is close run
        if (distanceToPlayer < runDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime * 2);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
        }
    }
}