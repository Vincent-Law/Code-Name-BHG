using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnrate;
    [SerializeField] private GameObject[] reaperPrefab;
    [SerializeField] private bool canSpawn = true;

    private void Start() 
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        
    }
}
