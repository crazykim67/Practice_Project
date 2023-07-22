using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const string LOCK = "Lock";
    private const string OPEN = "Open";
    private const string CLOSE = "Close";

    private const string TEXT_OPEN = "Open [E]";
    private const string TEXT_CLOSE = "Close [E]";

    public bool isLock;
    public bool isOpen = false;

    private Animator anim;

    [SerializeField]
    private bool isAct = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnActive()
    {
        if (!isAct)
            return;

        // Door State : Close
        if (!isOpen)
        {
            if (!isLock)
                Open();
            else
                Lock();
        }
        // Door State : Open
        else
        {
            Close();
        }
    }

    public void Open()
    {
        if (isLock)
        {
            Lock();
            return;
        }

        anim.Play(OPEN);
        isOpen = true;
    }

    public void Close()
    {
        anim.Play(CLOSE);
        isOpen = false;
    }

    public void Lock()
    {
        anim.Play(LOCK);
    }

    public void OnCast(TextMeshProUGUI _text)
    {
        if (!isAct)
            return;

        _text.enabled = true;

        if (!isOpen)
        {
            _text.text = "Open [E]";
        }
        else
        {
            _text.text = "Close [E]";
        }

        EntityManager.Instance.SetAction(OnActive);
    }

    #region Animation Check

    private void IsFalse()
    {
        isAct = false;
    }

    private void IsTrue()
    {
        isAct = true;
    }

    #endregion
}
