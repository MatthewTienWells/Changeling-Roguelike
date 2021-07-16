using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for adding drop weights to items being dropp
[System.Serializable]
public class itemDrop 
{
    public GameObject dropItem;
    public int weight;

}
public class DropManager : MonoBehaviour
{
    public List<itemDrop> drops; //list of drops
    private List<int> dropArray; //CDF Array
    [SerializeField]
    private Vector3 offset; //offset for item spawning cause they were spawning in the ground

    // Start is called before the first frame update
    void Start()
    {
        dropArray = new List<int>();
        for (int i = 0; i < drops.Count; i++) 
        {

            //set its drop array value
            //if its not your first time adding weight
            if (i > 0)
            {
                //add the previous weights and the new one to get total
                dropArray.Add(drops[i].weight + dropArray[i - 1]);
            }
            //otherwise if it is your first time
            else 
            {
                //add your current weight to get total
                dropArray.Add(drops[i].weight);
            }
        }
    }

    public void RandomItemDrop() 
    {
        //get a random number less than total of dropArray
        int rand = Random.Range(0, dropArray[dropArray.Count - 1]);
        //find the index the random number is in
        int selectedIndex = System.Array.BinarySearch(dropArray.ToArray(), rand);
        //binary search will tell where that number is only if it gets it EXACTLY, otherwise we get a bitwise or
        if (selectedIndex < 0) 
        {
            selectedIndex = ~selectedIndex;
        }
        //instantiate the item at the selected index
        Instantiate(drops[selectedIndex].dropItem, transform.position + offset, transform.rotation);
    }
}
