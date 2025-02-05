using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Neux : MonoBehaviour
{
    public float smoothTime = 0.3f; // Temps pour atteindre la position
    private Vector3 velocity = Vector3.zero;
    public List<Neux> list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject GetBrick()//retourne la Brick possèder
    {
        return gameObject.AddComponent<DropZone>().Brick;
    }
    public void SetBrick(GameObject brick)//modifie la Brick possèder
    {
        gameObject.AddComponent<DropZone>().Brick = brick;
    }
    public void DestroyBrick()
    {
        gameObject.AddComponent<DropZone>().Brick = null;
    }
    public void addBrick(ObjectPickup ob)
    {
        ob.transform.position = transform.position;
    }


    public void destroy()
    {
        if (gameObject.GetComponent<DropZone>().Brick == null)
        {
            Debug.Log("Brick null !");
        }
        else
        {
            Destroy(gameObject.GetComponent<DropZone>().Brick);
        }
    }

    public void MoveTargetTowardsSelf(Vector3 target)
    {
        Vector3.SmoothDamp(target, transform.position, ref velocity, smoothTime);
    }
    public void LanceMove(int i)//Prend le numéraux du neux relier et envoie la Brick
    {
        list[i].MoveTargetTowardsSelf(transform.position);
        list[i].SetBrick(this.GetBrick());
        DestroyBrick();
    }
    public void AddNeux(Neux neux)
    {
        list.Add(neux);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
