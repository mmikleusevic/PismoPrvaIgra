using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool shouldRotate = true;
    
    private void Update()
    {
        if (!shouldRotate) return;
        
        transform.Rotate(0,180 * Time.deltaTime,0);       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            shouldRotate = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            shouldRotate = true;
        }
    }
}
