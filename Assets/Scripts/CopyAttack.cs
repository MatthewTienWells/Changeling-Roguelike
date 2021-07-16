using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAttack : Attack
{

    // Start is called before the first frame update
    void Start()
    {
        damage_type = "copy";
        range = 3;
        radius = 1;
        damage_modifier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Trigger the parents Copy ability on a foe that collides with the attack
    private void OnCollisionEnter (Collision collision)
    {
        Collider other = collision.collider;
        if (other.gameObject.tag == "Combatant" && other.gameObject != parent)
        {
            Combatant target = other.GetComponent<Combatant>();
            parent.Copy(target);
            age = duration;
        }
    }
}
