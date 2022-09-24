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

    private void Start()
    {
        PossibleSelect(false);
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

    // ReSharper disable Unity.PerformanceAnalysis
    public void PossibleSelect(bool active)
    {
        GetComponent<MeshRenderer>().material.color = active ? possibleSelectionMaterial : originalMaterial;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Select()
    {
        GetComponent<MeshRenderer>().material.color = selectionMaterial;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Unselect()
    {
        GetComponent<MeshRenderer>().material.color = possibleSelectionMaterial;
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