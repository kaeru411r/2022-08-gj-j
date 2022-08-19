using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Rigidbody2D))]
abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("�Z�\")]
    [SerializeField] EnemyType _type = EnemyType.Non;
    [Tooltip("HP�����l")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("�U����")]
    [SerializeField] int _atk = 1;
    [Tooltip("�����̎��̃��C���[")]
    [SerializeField] int _friendLayer;
    [Tooltip("�G�̎��̃��C���[")]
    [SerializeField] int _enemyLayer;
    [Tooltip("�Ǐ]���̈ړ����x")]
    [SerializeField] float _followSpeed;
    [Tooltip("�v���C���[�Ƀ��[�v���鋗��")]
    [SerializeField] float _warpDistance = 3f;
    [Tooltip("�v���C���[�ւ̐ڋ߂����߂鋗��")]
    [SerializeField] float _near;
    [Tooltip("��Ԃ����|�b�v���鋗��")]
    [SerializeField] float _repopDistance = 10;
    [Tooltip("�G�������ɂȂ�Ƃ��ɌĂ�")]
    [SerializeField] UnityEvent _jumpSideEvent;

    /// <summary>HP</summary>
    int _hp;
    /// <summary>����</summary>
    EnemyHand _hand = EnemyHand.Enemy;
    /// <summary>���X�|�[���n�_</summary>
    Vector2 _respawnPoint;
    /// <summary>�G�l�~�[�̏��</summary>
    EnemyState _state;
    /// <summary>���W�b�h�{�f�B</summary>
    protected Rigidbody2D _rb;
    /// <summary>�v���C���[</summary>
    PlayerController _playerController;

    /// <summary>�Z�\</summary>
    public EnemyType Type { get => _type; }
    /// <summary>����</summary>
    public EnemyHand Hand { get => _hand; }
    /// <summary>���W�b�h�{�f�B</summary>
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
            Debug.LogWarning($"�v���C���[��������܂���ł���");
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
    /// �n�������͂Ɉڂ�
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>���͈ړ��ɐ���������</returns>
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
    /// �_���[�W��H�炤
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
    /// ���X�|�[������
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
    /// �v���C���[�ɒǏ]����
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
    /// �G�ɓ�����
    /// </summary>
    /// <param name="enemy"></param>
    void Hit(EnemyBase enemy)
    {
        enemy.Damage(_atk);
        Respawn();
    }




    /// <summary>
    /// ����
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
    /// HP�����Z�b�g����
    /// </summary>
    void HPReset()
    {
        _hp = _maxHp;
    }

    /// <summary>
    /// �G�̍s��
    /// </summary>
    virtual public void EnemyUpdate()
    {

    }

    /// <summary>
    /// �G�̍s�� FixedUpdate
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
        //�}�W�b�N�i���o�[�g�p
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
/// �G�̐���
/// </summary>
public enum EnemyHand
{
    /// <summary>�G</summary>
    Enemy,
    /// <summary>����</summary>
    Player,
}

/// <summary>
/// �G�l�~�[�̏��
/// </summary>
public enum EnemyState
{
    /// <summary>�ҋ@</summary>
    Idol,
    /// <summary>�Ǐ]</summary>
    Follow,
    /// <summary>�������Ă�</summary>
    Throw,
}