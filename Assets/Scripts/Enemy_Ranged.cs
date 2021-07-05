using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : Enemy
{
    private Transform target;
    public float minAttackDistance = 1f;
    public float runDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Find the target (the player)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //find distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        Vector3 moveDirection = transform.position - target.transform.position;

        //if player is close run
        if (distanceToPlayer < runDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed * Time.deltaTime);
        }
        else
        {
            speed = 1;
        }
    }
}
