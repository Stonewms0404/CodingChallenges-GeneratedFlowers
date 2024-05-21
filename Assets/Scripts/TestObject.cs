using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
using UnityEngine.WSA;

public class TestObject : MonoBehaviour
{
    public ShaderMemoryInformationScriptableObject SO;
    public ComputeShader shader;

    void Start()
    {
        int flowerCount = Random.Range(8, 50);
        SO.numOfFlowers = flowerCount;
        SO.computeShader = shader;

        SO.petals = new LineRenderer[flowerCount];
        SO.flowers = new FlowerStruct[flowerCount];
        SO.stems = new LineRenderer[flowerCount];

        for (int i = 0; i < flowerCount; i++)
        {
            GameObject flower = new("Flower " + (i + 1));
            flower.SetActive(false);
            flower.AddComponent<GPUFlower>();
            GPUFlower gpuFlower = flower.GetComponent<GPUFlower>();
            gpuFlower.computeShader = shader;
            gpuFlower.SO = SO;
            flower.SetActive(true);
        }
    }

}
