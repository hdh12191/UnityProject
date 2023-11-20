using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[Header("플레이어"), SerializeField]
    //GameObject _player;
    [Header("최소 속도"), SerializeField]
    float _minSpeed;
    [Header("최대 속도"), SerializeField]
    float _maxSpeed;
    // Start is called before the first frame update
    GameObject player;
    float speed;
    float timeLimmit;
    Condition con;
    void Start()
    {
        con = new Condition();
        player = GameObject.FindWithTag("Player");
        speed = Random.Range(_minSpeed, _maxSpeed) + con.ReturnSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(con.ReturnFlag() == false && con.ReturnWinFlag() != true)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            transform.position += dir * (speed) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && con.ReturnWinFlag() != true)
        {
            
            con.ConditionChange(true);
        }
    }
}
