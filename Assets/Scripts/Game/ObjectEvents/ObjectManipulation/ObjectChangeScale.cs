using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeScale : MonoBehaviour
{
    [SerializeField] private GameObject objectToChange;
    [SerializeField] private Vector3 scaleToChange = new Vector3(1,1,1);

    // Start is called before the first frame update
    private void Start()
    {
        if (scaleToChange == Vector3.zero)
        {
            scaleToChange = new Vector3(1,1,1);
        }
    }
    public void ChangeScale()
    {
        objectToChange.transform.localScale = scaleToChange;
    }
}
