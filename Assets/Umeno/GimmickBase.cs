using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class GimmickBase : MonoBehaviour
{
    [SerializeField] PlayerController _enemyList;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        var AllyList = _enemyList.GetComponent<PlayerController>();
        if (AllyList.AllyList.Where(a => a.Type == EnemyType.GimmickEnemy) != null)
        {
            DoorOpen();
        }
    }
    virtual public void DoorOpen()
    {

    }
}
