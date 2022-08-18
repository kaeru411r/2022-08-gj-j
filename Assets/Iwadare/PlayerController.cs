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
    List<GameObject> _allyList = new List<GameObject>();
    bool _jump;
    bool _brain;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        //Vector2 velocity = _rb.velocity;
        Flip(h);
        if (Input.GetButtonDown("Jump") && _jump)
        {
            //velocity.y = _jumpPower;
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
        if(Input.GetButtonDown("Fire2") && !_brain)
        {
            _brainLenge.gameObject.SetActive(true);
            _brain = true;
            StartCoroutine(Brawashtime());
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("‘Å‚¿o‚³‚ê‚½‚¼II");
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
    public void Getally(GameObject ally)
    {
        _allyList.Add(ally);
        if(_allyList.Count > _max)
        {
            _allyList.RemoveAt(0);
        }
    }
    IEnumerator Brawashtime()
    {
        yield return new WaitForSeconds(2f);
        _brainLenge.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _brain = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _jump = true;
        }
    }
}
