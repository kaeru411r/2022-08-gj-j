using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("�Z�\")]
    [SerializeField] EnemyType _type;
    [Tooltip("HP�����l")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("�U����")]
    [SerializeField] int _atk = 1;
    [Tooltip("�����̎��̃��C���[")]
    [SerializeField] LayerMask _friendLayer;
    [Tooltip("�G�̎��̃��C���[")]
    [SerializeField] LayerMask _enemyLayer;

    /// <summary>HP</summary>
    int _hp;
    /// <summary>����</summary>
    EnemyHand _hand = EnemyHand.Enemy;
    /// <summary>���X�|�[���n�_</summary>
    Vector2 _respawnPoint;
    /// <summary>�G�l�~�[�̏��</summary>
    EnemyState _state;

    /// <summary>�Z�\</summary>
    public EnemyType Type { get => _type; }
    /// <summary>����</summary>
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
    /// �n�������͂Ɉڂ�
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>���͈ړ��ɐ���������</returns>
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
    /// ���X�|�[������
    /// </summary>
    void Respawn()
    {
        transform.position = _respawnPoint;
        HPReset();
    }

    /// <summary>
    /// �v���C���[�ɒǏ]����
    /// </summary>
    void FollowPlayer()
    {
        
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
    /// �_���[�W��H�炤
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
    /// ����
    /// </summary>
    void Death()
    {
        Destroy(gameObject);
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
/// �G�̐���
/// </summary>
public enum EnemyHand{
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