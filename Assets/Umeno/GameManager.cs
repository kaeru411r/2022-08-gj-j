using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    [SerializeField] float _time = 3f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(_bossEnemy.HP >= 0 && _bossEnemy)
        //{
        //    SceneManager.LoadScene(_scnename);
        //}
    }

    public void GameCrear()
    {
        SceneChangeManager.Instance.StageCrear();
        StartCoroutine(EndWait());
    }

    IEnumerator EndWait()
    {
        yield return new WaitForSeconds(_time);

        SceneChangeManager.Instance.LoadScene(0);
    }
}
