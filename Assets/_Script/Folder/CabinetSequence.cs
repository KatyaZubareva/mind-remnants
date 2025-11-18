using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class SocketData
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;
    public int requiredFolderID;
    public Vector3 ejectForce = new Vector3(0, 2, -3);
}

public class CabinetSequence : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator cabinetAnimator;

    [Header("Audio")]
    public AudioClip openCabinetSound;
    private AudioSource audioSource;

    [Header("Socket Configuration")]
    public List<SocketData> allSocketsData;

    private bool isUnlocked = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        foreach (var data in allSocketsData)
        {
            if (data.socketInteractor != null)
            {
                data.socketInteractor.selectEntered.AddListener(_ => CheckWinCondition());
                data.socketInteractor.selectExited.AddListener(OnInteractableReleased);
            }
        }
    }

    public void CheckWinCondition()
    {
        if (isUnlocked) return;

        int correctPlacements = 0;
        int totalPlaced = 0;

        foreach (var data in allSocketsData)
        {
            if (data.socketInteractor is UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor selectInteractor)
            {
                var placedInteractable =
                    UnityEngine.XR.Interaction.Toolkit.Interactors.XRSelectInteractorExtensions.GetOldestInteractableSelected(selectInteractor);

                if (placedInteractable != null)
                {
                    totalPlaced++;

                    PuzzleFolder folder = placedInteractable.transform.GetComponent<PuzzleFolder>();
                    if (folder != null && folder.folderID == data.requiredFolderID)
                        correctPlacements++;
                }
            }
        }

        if (totalPlaced == allSocketsData.Count && correctPlacements == allSocketsData.Count)
        {
            UnlockCabinet();
        }
        else if (totalPlaced == allSocketsData.Count)
        {
            StartCoroutine(EjectAllFoldersCoroutine());
        }
    }

    private void UnlockCabinet()
    {
        isUnlocked = true;

        if (cabinetAnimator != null)
            cabinetAnimator.SetBool("IsOpen", true);

        if (openCabinetSound != null)
            audioSource.PlayOneShot(openCabinetSound);

        enabled = false;
    }

    private IEnumerator EjectAllFoldersCoroutine()
    {
        foreach (var data in allSocketsData)
        {
            var socket = data.socketInteractor;
            var interactable =
                UnityEngine.XR.Interaction.Toolkit.Interactors.XRSelectInteractorExtensions.GetOldestInteractableSelected(socket);

            if (interactable != null)
            {
                if (socket.interactionManager != null)
                    socket.interactionManager.SelectExit(socket, interactable);
                yield return null;

                Transform folder = interactable.transform;
                Rigidbody rb = folder.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;

                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    Vector3 forceDir = data.socketInteractor.transform.forward ;
                    rb.AddForce(forceDir * 4f + data.ejectForce, ForceMode.Impulse);
                    rb.AddTorque(Random.insideUnitSphere * 1.5f, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnInteractableReleased(SelectExitEventArgs args)
    {
        Rigidbody rb = args.interactableObject.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
