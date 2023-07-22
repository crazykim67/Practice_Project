using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager instance;

    public static EntityManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new EntityManager();
                return instance;
            }

            return instance; 
        }
    }

    public Action act;

    private void Awake() => instance = this;

    public void SetAction(Action _act)
    {
        if (act != null)
            act = null;

        act = _act;
    }

    public void OnInvoke()
    {
        act.Invoke();
        act = null;
    }

    public bool IsNull()
    {
        if(act == null)
            return false;
        else
            return true;
    }
}
