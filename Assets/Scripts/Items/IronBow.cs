using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBow : ItemCollider
{
    // Start is called before the first frame update
    void Start()
    {
        //Set stats
        weapon = true;
        attack = new Attack();
        attack.damage_type = "piercing";
        attack.damage_modifier = 2;
        attack.shape = "directional";
        attack.duration = 10000;
        attack.attackCollider = attackCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
