using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    private Material skyboxMaterial;
    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = Time.deltaTime * rotationSpeed;

        // Apply the rotation to the skybox material
        skyboxMaterial.SetFloat("_Rotation", skyboxMaterial.GetFloat("_Rotation") + rotationAmount);
    }
}
