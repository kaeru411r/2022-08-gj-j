using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyController : EnemyBase
{
    [SerializeField] Transform _player;
    [SerializeField] int _moveSpeed;
    [SerializeField] float _setArea;
    Vector3 _enemyPosition;
    [SerializeField] float _attackArea;
    private void Start()
    {
        _enemyPosition = transform.position;
        base.Start();
    }
    public override void EnemyUpdate()
    {
        //Enemyのローカルスケールを変数に代入
        var EnemyLocalScale = transform.localScale;
        //プレイヤーがエネミーより左側に行ったらローカルスケールを変更する
        if(_player.position.x > transform.position.x)
        {
            EnemyLocalScale.x = -1;
        }
        //プレイヤーがエネミーより右側に行ったらローカルスケールを変更する
        else
        {
            EnemyLocalScale.x = 1;
        }
        transform.localScale = EnemyLocalScale;
        float dir = Vector2.Distance(_player.position, transform.position);
        if (dir <= _attackArea)
        {
            Rb.velocity = (_player.position - transform.position).normalized * _moveSpeed;
        }
        else
        {
            if (_enemyPosition.x > transform.position.x)
            {
                EnemyLocalScale.x = -1;
            }
            //プレイヤーがエネミーより右側に行ったらローカルスケールを変更する
            else
            {
                EnemyLocalScale.x = 1;
            }
            float setPosition = Vector2.Distance(_enemyPosition, transform.position);
            if(setPosition >= _setArea)
            {
                Rb.velocity = (_enemyPosition - transform.position).normalized * _moveSpeed;
            }
        }
    }
}
