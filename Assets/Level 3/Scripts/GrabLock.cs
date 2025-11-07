using UnityEngine;


[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class GrabLock : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private bool locked = false;
    private Rigidbody rb;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    public void LockToSlot(Transform parent)
    {
        locked = true;
        grab.enabled = false;
        if (rb) { rb.isKinematic = true; }
        transform.SetParent(parent, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void UnlockFromSlot()
    {
        locked = false;
        grab.enabled = true;
        if (rb) { rb.isKinematic = false; }
        transform.SetParent(null, true);
    }
}
