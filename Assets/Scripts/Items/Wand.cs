using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : ItemCollider
{
    // Start is called before the first frame update
    void Start()
    {
        //Set stats
        weapon = true;
        attack = new Attack();
        attack.damage_type = "heat";
        attack.damage_modifier = 3;
        attack.range = 4;
        attack.radius = 2;
        attack.duration = 3000;
        attack.attackCollider = attackCollider;
        attack.tier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
