using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class StageSelectManager : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("ボタンの画像")]
    [SerializeField] Sprite _button;
    [Tooltip("錠の画像")]
    [SerializeField] Sprite _lock;

    /// <summary>レイアウトグループ</summary>
    HorizontalLayoutGroup _group;
    /// <summary>ボタンのオブジェクト</summary>
    GameObject[] _buttons;

    public void OnPointerClick(PointerEventData eventData)
    {
        Image img = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();
    }




    // Start is called before the first frame update
    void Start()
    {
        _buttons = new GameObject[SceneChangeManager.Instance.Stages.Length];
        for(int i = 0; i < SceneChangeManager.Instance.Stages.Length; i++)
        {
            GameObject g = new GameObject();
            g.transform.SetParent(transform);
            _buttons[i] = g;
            g.name = "1";
            Image img = g.AddComponent<Image>();
            img.sprite = _button;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public struct Stage
{
    public uint StageIndex;
    public bool IsOpen;
}
