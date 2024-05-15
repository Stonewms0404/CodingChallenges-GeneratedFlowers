using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class StartObject : MonoBehaviour
{
    /*GameObject obj;
    [SerializeField] int points;
    [SerializeField] float width, amplitude = 1, frequency = 1;
    [SerializeField] Vector2 pos;

    private void Update()
    {
        if (obj != null) Destroy(obj);
        obj = Create.Sine(pos, (int)points, width, new(Shader.Find("Unlit/Color")), amplitude, frequency);
    }*/
    void Awake()
    {
        System.Random rand = new();
        int flowerCount = rand.Next(15, 40);

        GameObject flowerContainer = new("FlowerContainer");

        for (int i = 0; i < flowerCount; i++)
        {
            new GameObject("Flower " + (i + 1)).AddComponent<InitializeFlower>().transform.SetParent(flowerContainer.transform);
        }


        Destroy(gameObject);
    }

}
