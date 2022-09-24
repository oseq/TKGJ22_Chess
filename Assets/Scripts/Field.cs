using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Field
{
    [SerializeField] public Vector2Int position;

    [SerializeField] private Character character;

    [SerializeField] private GameObject prefab;

    [SerializeField] private GameObject possibleMoveOverlay;
    [SerializeField] private GameObject selectedOverlay;

    public Field(GameObject prefab, GameObject possibleMoveOverlay, GameObject selectedOverlay, Vector2Int position)
    {
        this.prefab = prefab;
        this.position = position;
        this.possibleMoveOverlay = possibleMoveOverlay;
        this.selectedOverlay = selectedOverlay;

        SelectedOverlay(false);
        PossibleMoveOverlay(false);
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

    public void PossibleMoveOverlay(bool active)
    {
        possibleMoveOverlay.SetActive(active);
    }

    public void SelectedOverlay(bool active)
    {
        selectedOverlay.SetActive(active);
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