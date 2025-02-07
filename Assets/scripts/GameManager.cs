using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Neux> liste;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Neux neux in liste)
        {
            print("neux : "+neux);
            neux.isWillDestroy = neux.isDestroyFutur();
        }
        foreach (Neux neux in liste)
        {
            if (neux.isWillDestroy)
            {
                neux.destroy();
            }
        }
        foreach (Neux neux in liste)
        {
            neux.Gravity();
        }
    }

}