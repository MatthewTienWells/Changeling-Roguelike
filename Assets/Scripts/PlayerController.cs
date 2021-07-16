using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCombatant player;
    //store movement data
    public Vector2 movement;
    //Index of the attack the player is using currently
    private int loadedAttack = 0;
    //Index of secondary attack to swap to
    private int secondAttack = 1;
    //Copies the characteristics of the enemy provided
    void ResetAttacks()
    {
        if (player.attacks.Count > loadedAttack)
        {
            loadedAttack = player.attacks.Count;
        }
        if (player.attacks.Count > secondAttack)
        {
            secondAttack = player.attacks.Count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = new PlayerCombatant();
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
            player.attacks[loadedAttack].TriggerAttack();
        }

        if (Input.GetKeyDown("Fire2"))
        {
            int swap = loadedAttack;
            loadedAttack = secondAttack;
            secondAttack = swap;
        }

        if (Input.mouseScrollDelta.y > 0 && loadedAttack < player.attacks.Count)
        {
            loadedAttack += 1;
        }

        if (Input.mouseScrollDelta.y < 0 && loadedAttack > 0)
        {
            loadedAttack -= 1;
        }
    }
}
