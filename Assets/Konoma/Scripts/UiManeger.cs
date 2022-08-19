using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManeger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _LifeText;
    [SerializeField] TextMeshProUGUI _LimitText;

    [SerializeField] GameObject _Boss;

    [SerializeField] Image _upperlimit;

    [SerializeField] float _Limit;

    [SerializeField] float _limitCount;

    [SerializeField] float _limitCountup;
    [SerializeField] List<Image> _limitImage = new List<Image>();

    private int _life;
    public void Start()
    {
        EnemyBase enemyBase = FindObjectOfType<EnemyBase>();
        
        _life = enemyBase.HP;
        _LifeText.text = "Å~" + _life;

        Debug.Log(_life);

        PlayerController player = FindObjectOfType<PlayerController>();

        _Limit = player._max;
        _limitCount = player._max -1;
        _limitCountup = player._max + 1;

    }
    void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        _Limit = player._max;

        if (EnemyBase.OnDamage != null)
        {
            _LifeText.text = "Å~" + _life;
        }

        if (_Limit == _limitCount)
        {
           _LimitText.text = ""  + _Limit;
            _limitCount = - 1;
        }
        else if (_Limit == _limitCountup)
        {
            _LimitText.text = "" + _Limit;
            _limitCountup = +1;
        }
    }
}