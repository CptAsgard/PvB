﻿/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with CameraFit.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 */
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour
{
    public enum AnchorType
    {
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        TopLeft,
        TopCenter,
        TopRight,
    };
    public AnchorType anchorType;
    public Vector3 anchorOffset;

    // Use this for initialization
    void Start() {
        UpdateAnchor();
    }

    void UpdateAnchor() {
        switch( anchorType ) {
            case AnchorType.BottomLeft:
                SetAnchor( CameraFit.Instance.BottomLeft );
                break;
            case AnchorType.BottomCenter:
                SetAnchor( CameraFit.Instance.BottomCenter );
                break;
            case AnchorType.BottomRight:
                SetAnchor( CameraFit.Instance.BottomRight );
                break;
            case AnchorType.MiddleLeft:
                SetAnchor( CameraFit.Instance.MiddleLeft );
                break;
            case AnchorType.MiddleCenter:
                SetAnchor( CameraFit.Instance.MiddleCenter );
                break;
            case AnchorType.MiddleRight:
                SetAnchor( CameraFit.Instance.MiddleRight );
                break;
            case AnchorType.TopLeft:
                SetAnchor( CameraFit.Instance.TopLeft );
                break;
            case AnchorType.TopCenter:
                SetAnchor( CameraFit.Instance.TopCenter );
                break;
            case AnchorType.TopRight:
                SetAnchor( CameraFit.Instance.TopRight );
                break;
        }
    }

    void SetAnchor( Vector3 anchor ) {
        Vector3 newPos = anchor + anchorOffset;
        if( !transform.position.Equals( newPos ) ) {
            transform.position = newPos;
        }
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update() {
        UpdateAnchor();
    }
#endif
}