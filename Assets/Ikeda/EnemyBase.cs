using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    [Tooltip("‹Z”\")]
    [SerializeField] EnemyType _enemyType;
    [Tooltip("HP‰Šú’l")]
    [SerializeField] int _hp = 1;

    /// <summary>¨—Í</summary>
    EnemyHand _hand = EnemyHand.Enemy;

    /// <summary>¨—Í</summary>
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
    /// “n‚µ‚½¨—Í‚ÉˆÚ‚é
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>¨—ÍˆÚ“®‚É¬Œ÷‚µ‚½‚©</returns>
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
/// “G‚Ì¨—Í
/// </summary>
public enum EnemyHand{
    /// <summary>“G</summary>
    Enemy,
    /// <summary>–¡•û</summary>
    Player,
}