using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("利 橇府普"), SerializeField]
    GameObject _enemy;
    [Header("利 积己 弥家 困摹"), SerializeField]
    Vector3 _minTransform;
    [Header("利 积己 弥措 困摹"), SerializeField]
    Vector3 _maxTransform;
    [Header("利 积己 弥家 矫埃"), SerializeField]
    float _minSpawnTime;
    [Header("利 积己 弥措 矫埃"), SerializeField]
    float _maxSpawnTime;
    
    Vector3 spawnTransform;
    float spawnTime, timeDelta;
    Condition con;
    // Start is called before the first frame update
    void Start()
    {
        con = new Condition();
    }

    // Update is called once per frame
    void Update()
    {
        if (con.ReturnStartFlag() == true)
        {
            timeDelta += Time.deltaTime;
            if (timeDelta >= spawnTime && con.ReturnWinFlag() != true && con.ReturnFlag() != true)
            {
                for (int i = 0; i < con.ReturnEnemyCount(); i++)
                {
                    spawnTransform = new Vector3(
                    RandomVector(_minTransform.x, _maxTransform.x),
                    1.5f
                , RandomVector(_minTransform.z, _maxTransform.z));
                    spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
                    Instantiate(_enemy, spawnTransform, Quaternion.identity);

                }
                timeDelta = 0;
            }
        }
            
    }

    float RandomVector(float min, float max)
    {
        float f = Random.Range(min, max);
        while(true)
        {
            if (f < 8.0f && f > -8.0f)
            {
                f = Random.Range(min, max);
                
            }
            else
            {
                break;
            }
        }

        return f;
    }
}
