using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("技能")]
    [SerializeField] EnemyType _type;
    [Tooltip("HP初期値")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("敵状態での攻撃のクールタイム")]
    [SerializeField] float _attackCoolTime = 1;

    /// <summary>HP</summary>
    int _hp;
    /// <summary>勢力</summary>
    EnemyHand _hand = EnemyHand.Enemy;
    /// <summary>リスポーン地点</summary>
    Vector2 _respawnPoint;
    /// <summary>エネミーの状態</summary>
    EnemyState _state;

    /// <summary>技能</summary>
    public EnemyType Type { get => _type; }
    /// <summary>勢力</summary>
    public EnemyHand Hand { get => _hand; }
    /// <summary>HP</summary>
    public int HP { get => _hp;}

    // Start is called before the first frame update
    void Start()
    {
        _respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == EnemyState.Idol)
        {
            Attack();
        }
        else if(_state == EnemyState.Follow)
        {
            FollowPlayer();
        }
        else if(_state == EnemyState.Throw)
        {

        }
    }

    

    /// <summary>
    /// 渡した勢力に移る
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>勢力移動に成功したか</returns>
    virtual public bool JumpSide(EnemyHand hand)
    {
        if(hand != _hand)
        {
            _hand = hand;
            return true;
        }
        return false;
    } 

    void Respawn()
    {
        transform.position = _respawnPoint;
    }

    void FollowPlayer()
    {

    }

    void Hit()
    {
        Respawn();
    }

    abstract public void Attack();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_state == EnemyState.Throw)
        {
            Hit();
        }
    }

}

/// <summary>
/// 敵の勢力
/// </summary>
public enum EnemyHand{
    /// <summary>敵</summary>
    Enemy,
    /// <summary>味方</summary>
    Player,
}

public enum EnemyState
{
    /// <summary>待機</summary>
    Idol,
    /// <summary>追従</summary>
    Follow,
    /// <summary>投げられてる</summary>
    Throw,
}