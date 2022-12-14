using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class SceneChangeManager : MonoBehaviour
{
    static private SceneChangeManager _instance;
    static public SceneChangeManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<SceneChangeManager>();
                if (_instance == null)
                {
                    Debug.LogError($"{nameof(SceneChangeManager)}が見つかりません");
                    return null;
                }
            }
            return _instance;
        }
    }
    [Tooltip("ステージの番号")]
    [SerializeField] Stage[] _stages;
    [Tooltip("ステージ以外のシーンの数")]
    [SerializeField] int _outGameSceneNum = 1;

    public Stage[] Stages { get => _stages; set => _stages = value; }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }


    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int index)
    {
        Scene s = SceneManager.GetSceneAt(index);
        if (!s.IsValid())
        {
            return;
        }

        Cursor.visible = true;
        SceneManager.LoadScene(index);
    }

    public void LoadStage(int index)
    {
        if (index < 0) return;
        if (_stages.Length - 1 < index) return;

        SceneManager.LoadScene((int)_stages[index].StageIndex);
    }

    public void StageUnlock(int index)
    {
        if (index < 0) return;
        if (_stages.Length - 1 < index) return;
        _stages[index].IsOpen = true;
    }

    public void StageCrear()
    {
        for (int i = 0; i < _stages.Length; i++)
        {
            if (_stages[i].StageIndex == SceneManager.GetActiveScene().buildIndex)
            {
                StageUnlock(i + 1);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stages != null)
        {
            if (EditorBuildSettings.scenes.Length - _outGameSceneNum < _stages.Length)
            {
                Debug.LogWarning($"{nameof(_stages)}の数がステージの数を超えています");
            }
        }
    }
#endif
}
