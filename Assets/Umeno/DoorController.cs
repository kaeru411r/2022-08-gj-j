using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : GimmickBase
{
    [SerializeField] GameObject _doorOpen;
    [SerializeField] GameObject _doorClose;
    public override void DoorOpen()
    {
        _doorClose.gameObject.SetActive(false);
        _doorOpen.gameObject.SetActive(true);
    }
}
