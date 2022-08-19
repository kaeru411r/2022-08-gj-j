using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Rigidbody2D))]
abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("技能")]
    [SerializeField] EnemyType _type = EnemyType.Non;
    [Tooltip("HP初期値")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("攻撃力")]
    [SerializeField] int _atk = 1;
    [Tooltip("味方の時のレイヤー")]
    [SerializeField] int _friendLayer;
    [Tooltip("敵の時のレイヤー")]
    [SerializeField] int _enemyLayer;
    [Tooltip("追従時の移動速度")]
    [SerializeField] float _followSpeed;
    [Tooltip("プレイヤーにワープする距離")]
    [SerializeField] float _warpDistance = 3f;
    [Tooltip("プレイヤーへの接近を辞める距離")]
    [SerializeField] float _near;
    [Tooltip("門番がリポップする距離")]
    [SerializeField] float _repopDistance = 10;
    [Tooltip("敵が味方になるときに呼ぶ")]
    [SerializeField] UnityEvent _jumpSideEvent;

    /// <summary>HP</summary>
    int _hp;
    /// <summary>勢力</summary>
    EnemyHand _hand = EnemyHand.Enemy;
    /// <summary>リスポーン地点</summary>
    Vector2 _respawnPoint;
    /// <summary>エネミーの状態</summary>
    EnemyState _state;
    /// <summary>リジッドボディ</summary>
    protected Rigidbody2D _rb;
    /// <summary>プレイヤー</summary>
    PlayerController _playerController;

    /// <summary>技能</summary>
    public EnemyType Type { get => _type; }
    /// <summary>勢力</summary>
    public EnemyHand Hand { get => _hand; }
    /// <summary>リジッドボディ</summary>
    public Rigidbody2D Rb { get => _rb; }
    /// <summary>HP</summary>
    public int HP { get => _hp; }


    static public Action OnDamage;

    // Start is called before the first frame update
    void Awake()
    {
        _respawnPoint = transform.position;
    }

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = FindObjectOfType<PlayerController>();
        if (!_playerController)
        {
            Debug.LogWarning($"プレイヤーが見つかりませんでした");
        }
        HPReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == EnemyState.Idol)
        {
            EnemyUpdate();
        }
        else if (_state == EnemyState.Follow)
        {
        }
        else if (_state == EnemyState.Throw)
        {

        }
    }

    private void FixedUpdate()
    {
        if (_state == EnemyState.Idol)
        {
            EnemyFixedUpdate();
        }
        else if (_state == EnemyState.Follow)
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
        if (hand != _hand)
        {
            if (hand == EnemyHand.Enemy)
            {
                gameObject.layer = _enemyLayer;
            }
            else if (hand == EnemyHand.Player)
            {
                gameObject.layer = _friendLayer;
                _state = EnemyState.Follow;
                if (_jumpSideEvent != null)
                {
                    _jumpSideEvent.Invoke();
                }
            }
            _hand = hand;
            return true;
        }
        return false;
    }


    /// <summary>
    /// ダメージを食らう
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        _hp -= damage;
        if (_type == EnemyType.Boss)
        {
            CallOnDamage();
        }
        if (_hp <= 0)
        {
            Death();
        }
    }

    static void CallOnDamage()
    {
        if (OnDamage != null)
        {
            OnDamage.Invoke();
        }
    }

    public bool Throw(Vector2 axis, float speed)
    {
        return Throw(axis * speed);
    }

    public bool Throw(Vector2 velocity)
    {
        if (_hand == EnemyHand.Player)
        {
            transform.position = _playerController.transform.position;
            _rb.velocity = velocity;
            _state = EnemyState.Throw;
            return true;
        }

        return false;
    }

    /// <summary>
    /// リスポーンする
    /// </summary>
    void Respawn()
    {
        JumpSide(EnemyHand.Enemy);
        if (_playerController)
        {
            _playerController.RemoveAlly(this);
        }
        _state = EnemyState.Idol;
        transform.position = _respawnPoint;
        HPReset();
    }

    /// <summary>
    /// プレイヤーに追従する
    /// </summary>
    void FollowPlayer()
    {
        if (!_playerController)
        {
            return;
        }

        Vector3 target = _playerController.transform.position;
        if(Vector3.Distance(target, transform.position) >= _warpDistance)
        {
            transform.position = target;
        }
        else if (Vector3.Distance(target, transform.position) >= _near)
        {
            _rb.velocity = (target - transform.position).normalized * _followSpeed;
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        float dir = target.x - transform.position.x;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -(Mathf.Abs(dir) / dir), transform.localScale.y, transform.localScale.z);


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
    /// 死ぬ
    /// </summary>
    void Death()
    {
        if (_type != EnemyType.GimmickEnemy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            StartCoroutine(RePop());
        }
        if(_type == EnemyType.Boss)
        {
            SceneChangeManager.Instance.StageCrear();
        }
    }

    IEnumerator RePop()
    {
        while(Vector3.Distance(_respawnPoint, _playerController.transform.position) <= _repopDistance)
        {
            yield return null;
        }
        gameObject.SetActive(true);
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
        if (_state == EnemyState.Throw)
        {
            EnemyBase enemy;
            if (collision.gameObject.TryGetComponent<EnemyBase>(out enemy))
            {
                Hit(enemy);
            }
            else
            {
                Respawn();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //マジックナンバー使用
        if(collision.gameObject.layer == 3)
        {
            if (_type != EnemyType.Boss)
            {
                PlayerController p = collision.gameObject.GetComponentInParent<PlayerController>();
                if (p)
                {
                    p.GetAlly(this);
                    JumpSide(EnemyHand.Player);
                }
            }
        }
    }

}

/// <summary>
/// 敵の勢力
/// </summary>
public enum EnemyHand
{
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