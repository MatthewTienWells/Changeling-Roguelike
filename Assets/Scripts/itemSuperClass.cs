using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fineName = "New Equipment" , menuName = "inventory/Equipment")]

public class Equipment {

public EquipmentSlot EquipSlot;

public int armor;
public int weapon; 

public int flask;

//public override void use()
//{
//    base.use();
//    //equip item 
//    EquipmentManager.instance.equip(this);

//    // remove from inventory
//    RemoveFromInventory();
//}


}

public enum EquipmentSlot {Head, chest, legs, Weapon, feet, pocket}
