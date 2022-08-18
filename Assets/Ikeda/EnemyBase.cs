using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("技能")]
    [SerializeField] EnemyType _type;
    [Tooltip("HP初期値")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("攻撃力")]
    [SerializeField] int _atk = 1;
    [Tooltip("味方の時のレイヤー")]
    [SerializeField] LayerMask _friendLayer;
    [Tooltip("敵の時のレイヤー")]
    [SerializeField] LayerMask _enemyLayer;

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
    void Awake()
    {
        _respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == EnemyState.Idol)
        {
            EnemyUpdate();
        }
        else if(_state == EnemyState.Follow)
        {
        }
        else if(_state == EnemyState.Throw)
        {

        }
    }

    private void FixedUpdate()
    {
        if(_state == EnemyState.Idol)
        {
            EnemyFixedUpdate();
        }
        else if(_state == EnemyState.Follow)
        {
            FollowPlayer();
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
            if(hand == EnemyHand.Enemy)
            {
                gameObject.layer = _enemyLayer;
                Respawn();
            }
            else if(hand == EnemyHand.Player)
            {
                gameObject.layer = _friendLayer;
            }
            _hand = hand;
            return true;
        }
        return false;
    } 

    /// <summary>
    /// リスポーンする
    /// </summary>
    void Respawn()
    {
        transform.position = _respawnPoint;
        HPReset();
    }

    /// <summary>
    /// プレイヤーに追従する
    /// </summary>
    void FollowPlayer()
    {
        
    }


    /// <summary>
    /// 敵に当たる
    /// </summary>
    /// <param name="enemy"></param>
    void Hit(EnemyBase enemy)
    {
        enemy.Damage(_atk);
        Respawn();
    }


    /// <summary>
    /// ダメージを食らう
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Death();
        }
    }


    /// <summary>
    /// 死ぬ
    /// </summary>
    void Death()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// HPをリセットする
    /// </summary>
    void HPReset()
    {
        _hp = _maxHp;
    }

    /// <summary>
    /// 敵の行動
    /// </summary>
    virtual public void EnemyUpdate()
    {

    }

    /// <summary>
    /// 敵の行動 FixedUpdate
    /// </summary>
    virtual public void EnemyFixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_state == EnemyState.Throw)
        {
            EnemyBase enemy;
            if(TryGetComponent<EnemyBase>(out enemy))
            {
                Hit(enemy);
            }
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

/// <summary>
/// エネミーの状態
/// </summary>
public enum EnemyState
{
    /// <summary>待機</summary>
    Idol,
    /// <summary>追従</summary>
    Follow,
    /// <summary>投げられてる</summary>
    Throw,
}