using UnityEngine;

[DisallowMultipleComponent]
public class NoteData : MonoBehaviour
{
    [Tooltip("Уникальная дата/время заметки.")]
    public string isoDate = "2025-11-01T12:00:00Z";
    [TextArea]
    public string noteText = "Текст из дневника...";

    public System.DateTime GetDateTime()
    {
        System.DateTime dt;
        if (System.DateTime.TryParse(isoDate, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out dt))
            return dt;

        return System.DateTime.UtcNow;
    }
}
