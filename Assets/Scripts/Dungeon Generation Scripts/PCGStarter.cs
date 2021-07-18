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
        StartCoroutine(StartGenerating());
    }

    IEnumerator StartGenerating() 
    {
        yield return new WaitForSeconds(timer);
        generator.GenerateDungeon();
    }
}
