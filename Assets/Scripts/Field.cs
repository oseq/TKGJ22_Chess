using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Field : MonoBehaviour
{
    [SerializeField] public Vector2Int position;
    [SerializeField] private Character character;

    [SerializeField] private Color originalMaterial;
    [SerializeField] private Color possibleSelectionMaterial;
    [SerializeField] private Color selectionMaterial;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        Unselect();
    }

    public void SetPosition(Vector2Int vec)
    {
        position = vec;
    }

    public bool IsOccupied()
    {
        return character != null;
    }

    // Please notice you must put an initialized Character here  
    public bool Occupy(Player requester, Character ch, bool force)
    {
        if (character == null)
        {
            character = ch;

            character.SetField(this);
            character.SetOwner(requester);

            character.transform.position = transform.position;
            // ReSharper disable once Unity.InefficientPropertyAccess
            character.transform.parent = transform;

            return true;
        }

        if (!force) return false;

        Destroy(character);

        character = ch;
        return true;
    }

    public void Deoccupy()
    {
        character = null;
    }


    // Marks the field as possible to select
    public void PossibleSelect()
    {
        _meshRenderer.material.color = possibleSelectionMaterial;
    }

    // Marks the field as selected
    public void Select()
    {
        _meshRenderer.material.color = selectionMaterial;
    }

    // Deselect field (brings back original color)
    public void Unselect()
    {
        _meshRenderer.material.color = originalMaterial;
    }

    public Character GetCharacter()
    {
        return character;
    }
}

public static class FieldsExtension
{
    public static Field GetField(this IEnumerable<Field> input, Vector2Int selector)
    {
        return input.FirstOrDefault(field => field.position == selector);
    }

    public static IEnumerable<Field> GetFields(this IEnumerable<Field> input, IEnumerable<Vector2Int> selector)
    {
        return input.Where(field => selector.Contains(field.position)).ToList();
    }
}