using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _monPrefab;
    float _spawnRateMin = 1f;
    float _spawnRateMax = 3f;
    float _spawnRate;
    float _timeAfterSpawn;
    Vector3 pos;

    void Start()
    {
        _timeAfterSpawn = 0f;
        //pos = new Vector3(Random.Range(-9f, 9f), 0f, 22f);
        _spawnRate = Random.Range(_spawnRateMin, _spawnRateMax);
    }
    void CreateEnemy()
    {
        _timeAfterSpawn += Time.deltaTime;
        if (_timeAfterSpawn >= _spawnRate)
        {
            GameObject SpawnMon = Instantiate(_monPrefab);
            pos = new Vector3(Random.Range(-9f, 9f), 0f, 22f);
            SpawnMon.transform.position = pos;
            _timeAfterSpawn = 0f;
            _spawnRate = Random.Range(_spawnRateMin, _spawnRateMax);
        }
    }
    void Update()
    {
        CreateEnemy();
    }
}
