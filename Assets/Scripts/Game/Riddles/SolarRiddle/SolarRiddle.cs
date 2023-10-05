using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SolarRiddle : MonoBehaviour
{
    public GameObject ObjectPlacerPrefab;

    private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    private List<ObjectPlacer> objectPlacers = new List<ObjectPlacer>();
    private List<GameObject> clones = new List<GameObject>();
    private int snatchedObjectCount = 0;
    private bool allObjectsSnatched = false;
    private bool riddleSolved = false;


    [SerializeField]
    [Range(0,100)]
    private float capsuleShrinker = 10f;
    [SerializeField]
    [Range(0, 100)]
    private float snatchRadiusShrinker = 10f;
    private Animator animator;
    public event Action OnRiddleSolved;

    private void Awake()
    {
        GatherMeshObjects();
        allObjectsSnatched = false;
        riddleSolved = false;
        animator = GetComponent<Animator>();
    }
    public bool RiddleSolved()
    {
        return riddleSolved;
    }

    private void GatherMeshObjects()
    {
        skinnedMeshRenderers.AddRange(GetComponentsInChildren<SkinnedMeshRenderer>());
    }
    private void Update()
    {
        if (!riddleSolved)
        {
            if (allObjectsSnatched)
            {
                foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.enabled = true;
                }
                foreach (GameObject clone in clones)
                {
                    Destroy(clone);
                }
                clones.Clear();
                riddleSolved = true;
                OnRiddleSolved?.Invoke();
            }
        }

    }
    public void StartRiddle()
    {
        AssignPlacerPrefabs();
        InstantiateObjectClones();
    }

    private void AssignPlacerPrefabs()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {


            Bounds bounds = skinnedMeshRenderer.bounds;
            Vector3 position = bounds.center;

            Vector3 localPosition = skinnedMeshRenderer.transform.localPosition;
            Quaternion localRotation = skinnedMeshRenderer.transform.localRotation;

            GameObject instantiatedObject = Instantiate(ObjectPlacerPrefab, localPosition, Quaternion.identity);
            instantiatedObject.name = (skinnedMeshRenderer.gameObject.name + " Container");
            instantiatedObject.transform.SetParent(skinnedMeshRenderer.transform.parent);
            instantiatedObject.transform.localPosition = localPosition;

            //Here let's do calculation
            Vector3 meshSize = bounds.size;
            Vector3 prefabSize = instantiatedObject.transform.localScale;

            float scaleFactorX = meshSize.x / prefabSize.x;
            float scaleFactorY = meshSize.y / prefabSize.y;
            float scaleFactorZ = meshSize.z / prefabSize.z;

            float scaleFactor = Mathf.Max(scaleFactorX, scaleFactorY, scaleFactorZ);

            instantiatedObject.transform.localScale = (prefabSize * scaleFactor) / capsuleShrinker;

            instantiatedObject.transform.SetParent(null);

            ObjectPlacer objectPlacer = instantiatedObject.GetComponent<ObjectPlacer>();
            if (objectPlacer != null)
            {
                objectPlacer.CorrectObject = null;
                objectPlacer.OnCorrectObjectSnatched += ObjectPlacer_OnObjectSnatched;
                objectPlacer.SnatchRadius *= (scaleFactor / snatchRadiusShrinker);

                objectPlacers.Add(objectPlacer);
            }
        }
    }
    public void InstantiateObjectClones()
    {
        for (int i = 0; i < skinnedMeshRenderers.Count; i++)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRenderers[i];
            ObjectPlacer objectPlacer = objectPlacers[i];
            Vector3 localPosition = skinnedMeshRenderer.transform.localPosition;
            Quaternion localRotation = skinnedMeshRenderer.transform.localRotation;

            // Create a new GameObject with a MeshFilter and MeshRenderer
            GameObject cloneObject = new GameObject(skinnedMeshRenderer.gameObject.name);
            clones.Add(cloneObject);
            cloneObject.transform.SetParent(skinnedMeshRenderer.transform.parent);
            cloneObject.transform.localPosition = localPosition;
            cloneObject.transform.localRotation = localRotation;

            MeshFilter meshFilter = cloneObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = cloneObject.AddComponent<MeshRenderer>();

            // Create a new mesh based on the skinned mesh renderer's mesh data
            Mesh mesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(mesh);
            meshFilter.sharedMesh = mesh;
            skinnedMeshRenderer.enabled = false;

            // Copy materials from the skinned mesh renderer to the new mesh renderer
            Material[] materials = skinnedMeshRenderer.sharedMaterials;
            meshRenderer.sharedMaterials = materials;

            ObjectHolder objectHolder = cloneObject.GetComponent<ObjectHolder>();
            if (objectHolder == null)
            {
                objectHolder = cloneObject.AddComponent<ObjectHolder>();
            }
            cloneObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            if (objectPlacer != null)
            {
                objectPlacer.CorrectObject = cloneObject;
            }
        }
    }

    private void IncrementSnatchedObjectCount()
    {
        snatchedObjectCount++;

        if (snatchedObjectCount == objectPlacers.Count)
        {
            allObjectsSnatched = true;
        }
    }

    private void DecrementSnatchedObjectCount()
    {
        snatchedObjectCount--;

        if (snatchedObjectCount < objectPlacers.Count)
        {
            allObjectsSnatched = false;
        }
    }
    private void ObjectPlacer_OnObjectSnatched(ObjectPlacer objectPlacer)
    {
        IncrementSnatchedObjectCount();
        objectPlacer.OnCorrectObjectSnatched -= ObjectPlacer_OnObjectSnatched;
        Destroy(objectPlacer.gameObject);
    }
}


