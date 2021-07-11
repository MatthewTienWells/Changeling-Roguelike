using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    // createing a table/array to hold the weighted value
    // item 1 2 and 3 have a %60, %40, %10 chance of droping item index is asociated with hight to lower chance of drop 
    public List<GameObject> lights;
    public int[] table = {
        60, // A item
        30, // B item
        10  // C item
         };
    
    public int total;
    public int randomNumber;
    private void start()
    {
        foreach(var item in table)
        {
            total += item;
        }
        randomNumber = Random.Range(0, total);
        
        for(int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                lights[i].setActive(true);
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }


       
    }
