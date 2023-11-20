//==================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//==================================================================================
public class ExplosionQuiz : MonoBehaviour
{
    //--------------------------------
    //--------------------------------
    [Header("[ ���� �ݰ� ]"), SerializeField]
    float _expRadius = 20f;
    [Header("[ ���� ���߷� ]"), SerializeField]
    float _widthExpPower = 600f;
    [Header("[ ������ ���߷� ]"), SerializeField]
    float _upwardExpPower = 500f;
    //--------------------------------
    //  ���� ȿ�� ������
    public GameObject _expEffect;
    //--------------------------------
    Rigidbody _rBody;
    //--------------------------------
    private void Start() { _rBody = GetComponent<Rigidbody>(); }
    //--------------------------------
    void Update()
    {

    }// void Update()
    //--------------------------------
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(_expEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.5f);

        Vector3 expPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(expPos, _expRadius, 1 << 9);

        foreach (var coll in colliders)
        {
            var rgdBody = coll.GetComponent<Rigidbody>();

            rgdBody.mass = 5.0f;

            rgdBody.AddExplosionForce(_widthExpPower, expPos, _expRadius, _upwardExpPower);

            Destroy(coll.gameObject, 0.5f);

        }//	foreach (var coll in colliders)

    }// void OnCollisionEnter(Collision collision)
    //--------------------------------
}
//==================================================================================