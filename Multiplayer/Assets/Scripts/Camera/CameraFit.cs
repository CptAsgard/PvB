﻿#define CAMERAFIT_HORIZONTAL

using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent( typeof( Camera ) )]
public class CameraFit : MonoBehaviour
{
    #region FIELDS
    public float UnitsForLength = 1; // width/height of your scene in unity units
    public static CameraFit Instance;

    private float _width;
    private float _height;
    //*** bottom screen
    private Vector3 _bl;
    private Vector3 _bc;
    private Vector3 _br;
    //*** middle screen
    private Vector3 _ml;
    private Vector3 _mc;
    private Vector3 _mr;
    //*** top screen
    private Vector3 _tl;
    private Vector3 _tc;
    private Vector3 _tr;
    #endregion

    #region PROPERTIES
    public float Width {
        get {
            return _width;
        }
    }
    public float Height {
        get {
            return _height;
        }
    }

    // helper points:
    public Vector3 BottomLeft {
        get {
            return _bl;
        }
    }
    public Vector3 BottomCenter {
        get {
            return _bc;
        }
    }
    public Vector3 BottomRight {
        get {
            return _br;
        }
    }
    public Vector3 MiddleLeft {
        get {
            return _ml;
        }
    }
    public Vector3 MiddleCenter {
        get {
            return _mc;
        }
    }
    public Vector3 MiddleRight {
        get {
            return _mr;
        }
    }
    public Vector3 TopLeft {
        get {
            return _tl;
        }
    }
    public Vector3 TopCenter {
        get {
            return _tc;
        }
    }
    public Vector3 TopRight {
        get {
            return _tr;
        }
    }
    #endregion

    #region METHODS
    private void Awake() {
        Instance = this;
        try {
            if( (bool) GetComponent<Camera>() ) {
                if( GetComponent<Camera>().orthographic ) {
                    ComputeResolution();
                }
            }
        }
        catch( Exception e ) {
            Debug.LogException( e, this );
        }
    }

    private void ComputeResolution() {
        float leftX, rightX, topY, bottomY;

#if CAMERAFIT_HORIZONTAL
        GetComponent<Camera>().orthographicSize = 1f / GetComponent<Camera>().aspect * UnitsForLength / 2f;
#elif CAMERAFIT_VERTICAL
        GetComponent<Camera>().orthographicSize = UnitsForLength / 2f;
#endif

        _height = 2f * GetComponent<Camera>().orthographicSize;
        _width = _height * GetComponent<Camera>().aspect;

        float cameraX, cameraY;
        cameraX = GetComponent<Camera>().transform.position.x;
        cameraY = GetComponent<Camera>().transform.position.y;

        leftX = cameraX - _width / 2;
        rightX = cameraX + _width / 2;
        topY = cameraY + _height / 2;
        bottomY = cameraY - _height / 2;

        //*** bottom
        _bl = new Vector3( leftX, bottomY, 0 );
        _bc = new Vector3( cameraX, bottomY, 0 );
        _br = new Vector3( rightX, bottomY, 0 );
        //*** middle
        _ml = new Vector3( leftX, cameraY, 0 );
        _mc = new Vector3( cameraX, cameraY, 0 );
        _mr = new Vector3( rightX, cameraY, 0 );
        //*** top
        _tl = new Vector3( leftX, topY, 0 );
        _tc = new Vector3( cameraX, topY, 0 );
        _tr = new Vector3( rightX, topY, 0 );
    }

    private void Update() {
#if UNITY_EDITOR
        ComputeResolution();
#endif
    }

    private void OnDrawGizmos() {
        if( GetComponent<Camera>().orthographic ) {
            DrawGizmos();
        }
    }

    private void DrawGizmos() {
        //*** bottom
        Gizmos.DrawIcon( _bl, "point.png", false );
        Gizmos.DrawIcon( _bc, "point.png", false );
        Gizmos.DrawIcon( _br, "point.png", false );
        //*** middle
        Gizmos.DrawIcon( _ml, "point.png", false );
        Gizmos.DrawIcon( _mc, "point.png", false );
        Gizmos.DrawIcon( _mr, "point.png", false );
        //*** top
        Gizmos.DrawIcon( _tl, "point.png", false );
        Gizmos.DrawIcon( _tc, "point.png", false );
        Gizmos.DrawIcon( _tr, "point.png", false );

        Gizmos.color = Color.green;
        Gizmos.DrawLine( _bl, _br );
        Gizmos.DrawLine( _br, _tr );
        Gizmos.DrawLine( _tr, _tl );
        Gizmos.DrawLine( _tl, _bl );
    }

    private Vector2 GetGameView() {
        System.Type T = System.Type.GetType( "UnityEditor.GameView,UnityEditor" );
        System.Reflection.MethodInfo getSizeOfMainGameView =
        T.GetMethod( "GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static );
        System.Object resolution = getSizeOfMainGameView.Invoke( null, null );
        return (Vector2) resolution;
    }
    #endregion
}