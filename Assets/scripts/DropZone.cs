using UnityEngine;

public class DropZone : MonoBehaviour
{

    public GameObject Brick;
    private bool contain = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rentré");
        if (!contain)
        {
            Brick = other.gameObject;
            other.attachedRigidbody.isKinematic = true;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
        contain = true;
    }

    private void OnTriggerStay(Collider other)
    {
        contain = true;
    }

    private void OnTriggerExit(Collider other)
    {
        contain = false;
    }
}


