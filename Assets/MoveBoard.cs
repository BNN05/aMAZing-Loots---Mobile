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
        // Vérifie s'il y a eu un toucher sur l'écran
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Gère les différents événements tactiles
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Sauvegarde la position initiale du toucher
                    initialTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Calcule le déplacement depuis la position initiale
                    Vector2 touchDelta = touch.position - initialTouchPosition;

                    // Applique la rotation en fonction du déplacement
                    float rotationAmount = -touchDelta.x * rotationSpeed * Time.deltaTime;
                    transform.Rotate(Vector3.forward, rotationAmount, Space.World);
                    initialTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Réinitialise les variables de rotation
                    initialTouchPosition = Vector2.zero;
                    break;
            }
        }
    }
}
