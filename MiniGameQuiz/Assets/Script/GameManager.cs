using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _objs;
    [SerializeField]
    Text _text;
    float _time = 3f;
    void Start()
    {
        for (int i = 0; i < _objs.Length; i++)
        {
            _objs[i].SetActive(false);
        }
    }

    void Update()
    {
        _time -= Time.deltaTime;
        _text.text = ((int)_time).ToString();
        if(_time < 0f)
        {
            _text.enabled = false;
            for (int i = 0; i < _objs.Length; i++)
            {
                _objs[i].SetActive(true);
            }
        }
    }
}
