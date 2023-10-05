using System;
using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour, ICutsceneAction
{
    [Range(1f, 10f)][SerializeField] private float radius = 5f;
    [Range(1f, 50f)][SerializeField] private float force = 10f;

    [Range(0f, 10f)][SerializeField] private float delay = 0f;

    public void ExecuteAction()
    {
        StartCoroutine(ShockwaveCoroutine());
    }

    private IEnumerator ShockwaveCoroutine()
    {

        yield return new WaitForSeconds(delay);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate direction away from the center of the shockwave
                Vector3 direction = collider.transform.position - transform.position;

                // Apply force to the object in the calculated direction
                rb.AddForce(direction.normalized * force, ForceMode.Impulse);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
