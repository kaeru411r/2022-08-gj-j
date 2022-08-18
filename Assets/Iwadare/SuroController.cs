using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuroController : MonoBehaviour
{
    [SerializeField]int x;
    [SerializeField]int y;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 vec = new Vector2(x, y);
        rb.AddForce(vec, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
