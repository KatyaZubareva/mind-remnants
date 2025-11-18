using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CassetteDeckManager : MonoBehaviour
{
    [Header("Socket for Cassette")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor cassetteSocket;

    private bool cassetteInserted = false;
    private CassetteID currentCassetteID;

    [Header("UI Play Button")]
    public GameObject playCanvas;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Safe Interaction")]
    public VRSafeInteractive safeInteraction;

    private void OnEnable()
    {
        cassetteSocket.selectEntered.AddListener(OnCassetteInserted);
        cassetteSocket.selectExited.AddListener(OnCassetteRemoved);
    }

    private void OnDisable()
    {
        cassetteSocket.selectEntered.RemoveListener(OnCassetteInserted);
        cassetteSocket.selectExited.RemoveListener(OnCassetteRemoved);
    }

    private void Start()
    {
        if (playCanvas != null)
            playCanvas.SetActive(false);
    }

    private void OnCassetteInserted(SelectEnterEventArgs args)
    {
        cassetteInserted = true;

        GameObject cassetteObj = args.interactableObject.transform.gameObject;
        currentCassetteID = cassetteObj.GetComponent<CassetteID>();

        if (currentCassetteID != null)
        {
            audioSource.clip = currentCassetteID.audioClipToPlay;
        }

        if (playCanvas != null)
            playCanvas.SetActive(true);
    }

    private void OnCassetteRemoved(SelectExitEventArgs args)
    {
        cassetteInserted = false;
        currentCassetteID = null;

        if (playCanvas != null)
            playCanvas.SetActive(false);
        audioSource.clip = null;
    }

    public void PlayAudio()
    {
        if (!cassetteInserted)
        {
            return;
        }
        if (audioSource.clip == null){
            return;
        }
        audioSource.Play();
        if (playCanvas != null)
            playCanvas.SetActive(false);
        StartCoroutine(WaitForAudioEnd());
    }

    private IEnumerator WaitForAudioEnd()
    {
        while (audioSource.isPlaying)
            yield return null;
        if (safeInteraction != null)
            safeInteraction.EnableSafeInteraction();
    }
}
