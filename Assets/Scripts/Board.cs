using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Field
{
    [SerializeField] public Vector2Int position;
    [SerializeField] private Character character;
    [SerializeField] private GameObject field;

    public Field(GameObject field, Vector2Int position)
    {
        this.field = field;
        this.position = position;
    }

    public bool IsOccupied()
    {
        return character != null;
    }

    public bool Occupy(Character ch, bool force)
    {
        if (character == null)
        {
            character = ch;
            return true;
        }

        if (!force) return false;

        character.OnRemovedFromField();
        character = ch;
        return true;
    }
}

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject prefabWhite = null;
    [SerializeField] private GameObject prefabBlack = null;

    [SerializeField] private int columns = 8;
    [SerializeField] private int rows = 8;

    private Vector3 _startPosition;

    [SerializeField] private Field[] fields;

    private void Start()
    {
        CreateGrid();
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

                fields[arrPos] = new Field(prefab, new Vector2Int(i, j));

                black = !black;
            }

            black = !black;
        }
    }
}