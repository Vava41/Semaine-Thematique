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
            if (other.CompareTag("Brick"))
            {
                
                contain = true;
            }
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
        contain = false;
        other.transform.SetParent(null);
    }
}