using System.Collections.Generic;
using UnityEngine;

public class NextHotSpot : MonoBehaviour
{

    [SerializeField] private Transform OVRCameraRig;     // OVRCameraRig transform (root)
    [SerializeField] private Transform centerEyeAnchor;  // TrackingSpace/CenterEyeAnchor

    [Header("Hotspots (TargetPose transforms)")]
    [SerializeField] private List<Transform> hotspots = new List<Transform>();

    [Header("Input")]
    [SerializeField] private OVRInput.Button nextButton = OVRInput.Button.One; // A on right controller
    [SerializeField] private OVRInput.Controller controller = OVRInput.Controller.RTouch;

    [Header("Debounce")]
    [SerializeField] private float cooldownSeconds = 0.25f;

    private int _index = -1; // hotspot list index
    private float _nextAllowedTime = 0f;

    private void Reset()
    {
        // If you drop this onto something in-scene, try to auto-find common names:
        if (OVRCameraRig == null)
        {
            var rig = FindObjectOfType<OVRCameraRig>();
            if (rig) OVRCameraRig = rig.transform;
        }
        if (centerEyeAnchor == null && OVRCameraRig != null)
        {
            var t = OVRCameraRig.Find("TrackingSpace/CenterEyeAnchor");
            if (t) centerEyeAnchor = t;
        }
    }

    private void Update()
    {
        if (Time.time < _nextAllowedTime) return;

        if (OVRInput.GetDown(nextButton, controller))
        {
            _nextAllowedTime = Time.time + cooldownSeconds;
            TeleportToNext();
        }
    }

    public void TeleportToNext()
    {
        if (OVRCameraRig == null || centerEyeAnchor == null || hotspots == null || hotspots.Count == 0)
        {
            Debug.LogWarning("OVRHotspotCycler: Missing rig/head/hotspots reference.");
            return;
        }
        // TODO: Update the hotspot list index variable (Suggestion: use % operator) 
        _index = (_index + 1) % hotspots.Count;

        // TODO: Get the hotspot transform (actually TargetPose transform of a hotspot)
        Transform hotspotTransform = hotspots[_index].transform;


        // TODO: Call TeleportToNextHotspot
        TeleportToNextHotspot(hotspotTransform);

    }

    private void TeleportToNextHotspot(Transform target)
    {
        // Current world position of head is center eye anchor position
        Vector3 headPos = centerEyeAnchor.position;
        Vector3 rigPos = OVRCameraRig.position;

        // TODO: Compute offset from head to ovr camera rig (world): rig position - center eye anhcor position (i.e. head) 
        Vector3 offset = rigPos - headPos;

        // TODO: We want head XZ to land exactly on the hotspot XZ. Use centerEyeAnchor for y (height)
        //       Compute desired hotspot position to jump to

        rigPos.x = target.position.x;
        rigPos.z = target.position.z;
        rigPos.y = centerEyeAnchor.position.y;


        // TODO: Move the whole rig so head ends up there (change its position)
        // i.e. new rig position = desired hotspot pos + offset  
        rigPos += offset;

        // TODO: Use direction of hotspot (i.e. target pose around hospot y axis) to orient rig
        // hint: get euler angle y and create quaternion
        Quaternion rigRot = Quaternion.Euler(0, target.eulerAngles.y, 0);

        // TODO: Set OVR rig to position and orientation
        OVRCameraRig.position = rigPos;
        OVRCameraRig.localRotation = rigRot;

    }
}
