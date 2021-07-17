using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Components")]
    //for pawn's animator
    public Animator anim;
    //for pawn's Rigidbody2D
    public Rigidbody2D rb;
    //game object's sprite renderer
    public SpriteRenderer sr;
    //player controller just to get animation to work for now
    public TestController pc;

    [Header("Pawn Stats")]
    //for pawn speed
    public float moveSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //get components of game object
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<TestController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        anim.SetFloat("Horizontal", pc.movement.x); //pass x to animator horizontal
        anim.SetFloat("Vertical", pc.movement.y); //pass y to animator vertical
        anim.SetFloat("Speed", pc.movement.sqrMagnitude); //pass magnitude to animator
    }

    public void Move(Vector2 direction)
    {
        //move the rigidbody by Vector 2 multiplied by speed.
        rb.velocity = new Vector2(direction.x * moveSpeed * Time.fixedDeltaTime, direction.y * moveSpeed * Time.fixedDeltaTime);
    }
}
