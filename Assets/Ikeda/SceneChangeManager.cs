using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    static public SceneChangeManager Instance;
    [Tooltip("�X�e�[�W�̔ԍ�")]
    [SerializeField] Stage[] _stages;
    [Tooltip("�X�e�[�W�ȊO�̃V�[���̐�")]
    [SerializeField] int _outGameSceneNum = 1;

    public Stage[] Stages { get => _stages; set => _stages = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int index)
    {
        if(index < 0) return;
        if(EditorBuildSettings.scenes.Length < index) return; 

        SceneManager.LoadScene(index);
    }
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
}
