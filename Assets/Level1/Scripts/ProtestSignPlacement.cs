using UnityEngine;

public class ProtestSignPlacement : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;
    [SerializeField] private GameObject placedHint;

    private bool _used;

    private void Start()
    {
        if (placedHint != null)
            placedHint.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_used) return;

        ProtestSign sign = other.GetComponent<ProtestSign>();
        if (sign == null) return;

        if (snapPoint != null)
        {
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;
        }

        var grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;

        if (placedHint != null)
            placedHint.SetActive(true);

        _used = true;

        if (MissionManager.Instance != null)
            MissionManager.Instance.MarkSignPlaced();
    }
}