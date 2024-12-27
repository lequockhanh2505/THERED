using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        //// Lấy AudioSource từ đối tượng gắn script này
        //audioSource = GetComponent<AudioSource>();
        //if (audioSource == null)
        //{
        //    Debug.LogError("No AudioSource found on " + gameObject.name);
        //}
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    //public void PlaySoundWithFade(AudioClip clip, float fadeDuration)
    //{
    //    if (audioSource != null && clip != null)
    //    {
    //        StartCoroutine(FadeInSound(clip, fadeDuration));
    //    }
    //}

    //private IEnumerator FadeInSound(AudioClip clip, float duration)
    //{
    //    audioSource.clip = clip;
    //    audioSource.volume = 0f;
    //    audioSource.Play();

    //    float startVolume = 0f;
    //    while (audioSource.volume < 1f)
    //    {
    //        audioSource.volume += Time.deltaTime / duration;
    //        yield return null;
    //    }
    //}

}
