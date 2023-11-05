using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : OrePool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Ore _prefab;
    [SerializeField] private float _spawnRate;

    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        Initialize(_prefab);
        _waitForSeconds = new WaitForSeconds(_spawnRate);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return _waitForSeconds;

            if (TryGetOre(out Ore ore))
            {
                int randomPoint = Random.Range(0, _spawnPoints.Length);
                SetOre(ore, _spawnPoints[randomPoint].position);
            }
        }
    }

    private void SetOre(Ore ore, Vector3 spawnPosition)
    {
        ore.gameObject.SetActive(true);
        ore.transform.position = spawnPosition;
    }
}
