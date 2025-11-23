using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        // Hent animasjonslengden fra Animator
        float length = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        // Slett objektet når animasjonen er ferdig
        Destroy(gameObject, length);
    }
}
