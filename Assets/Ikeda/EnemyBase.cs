using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    [Tooltip("�Z�\")]
    [SerializeField] EnemyType _type;
    [Tooltip("HP�����l")]
    [SerializeField] int _maxHp = 1;

    /// <summary>HP</summary>
    int _hp;
    /// <summary>����</summary>
    EnemyHand _hand = EnemyHand.Enemy;

    /// <summary>�Z�\</summary>
    public EnemyType Type { get => _type; }
    /// <summary>����</summary>
    public EnemyHand Hand { get => _hand; }
    /// <summary>HP</summary>
    public int HP { get => _hp;}

    /// <summary>HP</summary>

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_hand == EnemyHand.Enemy)
        {

        }
        else if(_hand == EnemyHand.Player)
        {

        }
    }

    /// <summary>
    /// �n�������͂Ɉڂ�
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>���͈ړ��ɐ���������</returns>
    public bool JumpSide(EnemyHand hand)
    {
        if(hand != _hand)
        {
            _hand = hand;
            return true;
        }
        return false;
    } 

    void FollowPlayer()
    {

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