using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.9f, 1.1f);
    }
}
