using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ÆøÅº ÇÁ¸®ÆÕ"), SerializeField]
    GameObject _boom;
    [Header("´øÁö´Â À§Ä¡"), SerializeField]
    Transform _boomPosition;
    Camera mainCamera;
    bool clickChk = false;
    Vector3 startPoint, endPoint;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                startPoint = hit.point;
            }
            //Debug.Log(startPoint.ToString());
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                endPoint = hit.point;
            }
            //Debug.Log(endPoint.ToString());
            clickChk = true;
        }

        if (clickChk)
        {
            Vector3 dir = (endPoint - startPoint).normalized;
            float dis = Vector3.Distance(startPoint, endPoint);
            GameObject boom = Instantiate(_boom, _boomPosition.position, Quaternion.identity);
            boom.GetComponent<Rigidbody>().AddForce(dir * (dis * 100));
            clickChk = false;
        }
    }
}
