using System.Collections;
using System.Collections.Generic;
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
    private float timer = 0.0f;
    private float colorTimer = 0.0f;
    private float MaxDistance;
    private Vector3 initialPosContainer;

    [Header("BaseBackground")]
    [SerializeField]
    [Tooltip("Add one by one the Base Backgrounds")]
    private List<Renderer> BaseBackground;
    [SerializeField]
    private Color StartBaseBackgroundColor;

    [Header("Gradient")]
    [SerializeField]
    [Tooltip("Add one by one the Gradients of Backgrounds")]
    private List<Renderer> Gradient;
    [SerializeField]
    private Color StartGradientColor;

    [Header("Target")]
    [SerializeField]
    [Tooltip("Add the position when need to change the colors of the background and set it.")]
    private List<BackgroundChangeData> ChangePoint;
    private int _currentChangePoint;
    private float _cameraPosition;

    private CameraFollow _camera;

    private void Start()
    {

        SetInitialColors();

        if (ChangePoint.Count != 0)
            SortChangePointByX();

        _camera = GetComponentInParent<CameraFollow>();
        MaxDistance = LastBackground.position.x - FirstBackground.position.x;
        initialPosContainer = AllBackgroundObject.localPosition;
        _currentChangePoint = 0;
    }

    private void Update()
    {
        _cameraPosition = transform.position.x - _camera.offset.x;

        if (ChangePoint.Count != 0 && _cameraPosition > ChangePoint[_currentChangePoint].Target.position.x)
        {
            SetNewBackgroundColors();
        }
        else colorTimer = 0f;
        MoveBackground();
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
    }

    /// <summary>
    /// Change Background, use it when reach the new target
    /// </summary>
    private void SetNewBackgroundColors()
    {
        if (colorTimer < 1f)
        {
            colorTimer += Time.deltaTime / ChangePoint[_currentChangePoint].colorTransitionDuration;

            foreach (SpriteRenderer baseRenderer in BaseBackground)
            {
                Color CurrentColor = baseRenderer.color;
                baseRenderer.color = Color.Lerp(CurrentColor, ChangePoint[_currentChangePoint].BaseBackgroundColor, colorTimer);
            }

            foreach (SpriteRenderer gradientRenderer in Gradient)
            {
                Color CurrentColor = gradientRenderer.color;
                gradientRenderer.color = Color.Lerp(CurrentColor, ChangePoint[_currentChangePoint].GradientColor, colorTimer);
            }
        }
        else
        {
            if (_currentChangePoint == ChangePoint.Count - 1) return;
            _currentChangePoint++;
            colorTimer = 0f;
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
    public float colorTransitionDuration;
}
