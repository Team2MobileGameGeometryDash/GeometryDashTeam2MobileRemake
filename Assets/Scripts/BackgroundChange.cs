using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CameraFollow))]
public class BackgroundChange : MonoBehaviour
{
    [Header("BackgroundMovement")]
    [SerializeField]
    [Tooltip("The First background for the start")]
    private Transform FirstBackground;
    [SerializeField]
    [Tooltip("The Last background for the finish moving")]
    private Transform LastBackground;
    [SerializeField]
    [Tooltip("The GameObject With All Backgrounds")]
    private Transform AllBackgroundObject;
    [SerializeField]
    [Tooltip("Duration in Second of the Music")]
    private float MusicDurationInSeconds;
    private float timer;
    private float colorTimer;
    private float MaxDistance;
    private Vector3 initialPosContainer;

    [Header("BaseBackground")]
    [SerializeField]
    [Tooltip("Add one by one the Base Backgrounds")]
    private List<SpriteRenderer> BaseBackground;
    [SerializeField]
    private Color StartBaseBackgroundColor;
    private Color ActualBaseBackgroundColor;

    [Header("Gradient")]
    [SerializeField]
    [Tooltip("Add one by one the Gradients of Backgrounds")]
    private List<SpriteRenderer> Gradient;
    [SerializeField]
    private Color StartGradientColor;
    private Color ActualGradientColor;

    [Header("Target")]
    [SerializeField]
    [Tooltip("Add the position when need to change the colors of the background and set it.")]
    private List<BackgroundChangeData> ChangePoint;
    private int _currentChangePoint;
    private float _cameraPosition;

    private CameraFollow _camera;

    private bool finishColorsChange;

    private void Start()
    {

        SetInitialColors();

        if (ChangePoint.Count != 0) SortChangePointByX();

        _camera = GetComponentInParent<CameraFollow>();
        MaxDistance = LastBackground.position.x - FirstBackground.position.x;
        initialPosContainer = AllBackgroundObject.localPosition;
        _currentChangePoint = 0;
        colorTimer = 0.0f;
        timer = 0.0f;
        finishColorsChange = false;
    }

    private void FixedUpdate()
    {
        _cameraPosition = transform.position.x - _camera.offset.x;
        MoveBackground();
        if (ChangePoint.Count != 0 && !finishColorsChange)
        {
            if (_cameraPosition > ChangePoint[_currentChangePoint].Target.position.x && ChangePoint[_currentChangePoint].colorTransitionDuration == 0f)
            {
                BackgroundNoTransition();
                //Debug.Log("1");
            }
            else if (_cameraPosition > ChangePoint[_currentChangePoint].Target.position.x && ChangePoint[_currentChangePoint].colorTransitionDuration != 0f)
            {
                if (colorTimer ==  0.0f) SaveLastBackgroundColor();
                BackgroundTransition();
                //Debug.Log("2");
            }
            else if (_cameraPosition < ChangePoint[_currentChangePoint].Target.position.x && colorTimer != 0)
            {
                colorTimer = 0;
                //Debug.Log("3");
            }
        }
        //if (Input.GetKeyDown(KeyCode.F)) ResetBackGround();
        //Debug.Log(colorTimer);
    }

    /// <summary>
    /// Sort the list by the x position of target
    /// </summary>
    private void SortChangePointByX()
    {
        ChangePoint.Sort((a, b) => a.Target.position.x.CompareTo(b.Target.position.x));

    }

    /// <summary>
    /// Star Background
    /// </summary>
    private void SetInitialColors()
    {
        foreach (SpriteRenderer baseRenderer in BaseBackground)
        {
            baseRenderer.color = StartBaseBackgroundColor;
        }

        foreach (SpriteRenderer gradientRenderer in Gradient)
        {
            gradientRenderer.color = StartGradientColor;
        }
        if (finishColorsChange) finishColorsChange = false;
    }

    private void SaveLastBackgroundColor()
    {
        if (_currentChangePoint == 0)
        {
            ActualBaseBackgroundColor = StartBaseBackgroundColor;
            ActualGradientColor = StartGradientColor;
        }
        else
        {
            ActualBaseBackgroundColor = ChangePoint[_currentChangePoint - 1].BaseBackgroundColor;
            ActualGradientColor = ChangePoint[_currentChangePoint - 1].GradientColor;
        }
    }

    private void BackgroundNoTransition()
    {
        SaveLastBackgroundColor();
        foreach (SpriteRenderer baseRenderer in BaseBackground)
        {
            baseRenderer.color = ChangePoint[_currentChangePoint].BaseBackgroundColor;
        }

        foreach (SpriteRenderer gradientRenderer in Gradient)
        {
            gradientRenderer.color = ChangePoint[_currentChangePoint].GradientColor;
        }
        if (_currentChangePoint == ChangePoint.Count - 1)
        {
            finishColorsChange = true;
            return;
        }
        _currentChangePoint++;
        //Debug.Log("1.1");
    }

    /// <summary>
    /// Change Background, use it when reach the new target
    /// </summary>
    private void BackgroundTransition()
    {
        if (colorTimer < 1f)
        {
            colorTimer += Time.deltaTime / ChangePoint[_currentChangePoint].colorTransitionDuration;

            foreach (SpriteRenderer baseRenderer in BaseBackground)
            {
                baseRenderer.color = Color.Lerp(ActualBaseBackgroundColor, ChangePoint[_currentChangePoint].BaseBackgroundColor, colorTimer);
            }

            foreach (SpriteRenderer gradientRenderer in Gradient)
            {
                gradientRenderer.color = Color.Lerp(ActualGradientColor, ChangePoint[_currentChangePoint].GradientColor, colorTimer);
            }
            //Debug.Log("2.1");
        }
        else
        {
            if (_currentChangePoint == ChangePoint.Count - 1)
            {
                finishColorsChange = true;
                return;
            }
            _currentChangePoint++;
            colorTimer = 0f;
            //Debug.Log("2.2");
        }
    }

    public void ResetInitialPosContainer()
    {
        AllBackgroundObject.localPosition = initialPosContainer;
        timer = 0f;
        _currentChangePoint = 0;
    }

    /// <summary>
    /// Reset on Death
    /// </summary>
    public void ResetBackGround()
    {
        Invoke("SetInitialColors", 0.5f);
        Invoke("ResetInitialPosContainer", 0.5f);
    }

    private void MoveBackground()
    {
        timer += Time.deltaTime;

        float t = timer / MusicDurationInSeconds;
        Vector3 newPosContainer = Vector3.Lerp(initialPosContainer, initialPosContainer - new Vector3(MaxDistance, 0, 0), t);

        AllBackgroundObject.localPosition = newPosContainer;
    }
}



[System.Serializable]
public class BackgroundChangeData
{
    public Color BaseBackgroundColor;
    public Color GradientColor;
    public Transform Target;
    [Range(0, float.MaxValue)]
    public float colorTransitionDuration;
}
