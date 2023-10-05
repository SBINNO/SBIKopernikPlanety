using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using JetBrains.Annotations;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ObjectHolder : ObjectActionHandler
{

    public TextMeshProUGUI objectNameText;

    private GameObject heldObject;
    private bool isHoldingObject = false;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        if (GetComponent<CapsuleCollider>() == null)
        {
            gameObject.AddComponent<CapsuleCollider>();
        }
        FindObjectNameText();
    }

    private void FindObjectNameText()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas != null)
        {
            objectNameText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (isHoldingObject)
        {
            MoveHeldObject();
        }
    }


    public bool IsHoldingObject()
    {
        return isHoldingObject;
    }

    public void ReleaseObject()
    {
        isHoldingObject = false;
        HideObjectName();
    }
    private void MoveHeldObject()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position + player.transform.forward * 2;
            heldObject.transform.position = targetPosition;
        }
    }
    private void ShowObjectName(string name)
    {
        if (objectNameText != null)
        {
            objectNameText.text = name;
            objectNameText.gameObject.SetActive(true);
        }
    }
    private void HideObjectName()
    {
        if (objectNameText != null)
        {
            objectNameText.gameObject.SetActive(false);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!isHoldingObject)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                isHoldingObject = true;
                heldObject = gameObject;
                heldObject.transform.SetParent(player.transform);
                heldObject.GetComponent<Rigidbody>().isKinematic = true;

                string objectName = heldObject.name;
                ShowObjectName(objectName);

            }
        }
        else
        {
            isHoldingObject = false;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
            ReleaseObject();
        }
    }
}
