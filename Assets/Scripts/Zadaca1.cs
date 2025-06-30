using System.Collections;
using UnityEngine;

public class Zadaca1 : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    private bool isPoweredUp;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPoweredUp)
        {
            StartCoroutine(GetPowerUp());
        }       
    }

    private IEnumerator GetPowerUp()
    {
        isPoweredUp = true;
        
        GameObject powerUp = Instantiate(powerUpPrefab,
            new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        
        yield return new WaitForSeconds(5);
        Destroy(powerUp);
        
        isPoweredUp = false;
    }
}
