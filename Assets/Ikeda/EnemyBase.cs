using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    [Tooltip("�Z�\")]
    [SerializeField] EnemyType _enemyType;
    [Tooltip("HP�����l")]
    [SerializeField] int _hp = 1;

    /// <summary>����</summary>
    EnemyHand _hand = EnemyHand.Enemy;

    /// <summary>����</summary>
    public EnemyHand Hand { get => _hand; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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