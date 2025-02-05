using UnityEngine;

public class scritpBoite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick")
        {
            other.transform.position = transform.position;
            print("détecter");

        }
    }
}
