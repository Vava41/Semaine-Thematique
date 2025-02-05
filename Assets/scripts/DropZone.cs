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
            other.attachedRigidbody.isKinematic = true;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            other.transform.SetParent(this.gameObject.transform);
        }
        contain = true;
    }

    private void OnTriggerStay(Collider other)
    {
        contain = true;
        other.transform.SetParent(this.gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        contain = false;
        other.transform.SetParent(null);
    }

}


