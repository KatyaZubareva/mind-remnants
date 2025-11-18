using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Ans; 
    private string Answer = "1309";

    public ElevatorDoorManager elevatorManager; 
    public AudioClip correctClip; 
    public AudioClip wrongClip;    
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
    }

    public void Number(int number)
    {
        if (Ans.text.Length < 4) 
        {
            Ans.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            Ans.text = "Correct";
            if (correctClip != null) audioSource.PlayOneShot(correctClip);
            if (elevatorManager != null)
            {
                elevatorManager.OpenDoors();
            }
        }
        else
        {
            if (wrongClip != null) audioSource.PlayOneShot(wrongClip);
            Ans.text = "Invalid";
            StartCoroutine(ClearAfterDelay(1f)); 
        }
    }
    public void Backspace()
    {
        if (Ans.text.Length > 0 && Ans.text != "Invalid" && Ans.text != "Correct")
        {
            Ans.text = Ans.text.Substring(0, Ans.text.Length - 1);
        }
    }
    IEnumerator ClearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ans.text = ""; 
    }
}