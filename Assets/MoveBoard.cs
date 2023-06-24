using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoard : MonoBehaviour
{
    private bool isRotating = false;
    public Vector2 initialTouchPosition;
    public float rotationSpeed = 5f;

    void Update()
    {
        // V�rifie s'il y a eu un toucher sur l'�cran
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // G�re les diff�rents �v�nements tactiles
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Sauvegarde la position initiale du toucher
                    initialTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Calcule le d�placement depuis la position initiale
                    Vector2 touchDelta = touch.position - initialTouchPosition;

                    // Applique la rotation en fonction du d�placement
                    float rotationAmount = -touchDelta.x * rotationSpeed * Time.deltaTime;
                    transform.Rotate(Vector3.forward, rotationAmount, Space.World);
                    initialTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // R�initialise les variables de rotation
                    initialTouchPosition = Vector2.zero;
                    break;
            }
        }
    }
}
