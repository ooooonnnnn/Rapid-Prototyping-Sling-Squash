using UnityEngine;

public class RemoveTarget : MonoBehaviour
{
    [Header("Visual Components")]
    [SerializeField] private Renderer[] renderers;
    
    [Header("2D Collider Components")]
    [SerializeField] private Collider2D[] collider2Ds;
    
    [Header("Settings")]
    [SerializeField] private bool startVisible = true;
    [SerializeField] private bool affectColliders = true;
    
    private bool isVisible;
    private bool[] originalCollider2DStates;

    private void awake()
    {
        if (renderers == null)
            renderers = GetComponentInChildren<Renderer[]>();
        
        if (affectColliders && (collider2Ds == null))
            collider2Ds = GetComponentsInChildren<Collider2D>(true);
       
        
        SetVisibility(startVisible); // first u can see the target
        
        
    }

    public void SetVisibility(bool visible)
    {
        isVisible = visible; // according to what the func gets

        foreach (Renderer renderer in renderers)
        {
            if (renderer != null)
            {
                renderer.enabled = visible; // if visible is false ,not visible, etc
            }
        }
        if (affectColliders)
        {
            HandleColliders2D(visible);
        }
    }
    
    private void HandleColliders2D(bool visible)
    {
        // handle 2D colliders
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i] != null)
            {
                if (visible)
                    collider2Ds[i].enabled = true; // restore original state
                else
                    collider2Ds[i].enabled = false; // disable collider
            }
        }
    }

    public void MakeVisible()
    {
        SetVisibility(true);
    }

    public void MakeInvisible()
    {
        SetVisibility(false);
    }

    public bool IsVisible => isVisible; // property to see visible state
}
