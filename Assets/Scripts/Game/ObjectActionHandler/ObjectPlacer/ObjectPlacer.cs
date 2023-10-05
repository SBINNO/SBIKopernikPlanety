using System;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public float snatchRadius = 2f;

    private bool hasSnatchedObject = false;
    public GameObject correctObject;

    public event Action<ObjectPlacer> OnCorrectObjectSnatched;


    private void Update()
    {
        if (hasSnatchedObject)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, snatchRadius);
        foreach (Collider collider in colliders)
        {
            ObjectHolder objectHolder = collider.GetComponent<ObjectHolder>();
            if (objectHolder != null && objectHolder.IsHoldingObject())
            {
                GameObject heldObject = collider.gameObject;
                if (heldObject == correctObject)
                {
                    SnatchCorrectObject(objectHolder, heldObject);
                    break;
                }
                else
                {
                    SnatchIncorrectObject(objectHolder, heldObject);
                    break;
                }
            }
        }
    }
    public GameObject CorrectObject
    {
        get { return correctObject; }
        set { correctObject = value; }
    }
    public bool HasSnatchedObject
    {
        get { return hasSnatchedObject; }
        set { hasSnatchedObject = value; }
    }
    public float SnatchRadius
    {
        get { return snatchRadius; }
        set { snatchRadius = value; }
    }
    private void SnatchCorrectObject(ObjectHolder objectHolder, GameObject heldObject)
    {
        Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
        if (heldObjectRigidbody != null)
        {
            heldObjectRigidbody.isKinematic = true;
            hasSnatchedObject = true;
            DisableCapsuleCollider(heldObject);
            ResetObjectTransforms(objectHolder, heldObject);

            OnCorrectObjectSnatched?.Invoke(this);
        }
    }
    private void SnatchIncorrectObject(ObjectHolder objectHolder, GameObject heldObject)
    {
        Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
        if (heldObjectRigidbody != null)
        {
            heldObjectRigidbody.isKinematic = false;
            ResetObjectTransforms(objectHolder, heldObject);
        }
    }

    private void DisableCapsuleCollider(GameObject heldObject)
    {
        CapsuleCollider capsuleCollider = heldObject.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
            capsuleCollider.enabled = false;
    }

    private void ResetObjectTransforms(ObjectHolder objectHolder, GameObject heldObject)
    {
        Vector3 targetPosition = transform.position;
        Quaternion targetRotation = transform.rotation;
        objectHolder.ReleaseObject();
        heldObject.transform.SetParent(null);
        heldObject.transform.position = targetPosition;
        heldObject.transform.rotation = targetRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snatchRadius);
    }
}
