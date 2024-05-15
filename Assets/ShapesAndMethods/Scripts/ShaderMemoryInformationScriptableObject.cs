using UnityEngine;

[CreateAssetMenu(fileName = "ShaderMemoryInformation",
    menuName = "Shapes And Methods/ShaderMemoryInformation")]
public class ShaderMemoryInformationScriptableObject : ScriptableObject
{
    public int bytesForShader, numOfFlowers;

    public ComputeShader computeShader;

    public FlowerStruct[] flowers;
    public LineRenderer[] petals, stems;
}
