using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkedaTestAction : MonoBehaviour
{
    [SerializeField] EnemyBase _enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        EnemyBase.OnDamage += Damage;
        //_enemyBase.Damage(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        Debug.Log(1);
    }
}
