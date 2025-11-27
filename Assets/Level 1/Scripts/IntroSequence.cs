using System.Collections;
using TMPro;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI firstLine;
    [SerializeField] private TextMeshProUGUI secondLine;

    [Header("Timing (seconds)")]
    [SerializeField] private float firstDelay = 0.5f;
    [SerializeField] private float secondDelay = 4f;
    [SerializeField] private float openDoorsDelayAfterSecond = 2f;

    [Header("Other")]
    [SerializeField] private AudioSource heartbeatAudio;
    [SerializeField] private ElevatorDoorController elevator;

    private void Start()
    {
        if (firstLine != null) firstLine.alpha = 0f;
        if (secondLine != null) secondLine.alpha = 0f;

        StartCoroutine(RunIntro());
    }

    private IEnumerator RunIntro()
    {
        if (heartbeatAudio != null)
            heartbeatAudio.Play();

        yield return new WaitForSeconds(firstDelay);

        if (firstLine != null)
        {
            firstLine.text = "Господи… что произошло? Где я?.. Я… ничего не помню.";
            firstLine.CrossFadeAlpha(1f, 1.5f, false);
        }

        yield return new WaitForSeconds(secondDelay);

        if (secondLine != null)
        {
            secondLine.text = "На полу что-то лежит…";
            secondLine.CrossFadeAlpha(1f, 1.5f, false);
        }

        yield return new WaitForSeconds(openDoorsDelayAfterSecond);

        if (elevator != null)
            elevator.OpenDoors();
    }
}