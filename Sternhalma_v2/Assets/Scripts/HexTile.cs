using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject highlightOnSelect;

    public BaseUnit OccupiedUnit;
    public bool isEmpty => this.OccupiedUnit == null;

    public Vector3 posEasy;
    public Vector3 posHard;

    private void OnMouseEnter()
    {
        if (GameManager.Instance.GameState == GameState.PlayerTurn)
        {
            _highlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        this.OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void RemoveUnit(BaseUnit unit)
    {
        unit.OccupiedTile = null;
        this.OccupiedUnit = null;
        Destroy(unit.gameObject);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (this.OccupiedUnit == null && selectedTile == null)
        {
            UnitManager.Instance.SetSelectedTile(null);
            return;
        }
        else if (this.OccupiedUnit != null && selectedTile != null)
        {
            UnitManager.Instance.SetSelectedTile(null);
            selectedTile.highlightOnSelect.SetActive(false);
            return;
        }
        else if (this.OccupiedUnit != null && selectedTile == null)
        {
            UnitManager.Instance.SetSelectedTile(this);
            this.highlightOnSelect.SetActive(true);
            return;
        }
        else
        {
            Vector3 selectedPos = selectedTile.posEasy;
            Vector3 currentPos = this.posEasy;

            if ((selectedPos.x == currentPos.x && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 2)) ||
                ((int)Mathf.Abs(currentPos.x - selectedPos.x) == 3) && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 1) &&
                !UnitManager.Instance.currentStatus.ContainsKey(currentPos))
            {
                Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                if (selectedTile.OccupiedUnit.Faction == Faction.Rock)
                {
                    if (midTile.OccupiedUnit != null && midTile.OccupiedUnit.Faction == Faction.Scissor && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Scissor)
                    {
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                    }
                    else
                    {
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        return;
                    }
                }
                else if (selectedTile.OccupiedUnit.Faction == Faction.Paper)
                {
                    if (midTile.OccupiedUnit != null && midTile.OccupiedUnit.Faction == Faction.Rock && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Rock)
                    {
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                    }
                    else
                    {
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        return;
                    }
                }
                else if (selectedTile.OccupiedUnit.Faction == Faction.Scissor)
                {
                    if (midTile.OccupiedUnit != null && midTile.OccupiedUnit.Faction == Faction.Paper && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Paper)
                    {
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                    }
                    else
                    {
                        UnitManager.Instance.SetSelectedTile(null);
                        selectedTile.highlightOnSelect.SetActive(false);
                        return;
                    }
                }
                else
                {
                    UnitManager.Instance.SetSelectedTile(null);
                    selectedTile.highlightOnSelect.SetActive(false);
                    return;
                }
            }
            else
            {
                UnitManager.Instance.SetSelectedTile(null);
                selectedTile.highlightOnSelect.SetActive(false);
                return;
            }
        }
    }
}
