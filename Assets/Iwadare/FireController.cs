using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] Transform _crosshair;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("マウスを追従するオブジェクトにCrosshairControllerコンポーネントをつけないといけません。");
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_crosshair)
        {
            Vector2 dir = _crosshair.position - transform.position;
            transform.up = dir;
        }
    }
}
