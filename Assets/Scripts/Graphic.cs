
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static GraphicTween;

public class Graphic : MonoBehaviour
{
    public int TweenMethodIndex;

    [Header("Graph")]
    public float GraphSize = 1f;
    [Range(0, 1000)]
    public int Resolution = 100;

    [Header("Points")]
    public Transform PlotPointPrefab;
    public float PointScaleFactor = 1f;
    public Material PlotPointMaterial;

    public float DistanceBetweenPoints = 1f;

    [Header("Axis")]
    public GraphicAxis Axis;
    Transform[] _points;
    float _pointScale;

    Vector3 _scale,_position;
    Transform _point;

    List<MethodInfo> _methodList;
    Vector2 _pointValues = Vector2.zero;
    object[] _parameter;
    GraphicAxis _axis;

    Vector3 _positionPointInitialPosition,_positionPointVerticalInitialPosition,_rotationPointInitialRotation,_scalePointInitialScale;

    public string[] GetMethodsList()
    {
        FillMethodList();
        List<string> methodNames = new List<string>();
        foreach (MethodInfo method in _methodList)
        {
            methodNames.Add(method.Name);
        }
        string[] _typeDisplays = methodNames.ToArray();
        return _typeDisplays;
    }

    public float InvokeTween(int index, object[] parameters)
    {
        return (float)_methodList[index].Invoke(this, parameters);
    }

    public string TweenName(int index)
    {
        if (_methodList == null)
        {
            FillMethodList();
        }
        return _methodList[index].Name;
    }
    public string CurveName(int index)
    {
        return GetCurveMethod((TweenCurve)index).ToString();
    }
    void FillMethodList()
    {
        BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
        MethodInfo[] methods = typeof(TweenDefinitions).GetMethods(flags);
        _methodList = methods.OrderBy(item => item.Name).ToList();
    }

    void OnEnable()
    {
        _parameter = new object[1];
    }

    void Start()
    {
        FillMethodList();
        DrawGraph();
    }

    void Initialization()
    {
        _points = new Transform[Resolution];
        DistanceBetweenPoints = GraphSize / Resolution;
        _pointScale = DistanceBetweenPoints * PointScaleFactor;
        _scale = _pointScale * Vector3.one;
        _position = Vector3.zero;
    }

    public void DrawGraph()
    {
        Cleanup();
        Initialization();
        DrawAxis();
        DrawPoints();
    }

    void DrawAxis()
    {
        _axis = Instantiate(Axis);

        _axis.SetLabel(TweenName(TweenMethodIndex).Replace("_", " "));
        _axis.SetSectionLabel(CurveName(TweenMethodIndex));

        _axis.transform.SetParent(this.transform);
        _axis.transform.localPosition = Vector3.zero;

        _positionPointInitialPosition = _axis.PositionPoint.transform.localPosition;
        _positionPointVerticalInitialPosition = _axis.PositionPointVertical.transform.localPosition;
        _rotationPointInitialRotation = _axis.RotationPoint.transform.localEulerAngles;
        _scalePointInitialScale = _axis.ScalePoint.transform.localScale;
    }

    void DrawPoints()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _point = Instantiate(PlotPointPrefab);
            _point.name = this.name + "Point" + i;

            _pointValues.x = i * (1f / Resolution);

            _parameter[0] = _pointValues.x;
            _pointValues.y = InvokeTween(TweenMethodIndex, _parameter);

            _position.x = i * DistanceBetweenPoints;
            _position.y = _pointValues.y * GraphSize;

            _point.localPosition = _position;
            _point.localScale = _scale;

