using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    float h;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpPower = 5f;
    [SerializeField] float _max;
    List<GameObject> _allylist = new List<GameObject>();

    bool _jump;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        Flip(h);
        if(Input.GetButtonDown("Jump") && _jump)
        {
            //Vector2 velocity = _rb.velocity;
            //velocity.y = _jumpPower;
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(h * _speed, _rb.velocity.y);
    }
    void Flip(float horizontal)
    {
        if(horizontal > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if(horizontal < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _jump = true;
        }
    }
}
