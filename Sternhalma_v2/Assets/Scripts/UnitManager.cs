using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{

    public static UnitManager Instance;

    private List<ScriptableUnit> units;
    public BaseUnit selectedUnit;
    public Vector3 selectedUnitCoordinate;
    public HexTile selectedTile;

    public Dictionary<HexTile, BaseUnit> tileToUnit;
    public Dictionary<Vector3, BaseUnit> currentStatus;

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    private T GetUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).First().UnitPrefab;
    }

    public void SpawnObjects()
    {
        tileToUnit = new Dictionary<HexTile, BaseUnit>();
        currentStatus = new Dictionary<Vector3, BaseUnit>();

        List<Vector3> scissorList = new List<Vector3> { new Vector3(0, 0) };
        var scissorCount = scissorList.Count;

        for (int i=0; i<scissorCount; i++)
        {
            var scissorPrefab = GetUnit<Scissor>(Faction.Scissor);
            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            HexTile scissorTile = GridManager.Instance.GetTileAtPos
                (
                GridManager.Instance.GetTranslatedPos(scissorList[i])
                );

            scissorTile.SetUnit(spawnedScissor);
            tileToUnit[scissorTile] = spawnedScissor;
            currentStatus[scissorList[i]] = spawnedScissor;
        }

        List<Vector3> rockList = new List<Vector3> { new Vector3(0, 1) };
        var rockCount = rockList.Count;

        for (int i = 0; i < rockCount; i++)
        {
            var rockPrefab = GetUnit<Rock>(Faction.Rock);
            BaseUnit spawnedRock = Instantiate(rockPrefab);
            HexTile rockTile = GridManager.Instance.GetTileAtPos
                (
                GridManager.Instance.GetTranslatedPos(rockList[i])
                );

            rockTile.SetUnit(spawnedRock);
            tileToUnit[rockTile] = spawnedRock;
            currentStatus[rockList[i]] = spawnedRock;
        }

        List<Vector3> paperList = new List<Vector3> { new Vector3(1.5f, -1) };
        var paperCount = paperList.Count;

        for (int i = 0; i < paperCount; i++)
        {
            var paperPrefab = GetUnit<Paper>(Faction.Paper);
            BaseUnit spawnedPaper = Instantiate(paperPrefab);
            HexTile paperTile = GridManager.Instance.GetTileAtPos
                (
                GridManager.Instance.GetTranslatedPos(paperList[i])
                );

            paperTile.SetUnit(spawnedPaper);
            tileToUnit[paperTile] = spawnedPaper;
            currentStatus[paperList[i]] = spawnedPaper;
        }

        //GameManager.Instance.ChangeState(GameState.PlayerTurn]);
    }
}