            _point.gameObject.GetComponentNoAlloc<MeshRenderer>().material = PlotPointMaterial;
            _point.SetParent(transform, false);
            _points[i] = _point;
        }
    }

    public void SetMaterial(Material newMaterial)
    {
        PlotPointMaterial = newMaterial;
    }

    void Cleanup()
    {
        DestroyAllChildren(this.transform);
    }

    [Header("Movement")]
    public float MovementPauseDuration = 0.5f;
    float _currentMovement = 0f,_lastMovementEndedAt = 0f;
    Vector3 _curvePointNewMovement = Vector3.zero;
    string _timeString;
    float _plotterCurvePointScale = 0.1f;
    Vector3 _newScale;
    float _newValue, _newScaleUnit;
    Vector3 Vector3Zero = Vector3.zero;

    void Update()
    {
        _curvePointNewMovement = Vector3Zero;
        _curvePointNewMovement.x = _currentMovement;
        _parameter[0] = _currentMovement;
        _newValue = InvokeTween(TweenMethodIndex, _parameter);

        _curvePointNewMovement.y = _newValue;
        _curvePointNewMovement *= GraphSize;
        _axis.PlotterCurvePoint.transform.localPosition = _curvePointNewMovement;

        _curvePointNewMovement = _positionPointInitialPosition;
        _curvePointNewMovement.x = _newValue;
        _axis.PositionPoint.transform.localPosition = _curvePointNewMovement;

        _curvePointNewMovement = _positionPointVerticalInitialPosition;
        _curvePointNewMovement.y = _newValue;
        _axis.PositionPointVertical.transform.localPosition = _curvePointNewMovement;

        _curvePointNewMovement = _rotationPointInitialRotation;
        _curvePointNewMovement.z = _newValue * 360f;
        _axis.RotationPoint.transform.localEulerAngles = _curvePointNewMovement;

        _curvePointNewMovement = _scalePointInitialScale;
        _curvePointNewMovement *= _newValue;
        _axis.ScalePoint.transform.localScale = _curvePointNewMovement;

        if (Time.unscaledTime - _lastMovementEndedAt < MovementPauseDuration)
        {
            if (Time.unscaledTime - _lastMovementEndedAt < MovementPauseDuration / 2f)
            {
                _currentMovement = 1f;
                _newScaleUnit = GraphicTween.Tween(Time.unscaledTime - _lastMovementEndedAt, 0f, (MovementPauseDuration / 2f), 1f, 0f, GraphicTween.TweenCurve.EaseInCubic);
                _newScale = Vector3.one * _newScaleUnit;

                _axis.PlotterCurvePoint.localScale = _newScale * _plotterCurvePointScale;
                _axis.PositionPoint.localScale = _newScale;
                _axis.PositionPointVertical.localScale = _newScale;
                _axis.RotationPoint.localScale = _newScale;
                _axis.ScalePoint.localScale = _newScale;
            }
            else
            {
                _currentMovement = 0f;
                _newScaleUnit = GraphicTween.Tween(Time.unscaledTime - _lastMovementEndedAt, (MovementPauseDuration / 2f), MovementPauseDuration, 0f, 1f, GraphicTween.TweenCurve.EaseOutCubic);
                _newScale = Vector3.one * _newScaleUnit;

                _axis.PlotterCurvePoint.localScale = _newScale * _plotterCurvePointScale;
                _axis.PositionPointVertical.localScale = _newScale;
                _axis.PositionPoint.localScale = _newScale;
                _axis.RotationPoint.localScale = _newScale;
                _axis.ScalePoint.localScale = Vector3.zero;
            }
        }
        else
        {
            _axis.PlotterCurvePoint.localScale = Vector3.one * _plotterCurvePointScale;
            _currentMovement += Time.unscaledDeltaTime;
        }

        if (_currentMovement > 1f)
        {
            _lastMovementEndedAt = Time.unscaledTime;
            _currentMovement = 1f;
        }

        _axis.TimeLabel.text = _timeString;
    }
    void DestroyAllChildren(Transform transform)
    {
        for (int t = transform.childCount - 1; t >= 0; t--)
        {
            if (Application.isPlaying)
            {
                UnityEngine.Object.Destroy(transform.GetChild(t).gameObject);
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(t).gameObject);
            }
        }
    }
   
}
