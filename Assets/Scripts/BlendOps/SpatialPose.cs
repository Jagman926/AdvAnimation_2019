using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SPATIAL POSE HELPER CLASS BY DAN BUCKSTEIN

public class SpatialPose : UnityEngine.Transform
{
    // DEFAULTS    
    public static readonly Quaternion defaultRotation = Quaternion.identity;
    public static readonly Vector3 defaultRotationEuler = Vector3.zero;
    public static readonly Vector3 defaultTranslation = Vector3.zero;
    public static readonly Vector3 defaultScale = Vector3.one;

    // CONSTRUCTORS    
    public SpatialPose() :
    base()
    {
        MakeIdentity();
    }

    public SpatialPose(Quaternion rotation, Vector3 translation, Vector3 scale) :
    base()
    {
        MakeRotationTranslationScale(rotation, translation, scale);
    }

    public SpatialPose(Vector3 rotationEuler, Vector3 translation, Vector3 scale) :
    base()
    {
        MakeRotationEulerTranslationScale(rotationEuler, translation, scale);
    }

    // SETTERS    
    public SpatialPose MakeIdentity()
    {
        localRotation = defaultRotation;
        localPosition = defaultTranslation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeRotation(Quaternion rotation)
    {
        localRotation = rotation;
        localPosition = defaultTranslation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeRotationTranslation(Quaternion rotation, Vector3 translation)
    {
        localRotation = rotation;
        localPosition = translation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeRotationScale(Quaternion rotation, Vector3 scale)
    {
        localRotation = rotation;
        localPosition = defaultTranslation;
        localScale = scale;
        return this;
    }

    public SpatialPose MakeRotationTranslationScale(Quaternion rotation, Vector3 translation, Vector3 scale)
    {
        localRotation = rotation;
        localPosition = translation;
        localScale = scale;
        return this;
    }

    public SpatialPose MakeRotationEuler(Vector3 rotationEuler)
    {
        localEulerAngles = rotationEuler;
        localPosition = defaultTranslation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeRotationEulerTranslation(Vector3 rotationEuler, Vector3 translation)
    {
        localEulerAngles = rotationEuler;
        localPosition = translation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeRotationEulerScale(Vector3 rotationEuler, Vector3 scale)
    {
        localEulerAngles = rotationEuler;
        localPosition = defaultTranslation;
        localScale = scale;
        return this;
    }

    public SpatialPose MakeRotationEulerTranslationScale(Vector3 rotationEuler, Vector3 translation, Vector3 scale)
    {
        localEulerAngles = rotationEuler;
        localPosition = translation;
        localScale = scale;
        return this;
    }

    public SpatialPose MakeTranslation(Vector3 translation)
    {
        localRotation = defaultRotation;
        localPosition = translation;
        localScale = defaultScale;
        return this;
    }

    public SpatialPose MakeTranslationScale(Vector3 translation, Vector3 scale)
    {
        localRotation = defaultRotation;
        localPosition = translation;
        localScale = scale;
        return this;
    }

    public SpatialPose MakeScale(Vector3 scale)
    {
        localRotation = defaultRotation;
        localPosition = defaultTranslation;
        localScale = scale;
        return this;
    }

    // FACTORIES    
    public static SpatialPose NewIdentity()
    {
        return new SpatialPose();
    }

    public static SpatialPose NewRotation(Quaternion rotation)
    {
        return new SpatialPose(rotation, defaultTranslation, defaultScale);
    }

    public static SpatialPose NewRotationTranslation(Quaternion rotation, Vector3 translation)
    {
        return new SpatialPose(rotation, translation, defaultScale);
    }

    public static SpatialPose NewRotationScale(Quaternion rotation, Vector3 scale)
    {
        return new SpatialPose(rotation, defaultTranslation, scale);
    }

    public static SpatialPose NewRotationTranslationScale(Quaternion rotation, Vector3 translation, Vector3 scale)
    {
        return new SpatialPose(rotation, translation, scale);
    }

    public static SpatialPose NewRotationEuler(Vector3 rotationEuler)
    {
        return new SpatialPose(rotationEuler, defaultTranslation, defaultScale);
    }

    public static SpatialPose NewRotationEulerTranslation(Vector3 rotationEuler, Vector3 translation)
    {
        return new SpatialPose(rotationEuler, translation, defaultScale);
    }

    public static SpatialPose NewRotationEulerScale(Vector3 rotationEuler, Vector3 scale)
    {
        return new SpatialPose(rotationEuler, defaultTranslation, scale);
    }

    public static SpatialPose NewRotationEulerTranslationScale(Vector3 rotationEuler, Vector3 translation, Vector3 scale)
    {
        return new SpatialPose(rotationEuler, translation, scale);
    }

    public static SpatialPose NewTranslation(Vector3 translation)
    {
        return new SpatialPose(defaultRotation, translation, defaultScale);
    }

    public static SpatialPose NewTranslationScale(Vector3 translation, Vector3 scale)
    {
        return new SpatialPose(defaultRotation, translation, scale);
    }

    public static SpatialPose NewScale(Vector3 scale)
    {
        return new SpatialPose(defaultRotation, defaultTranslation, scale);
    }
}
