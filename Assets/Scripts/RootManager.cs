using UnityEngine;

public abstract class RootManager<T> : MonoBehaviourSingleton<T> where T : MonoBehaviour
{
    [SerializeField]
    private GameObject _rootObject;

    private void Start()
    {
        RegisterManager();
    }

    protected abstract void RegisterManager();

    public virtual void ShowRoot()
    {
        _rootObject.SetActive(true);
    }

    public virtual void HideRoot()
    {
        _rootObject.SetActive(false);
    }
}