using UnityEngine;

public class ElevatorDoorController : MonoBehaviour
{
    [SerializeField] private Animator leftDoorAnimator;
    [SerializeField] private Animator rightDoorAnimator;

    private static readonly int OpenHash = Animator.StringToHash("Open");
    private static readonly int CloseHash = Animator.StringToHash("Close");

    public void OpenDoors()
    {
        if (leftDoorAnimator != null)
            leftDoorAnimator.SetTrigger(OpenHash);
        if (rightDoorAnimator != null)
            rightDoorAnimator.SetTrigger(OpenHash);
    }

    public void CloseDoors()
    {
        if (leftDoorAnimator != null)
            leftDoorAnimator.SetTrigger(CloseHash);
        if (rightDoorAnimator != null)
            rightDoorAnimator.SetTrigger(CloseHash);
    }
}