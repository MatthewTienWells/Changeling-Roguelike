using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public Canvas healthNotification;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange) //if we're in range to interact
        {
            if (Input.GetKey(interactKey)) //and Player presses key
            {
                interactAction.Invoke(); //activate event
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isInRange = true;
            //GameManager.instance.NotifyPlayer();
            healthNotification.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            //GameManager.instance.DeNotifyPlayer();
            healthNotification.enabled = false;
        }
    }
}
