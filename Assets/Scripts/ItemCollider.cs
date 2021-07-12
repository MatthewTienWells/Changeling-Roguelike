using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    //Boolean representing whether this item is a healing item or a weapon
    public bool weapon = false;

    //Amount of health that should be recovered if this item is a healing item
    public int recovery = 5;
    //Attack that this item unlocks if it is a weapon
    public Attack attack;
    //Collider for attack if this item is a weapon
    public Rigidbody attackCollider;

    //When the player picks up the item
    private void OnCollisionEnter (Collision collision)
    {
        Collider other = collision.collider; //Get the triggering entity
        if (other.gameObject.tag == "Player") //If the entity was the player
        {
            Combatant player = other.GetComponent<Combatant>();
            if (weapon) //If this item is a weapon
            {
                bool attackFound = false; //Flag to see if this item's attack is known by the player
                foreach (Attack curAttack in player.attacks) //Iterate through the player's known attacks to see if this is a direct upgrade
                {
                    if (attack.shape == curAttack.shape && attack.damage_type == curAttack.damage_type) //If the attack is already known
                    {
                        if (attack.tier > curAttack.tier) //If this item upgrades the attack
                        {
                            int index = player.attacks.IndexOf(curAttack); //Get the position of the attack
                            player.attacks.Remove(curAttack); //Delete the current version of the attack
                            player.attacks.Insert(index, attack); //Insert the new version of the attack in the same slot
                        }
                        attackFound = true; //Mark the attack as already present in the player's controls
                    }
                }
                if (!attackFound) //If the player doesn't have access to the attack
                {
                    player.attacks.Add(attack); //Add the attack to the player's options
                }
            }
            else //If this item is a healing item
            {
                //Increase player's health either by the recovery value or to max, whichever is less.
                player.health = Mathf.Min(player.health + recovery, player.max_health);
            }
            //Despawn the item after the player picks it up
            Destroy(this.gameObject);
        }
    }
}
