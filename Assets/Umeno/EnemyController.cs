using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyController: EnemyBase
{
    [SerializeField] Transform _player;
    //[SerializeField] float _attackArea;
    //[SerializeField] float _attackIntarval;
    //[SerializeField] EnemyAttack _bullet;
    //[SerializeField] float _patrolArea;
    //[SerializeField] CircleCollider2D _attackRange;
    //[SerializeField] Transform _patrol;
    //[SerializeField] int _moveSpeed;
    //Transform _patrolPosition;
    //float _timer;
    //bool _retrunPatol = true;
    //private void Start()
    //{
    //    _patrolPosition = _patrol;
    //}
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
        //RB.velocity = (_patrolPosition.position).normalized * _moveSpeed;
        //float dir = Vector2.Distance(_player.position, transform.position);
        //float ptrolDir = Vector2.Distance(_patrolPosition.position, transform.position);
        //if(ptrolDir <= _patrolArea)
        //{
        //    if(_retrunPatol)
        //    {
        //        _playerX = -1;
        //        _patrolPosition = _patrol;
        //    }
        //    else
        //    {
        //        _playerX = 1;
        //        _patrolPosition = _patrol;
        //    }
        //}
        //if (dir <= _attackArea)
        //{
        //    _timer += Time.deltaTime;

        //    if (_timer >= _attackIntarval)
        //    {
        //        StartCoroutine(EnemyAttack());
        //        _timer = 0;
        //    }
        //    //Instantiate(_bullet, transform.position, transform.rotation);
        //    //transform.Translate(_player - transform.position);
        //}   
    }
    //IEnumerator EnemyAttack()
    //{
    //    _attackRange.enabled = true;
    //    yield return new WaitForSeconds(0.1f);
    //    _attackRange.enabled = false;
    //}
}
