using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] Vector3 _player;
    [SerializeField] int _moveSpeed;
    //[SerializeField] EnemyAttack _bullet;
    [SerializeField] float _attackArea;
    [SerializeField] float _attackIntarval;
    [SerializeField] CircleCollider2D _attackRange;
    float _timer;
    public override void EnemyUpdate()
    {
        float dir = Vector2.Distance(_player, transform.position);
        if (dir <= _attackArea)
        {
            _timer += Time.deltaTime;
            if (_timer >= _attackIntarval)
            {
                transform.LookAt(_player);
                _attackRange.enabled = true;
            }
            //Instantiate(_bullet, transform.position, transform.rotation);
            //transform.Translate(_player - transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
