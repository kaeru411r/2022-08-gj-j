using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Linq;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class StageSelectManager : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("ボタンのプレハブ")]
    [SerializeField] GameObject _buttonPrefab;
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
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if(go != null)
        {
            for(int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] == go)
                {
                    SceneChangeManager.Instance.LoadStage(i);
                }
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        if (!_button)
        {
            Debug.LogWarning($"{nameof(_button)}がアサインされてません");
        }
        if (!_lock)
        {
            Debug.LogWarning($"{nameof(_lock)}がアサインされてません");
        }
        if (!_buttonPrefab)
        {
            Debug.LogWarning($"{nameof(_buttonPrefab)}がアサインされてません");
        }
        StartCoroutine(StartSet());
    }

    GameObject ButtonGenerate()
    {
        GameObject go;
        if (_buttonPrefab)
        {
            go = Instantiate(_buttonPrefab);
        }
        else
        {
            go = new GameObject();
        }
        return go;
    }

    Image GetImage(GameObject go)
    {
        Image img;
        if(!go.TryGetComponent<Image>(out img))
        {
            img = go.AddComponent<Image>();
            if (go.transform.childCount > 0)
            {
                img = go.GetComponentInChildren<Image>();
                if (!img)
                {
                    img = go.transform.GetChild(0).gameObject.AddComponent<Image>();
                }
            }
        }
        return img;
    }

    Text GetText(GameObject go)
    {
        Text t;
        if(!go.TryGetComponent<Text>(out t))
        {
            t = go.AddComponent<Text>();
            if(go.transform.childCount > 0)
            {
                t = go.GetComponentInChildren<Text>();
                if (!t)
                {
                    t = go.transform.GetChild(0).gameObject.AddComponent<Text>();
                }
            }
        }
        return t;
    }

    IEnumerator StartSet()
    {
        while (SceneChangeManager.Instance)
        {
            yield return null;
        }
        while (SceneChangeManager.Instance.Stages == null)
        {
            yield return null;
        }
        _buttons = new GameObject[SceneChangeManager.Instance.Stages.Length];
        for (int i = 0; i < SceneChangeManager.Instance.Stages.Length; i++)
        {
            GameObject g = ButtonGenerate();
            g.transform.SetParent(transform);
            _buttons[i] = g;
            g.name = $"button{i + 1}";
            Image img = GetImage(g);
            if (_button)
            {
                img.sprite = _button;
            }
            GetText(g).text = $"{i + 1}";
            if (!SceneChangeManager.Instance.Stages[i].IsOpen)
            {
                //img.raycastTarget = false;
                GameObject g2 = ButtonGenerate();
                g2.transform.SetParent(g.transform);
                g2.name = $"lock{i + 1}";
                Image img2 = GetImage(g2);
                if (_lock)
                {
                    img2.sprite = _lock;
                }
                GetText(g2).text = "";
                //img2.raycastTarget = false;
            }
        }

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
