using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class PlayerHurtAtkEventArgs : EventArgs
{
    public PlayerHurtAtkEventArgs(GameObject _hurted, GameObject _sender,
        GameObject _origin, int _damage, int _atkId,  Dictionary<PlayerHurtDataType, string> _data, bool _forceHurt = false)
    {
        hurted = _hurted;
        sender = _sender;
        damage = _damage;
        atkId = _atkId;
        data = _data;
        origin = _origin;
        forceHurt = _forceHurt;
    }

    public int atkId;

    public int damage;

    public  Dictionary<PlayerHurtDataType, string> data;

    public GameObject hurted;

    public GameObject origin;

    public GameObject sender;

    public bool forceHurt;
}