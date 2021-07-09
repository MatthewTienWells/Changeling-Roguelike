using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    //number of iterations our generator goes through to generate our dungeon
    public int iterations = 10, walkLength = 10; //number of spaces our generator "walks" before it is done
    public bool startRandomlyEachIteration = true; //boolean for starting at a random position each iteration
}
