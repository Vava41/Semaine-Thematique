using UnityEngine;

public class DropZone : MonoBehaviour
{
    private bool contain= false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rentré");
        if (!contain)
        {
            other.transform.SetParent(this.gameObject.transform);
        }
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;
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