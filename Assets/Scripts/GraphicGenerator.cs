
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GraphicGenerator : MonoBehaviour
{
    public Graphic GraphicPrefab;

    public Vector2 Spacing;
    public float VerticalOddSpacing;
    public int RowLength;
    public bool GeneratePlottersButton;

    [Header("Materials")]
    public Material LinearMaterial;
    public Material QuadraticMaterial;
    public Material CubicMaterial;
    public Material QuarticMaterial;
    public Material QuinticMaterial;
    public Material SinusoidalMaterial;
    public Material BounceMaterial;
    public Material OverheadMaterial;
    public Material ExponentialMaterial;
    public Material ElasticMaterial;
    public Material CircularMaterial;

    Vector2 _position;
    public int selectGraphic;
    public List<Graphic> GraphicList=new List<Graphic>();
    private void Start()
    {
        Time.timeScale = 0f;

        GenerateGraphic();
    }

    void GenerateGraphic()
    {
        DestroyAllChildren(this.transform);

        BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
        MethodInfo[] methods = typeof(TweenDefinitions).GetMethods(flags);

        int row = 0;
        int column = 0;
        float yCoordinate = 0;

        for (int i = 0; i < methods.Length; i++)
        {
            _position.x = column * Spacing.x;


            _position.y = yCoordinate;

            Graphic plotter = Instantiate(GraphicPrefab);
            plotter.transform.SetParent(this.transform);
            plotter.transform.localPosition = _position;
            plotter.TweenMethodIndex = i;
            string tweenName = plotter.TweenName(plotter.TweenMethodIndex);
            plotter.gameObject.name = tweenName;
            GraphicList.Add(plotter);
            plotter.gameObject.SetActive(false);
            Material newMaterial = LinearMaterial;
            if (tweenName.Contains("Linear")) { newMaterial = LinearMaterial; }
            if (tweenName.Contains("Quadratic")) { newMaterial = QuadraticMaterial; }
            if (tweenName.Contains("Cubic")) { newMaterial = CubicMaterial; }
            if (tweenName.Contains("Quartic")) { newMaterial = QuarticMaterial; }
            if (tweenName.Contains("Quintic")) { newMaterial = QuinticMaterial; }
            if (tweenName.Contains("Sinusoidal")) { newMaterial = SinusoidalMaterial; }
            if (tweenName.Contains("Bounce")) { newMaterial = BounceMaterial; }
            if (tweenName.Contains("Overhead")) { newMaterial = OverheadMaterial; }
            if (tweenName.Contains("Exponential")) { newMaterial = ExponentialMaterial; }
            if (tweenName.Contains("Elastic")) { newMaterial = ElasticMaterial; }
            if (tweenName.Contains("Circular")) { newMaterial = CircularMaterial; }

            plotter.SetMaterial(newMaterial);
            plotter.GetMethodsList();
            plotter.DrawGraph();
        }
        GraphicList[selectGraphic].gameObject.SetActive(true);
    }
    public void DestroyAllChildren(Transform transform)
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) GraphicChange(false);
        if(Input.GetKeyDown(KeyCode.A)) GraphicChange(true);

    }
    public void GraphicChange(bool back)
    {
        GraphicList[selectGraphic].gameObject.SetActive(false);
        if (back)
        {
            if (selectGraphic == 0) selectGraphic = GraphicList.Count - 1;
            else selectGraphic--;
        }
        else
        {
            if (selectGraphic == GraphicList.Count - 1) selectGraphic = 0;
            else selectGraphic++;
        }
        GraphicList[selectGraphic].gameObject.SetActive(true);
    }
}
