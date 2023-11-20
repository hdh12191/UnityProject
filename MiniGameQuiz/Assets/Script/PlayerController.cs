using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Text _text;
    [SerializeField]
    Text _result;
    [SerializeField]
    Image _image;
    Vector3 _startPos, _endPos;
    float _dist;
    float _time = 30f;
    void Start()
    {
        _image.enabled = false;
        _result.enabled = false;
    }
    void Update()
    {
        _time -= Time.deltaTime;
        if (_time > 0f)
        {
            int time = (int)_time;
            _text.text = time.ToString();
        }
        else if (_time < 0f)
        {
            _image.enabled = true;
            _result.enabled = true;
            _result.text = "GameClear";
            Time.timeScale = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            Debug.Log(_startPos);
        }
        if(Input.GetMouseButtonUp(0))
        {
            _endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            Debug.Log(_endPos);
            _dist = Vector3.Distance(_startPos, _endPos);
            Debug.Log(_dist);
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.position = new Vector3(0f, 1.5f, -7f);
            Vector3 dir = _endPos - _startPos;
            dir.Normalize();
            bullet.GetComponent<Rigidbody>().AddForce(dir * _dist * 500);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Monster"))
        {
            _image.enabled = true;
            _result.enabled = true;
            _result.text = "GameOver";
            Time.timeScale = 0;
        }
    }
}
