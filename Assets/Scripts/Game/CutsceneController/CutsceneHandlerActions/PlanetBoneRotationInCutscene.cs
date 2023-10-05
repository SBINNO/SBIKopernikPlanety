using System.Collections;
using UnityEngine;

public class PlanetBoneRotationInCutscene : CutsceneActionBase
{
    [SerializeField] private Transform bone;
    [SerializeField] private float rotationSpeed = 50f;

    private bool isSpinning = false;
    private bool shouldStop = false;
    private float totalRotation = 0f;
    private int spinCount = 0;

    protected override void HandleCutsceneStart()
    {
        StartSpinning();
    }

    protected override void HandleCutsceneEnd()
    {
        shouldStop = true;
    }

    private void Update()
    {
        if (isSpinning && bone != null)
        {
            SpinBone();
        }
    }

    public void StartSpinning()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinCoroutine());
        }
    }

    public void StopSpinning()
    {
        shouldStop = true;
    }

    private void SpinBone()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        bone.Rotate(Vector3.up, rotationAmount);
        totalRotation += rotationAmount;
    }

    private IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        shouldStop = false;
        totalRotation = 0f;
        spinCount = 0;

        // Store the initial rotation of the bone
        Quaternion initialRotation = bone.rotation;

        while (true)
        {
            yield return null;

            if (shouldStop && totalRotation >= 360f)
            {
                // Stop spinning after completing a full rotation
                break;
            }

            // Check if a full rotation is completed
            if (totalRotation >= 360f)
            {
                totalRotation = 0f;
                spinCount++;
            }
        }

        // Check if additional spins are needed
        while (spinCount < Mathf.CeilToInt(totalRotation / 360f))
        {
            yield return null;

            if (shouldStop && totalRotation >= (spinCount + 1) * 360f)
            {
                // Stop spinning after completing the additional spins
                break;
            }
        }

        isSpinning = false;

        // Reset the bone's rotation to the initial rotation
        bone.rotation = initialRotation;
    }
}
