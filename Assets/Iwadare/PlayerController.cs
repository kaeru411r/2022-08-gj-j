using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    float h;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpPower = 5f;
    public float _max = 5f;
    [SerializeField] GameObject _brainLenge;
    [SerializeField] GameObject _mazzle;
    [SerializeField] GameObject _gameOverCanvas;
    List<EnemyBase> _allyList = new List<EnemyBase>();
    Animator _anim;
    AudioSource _audio;
    EnemyBase _hide;
    float minas = 1;
    bool _jump;
    bool _brain;
    bool _gameover;
    bool _gameoverjump;
    bool _audioplay;
    //[SerializeField] CrosshairController _mouse;
    public List<EnemyBase> AllyList { get => _allyList; }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        //_mouse = GetComponent<CrosshairController>();
        _audio = GetComponent<AudioSource>();
        _hide = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameover)
        {
            h = Input.GetAxis("Horizontal");
            //Debug.Log(h);
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
                //Vector2 vector = new Vector2(Mathf.Abs(_mouse.mousePosition.x - transform.position.x) * minas, _mouse.mousePosition.y);
                Vector2 vector = new Vector2(6f * minas, 6f);
                if (_allyList != null)
                {
                    _allyList[0].Throw(vector);
                }
                Debug.Log("打ち出されたぞ！！");
            }
            if(h != 0 && !_audioplay && _jump)
            {
                _audio.Play();
                _audioplay = true;
            }
            else
            {
                _audio.Pause();
                _audioplay = false;
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
    public bool GetAlly(EnemyBase ally)
    {
        if(_allyList.Count < _max)
        {
            _allyList.Add(ally);
            return true;
        }
        return false;
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
            if(_gameOverCanvas)
            {
                _gameOverCanvas.SetActive(true);
            }
            _gameover = true;
            StartCoroutine(GameOverTime());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            Debug.Log("Gameover");
            if (_gameOverCanvas)
            {
                _gameOverCanvas.SetActive(true);
            }
            else
            {
                Debug.LogWarning("GameOverCanvasをアタッチしてください。");
            }
            _gameover = true;
            StartCoroutine(GameOverTime());
        }
        if (collision.gameObject.tag == "Item")
        {
            _max++;
            Destroy(collision.gameObject);
        }
    }
    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("タイトルへ戻る");
        SceneChangeManager.Instance.LoadScene(0);
    }
}
