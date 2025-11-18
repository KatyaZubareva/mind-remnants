using UnityEngine;

public class PuzzleFolder : MonoBehaviour
{
    public int folderID;

    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) 
        {
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }
}