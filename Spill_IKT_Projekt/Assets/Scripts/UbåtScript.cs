using UnityEngine;

public class UbåtScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float oppKraft = 7f;
    public float maksOppHastighet = 8f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Legg til kraft oppover så lenge vi ikke går for fort
            if (myRigidbody.linearVelocity.y < maksOppHastighet)
            {
                myRigidbody.AddForce(Vector2.up * oppKraft);
            }
        }
    }
}
