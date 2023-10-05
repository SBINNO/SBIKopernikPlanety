using System.Collections;
using UnityEngine;

public class CustomEntranceDoorController : DoorController
{
    [SerializeField]
    CutsceneSequencer entranceSequencer;
    [SerializeField]
    CutsceneController singleAnimationTest;
    [SerializeField]
    private Transform playerCharacterTeleportLocation;
    private GameObject playerCharacter;

    protected override void Awake()
    {
        base.Awake();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void OpenDoor(Collider other)
    {
        base.OpenDoor(other);
        StartCoroutine(TeleportPlayer(0.1f));
        if (singleAnimationTest != null)
        {
            singleAnimationTest.Initialize();
            return;
        }
        if (entranceSequencer != null)
        {
            Debug.Log("Normal Cutscene goes");
            entranceSequencer.StartNextCutscene();
            return;
        }
    }
    private IEnumerator TeleportPlayer(float delay)
    {
        if (playerCharacter != null && playerCharacterTeleportLocation != null)
        {
            Debug.Log("Teleport!");
            DisablePlayerControls();
            playerCharacter.transform.position = playerCharacterTeleportLocation.position;
            playerCharacter.transform.rotation = playerCharacterTeleportLocation.rotation;
            yield return new WaitForSeconds(delay);
            EnablePlayerControls();
        }
    }
    public virtual void EnablePlayerControls()
    {
        MonoBehaviour[] scripts = playerCharacter.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
       // playerCharacter.GetComponent<PlayerWalk>().enabled = true;
    }
    public virtual void DisablePlayerControls()
    {
        MonoBehaviour[] scripts = playerCharacter.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
       // playerCharacter.GetComponent<PlayerWalk>().enabled = false;
    }
}
