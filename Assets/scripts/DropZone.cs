using UnityEngine;

public class DropZone : MonoBehaviour
{
    private Rigidbody storedObject;
    public Transform storedPosition; // Position de stockage de l'objet
    public Camera playerCamera; // Cam�ra du joueur
    public float interactRange = 3f; // Distance d'interaction
    public ObjectPickup OP; // R�f�rence au script ObjectPickup
    public Transform holdPosition;

    private bool isOnCooldown = false;
    private float cooldownTime = 0.5f;
    private float cooldownTimer = 0f;
    public bool isHoldingObject = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown) // Clic gauche pour r�cup�rer, sans spam
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
            {
                if (hit.collider.gameObject == gameObject && storedObject != null)
                {
                    RemoveObject();
                }
            }
        }

        // Gestion du cooldown pour �viter le spam
        if (isOnCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                isOnCooldown = false;
                cooldownTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (storedObject == null && other.attachedRigidbody != null && !isOnCooldown)
        {
            storedObject = other.attachedRigidbody;
            storedObject.useGravity = false;
            storedObject.isKinematic = true;
            storedObject.transform.position = storedPosition.position;
            storedObject.transform.rotation = storedPosition.rotation;
            storedObject.transform.parent = storedPosition;
        }
    }

    void RemoveObject()
    {
        if (storedObject != null && !isHoldingObject) // V�rifie si on n'a pas d�j� un objet en main
        {
            Debug.Log("Retrait de l'objet : " + storedObject.name);

            // Lib�rer l�objet de la case
            storedObject.transform.SetParent(null);

            // D�sactiver la gravit� et les collisions pour qu'il reste stable dans la main
            storedObject.useGravity = false;
            storedObject.isKinematic = true;

            // Placer l�objet dans la main du joueur
            storedObject.transform.position = holdPosition.position;
            storedObject.transform.rotation = holdPosition.rotation;
            storedObject.transform.parent = holdPosition;

            // V�rifier que ObjectPickup est bien assign� avant d�appeler TryPickupObject()
            if (OP == null)
            {
                Debug.LogError("ObjectPickup n'est pas assign� !");
                return;
            }

            // Passer l�objet � ObjectPickup pour que le joueur puisse le l�cher
            OP.TryPickupObject();
            isHoldingObject = true; // Indique qu'un objet est en main

            // Supprimer la r�f�rence de la case
            storedObject = null;
        }
    }
}
