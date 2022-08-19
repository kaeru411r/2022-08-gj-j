using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMove : MonoBehaviour
{
    [SerializeField] GameObject[] _helps;

    int _index = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(_helps.Length <= 0)
        {
            Destroy(this);
        }
    }

    public void Open()
    {
        _helps[0].SetActive(true);
        _index = 0;
    }

    public void MoveNext()
    {
        _helps[_index].SetActive(false);
        _index++;
        _helps[_index].SetActive(true);
    }

    public void MovePrev()
    {
        _helps[_index].SetActive(false);
        _index--;
        _helps[_index].SetActive(true);
    }

    public void Close()
    {
        _helps[_index].SetActive(false);
        _index = 0;
    }
    public void GameClose()
    {
        Application.Quit();
    }
}
