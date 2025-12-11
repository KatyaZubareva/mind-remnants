using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class InteractableNote : MonoBehaviour
{
    [SerializeField] private GameObject noteUIPanel;
    [SerializeField] private TextMeshProUGUI noteText;

    [TextArea]
    [SerializeField] private string text =
        "Адрес: ул. Заводская, 3";

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grab;

    private void Awake()
    {
        _grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (noteUIPanel != null)
            noteUIPanel.SetActive(false);
        if (noteText != null)
            noteText.text = text;
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
        if (noteUIPanel != null)
            noteUIPanel.SetActive(true);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (noteUIPanel != null)
            noteUIPanel.SetActive(false);
    }
}