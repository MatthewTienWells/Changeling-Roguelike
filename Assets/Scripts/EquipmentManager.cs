// EquipmentManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentManager : MonoBehaviour {
    #region Singleton 
public static EquipmentManager instance;

void Awake()
{

    instance = this;
}

#endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged (Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    //Iventory inventory;
    // this starts the inventory instance 
    void Start()
    {
        //inventory = inventory.instace;


         int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
         currentEquipment = new Equipment[numSlots];
    }
    // this adddes any times you pick up to the enventory array list 
    public void equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null ) 
        {// add unequiped item back to inventory 
            oldItem = currentEquipment[slotIndex];
            //inventory.Add(oldItem);
        }
        if (onEquipmentChanged != null )
        {// item switch say for a helmet switch old item with new item 
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void RemoveFromInventory() 
    {
        //RemoveFromInventory.instance.remove(this);
    }
        // the fuction for automaticly unequiping items 
    public void Unequip (int slotIndex)
    {// slot index is calling the array that the items sit in 
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            //inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null )
        {
            onEquipmentChanged.Invoke(null, oldItem);
        }
        }
    }
    // for Unequiping the items 
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        
    }
// to update the inventory 
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U))
        UnequipAll();
    }

}
