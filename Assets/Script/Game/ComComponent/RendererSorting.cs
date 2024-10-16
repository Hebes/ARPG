using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RendererSorting : MonoBehaviour
{
    private void Start()
    {
        SetSortingLayerAndOrder();
    }

    private void SetSortingLayerAndOrder()
    {
        Renderer component = GetComponent<Renderer>();
        if (!string.IsNullOrEmpty(sortingLayerName))
        {
            component.sortingLayerName = sortingLayerName;
            component.sortingOrder = sortOrder;
        }
    }

    public int sortOrder;

    public string sortingLayerName;

    public int sortingLayerID;
}