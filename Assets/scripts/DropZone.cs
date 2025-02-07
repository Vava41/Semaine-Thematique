using UnityEngine;

public class DropZone : MonoBehaviour
{
    private bool contain= false;
    public string couleur;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rentré");
        if (!contain)
        {
            other.transform.SetParent(this.gameObject.transform);
            other.GetComponentInParent<Rigidbody>().isKinematic = true;
            other.GetComponentInParent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;
        contain = true;
        other.transform.SetParent(this.gameObject.transform);
        if (contain == true)
        {
            couleur = other.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInParent<Rigidbody>().isKinematic = false;
        other.GetComponentInParent<Rigidbody>().useGravity = true;
        contain = false;
        other.transform.SetParent(null);
    }
}