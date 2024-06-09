using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public BaseUnit OccupiedUnit;
    public bool isEmpty => this.OccupiedUnit == null;

    public Vector3 posEasy;
    public Vector3 posHard;

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
        //Debug.Log(unit);
        //unit.SetTile(null);
        unit.OccupiedTile = null;
        this.OccupiedUnit = null;
        Destroy(unit.gameObject);
    }

    //public void ValidMove(HexTile selectedTile, HexTile midTile)
    //{
    //    selectedTile.OccupiedUnit.transform.position = this.transform.position;
    //    this.OccupiedUnit = selectedTile.OccupiedUnit;
    //    selectedTile.OccupiedUnit = null;

    //    midTile.OccupiedUnit.SetActive(false);
    //    midTile.OccupiedUnit = null;
    //}

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        HexTile selectedTile = UnitManager.Instance.selectedTile;

        if (this.OccupiedUnit == null && selectedTile == null)
        {
            Debug.Log("1");
            UnitManager.Instance.SetSelectedTile(null);
            return;
        }

        else if (this.OccupiedUnit != null && selectedTile != null)
        {
            Debug.Log("2");
            UnitManager.Instance.SetSelectedTile(null);
            return;
        }

        else if (this.OccupiedUnit != null && selectedTile == null)
        {
            Debug.Log("3");
            UnitManager.Instance.SetSelectedTile(this);
            return;
        }

        else
        {
            Debug.Log("4");
            //Debug.Log(selectedTile.OccupiedUnit);
            Vector3 selectedPos = selectedTile.posEasy;
            Vector3 currentPos = this.posEasy;

            if ((selectedPos.x == currentPos.x && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 2)) ||
                ((int)Mathf.Abs(currentPos.x - selectedPos.x) == 3) && ((int)Mathf.Abs(currentPos.y - selectedPos.y) == 1) &&
                !UnitManager.Instance.currentStatus.ContainsKey(currentPos)
               )
            {
                if (selectedTile.OccupiedUnit.Faction == Faction.Rock)
                {
                    Debug.Log("5");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (midTile.OccupiedUnit != null && (midTile.OccupiedUnit.Faction == Faction.Scissor) && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Scissor)
                    {
                        Debug.Log("6");
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                    }

                    else
                    {
                        Debug.Log("7");
                        UnitManager.Instance.SetSelectedTile(null);
                        return;
                    }
                }

                else if (selectedTile.OccupiedUnit.Faction == Faction.Paper)
                {
                    Debug.Log("8");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (midTile.OccupiedUnit != null && (midTile.OccupiedUnit.Faction == Faction.Rock) && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Rock)
                    {
                        Debug.Log("9");
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                    }

                    else
                    {
                        Debug.Log("10");
                        UnitManager.Instance.SetSelectedTile(null);
                        return;
                    }
                }

                else if (selectedTile.OccupiedUnit.Faction == Faction.Scissor)
                {
                    Debug.Log("11");
                    Vector3 midPos = Vector3.Lerp(selectedPos, currentPos, 0.5f);
                    HexTile midTile = GridManager.Instance.GetTileAtPos(GridManager.Instance.GetTranslatedPos(midPos));

                    if (midTile.OccupiedUnit != null && (midTile.OccupiedUnit.Faction == Faction.Paper) && UnitManager.Instance.currentStatus[midPos].Faction == Faction.Paper)
                    {
                        Debug.Log("12");
                        this.SetUnit(selectedTile.OccupiedUnit);
                        UnitManager.Instance.UpdateCurrentStatus(selectedPos, midPos, currentPos);
                        midTile.RemoveUnit(midTile.OccupiedUnit);
                        UnitManager.Instance.SetSelectedTile(null);
                    }

                    else
                    {
                        Debug.Log("13");
                        UnitManager.Instance.SetSelectedTile(null);
                        return;
                    }
                }

                else
                {
                    Debug.Log("14");
                    UnitManager.Instance.SetSelectedTile(null);
                    return;
                }
            }
            else
            {
                Debug.Log("15");
                UnitManager.Instance.SetSelectedTile(null);
                return;
            }
        }
    }
}
