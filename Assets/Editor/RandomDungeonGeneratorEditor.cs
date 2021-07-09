using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//make the custom editor attribute work for all child classes of abstract dungeon generator
[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator; //reference for our dungeon generator

    //fill our reference on awake
    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //if the create dungeon button is pressed
        if (GUILayout.Button("Create Dungeon")) 
        {
            //generate dungeon
            generator.GenerateDungeon();
        }
    }
}
