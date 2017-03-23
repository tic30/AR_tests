using UnityEngine;
using UnityEngine.VR.WSA;

public class CubeAnchor : MonoBehaviour
{

    public WorldAnchor anchor;


    // Use this for initialization
    void Start()
    {
        anchor = gameObject.AddComponent<WorldAnchor>();
        anchor.OnTrackingChanged += Anchor_OnTrackingChanged;

        Anchor_OnTrackingChanged(anchor, anchor.isLocated);
    }

    private void Anchor_OnTrackingChanged(WorldAnchor self, bool located)
    {
        // This simply activates/deactivates this object and all children when tracking changes
        self.gameObject.SetActive(located);
    }
}