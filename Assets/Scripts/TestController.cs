using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public Pawn pawn;
    //store movement data
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = Vector2.Lerp(transform.position, Input.mousePosition, 1);
    }

    private void FixedUpdate()
    {
        pawn.Move(movement);
    }
}
