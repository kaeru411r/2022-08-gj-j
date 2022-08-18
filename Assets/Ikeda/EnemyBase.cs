using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class EnemyBase : MonoBehaviour
{
    [Tooltip("�Z�\")]
    [SerializeField] EnemyType _type;
    [Tooltip("HP�����l")]
    [SerializeField] int _maxHp = 1;
    [Tooltip("�G��Ԃł̍U���̃N�[���^�C��")]
    [SerializeField] float _attackCoolTime = 1;

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
    /// �n�������͂Ɉڂ�
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>���͈ړ��ɐ���������</returns>
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
/// �G�̐���
/// </summary>
public enum EnemyHand{
    /// <summary>�G</summary>
    Enemy,
    /// <summary>����</summary>
    Player,
}

public enum EnemyState
{
    /// <summary>�ҋ@</summary>
    Idol,
    /// <summary>�Ǐ]</summary>
    Follow,
    /// <summary>�������Ă�</summary>
    Throw,
}