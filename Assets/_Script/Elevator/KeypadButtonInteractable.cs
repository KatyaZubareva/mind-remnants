using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class KeypadButtonInteractable : XRGrabInteractable
{
    private Pose startingPose;
    public AudioClip clickClip;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    protected override void Grab()
    {
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        audioSource.PlayOneShot(clickClip);
        startingPose = new Pose(transform.position, transform.rotation);
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected)
        {
            transform.position = startingPose.position;
            transform.rotation = startingPose.rotation;
        }
    }
}