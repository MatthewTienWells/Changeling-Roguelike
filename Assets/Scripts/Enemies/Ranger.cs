using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ranger : Enemy
{
    private Transform target;
    public float fleeDistance = 3f;
    //Rigidbody for the anklebiter's melee attack
    public GameObject rangedAttack;

    // Start is called before the first frame update
    void Start()
    {
        //Find the target (the player)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Attack ranged = new Attack(); //Create a melee attack
        ranged.attackCollider = rangedAttack; //Set collider template
        ranged.damage_type = "piercing"; //Reflects the damage being a bowshot
        ranged.damage_modifier = 3;
        ranged.shape = "directional";
        ranged.special = true;
        speed = 1;
    }

    // Update is called once per frame
    void CombatMovement()
    {

        //find distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        Vector2 moveDirection = transform.position - target.transform.position;

        //if player is close run
        if (distanceToPlayer < fleeDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection * -1, speed * Time.deltaTime * 2);
        }
        else if (Mathf.Abs(target.position.x - transform.position.x) < Mathf.Abs(target.position.y - transform.position.y))
        {
            if (target.position.x < transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, Vector2.left, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Vector2.right, speed * Time.deltaTime);
            }
        }
        else 
        {
            if (target.position.y < transform.position.y)
            {
                transform.position = Vector2.MoveTowards(transform.position, Vector2.down, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Vector2.up, speed * Time.deltaTime);
            }
        }
    }
}
