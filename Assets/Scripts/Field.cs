using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Field : MonoBehaviour
{
    [SerializeField] public Vector2Int position;
    [SerializeField] private Character character;

    [SerializeField] private GameObject possibleMoveOverlay;
    [SerializeField] private GameObject selectedOverlay;

    private void Start()
    {
        SelectedOverlay(false);
        PossibleMoveOverlay(false);
    }

    public void SetPosition(Vector2Int vec)
    {
        position = vec;
    }

    public bool IsOccupied()
    {
        return character != null;
    }

    public bool Occupy(Player requester, Character ch, bool force)
    {
        if (character == null)
        {
            character = ch;
            character.SetField(this);
            character.SetOwner(requester);
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