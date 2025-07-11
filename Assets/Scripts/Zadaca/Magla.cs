using System;
using UnityEngine;

public class Magla : MonoBehaviour
{
    [SerializeField] private float speedReductionFactor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.ReduceSpeed(speedReductionFactor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.ResetSpeed();
        }
    }
}
