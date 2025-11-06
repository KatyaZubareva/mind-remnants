using System;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance { get; private set; }

    [Tooltip("Количество слотов на столе")]
    public int slotCount = 3;

    private GameObject[] slots;

    [Header("Settings")]
    public float snapLerp = 1f;
    public Transform[] slotSnapPoints;

    [Header("Decision UI")]
    public GameObject decisionUI;
    public UnityEngine.UI.Button acceptButton;
    public UnityEngine.UI.Button forgetButton;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        Instance = this;
        slots = new GameObject[slotCount];
        if (decisionUI) decisionUI.SetActive(false);

        if (acceptButton != null) acceptButton.onClick.AddListener(OnAccept);
        if (forgetButton != null) forgetButton.onClick.AddListener(OnForget);
    }

    public void PlaceNoteInSlot(GameObject noteObj, int slotIndex, Transform snapPoint)
    {
        if (slotIndex < 0 || slotIndex >= slotCount) return;
        if (slots[slotIndex] != null && slots[slotIndex] != noteObj) return;

        slots[slotIndex] = noteObj;

        Rigidbody rb = noteObj.GetComponent<Rigidbody>();
        if (rb) { rb.isKinematic = true; rb.linearVelocity = Vector3.zero; }

        noteObj.transform.position = snapPoint.position;
        noteObj.transform.rotation = snapPoint.rotation;
        noteObj.transform.SetParent(snapPoint, true);

        CheckAllSlots();
    }

    public void RemoveNoteFromSlot(GameObject noteObj, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slotCount) return;
        if (slots[slotIndex] == noteObj)
        {
            slots[slotIndex] = null;
            Rigidbody rb = noteObj.GetComponent<Rigidbody>();
            if (rb) { rb.isKinematic = false; }
            noteObj.transform.SetParent(null, true);
        }
    }

    private void CheckAllSlots()
    {
        foreach (var s in slots)
        {
            if (s == null) return;
        }
        bool correct = AreSlotsChronological();
        if (decisionUI) decisionUI.SetActive(true);
        if (!correct)
        {
            Debug.Log("Порядок неверный — подсказка");
        }
    }

    private bool AreSlotsChronological()
    {
        DateTime prev = DateTime.MinValue;
        for (int i = 0; i < slotCount; i++)
        {
            var nd = slots[i].GetComponent<NoteData>();
            if (nd == null) return false; // ошибка
            DateTime dt = nd.GetDateTime();
            if (i > 0 && dt < prev) return false;
            prev = dt;
        }
        return true;
    }

    private void OnAccept()
    {
        Debug.Log("Игрок решил: признать");
        SaveDecision(true);

        decisionUI.SetActive(false);
    }

    private void OnForget()
    {
        Debug.Log("Игрок решил: забыть");
        SaveDecision(false);
        decisionUI.SetActive(false);
    }

    private void SaveDecision(bool accepted)
    {
        PlayerPrefs.SetInt("player_decision_accepted", accepted ? 1 : 0);
        PlayerPrefs.Save();
    }

}
