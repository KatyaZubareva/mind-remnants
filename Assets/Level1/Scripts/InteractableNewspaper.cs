using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class InteractableNewspaper : MonoBehaviour
{
    [SerializeField] private GameObject newspaperUIPanel;
    [SerializeField] private TextMeshProUGUI newspaperText;

    [TextArea(5, 10)]
    [SerializeField] private string text =
        "13.09 — Неизвестный сбой парализовал город.\n" +
        "13 сентября в нескольких районах города произошёл масштабный сбой. " +
        "Системы связи, освещения и транспорта вышли из строя. Причина инцидента не известна. " +
        "Представители компании Helion Dynamics заявили, что «всё под контролем», " +
        "однако власти не комментируют ситуацию. Жители сообщают о странных звуках " +
        "и световых импульсах перед отключением сети.";

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grab;
    private bool _missionCounted;

    private void Awake()
    {
        _grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (newspaperUIPanel != null)
            newspaperUIPanel.SetActive(false);
        if (newspaperText != null)
            newspaperText.text = text;
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
        if (newspaperUIPanel != null)
            newspaperUIPanel.SetActive(true);

        if (!_missionCounted && MissionManager.Instance != null)
        {
            _missionCounted = true;
            MissionManager.Instance.MarkNewspaperRead();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (newspaperUIPanel != null)
            newspaperUIPanel.SetActive(false);
    }
}