using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorReturnTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Level2";
    [SerializeField] private ElevatorDoorController elevator;
    [SerializeField] private float delayBeforeLoad = 2f;

    private bool _loading;

    private void OnTriggerEnter(Collider other)
    {
        if (_loading) return;

        if (!other.CompareTag("Player")) return;

        if (MissionManager.Instance == null || !MissionManager.Instance.AllMissionsCompleted)
        {
            Debug.Log("Миссии ещё не выполнены, лифт пока не активен.");
            return;
        }

        _loading = true;
        StartCoroutine(LoadNext());
    }

    private IEnumerator LoadNext()
    {
        if (elevator != null)
            elevator.CloseDoors();

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(nextSceneName);
    }
}