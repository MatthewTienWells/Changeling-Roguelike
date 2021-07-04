using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Collidable rigidbody for the attack
    public Rigidbody attackCollider;
    
    //Duplicate rigidbody that is cloned and then destroyed when the attack is used
    private Rigidbody duplicateBody;
    //Damage type dealt by this attack- piercing, slashing, bludgeoning, heat
    public string damage_type = "piercing";

    //Shape the attack is effective within- melee, directional line, or area
    public string shape = "melee";

    //Multiplier for damage applied to the parent's strength
    public int damage_modifier = 1;

    //Length of time in game ticks the attack is active
    public int duration = 100;

    //Speed the attack moves if it's a directional line
    public float speed = 10;

    //Radius of the attack if it's a melee or area attack
    public double radius = 1;

    //Range at which area of effect attacks appear
    public float range = 1;

    //Entity which called this attack
    public Combatant parent;

    //isActive determines whether the attack is ticking
    public bool isActive = false;

    //Current age of the attack- when this is equal to the duration, the attack is destroyed
    private int age = 0;

    //Function returns if an attack will hit a given target from a given location
    public bool canHit(float pawnLocationX, float selfLocationX, float pawnLocationY, float selfLocationY)
    {
        if (shape == "melee" && 
            Mathf.Abs(pawnLocationX - selfLocationX)*Mathf.Abs(pawnLocationX - selfLocationX) 
            + Mathf.Abs(pawnLocationY - selfLocationY)*Mathf.Abs(pawnLocationY - selfLocationY) 
            <= radius*radius) //If this attack is melee and the target is within the radius of the attack, return true
        {
            return true;
        }
        else if (shape == "directional" &&
            (Mathf.Abs(pawnLocationX - selfLocationX) < 1 || Mathf.Abs(pawnLocationY - selfLocationY) < 1))
            //If this attack is directional and the target is lined up with the attacker, return true
        {
            return true;
        }
        else if (shape == "area" &&
            Mathf.Abs(pawnLocationX - selfLocationX)*Mathf.Abs(pawnLocationX - selfLocationX) 
            + Mathf.Abs(pawnLocationY - selfLocationY)*Mathf.Abs(pawnLocationY - selfLocationY) 
            <= (radius+range*radius+range) &&
            Mathf.Abs(pawnLocationX - selfLocationX)*Mathf.Abs(pawnLocationX - selfLocationX) 
            + Mathf.Abs(pawnLocationY - selfLocationY)*Mathf.Abs(pawnLocationY - selfLocationY) 
            >= (range-radius*range-radius) //If the attack is area, and the target is within one radius of its range, return true
            )
            {
                return true;
            }
        else //In all other cases, return false
        {
            return false;
        }
    }

    //When called, instantiates a new rigidbody to allow for collisions
    public void TriggerAttack()
    {
        duplicateBody = Instantiate(attackCollider, parent.transform.position, parent.transform.rotation);
        if (shape == "area")
        {
            transform.position += transform.forward * range;
        }
        else if (shape == "directional")
        {
            duplicateBody.velocity = transform.forward*speed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            age += 1;
        }
        if (age > duration)
        {
            Destroy(duplicateBody);
            duplicateBody = new Rigidbody();
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        Collider other = collision.collider;
        if (other.gameObject.tag == "Combatant" && other.gameObject != parent)
        {
            Combatant target = other.GetComponent<Combatant>();
            if (target.resistances.Contains(damage_type) && damage_modifier * parent.base_attack > target.defense)
            {
                target.health -= damage_modifier * parent.base_attack - target.defense;
                target.TakeDamage();
            }
            else
            {
                target.health -= damage_modifier * parent.base_attack;
                target.TakeDamage();
            }
            age = duration;
        }
    }
}
