using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera playerCamera; // R�f�rence � la cam�ra du joueur
    public float pickupRange = 3f; // Distance maximale de ramassage
    public float throwForce = 5f; // Force de lancer (optionnel)
    public Transform holdPosition; // Position o� l'objet est maintenu

    private Rigidbody heldObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            if (hit.rigidbody != null && !hit.rigidbody.isKinematic) // V�rifier que l'objet a un Rigidbody
            {
                heldObject = hit.rigidbody;
                heldObject.useGravity = false;
                heldObject.linearDamping = 10;
                heldObject.transform.parent = holdPosition;
                heldObject.transform.position = holdPosition.position;
            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.linearDamping = 1;
            heldObject.transform.parent = null;
            heldObject = null;
        }
    }

    void FixedUpdate()
    {
        if (heldObject != null)
        {
            Vector3 moveDirection = (holdPosition.position - heldObject.transform.position);
            heldObject.linearVelocity = moveDirection * 10f;
        }
    }
}