using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    //player pawn
    public Pawn pawn;
    //store movement data
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        //set movement x and y values to appropriate axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (pawn == null) 
        {
            GameObject _player = Instantiate<GameObject>(player, transform.position, Quaternion.identity);
            pawn = _player.GetComponent<Pawn>();
        }
    }

    private void FixedUpdate()
    {
        pawn.Move(movement);
    }
}
