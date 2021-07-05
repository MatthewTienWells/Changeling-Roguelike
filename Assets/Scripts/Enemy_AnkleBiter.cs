using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnkleBiter : Enemy
{
    private Transform target;
    public float minAttackDistance = .5f;

    // Start is called before the first frame update
    void Start()
    {
        //Find the target (the player)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move towards the player at all times.
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //find distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //if close to player do attack
        if (distanceToPlayer < minAttackDistance)
        {
            speed = 0;
        }
        else
        {
            speed = 1;
        }
    }
}
