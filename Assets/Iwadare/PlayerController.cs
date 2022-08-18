using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    float h;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpPower = 5f;
    [SerializeField] float _max = 5f;
    [SerializeField] GameObject _brainLenge;
    [SerializeField] GameObject _mazzle;
    List<EnemyBase> _allyList = new List<EnemyBase>();
    Animator _anim;
    float minas = 1;
    bool _jump;
    bool _brain;
    bool _gameover;
    bool _gameoverjump;
    [SerializeField] CrosshairController _mouse;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _mouse = GetComponent<CrosshairController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameover)
        {
            h = Input.GetAxis("Horizontal");
            //Vector2 velocity = _rb.velocity;
            Flip(h);
            if (Input.GetButtonDown("Jump") && _jump)
            {
                //velocity.y = _jumpPower;
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _jump = false;
            }
            if (Input.GetButtonDown("Fire2") && !_brain)
            {
                _brainLenge.gameObject.SetActive(true);
                _brain = true;
                StartCoroutine(Brawashtime());
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Vector2 vector = new Vector2(10f * minas, _mouse.mousePosition.y);
                _allyList[0].Throw(vector);
                Debug.Log("‘Å‚¿o‚³‚ê‚½‚¼II");
            }
        }
        else
        {
            h = 0;
            if (!_gameoverjump)
            {
                //_rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _gameoverjump = true;
            }

        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(h * _speed, _rb.velocity.y);
        if (Input.GetButtonDown("Jump") && _jump)
        {
            //velocity.y = _jumpPower;
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _jump = false;
        }
    }
    private void LateUpdate()
    {
        if(_anim)
        {
            _anim.SetFloat("SpeedX", Mathf.Abs(_rb.velocity.x));
            _anim.SetFloat("SpeedY", Mathf.Abs(_rb.velocity.y));
            _anim.SetBool("IsGround", _jump);
            _anim.SetBool("Brain", _brain);
        }
    }
    void Flip(float horizontal)
    {
        if(horizontal > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            minas = 1;
        }
        else if(horizontal < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            minas = -1;
        }
    }
    public void GetAlly(EnemyBase ally)
    {
        if(_allyList.Count > _max)
        {
            _allyList.Add(ally);
        }
    }
    public void RemoveAlly(EnemyBase ally)
    {
        _allyList.Remove(ally);
    }
    IEnumerator Brawashtime()
    {
        yield return new WaitForSeconds(2f);
        _brainLenge.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _brain = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _jump = true;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Gameover");
            _gameover = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            Debug.Log("Gameover");
            _gameover = true;
        }
        if (collision.gameObject.tag == "item")
        {
            _max++;
        }
    }
}
