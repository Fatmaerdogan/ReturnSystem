using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TweenType 
{
    public TweenDefinitionTypes TweenDefinitionType = TweenDefinitionTypes.Tween;
    public GraphicTween.TweenCurve TweenCurve = GraphicTween.TweenCurve.EaseInCubic;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1f));
    public bool Initialized = false;
}
public enum TweenDefinitionTypes { Tween, AnimationCurve }