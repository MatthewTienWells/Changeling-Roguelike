using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    private Transform target;
    public float fleeDistance = 4f;
    //Rigidbody for the mage's spell attack
    public GameObject magicAttack;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //Find the target (the player)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Attack spell = new Attack(); //Create a melee attack
        spell.attackCollider = magicAttack; //Set collider template
        spell.damage_type = "heat"; //Reflects the damage being a fireball
        spell.damage_modifier = 3;
        spell.shape = "area";
        spell.radius = 2;
        spell.range = 5;
        spell.duration = 500;
        spell.special = true;
        speed = 4;
    }

    // Update is called once per frame
    protected override void CombatMovement()
    {

        //find distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        Vector2 moveDirection = transform.position - target.transform.position;

        //if player is close run
        if (distanceToPlayer < fleeDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection * -1, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
        }
    }
}
