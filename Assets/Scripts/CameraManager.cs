using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float smoothing = 5f;
    [SerializeField]
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
     
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); 
        }
    }
}
    

