using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject BallBasketPrefab;
   // private ShapeGenrator shapeGenrator;
    public float DesiredXpos = 1.89f;
    public float spacing = 0.6f;

    [Header("Bet Spawner")]
    public float minX;
    public float maxX;
    public float spawnInterval;

    public Toggle Auto, Manual;

    public Button PlayButton;
    public void StartSpawning()
    {
        if (AmountManager.instance.Canplay)
        {
            if (Auto.isOn && !Manual.isOn)
            {
                Debug.Log(Auto.isOn);
                StartCoroutine(SpawnCoroutine());
            }
            else if (Manual.isOn && !Auto.isOn)
            {
                Debug.Log(Manual.isOn);
                float randomX = Random.Range(minX, maxX);
                GameObject projectile = Objectpool.poolSharedInstance.GetpoolObject();
                if (projectile != null)
                {
                    projectile.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
                    projectile.transform.SetParent(transform);
                }
                PlayButton.interactable = false;
                StartCoroutine(SpawnManualCoroutine());
            }
        }

        if (AmountManager.instance.Broke)
        {
            PlayButton.interactable = false;
        }
        else
        {
            PlayButton.interactable = true;
        }

    }

 
    private IEnumerator SpawnManualCoroutine()
    {
   
            yield return new WaitForSeconds(spawnInterval);
        PlayButton.interactable = true;

    }
    private IEnumerator SpawnCoroutine()
    {
     
            while (true && Auto.isOn && !Manual.isOn)
            {
              
                // Calculate a random position within the given X range
                float randomX = Random.Range(minX, maxX);

                // Instantiate the objectPrefab at the calculated position

                GameObject projectile = Objectpool.poolSharedInstance.GetpoolObject();
                if (projectile != null)
                {
                    projectile.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
                    projectile.transform.SetParent(transform);
                }

                AmountManager.instance.CurrentAmount -= AmountManager.instance.BetAmount;
                // Wait for the spawnInterval before spawning the next object
                yield return new WaitForSeconds(spawnInterval);
              
            }
      
      
    }
}
