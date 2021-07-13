using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : Combatant
{
    //Copies the characteristics of the enemy provided
    void Copy(Combatant target)
    {
        max_health = target.max_health;
        speed = target.speed;
        defense = target.defense;
        resistances = target.resistances;
        base_attack = target.base_attack;
        cooldown = target.cooldown;
        foreach (Attack curAttack in attacks)
        {
            if (curAttack.special)
            {
                attacks.Remove(curAttack);
            }
        }
        attacks.AddRange(target.attacks);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
