using System;
using System.Collections;
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
        
        StartCoroutine(BalloonFade());
    }

    private void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.y > 1f)
        {
            GameManager.Instance.DecreaseScore();
            DestroyBalloon();
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0) return;
        
        clicksToPop--;
        Debug.Log($"Pressed");

        if (clicksToPop <= 0)
        {
            AudioManager.Instance.PlayPopSound();
            GameManager.Instance.IncreaseScore();
            DestroyBalloon();
            return;
        }
        
        AudioManager.Instance.PlayFillSound();

        StartCoroutine(IncreaseScale());
    }

    private IEnumerator IncreaseScale()
    {
        float originalScale = transform.localScale.x;

        for (float scale = transform.localScale.x; scale <= originalScale + 0.2f; scale += 0.01f)
        {
            transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }
    }

    private IEnumerator BalloonFade()
    {
        Color color = meshRenderer.material.color;
        
        yield return new WaitForSeconds(0.5f);

        for (float alpha = 1; alpha >= 0; alpha -= 0.1f)
        {
            color.a = alpha;
            meshRenderer.material.color = color;
            
            yield return new WaitForSeconds(0.25f);
        }

        GameManager.Instance.DecreaseScore();
        DestroyBalloon();
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
    
    public void DestroyBalloon()
    {
        Destroy(gameObject);
    }
}