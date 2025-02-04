using UnityEngine;

public class DropZone : MonoBehaviour
{
    public ObjectPickup OP;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bloc"))
        {
            OP.DropObject();
            Debug.Log("rentré");
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
    }
}
