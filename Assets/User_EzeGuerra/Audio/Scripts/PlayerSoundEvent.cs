using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    public void PlayFootstepOnFrame()
    {
        PlayerSoundSystem.PlaySound(SoundType.FOOTSTEPS);
    }
}
