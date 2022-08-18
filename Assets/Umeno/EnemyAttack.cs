using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Vector3 _player;
    Rigidbody2D _rb;
    int _moveSpeed;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = (_player - transform.position).normalized * _moveSpeed;
    }
}
