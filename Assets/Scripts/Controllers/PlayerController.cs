using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameObject for spawning player at the beginning of game
    [SerializeField]
    private GameObject playerPrefab;
    //store player combatant to pass inputs to
    public PlayerCombatant player;
    //store movement data
    private Vector2 movement;
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
        if (!player)
        {
            GameObject newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            player = newPlayer.GetComponent<PlayerCombatant>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (UIManager.isPaused == false)
            {
                //set movement x and y values to appropriate axis
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");
                player.SetAnimations(movement);
                player.Move(movement);

                if (Input.GetButtonDown("Attack"))
                {
                    player.attacks[loadedAttack].TriggerAttack();
                }

                if (Input.GetButtonDown("SwitchAttacks"))
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
    }
}
