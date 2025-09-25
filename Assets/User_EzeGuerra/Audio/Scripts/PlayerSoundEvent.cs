using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    public void PlayFootstepOnFrame()
    {
        PlayerSoundSystem.PlaySonido(TipoSonido.PASOS);
    }
}
