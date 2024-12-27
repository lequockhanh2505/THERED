using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public int blockType;
    private bool isDragging = false;
    [SerializeField] private GameObject selectedObject = null;
    private SpriteRenderer[] cellRenderers;
    [SerializeField] private List<SnapZone> validSnapZones = new List<SnapZone>();
    RaycastHit2D hit;
    int cellCount;

    private void Awake()
    {
        cellRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        cellCount = cellRenderers.Length;
    }

    private void Update()
    {
        Drag();
        Drop();

        if (isDragging)
        {
            ResetOccupiedSnapZones();
            RotationBlock();
        }
    }

    private void RotationBlock()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            selectedObject.gameObject.transform.rotation *= Quaternion.Euler(0, 0, 90f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            selectedObject.gameObject.transform.rotation *= Quaternion.Euler(0, 0, -90f);
        }
    }

    private void LateUpdate()
    {
        UpdatePositionBlock();
    }

    private void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D i in hits)
            {
                if (i.collider.CompareTag("Draggable"))
                {
                    hit = i;
                    break;
                }
            }

            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                selectedObject = gameObject;

                SetCellOrderInLayer(10);
            }
        }
    }

    private void UpdatePositionBlock()
    {
        if (isDragging && selectedObject == gameObject)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            CheckSnapZones();
        }
    }

    private void Drop()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (AreAllCellsValid())
            {
                SnapToPosition();
            }

            selectedObject = null;

            SetCellOrderInLayer(7);
        }
    }

    private void CheckSnapZones()
    {
        ResetOccupiedSnapZones();
        validSnapZones.Clear();
        HashSet<SnapZone> usedSnapZones = new HashSet<SnapZone>();

        for (int i = 0; i < cellCount; i++)
        {
            Vector2 cellWorldPosition = cellRenderers[i].transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(cellWorldPosition, 0.01f);
            SnapZone snapZone = null;

            foreach (var collider in colliders)
            {
                SnapZone zone = collider.GetComponent<SnapZone>();
                if (zone != null && !zone.isOccupied && !usedSnapZones.Contains(zone) && zone.requiredBlockType == blockType)
                {
                    bool isValid = !zone.isOccupied && zone.requiredBlockType == blockType;
                    zone.SetHighlight(true, isValid);

                    if (isValid)
                    {
                        snapZone = zone;
                        break;
                    }
                }
            }

            if (snapZone == null)
            {
                break;
            }
            validSnapZones.Add(snapZone);
            usedSnapZones.Add(snapZone);
            snapZone.isOccupied = true;
        }
    }

    private bool AreAllCellsValid()
    {
        return validSnapZones.Count == cellCount;
    }

    private void SnapToPosition()
    {
        if (AreAllCellsValid())
        {
            Vector3 totalOffset = Vector3.zero;

            for (int i = 0; i < cellCount; i++)
            {
                Vector3 cellPosition = cellRenderers[i].transform.position;
                Vector3 snapZonePosition = validSnapZones[i].transform.position;
                totalOffset += snapZonePosition - cellPosition;
            }

            Vector3 averageOffset = totalOffset / cellCount;
            transform.position += averageOffset;

            // Cập nhật trạng thái SnapZone sau khi căn chỉnh
            foreach (var snapZone in validSnapZones)
            {
                snapZone.isOccupied = true;
            }
        }
        else
        {
            ResetOccupiedSnapZones();
        }
    }

    private void ResetOccupiedSnapZones()
    {
        foreach (var snapZone in validSnapZones)
        {
            snapZone.isOccupied = false;
            snapZone.SetHighlight(false, false);
        }

        validSnapZones.Clear();
    }

    private void SetCellOrderInLayer(int order)
    {
        foreach (var renderer in cellRenderers)
        {
            renderer.sortingOrder = order;
        }
    }
}
