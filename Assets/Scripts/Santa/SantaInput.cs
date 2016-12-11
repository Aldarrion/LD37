using UnityEngine;
using System.Collections;

public class SantaInput : MonoBehaviour { 

    [HideInInspector]
    public Collider2D walkableArea;

    // Initializes collider areas and transforms from children in the ORDER as they are in the Hierarchy view
    void Awake()
    {
        walkableArea = GetComponent<Collider2D>();
    }

    // called each frame, checks for mouse clicks on movable areas to move the character there
    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y); // Cast the mouse position to 2D

            if (walkableArea.OverlapPoint(mousePos2D)) //checks whether mouse is in the walkable area
            {
                SantaController.controller.MoveTo(new Vector3(mousePos2D.x, mousePos2D.y));
            }
        }
    }
}
