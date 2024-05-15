using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class StartObject : MonoBehaviour
{
    [SerializeField] ComputeShader computeShader;
    [SerializeField] ShaderMemoryInformationScriptableObject shaderInfo;

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
