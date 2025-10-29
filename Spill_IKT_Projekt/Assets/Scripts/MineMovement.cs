using UnityEngine;

public class MineMovement : MonoBehaviour
{
    public GameObject Mine; // Dra inn minen-prefaben her i Inspector
    public float speed = 2f; // Hastigheten minene flytter seg med
    public float resetPositionX = -10f; // Når minen forsvinner ut av bildet
    public float startPositionX = 10f; // Hvor den skal starte på nytt

    private GameObject currentMine;

    void Start()
    {
        // Lag en kopi (instans) av prefaben i starten
        if (Mine != null)
        {
            currentMine = Instantiate(Mine, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("⚠️ Du må dra inn minen-prefaben i Inspector på " + gameObject.name);
        }
    }

    void Update()
    {
        if (currentMine == null) return;

        // Flytt minen mot venstre
        currentMine.transform.position += Vector3.left * speed * Time.deltaTime;

        // Hvis den går utenfor bildet, reset posisjonen
        if (currentMine.transform.position.x < resetPositionX)
        {
            ResetMine();
        }
    }

    void ResetMine()
    {
        float randomY = Random.Range(-1.5f, 2.0f); // tilfeldig høyde
        Vector3 newPos = new Vector3(startPositionX, randomY, 0);
        currentMine.transform.position = newPos;
    }
}
