using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PCSoundProximity : MonoBehaviour
{
    public Transform player;          // Asigna aquí el transform del jugador 
    public float maxDistance = 10f;   // Distancia máxima a la que se escucha
    public float minVolume = 0f;      // Volumen mínimo
    public float maxVolume = 1f;      // Volumen máximo

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, volume);
    }
}
