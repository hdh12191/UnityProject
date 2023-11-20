using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletController : MonoBehaviour
{
    [Header("[ ���� �ݰ� ]"), SerializeField]
    float _expRadius = 20f;
    [Header("[ ���� ���߷� ]"), SerializeField]
    float _widthExpPower = 600f;
    [Header("[ ������ ���߷� ]"), SerializeField]
    float _upwardExpPower = 500f;
    //--------------------------------
    //  ���� ȿ�� ������
    public GameObject _expEffect;
    void Start() 
    {

    }
    //--------------------------------
    void Update()
    {


    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(_expEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);

        Vector3 expPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(expPos, _expRadius, 1 << 8);

        foreach (var coll in colliders)
        {
            var rgdBody = coll.GetComponent<Rigidbody>();

            rgdBody.mass = 1.0f;

            rgdBody.AddExplosionForce(_widthExpPower, expPos, _expRadius, _upwardExpPower);
            Destroy(coll);
            Destroy(coll.gameObject, 5f);
        }

    }
}
