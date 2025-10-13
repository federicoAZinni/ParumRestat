using UnityEngine;

public class Tasa : Item, ISound
{
    [SerializeField] AudioSource _audioSource;
    public override void Interact()
    {
        PlaySound(); //si el player tiene una taza en la mano e intenta interactuar con el teléfono, suena el sonido de la taza
    }                  //capaz unos métodos en IInteractable que bloqueen/desbloqueen la interacción según si tiene las manos ocupadas? -eze

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
