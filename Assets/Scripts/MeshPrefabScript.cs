using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshPrefabScript : MonoBehaviour
{
    public int Row = 8;
    public int Col = 5;
    public Transform BlockPrefab;
    private Dictionary<string, Transform> BlockList = new Dictionary<string, Transform>();
    private void Start()
    {
        SetMeshUI();
    }

    public void SetMeshUI()
    {
        var maxNumber = Row > Col ? Row : Col;
        var meshWidth = transform.GetComponent<RectTransform>().rect.width;
        var length = (meshWidth- (maxNumber-1)*1) / maxNumber;
        transform.GetComponent<GridLayoutGroup>().cellSize = new Vector2(length, length);
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(length * Col+ (maxNumber-1)*1, length * Row+ (maxNumber-1) * 1);
        for (var i = 1; i <= Row; i++)
        {
            for (var j = 1; j <= Col; j++)
            {
                var block = Instantiate(BlockPrefab, transform);
                block.gameObject.name = "block_" + i + "_" + j;
                BlockList.Add(block.gameObject.name, block);
            }
        }
    }
    public void ClearMeshUI()
    {
        foreach (KeyValuePair<string, Transform> kv in BlockList)
        {
            Destroy(kv.Value);
        }
        BlockList.Clear();
    }

    public Dictionary<string, Transform> GetCheckedBlock()
    {
        return BlockList.Where(node => node.Value.GetComponent<Toggle>().isOn).ToDictionary(node=>node.Key, node=>node.Value);
    }
}
