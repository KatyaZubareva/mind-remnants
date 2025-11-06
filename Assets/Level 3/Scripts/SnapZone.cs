using UnityEngine;

public class SnapZone : MonoBehaviour
{
    [Tooltip("Индекс слота (0..N-1)")]
    public int slotIndex = 0;
    [Tooltip("Позиция снэпа внутри слота")]
    public Transform snapPoint;

    private void Awake()
    {
        if (snapPoint == null) snapPoint = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        var note = other.GetComponent<NoteData>();
        if (note != null)
        {
            TimelineManager.Instance.PlaceNoteInSlot(note.gameObject, slotIndex, snapPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var note = other.GetComponent<NoteData>();
        if (note != null)
        {
            TimelineManager.Instance.RemoveNoteFromSlot(note.gameObject, slotIndex);
        }
    }
}
