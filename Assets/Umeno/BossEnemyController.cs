using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _attack;
    [SerializeField] float _attackArea;
    [SerializeField] float _attackIntarval;
    [SerializeField] int _moveSpeed;
    [SerializeField] float _attackStart;
    [SerializeField] float _attackEnd;
    Animator _anim;
    Rigidbody2D _rb2;
    public float _timer;

    private void Start()
    {
        _rb2 = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    public override void EnemyUpdate()
    {
        //base.Start();
        float dir = Vector2.Distance(_player.position, transform.position);
        var EnemyLocalScale = transform.localScale;
        //プレイヤーがエネミーより左側に行ったらローカルスケールを変更する
        if (_player.position.x > transform.position.x)
        {
            EnemyLocalScale.x = -1;
        }
        //プレイヤーがエネミーより右側に行ったらローカルスケールを変更する
        else
        {
            EnemyLocalScale.x = 1;
        }
        transform.localScale = EnemyLocalScale;
        if (dir <= _attackArea)
        {
            _timer += Time.deltaTime;
            _rb2.velocity = (_player.position - transform.position).normalized * _moveSpeed;
            if (_timer >= _attackIntarval)
            {
                StartCoroutine(EnemyAttack());
                _timer = 0;
            }
        }
        IEnumerator EnemyAttack()
        {
            _anim.SetBool("Attack", true);
            yield return new WaitForSeconds(_attackStart);
            _attack.gameObject.SetActive(true);
            yield return new WaitForSeconds(_attackEnd);
            _anim.SetBool("Attack", false);
            _attack.gameObject.SetActive(false);
        }
    }
}
