using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [Header("UI / Audio")]
    [SerializeField] private GameObject returnToLiftUI;
    [SerializeField] private AudioSource liftBellAudio;

    private bool _newspaperRead;
    private bool _badgeTaken;
    private bool _signPlaced;
    private bool _allCompleted;

    public bool AllMissionsCompleted => _allCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (returnToLiftUI != null)
            returnToLiftUI.SetActive(false);
    }

    public void MarkNewspaperRead()
    {
        if (_newspaperRead) return;
        _newspaperRead = true;
        CheckAll();
    }

    public void MarkBadgeTaken()
    {
        if (_badgeTaken) return;
        _badgeTaken = true;
        CheckAll();
    }

    public void MarkSignPlaced()
    {
        if (_signPlaced) return;
        _signPlaced = true;
        CheckAll();
    }

    private void CheckAll()
    {
        if (_allCompleted) return;

        if (_newspaperRead && _badgeTaken && _signPlaced)
        {
            _allCompleted = true;

            if (liftBellAudio != null)
                liftBellAudio.Play();

            if (returnToLiftUI != null)
                returnToLiftUI.SetActive(true);

            Debug.Log("Все миссии выполнены. Вернись в лифт.");
        }
    }
}