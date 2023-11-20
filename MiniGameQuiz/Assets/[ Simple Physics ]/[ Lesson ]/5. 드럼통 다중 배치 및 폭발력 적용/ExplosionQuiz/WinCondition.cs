using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [Header("�¸� �ð�"), SerializeField]
    float winTime;
    [Header("�߰� �ӵ�"), SerializeField]
    float _addSpeed;
    [Header("�߰� �ӵ� ���� �ð�"), SerializeField]
    float _addTime;
    [Header("�� �߰� ���� ��"), SerializeField]
    int _enemyCreateCount;
    [Header("�� �߰� ���� �ð�"), SerializeField]
    float _enemyCreateTime;
    [Header("ī��Ʈ �ٿ�"), SerializeField]
    float _countDown;
    Text _condition, _timer, _startTimer;
    Condition con;
    float addTime;
    float totalTime;
    float enemyCreateTime;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        con = new Condition();
        _condition = GameObject.Find("Condition").GetComponent<Text>();
        _timer = GameObject.Find("Timer").GetComponent<Text>();
        _startTimer = GameObject.Find("StartTimer").GetComponent<Text>();
        addTime = _addTime;
        totalTime = winTime;
        enemyCreateTime = _enemyCreateTime;
        con.SetEnemyCount(_enemyCreateCount);
        countDown = _countDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (con.ReturnStartFlag() == false)
        {
            countDown -= Time.deltaTime;
            _startTimer.text = Mathf.Round(countDown).ToString();
            con.SetStartTime(countDown);
            if (Mathf.Round(con.ReturnStartTime()) == 0)
            {
                con.SetStartFlag(true);
            }
        }
        if (con.ReturnStartFlag() == true)
        {
            _startTimer.text = string.Empty;
            con.SetTime(Time.deltaTime);
            if (con.ReturnTime() >= winTime && con.ReturnFlag() == false)
            {
                _condition.text = "Win!";
                con.SetWinFlag(true);
            }
            else if (con.ReturnFlag() == true && con.ReturnWinFlag() != true)
            {
                _condition.text = "False~~~";
            }

            if (con.ReturnTime() >= addTime)
            {
                con.SetSpeed(_addSpeed);
                addTime += _addTime;
            }

            if (con.ReturnTime() >= enemyCreateTime)
            {
                con.SetEnemyCount(_enemyCreateCount);
                enemyCreateTime += _enemyCreateTime;
            }
            totalTime -= Time.deltaTime;
            _timer.text = "���� �ð� : " + Mathf.Round(totalTime);
        }
    }

}
