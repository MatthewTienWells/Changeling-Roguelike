using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : ItemCollider
{
    // Start is called before the first frame update
    void Start()
    {
        //Set stats
        weapon = true;
        attack = new Attack();
        attack.damage_type = "slashing";
        attack.damage_modifier = 2;
        attack.duration = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
