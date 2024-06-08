using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public BaseUnit OccupiedUnit;
    public bool isEmpty => OccupiedUnit == null;

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if(unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
