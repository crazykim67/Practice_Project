using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EntityType
{
    None,
    Door,
}

public class EntityInteraction : MonoBehaviour
{
    [SerializeField]
    private float dir = 3f;

    [SerializeField]
    private TextMeshProUGUI entityText;

    [Header("Entities")]
    [SerializeField]
    private Door currentDoor = null;

    private void Update()
    {
        Raycast();

        if(Input.GetKeyDown(KeyCode.E))
            if (EntityManager.Instance.IsNull())
                EntityManager.Instance.OnInvoke();
    }

    private void Raycast()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Entity");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, ray.direction * dir, Color.yellow);

        if(Physics.Raycast(ray, out hit, dir, layerMask))
        {
            if(hit.transform != null)
                OnInteraction(hit);
            else
            {
                entityText.enabled = false;
                entityText.text = "";
                Clear();
            }
        }
        else
        {
            entityText.enabled = false;
            entityText.text = "";
            Clear();
        }
    }

    public void OnInteraction(RaycastHit _hit)
    {
        if (_hit.transform == null)
            return;

        switch (_hit.transform.tag)
        {
            case "Door":
                {
                    currentDoor = _hit.transform.GetComponent<Door>();
                    currentDoor.OnCast(entityText);
                    break;
                }
        }
    }

    private void Clear()
    {
        currentDoor = null;
    }
}
