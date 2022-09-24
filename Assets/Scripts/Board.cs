using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject prefabWhite;
    [SerializeField] private GameObject prefabBlack;

    [SerializeField] private GameObject possibleMoveOverlay;
    [SerializeField] private GameObject selectedOverlay;

    [SerializeField] private int columns = 8;
    public int GetNumberOfColumns() {
        return this.columns;
    }
    [SerializeField] private int rows = 8;
    public int GetNumberOfRows() {
        return this.rows;
    }

    [SerializeField] private Field[] fields;

    private void Start()
    {
        CreateGrid();
    }

    public IEnumerable<Field> CurrentState()
    {
        return fields;
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

                fields[arrPos] = new Field(prefab, possibleMoveOverlay, selectedOverlay, new Vector2Int(i, j));

                black = !black;
            }

            black = !black;
        }
    }
}