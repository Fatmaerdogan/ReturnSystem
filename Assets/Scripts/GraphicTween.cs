using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicTween : MonoBehaviour
{
    public enum TweenCurve
    {
        LinearTween,
        EaseInQuadratic, EaseOutQuadratic, EaseInOutQuadratic,
        EaseInCubic, EaseOutCubic, EaseInOutCubic,
        EaseInQuartic, EaseOutQuartic, EaseInOutQuartic,
        EaseInQuintic, EaseOutQuintic, EaseInOutQuintic,
        EaseInSinusoidal, EaseOutSinusoidal, EaseInOutSinusoidal,
        EaseInBounce, EaseOutBounce, EaseInOutBounce,
        EaseInOverhead, EaseOutOverhead, EaseInOutOverhead,
        EaseInExponential, EaseOutExponential, EaseInOutExponential,
        EaseInElastic, EaseOutElastic, EaseInOutElastic,
        EaseInCircular, EaseOutCircular, EaseInOutCircular,
        AntiLinearTween, AlmostIdentity
    }
    public enum Curve
    {
        EaseIn=0,
        EaseInOut=1,
        EaseOut=2,
        Linear=3
    }

    public static TweenDelegate[] TweenDelegateArray = new TweenDelegate[]
    {
            LinearTween,
            EaseInQuadratic,    EaseOutQuadratic,   EaseInOutQuadratic,
            EaseInCubic,        EaseOutCubic,       EaseInOutCubic,
            EaseInQuartic,      EaseOutQuartic,     EaseInOutQuartic,
            EaseInQuintic,      EaseOutQuintic,     EaseInOutQuintic,
            EaseInSinusoidal,   EaseOutSinusoidal,  EaseInOutSinusoidal,
            EaseInBounce,       EaseOutBounce,      EaseInOutBounce,
            EaseInOverhead,     EaseOutOverhead,    EaseInOutOverhead,
            EaseInExponential,  EaseOutExponential, EaseInOutExponential,
            EaseInElastic,      EaseOutElastic,     EaseInOutElastic,
            EaseInCircular,     EaseOutCircular,    EaseInOutCircular,
            AntiLinearTween,    AlmostIdentity
    };

    // Core methods ---------------------------------------------------------------------------------------------------------------
    public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, TweenCurve curve)
    {
        currentTime = Maths.Remap(currentTime, initialTime, endTime, 0f, 1f);
        currentTime = TweenDelegateArray[(int)curve](currentTime);
        return startValue + currentTime * (endValue - startValue);
    }

    public static float Evaluate(float t, TweenCurve curve)
    {
        return TweenDelegateArray[(int)curve](t);
    }

    public static float Evaluate(float t, TweenType tweenType)
    {
        if (tweenType.TweenDefinitionType == TweenDefinitionTypes.Tween)
        {
            return Evaluate(t, tweenType.TweenCurve);
        }
        if (tweenType.TweenDefinitionType == TweenDefinitionTypes.AnimationCurve)
        {
            return tweenType.Curve.Evaluate(t);
        }
        return 0f;
    }

    public delegate float TweenDelegate(float currentTime);

    public static float LinearTween(float currentTime) { return TweenDefinitions.Linear_Tween(currentTime); }
    public static float AntiLinearTween(float currentTime) { return TweenDefinitions.LinearAnti_Tween(currentTime); }
    public static float EaseInQuadratic(float currentTime) { return TweenDefinitions.EaseIn_Quadratic(currentTime); }
    public static float EaseOutQuadratic(float currentTime) { return TweenDefinitions.EaseOut_Quadratic(currentTime); }
    public static float EaseInOutQuadratic(float currentTime) { return TweenDefinitions.EaseInOut_Quadratic(currentTime); }
    public static float EaseInCubic(float currentTime) { return TweenDefinitions.EaseIn_Cubic(currentTime); }
    public static float EaseOutCubic(float currentTime) { return TweenDefinitions.EaseOut_Cubic(currentTime); }
    public static float EaseInOutCubic(float currentTime) { return TweenDefinitions.EaseInOut_Cubic(currentTime); }
    public static float EaseInQuartic(float currentTime) { return TweenDefinitions.EaseIn_Quartic(currentTime); }
    public static float EaseOutQuartic(float currentTime) { return TweenDefinitions.EaseOut_Quartic(currentTime); }
    public static float EaseInOutQuartic(float currentTime) { return TweenDefinitions.EaseInOut_Quartic(currentTime); }
    public static float EaseInQuintic(float currentTime) { return TweenDefinitions.EaseIn_Quintic(currentTime); }
    public static float EaseOutQuintic(float currentTime) { return TweenDefinitions.EaseOut_Quintic(currentTime); }
    public static float EaseInOutQuintic(float currentTime) { return TweenDefinitions.EaseInOut_Quintic(currentTime); }
    public static float EaseInSinusoidal(float currentTime) { return TweenDefinitions.EaseIn_Sinusoidal(currentTime); }
    public static float EaseOutSinusoidal(float currentTime) { return TweenDefinitions.EaseOut_Sinusoidal(currentTime); }
    public static float EaseInOutSinusoidal(float currentTime) { return TweenDefinitions.EaseInOut_Sinusoidal(currentTime); }
    public static float EaseInBounce(float currentTime) { return TweenDefinitions.EaseIn_Bounce(currentTime); }
    public static float EaseOutBounce(float currentTime) { return TweenDefinitions.EaseOut_Bounce(currentTime); }
    public static float EaseInOutBounce(float currentTime) { return TweenDefinitions.EaseInOut_Bounce(currentTime); }
    public static float EaseInOverhead(float currentTime) { return TweenDefinitions.EaseIn_Overhead(currentTime); }
    public static float EaseOutOverhead(float currentTime) { return TweenDefinitions.EaseOut_Overhead(currentTime); }
    public static float EaseInOutOverhead(float currentTime) { return TweenDefinitions.EaseInOut_Overhead(currentTime); }
    public static float EaseInExponential(float currentTime) { return TweenDefinitions.EaseIn_Exponential(currentTime); }
    public static float EaseOutExponential(float currentTime) { return TweenDefinitions.EaseOut_Exponential(currentTime); }
    public static float EaseInOutExponential(float currentTime) { return TweenDefinitions.EaseInOut_Exponential(currentTime); }
    public static float EaseInElastic(float currentTime) { return TweenDefinitions.EaseIn_Elastic(currentTime); }
    public static float EaseOutElastic(float currentTime) { return TweenDefinitions.EaseOut_Elastic(currentTime); }
    public static float EaseInOutElastic(float currentTime) { return TweenDefinitions.EaseInOut_Elastic(currentTime); }
    public static float EaseInCircular(float currentTime) { return TweenDefinitions.EaseIn_Circular(currentTime); }
    public static float EaseOutCircular(float currentTime) { return TweenDefinitions.EaseOut_Circular(currentTime); }
    public static float EaseInOutCircular(float currentTime) { return TweenDefinitions.EaseInOut_Circular(currentTime); }
    public static float AlmostIdentity(float currentTime) { return TweenDefinitions.AlmostIdentity(currentTime); }

    
    public static Curve GetCurveMethod(TweenCurve tween)
    {
        switch (tween)
        {
            case TweenCurve.LinearTween: return Curve.Linear;
            case TweenCurve.AntiLinearTween: return Curve.Linear;
            case TweenCurve.EaseInQuadratic: return Curve.EaseIn; 
            case TweenCurve.EaseOutQuadratic: return Curve.EaseOut;
            case TweenCurve.EaseInOutQuadratic: return Curve.EaseInOut;
            case TweenCurve.EaseInCubic: return Curve.EaseIn;
            case TweenCurve.EaseOutCubic: return Curve.EaseOut;
            case TweenCurve.EaseInOutCubic: return Curve.EaseInOut;
            case TweenCurve.EaseInQuartic: return Curve.EaseIn;
            case TweenCurve.EaseOutQuartic: return Curve.EaseOut;
            case TweenCurve.EaseInOutQuartic: return Curve.EaseInOut;
            case TweenCurve.EaseInQuintic: return Curve.EaseIn;
            case TweenCurve.EaseOutQuintic: return Curve.EaseOut;
            case TweenCurve.EaseInOutQuintic: return Curve.EaseInOut;
            case TweenCurve.EaseInSinusoidal: return Curve.EaseIn;
            case TweenCurve.EaseOutSinusoidal: return Curve.EaseOut;
            case TweenCurve.EaseInOutSinusoidal: return Curve.EaseInOut;
            case TweenCurve.EaseInBounce: return Curve.EaseIn;
            case TweenCurve.EaseOutBounce: return Curve.EaseOut;
            case TweenCurve.EaseInOutBounce: return Curve.EaseInOut;
            case TweenCurve.EaseInOverhead: return Curve.EaseIn;
            case TweenCurve.EaseOutOverhead: return Curve.EaseOut;
            case TweenCurve.EaseInOutOverhead: return Curve.EaseInOut;
            case TweenCurve.EaseInExponential: return Curve.EaseIn;
            case TweenCurve.EaseOutExponential: return Curve.EaseOut;
            case TweenCurve.EaseInOutExponential: return Curve.EaseInOut;
            case TweenCurve.EaseInElastic: return Curve.EaseIn;
            case TweenCurve.EaseOutElastic: return Curve.EaseOut;
            case TweenCurve.EaseInOutElastic: return Curve.EaseInOut;
            case TweenCurve.EaseInCircular: return Curve.EaseIn;
            case TweenCurve.EaseOutCircular: return Curve.EaseOut;
            case TweenCurve.EaseInOutCircular: return Curve.EaseInOut;
            case TweenCurve.AlmostIdentity: return Curve.Linear; ;
        }
        return Curve.Linear;
    }

}
