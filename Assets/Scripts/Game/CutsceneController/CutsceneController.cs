using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private GameObject playerCharacter;
    protected Camera mainCamera;
    [SerializeField] protected Camera cutsceneCamera;
    [SerializeField] protected float cutsceneDuration;
    [SerializeField] protected CutsceneSequencer cutsceneSequencer;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float audioDelay = 0f;

    public delegate void CutsceneStartAction();
    public event CutsceneStartAction OnCutsceneStart;
    public delegate void CutsceneEndAction();
    public event CutsceneEndAction OnCutsceneEnd;

    protected bool isPartOfSequence = false;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private Vector3 initialCameraLocalPosition;
    private Quaternion initialCameraLocalRotation;

    protected Vector3 InitialCameraPosition => initialCameraPosition;
    protected Quaternion InitialCameraRotation => initialCameraRotation;
    protected Vector3 InitialCameraLocalPosition => initialCameraLocalPosition;
    protected Quaternion InitialCameraLocalRotation => initialCameraLocalRotation;

    protected bool IsCutsceneActive { get; private set; }

    [SerializeField] FlashingImage flashImage = null;

    protected virtual void Awake()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        PrepareSoundDuration();
        if (cutsceneSequencer !=null)
        {
            isPartOfSequence = true;
        } else
        {
            isPartOfSequence = false;
        }
        
    }
    protected virtual void Start()
    {
        mainCamera = playerCharacter.GetComponentInChildren<Camera>();
        InitializeCameraTransforms();
    }
    private void InitializeCameraTransforms()
    {
        initialCameraPosition = mainCamera.transform.position;
        initialCameraRotation = mainCamera.transform.rotation;
        initialCameraLocalPosition = mainCamera.transform.localPosition;
        initialCameraLocalRotation = mainCamera.transform.localRotation;
    }
    private void ChangeCurrentCameraTransforms()
    {
        initialCameraPosition = cutsceneSequencer.GetCurrentCameraPosition();
        initialCameraRotation = cutsceneSequencer.GetCurrentCameraRotation();

    }
    public virtual void Initialize()
    {
        StartCutscene(InitialCameraPosition, InitialCameraRotation);
    }

    public virtual void StartCutscene(Vector3 currentCameraPosition, Quaternion currentCameraRotation)
    {
        OnCutsceneStart?.Invoke();
        DisablePlayerControls();
        IsCutsceneActive = true;
        if (flashImage != null)
        flashImage.StartFlash(1f, 1f, Color.black);
        if (isPartOfSequence)
        {
            ChangeCurrentCameraTransforms();
        }
        Invoke("PlayAudio", audioDelay);
    }
    public virtual void ResetMainCameraTransforms()
    {
        mainCamera.transform.localPosition = InitialCameraLocalPosition;
        mainCamera.transform.localRotation = InitialCameraLocalRotation;
    }

    public virtual void EndCutscene()
    {
        OnCutsceneEnd?.Invoke();
        if (isPartOfSequence)
        {
            cutsceneSequencer.StartNextCutscene();

        } else
        {
            EnablePlayerControls();
            ResetMainCameraTransforms();
        }
        IsCutsceneActive = false;
    }
    public virtual void EnablePlayerControls()
    {
        MonoBehaviour[] scripts = playerCharacter.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
        //playerCharacter.GetComponent<PlayerWalk>().enabled = true;
    }
    public virtual void DisablePlayerControls()
    {
        MonoBehaviour[] scripts = playerCharacter.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        //playerCharacter.GetComponent<PlayerWalk>().enabled = false;
    }
    public virtual void DisableCutsceneCamera()
    {
        cutsceneCamera.enabled = false;
    }
    public virtual void SwitchToMainCamera()
    {
        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;
    }
    protected virtual void SwitchToCutsceneCamera(Camera cutsceneCamera)
    {
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;
    }
    protected virtual void PrepareSoundDuration()
    {
        if (sound != null)
        {
            audioDelay = Mathf.Clamp(audioDelay, 0f, Mathf.Infinity);
            cutsceneDuration += audioDelay;
            cutsceneDuration += sound.length;
        }
    }
    protected virtual void PlayAudio()
    {
        if (sound != null && audioSource != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
