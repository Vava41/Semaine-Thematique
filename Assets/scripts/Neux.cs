using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]


public class Neux : MonoBehaviour
{


    public bool asBrick;
    public bool isWillDestroy = false;
    public bool isRun = false;
    public Neux cibleNeux;
    public bool var = true;
    private float timer = -1;
    public int timer_max;
    private List<LineRenderer> lineRenderers;
    public bool test;
    private GameObject test_game;
    public int Number_sol;
    // Propriétés publiques pour modifier la ligne dans l'inspecteur
    public Material lineMaterial;    // Matériau de la ligne
    public float lineWidth = 0.1f;   // Épaisseur de la ligne


    public float smoothTime = 0.3f; // Temps pour atteindre la position
    private Vector3 velocity = Vector3.zero;
    public List<Neux> liste;
    private bool drapeau_MoveTargetTowardsSelf = false;
    private Vector3 Target_MoveTargetTowardsSelf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Définir l'épaisseur à la fin


        foreach (Neux neux in liste)
        {
            AddNeux(neux);
            print("(neux,neux) : " + this + "," + neux);
        }
        if (test)
        {
            LanceMove(liste[0]);
        }
    }

    public bool AsBrick()
    {
        foreach (Transform game in transform)
        {
            if (game.tag == "Brick")
            {
                return true;
            }
        }
        return false;
    }

    /*public string addColorNeux(Neux neux_)
    {
        list
    }*/


    // Update is called once per framet
    void Update()
    {
        if (AsBrick())
        {
            gameObject.GetComponent<DropZone>().contain = true;
        }
        asBrick = AsBrick();
        Gravity();
        if (drapeau_MoveTargetTowardsSelf)
        {
            if (timer > -1)
            {
                test_game.transform.position = Vector3.SmoothDamp(test_game.transform.position, Target_MoveTargetTowardsSelf, ref velocity, smoothTime);

                timer += Time.deltaTime;
                if (timer > timer_max)
                {
                    test_game.transform.position = Target_MoveTargetTowardsSelf;
                    timer = -1;
                    drapeau_MoveTargetTowardsSelf = false;
                    isRun = false;
                    cibleNeux.isfinich();
                }
            }
        }
    }
    public string addColor()
    {
        return gameObject.GetComponent<DropZone>().couleur;
    }
    void DrawLineBetweenPoints(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject();
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
        lineRenderer.startWidth = lineWidth;  // Définir l'épaisseur au début
        lineRenderer.endWidth = lineWidth;
        // Définir les positions de départ et de fin de la ligne
        lineRenderer.positionCount = 2;   // La ligne a 2 points (start et end)
        lineRenderer.SetPosition(0, start);   // Point de départ
        lineRenderer.SetPosition(1, end);     // Point d'arrivée
    }

    public GameObject GetBrick()//retourne la Brick possèder
    {
        foreach (Transform game in transform)
        {
            if (game.tag == "Brick")
            {
                return game.gameObject;
            }
        }
        return null;
    }
    public void SetBrick(GameObject brick)//prend la Brick et la mais à la place de la Brick précédente, sans détruir la Brick préssédente
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
            brick.transform.SetParent(gameObject.transform);
            brick.transform.SetAsFirstSibling(); // Place l'enfant en première position
        }
        else
        {
            brick.transform.SetParent(gameObject.transform);
            brick.transform.SetAsFirstSibling(); // Place l'enfant en première position

        }
    }
    public void DestroyBrickFiliation()//Enlève la Brick de la filiation
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
        }
    }

    public void isfinich()
    {
        isRun = false;
    }
    public void destroy()
    {
        if (!isRun)
        {
            foreach (Transform game in transform)
            {
                if (game.tag != "Sphère")
                {
                    Destroy(game.gameObject);
                }
            }
        }

    }

    public void MoveTargetTowardsSelf(Vector3 target, Neux cibleNeux)
    {
        drapeau_MoveTargetTowardsSelf = true;
        Target_MoveTargetTowardsSelf = target;
        test_game = GetBrick();
        this.cibleNeux = cibleNeux;
    }
    public void LanceMove(Neux neu)//Prend le numéraux du neux relier et envoie la Brick vers vous
    {
        if (!isRun && !neu.isRun)
        {
            print("tttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            isRun = true;
            neu.isRun = true;
            neu.timer = 0;
            neu.MoveTargetTowardsSelf(transform.position, this);
            this.SetBrick(neu.GetBrick());
            neu.DestroyBrickFiliation();
        }
    }
    public bool isDestroyFutur()
    {
        if (AsBrick() && !isRun)
        {
            if (addColor() == "rouge")
            {
                for (int i = 0; i < liste.Capacity; i++)
                {
                    if (liste[i].addColor() == "rouge")
                    {
                        return true;
                    }
                }
                return false;
            }

            if (addColor() == "jaune")
            {
                for (int i = 0; i < liste.Capacity; i++)
                {
                    if (liste[i].AsBrick())
                    {
                        return false;
                    }
                }
                return true;
            }
            if (addColor() == "vert")
            {
                for (int i = 0; i < liste.Capacity; i++)
                {
                    if (liste[i].addColor() == "vert")
                    {
                        return true;
                    }
                }
                return false;
            }
            if (addColor() == "bleu")
            {
                for (int i = 0; i < liste.Capacity; i++)
                {
                    if (liste[i].AsBrick())
                    {
                        for (int j = 0; j < liste.Capacity; j++)
                        {
                            if (liste[j].AsBrick())
                            {
                                if (!string.Equals(liste[i].addColor(), liste[j].addColor()))
                                {
                                    print("liste[i].addColor() : " + liste[i] + "," + liste[i].addColor() + "," + liste[i].AsBrick() + "  liste[j].addColor()  :  " + liste[j] + "," + liste[j].addColor() + "," + liste[i].AsBrick());
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public void AddNeux(Neux neux)
    {
        DrawLineBetweenPoints(transform.position, neux.transform.position);
    }
    public void Gravity()//A chaque moments
    {
        if (!AsBrick())
        {
            foreach (var item in liste)
            {
                if (item.Number_sol > Number_sol)
                {
                    if (item.AsBrick())
                    {
                        LanceMove(item);
                    }
                }
            }
        }
    }
}