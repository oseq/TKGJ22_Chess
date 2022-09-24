using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Field : MonoBehaviour
{
    [SerializeField] public Vector2Int position;
    [SerializeField] private Character character;

    [SerializeField] private Color originalMaterial;
    [SerializeField] private Color selectedMaterial;

    private void Start()
    {
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

    // ReSharper disable Unity.PerformanceAnalysis
    public void PossibleMoveOverlay(bool active)
    {
        GetComponent<MeshRenderer>().material.color = active ? selectedMaterial : originalMaterial;
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