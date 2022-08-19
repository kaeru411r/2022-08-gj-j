using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMove : MonoBehaviour
{
    [SerializeField] GameObject _help1;
    [SerializeField] GameObject _help2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HelpMoves()
    {
        _help1.gameObject.SetActive(false);
        _help2.gameObject.SetActive(true);
    }
    public void GameClose()
    {
        Application.Quit();
    }
}
