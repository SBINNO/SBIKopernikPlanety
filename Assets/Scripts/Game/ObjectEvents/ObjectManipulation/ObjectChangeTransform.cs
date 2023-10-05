using UnityEngine;

public class ObjectChangeTransform : MonoBehaviour, ICutsceneAction
{
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float movementDuration = 1f;

    private Vector3 initialPosition;
    private float timer;

    private void Start()
    {
        initialPosition = objectToMove.position;
    }

    private System.Collections.IEnumerator MoveToTargetPosition()
    {
        timer = 0f;
        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / movementDuration);
            objectToMove.position = Vector3.Lerp(initialPosition, targetPosition.position, t);
            yield return null;
        }

        objectToMove.position = targetPosition.position;
    }

    public void ExecuteAction()
    {
        StartCoroutine(MoveToTargetPosition());
    }
}
