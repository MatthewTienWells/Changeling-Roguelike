using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHammer : ItemCollider
{
    // Start is called before the first frame update
    void Start()
    {
        //Set stats
        weapon = true;
        attack = new Attack();
        attack.damage_type = "bludgeoning";
        attack.radius = 2;
        attack.duration = 1000;
        attack.attackCollider = attackCollider;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
