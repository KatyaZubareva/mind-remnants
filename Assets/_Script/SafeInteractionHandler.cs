using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VRSafeInteractive : UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable
{
    [Header("UI Settings")]
    public GameObject notificationUI; 
    public TextMeshProUGUI notificationTextDisplay;
    public string notificationMessage = "Open";

    [Header("Safe Settings")]
    public Animator safeAnimator; 
    private bool isOpen = false;
    private bool isReadyToInteract = false; 

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(UnlockSafe);
        hoverEntered.AddListener(ShowNotification);
        hoverExited.AddListener(HideNotification);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(UnlockSafe);
        hoverEntered.RemoveListener(ShowNotification);
        hoverExited.RemoveListener(HideNotification);
    }
    void Start()
    {
        if (notificationUI != null) notificationUI.SetActive(false);
        enabled = false; 
    }

    public void EnableSafeInteraction()
    {
        isReadyToInteract = true;
        enabled = true;
    }


    private void UnlockSafe(SelectEnterEventArgs args)
    {
        if (isOpen || !isReadyToInteract) return;
        isOpen = true;
        if (safeAnimator != null)
        {
            safeAnimator.SetBool("IsOpen", true);
        }
        HideNotification(null);
    }

    private void ShowNotification(HoverEnterEventArgs args)
    {
        if (isOpen) return;
        if (notificationUI != null)
        {
            notificationUI.SetActive(true);
            if (notificationTextDisplay != null)
            {
                notificationTextDisplay.text = notificationMessage;
            }
        }
    }

    private void HideNotification(HoverExitEventArgs args)
    {
        if (notificationUI != null)
        {
            notificationUI.SetActive(false);
        }
    }
}