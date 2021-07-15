using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Combatant player;
    //store movement data
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        player.SetAnimations(movement);

        if (Input.GetKeyDown("Fire1"))
        {
            //player.TriggerAttack();
        }

        if (Input.GetKeyDown("Fire2"))
        {
            // switch weapons
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            // swtch to next attack
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            //switch to previse attack
        }
    }

    private void FixedUpdate()
    {
        player.CombatMovement(movement);
    }
}
