using UnityEngine;

public class ElevatorDoorManager : MonoBehaviour
{
    public Animator elevatorAnimator;
    public AudioClip openDoorClip;
    private AudioSource audioSource;
    private bool isOpen = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; 
    }

    public void OpenDoors()
    {
        if (isOpen) return;
        isOpen = true;

        if (elevatorAnimator != null)
        {
            elevatorAnimator.SetBool("IsOpen", true);
        }
        if (openDoorClip != null)
        {
            audioSource.PlayOneShot(openDoorClip);
        }
    }
}