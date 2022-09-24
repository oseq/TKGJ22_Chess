using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour // actually a field container & initializer
{
    [SerializeField] private StartingBoardConfig startingConfig;

    [SerializeField] private Field prefabWhite;
    [SerializeField] private Field prefabBlack;

    [SerializeField] private int columns = 8;
    [SerializeField] private int rows = 8;

    // All initialized fields
    [SerializeField] private Field[] fields;

    private void Start()
    {
        CreateGrid();

        foreach (var startingConfigItem in startingConfig.items)
        {
            var character = Instantiate(startingConfigItem.character);
            character.gameObject.SetActive(true);

            fields.GetField(startingConfigItem.position).Occupy(
                startingConfigItem.owner, character, true);
        }
    }

    public IEnumerable<Field> CurrentState()
    {
        return fields;
    }

    public int GetNumberOfColumns()
    {
        return columns;
    }

    public int GetNumberOfRows()
    {
        return rows;
    }

    private void CreateGrid()
    {
        fields = new Field[columns * rows];

        var black = true; // first row - start from black

        for (var i = 0; i < columns; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                var prefabField = black ? prefabBlack : prefabWhite;

                var arrPos = i * columns + j;
                var prefab = Instantiate(
                    prefabField,
                    new Vector3(i, 0, j) + transform.position,
                    Quaternion.Euler(0f, 0f, 0f),
                    transform
                );
                prefab.SetPosition(new Vector2Int(i, j));

                fields[arrPos] = prefab;
                black = !black;
            }

            black = !black;
        }
    }
}