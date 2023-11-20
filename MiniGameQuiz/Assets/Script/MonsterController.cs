using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    GameObject _target;
    float _speed;
	public float _expRadius = 10.0f;
	void Start()
    {
        _speed = Random.Range(1f, 3f);
        InvokeRepeating("SpeedUp", 1f, 2f);
    }
    void SpeedUp()
    {
        _speed++;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
	}
}
