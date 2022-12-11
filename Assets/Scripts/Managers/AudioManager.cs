using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
