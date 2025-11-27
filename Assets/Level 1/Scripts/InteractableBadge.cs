using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class InteractableBadge : MonoBehaviour
{
    [SerializeField] private GameObject badgeUIPanel;
    [SerializeField] private TextMeshProUGUI badgeText;

    [TextArea]
    [SerializeField] private string text =
        "Andrew Vlasov\nSystem Engineer\nHelion Dynamics";

    private XRGrabInteractable _grab;
    private bool _missionCounted;

    private void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
        if (badgeUIPanel != null)
            badgeUIPanel.SetActive(false);
        if (badgeText != null)
            badgeText.text = text;
    }

    private void OnEnable()
    {
        _grab.selectEntered.AddListener(OnSelectEntered);
        _grab.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        _grab.selectEntered.RemoveListener(OnSelectEntered);
        _grab.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (badgeUIPanel != null)
            badgeUIPanel.SetActive(true);

        if (!_missionCounted && MissionManager.Instance != null)
        {
            _missionCounted = true;
            MissionManager.Instance.MarkBadgeTaken();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (badgeUIPanel != null)
            badgeUIPanel.SetActive(false);
    }
}