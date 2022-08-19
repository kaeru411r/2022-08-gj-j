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
                    Debug.LogError($"{nameof(SceneChangeManager)}��������܂���");
                    return null;
                }
            }
            return _instance;
        }
    }
    [Tooltip("�X�e�[�W�̔ԍ�")]
    [SerializeField] Stage[] _stages;
    [Tooltip("�X�e�[�W�ȊO�̃V�[���̐�")]
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
        if (index < 0) return;
        if (EditorBuildSettings.scenes.Length < index) return;

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
                StageUnlock(i);
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
                Debug.LogWarning($"{nameof(_stages)}�̐����X�e�[�W�̐��𒴂��Ă��܂�");
            }
        }
    }
#endif
}
