using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuroController : MonoBehaviour
{
    [SerializeField]int x;
    [SerializeField]int y;
    Rigidbody2D _rb;
    bool _jumpTime;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 vec = new Vector2(x, y);
        _rb.AddForce(vec, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_jumpTime)
        //{
        //    _jumpTime = true;
        //    StartCoroutine(JumpTime());
        //}
    }
    //IEnumerator JumpTime()
    //{
    //    //yield return new WaitForSeconds(2f);
    //    //_rb.AddForce(Vector2.up * y, ForceMode2D.Impulse);
    //    //_jumpTime = false;
    //}
}
