using UnityEngine;

[ExecuteInEditMode]
public class AutoColliderFromChildren : MonoBehaviour
{
    void Start()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds totalBounds = renderers[0].bounds;
        foreach (var r in renderers)
        {
            totalBounds.Encapsulate(r.bounds);
        }

        BoxCollider col = GetComponent<BoxCollider>();
        if (col == null)
            col = gameObject.AddComponent<BoxCollider>();

        // Convertir a espacio local
        col.center = transform.InverseTransformPoint(totalBounds.center);
        col.size = totalBounds.size;
    }
}