using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    [SerializeField] private int clicksToPop;
    [SerializeField] private Material[] materials;

    private MeshRenderer meshRenderer;
    private Rigidbody rigidbody;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetRandomBalloonDamping();
    }

    private void OnMouseDown()
    {
        clicksToPop--;
        Debug.Log($"Pressed");

        if (clicksToPop <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void ChangeMaterial()
    {
        int index = RandomGeneratedNumber.RandomNumber(materials.Length);
        meshRenderer.material = materials[index];
    }

    private void SetRandomBalloonDamping()
    {
        rigidbody.linearDamping = Random.Range(0, 9.81f);
    }
}