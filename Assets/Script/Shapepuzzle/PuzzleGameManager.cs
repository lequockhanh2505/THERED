using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    [SerializeField] private GameObject moldShapePrefabs;
    [SerializeField] private GameObject moldShapePosition;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject[] chests;
    private List<SnapZone> snapZones = new List<SnapZone>();
    private List<GameObject> collectedBlocks = new List<GameObject>();

    public static event Action OnPuzzleCompleted;

    private void OnEnable()
    {
        foreach (var chest in chests)
        {
            var chestController = chest.GetComponent<ChestController>();
            if (chestController != null)
            {
                chestController.OnChestOpened += HandleChestOpened;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var chest in chests)
        {
            var chestController = chest.GetComponent<ChestController>();
            if (chestController != null)
            {
                chestController.OnChestOpened -= HandleChestOpened;
            }
        }
    }

    private void Start()
    {
        InitializeSnapZones(moldShapePosition.transform.position);
    }

    private void Update()
    {
        CheckCompletion();
    }

    private void HandleChestOpened(GameObject[] items)
    {
        foreach (var item in items)
        {
            item.SetActive(true);
        }
    }

    private void InitializeSnapZones(Vector3 position)
    {
        GameObject instantiatedMoldShape = Instantiate(moldShapePrefabs, position, Quaternion.identity, transform);

        SnapZone[] snapZoneComponents = instantiatedMoldShape.GetComponentsInChildren<SnapZone>();

        foreach (SnapZone snapZone in snapZoneComponents)
        {
            snapZones.Add(snapZone);
        }
    }

    private void CheckCompletion()
    {
        foreach (var snapZone in snapZones)
        {
            if (!snapZone.isOccupied || snapZone.requiredBlockType != GetBlockTypeInZone(snapZone))
            {
                return;
            }
        }

        OnPuzzleCompleted?.Invoke();
    }

    private int GetBlockTypeInZone(SnapZone snapZone)
    {
        Collider2D collider = Physics2D.OverlapCircle(snapZone.transform.position, 0.01f);
        if (collider != null)
        {
            Block block = collider.GetComponent<Block>();
            if (block != null)
            {
                return block.blockType;
            }
        }
        return -1;
    }

    public void UpdateSnapZone(SnapZone snapZone)
    {
        if (snapZones.Contains(snapZone))
        {
            Debug.Log($"SnapZone {snapZone.name} updated: isOccupied = {snapZone.isOccupied}");
        }
    }
}
