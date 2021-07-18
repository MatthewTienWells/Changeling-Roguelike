using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGStarter : MonoBehaviour
{
    [SerializeField]
    private AbstractDungeonGenerator generator;
    [SerializeField]
    private float timer = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        generator = GameObject.FindGameObjectWithTag("PCGenerator").GetComponent<RoomFirstDungeonGenerator>();
    }

    void Start()
    {
        generator.GenerateDungeon();
    }

    //will be used later, deadline for class has been reached tho
    IEnumerator StartGenerating() 
    {
        yield return new WaitForSeconds(timer);
        generator.GenerateDungeon();
    }
}
