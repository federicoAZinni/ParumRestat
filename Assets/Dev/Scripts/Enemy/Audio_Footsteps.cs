using UnityEngine;

public class Audio_Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public float stepInterval = 2f; // cada 2 segundos

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stepInterval)
        {
            if (audioSource != null && footstepClip != null)
                audioSource.PlayOneShot(footstepClip);

            timer = 0f;
        }
    }
}
