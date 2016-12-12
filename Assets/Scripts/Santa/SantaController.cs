using UnityEngine;
using System.Collections;
using Spine.Unity;

public class SantaController : MonoBehaviour {

    [SpineSlot]
    public string leftShoe;
    [SpineSlot]
    public string rightShoe;
    [SpineSlot]
    public string leftGlove;
    [SpineSlot]
    public string rightGlove;
    [SpineSlot]
    public string leftGloveStretch;
    [SpineSlot]
    public string rightGloveStretch;
    [SpineSlot]
    public string bodyCoat;
    [SpineSlot]
    public string leftHandCoat;
    [SpineSlot]
    public string rightHandCoat;
    [SpineSlot]
    public string leftHandCoatStretch;
    [SpineSlot]
    public string rightHandCoatStretch;
    [SpineSlot]
    public string hat;

    public static SantaController controller = null;
    private SkeletonRenderer skeletonRenderer;

    public float Speed;
    public float SpeedFall;
    public AnimationCurve Curve;

    private bool busy;
    private Coroutine move;
    private SkeletonAnimation skeletonAnim;

    //Debug
    public bool hatSwitchHandler = false;
    private bool hatToggle = false;
    public bool shoesSwitchHandler = false;
    private bool shoesToggle = false;
    public bool coatSwitchHandler = false;
    private bool coatToggle = false;
    public bool glovesSwitchHandler = false;
    private bool glovesToggle = false;

    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        skeletonRenderer = GetComponent<SkeletonRenderer>();
        busy = false;
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
    }

    public void MoveTo(Vector3 target)
    {
        if (!busy)
        {
            if (move != null)
                StopCoroutine(move);

            move = StartCoroutine(MoveToCoroutine(target));
        }
    }

    IEnumerator MoveToCoroutine(Vector3 target)
    {
        // Can we continue already started move animation?
        if (skeletonAnim.AnimationName != "walk")
            skeletonAnim.AnimationState.SetAnimation(0, "walk", true).timeScale = 2f; 
                
        if (transform.position.x < target.x) // Going right
        {
            skeletonAnim.skeleton.FlipX = false;
        }
        else // Going left
        {
            skeletonAnim.skeleton.FlipX = true;
        }

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            if (GameController.Instance.IsInputEnabled)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
                yield return null;
            }
            else
                break;
        }
        skeletonAnim.AnimationState.SetAnimation(0, "idle", true);
    }

    public void FallTo(Transform target)
    {
        StartCoroutine(FallToCoroutine(target.position));
    }

    IEnumerator FallToCoroutine(Vector3 target)
    {
        // Can we continue already started fall animation?
        if (skeletonAnim.AnimationName != "fall")
            skeletonAnim.AnimationState.SetAnimation(0, "fall", true);
        
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, SpeedFall * Time.deltaTime);
            yield return null;
        }
        skeletonAnim.AnimationState.SetAnimation(0, "hitGround", false);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0);

    }

    public void changeHat(bool toggle) {
        if (toggle) skeletonRenderer.skeleton.SetAttachment(hat, hat);
        else skeletonRenderer.skeleton.SetAttachment(hat, null);
    }

    public void changeShoes(bool toggle)
    {
        if (toggle)
        {
            skeletonRenderer.skeleton.SetAttachment(leftShoe, leftShoe);
            skeletonRenderer.skeleton.SetAttachment(rightShoe, rightShoe);
        }
        else
        {
            skeletonRenderer.skeleton.SetAttachment(leftShoe, null);
            skeletonRenderer.skeleton.SetAttachment(rightShoe, null);
        }
    }

    public void changeCoat(bool toggle)
    {
        if (toggle)
        {
            skeletonRenderer.skeleton.SetAttachment(bodyCoat, bodyCoat);
            skeletonRenderer.skeleton.SetAttachment(leftHandCoat, leftHandCoat);
            skeletonRenderer.skeleton.SetAttachment(rightHandCoat, rightHandCoat);
            //skeletonRenderer.skeleton.SetAttachment(leftHandCoatStretch, leftHandCoatStretch);
            //skeletonRenderer.skeleton.SetAttachment(rightHandCoatStretch, rightHandCoatStretch);
        }
        else
        {
            skeletonRenderer.skeleton.SetAttachment(bodyCoat, null);
            skeletonRenderer.skeleton.SetAttachment(leftHandCoat, null);
            skeletonRenderer.skeleton.SetAttachment(rightHandCoat, null);
            skeletonRenderer.skeleton.SetAttachment(leftHandCoatStretch, null);
            skeletonRenderer.skeleton.SetAttachment(rightHandCoatStretch, null);
        }
    }

    public void changeGloves(bool toggle)
    {
        if (toggle)
        {
            skeletonRenderer.skeleton.SetAttachment(leftGlove, leftGlove);
            skeletonRenderer.skeleton.SetAttachment(rightGlove, rightGlove);
            //skeletonRenderer.skeleton.SetAttachment(leftGloveStretch, leftGloveStretch);
            //skeletonRenderer.skeleton.SetAttachment(rightGloveStretch, rightGloveStretch);
        }
        else
        {
            skeletonRenderer.skeleton.SetAttachment(leftGlove, null);
            skeletonRenderer.skeleton.SetAttachment(rightGlove, null);
            skeletonRenderer.skeleton.SetAttachment(leftGloveStretch, null);
            skeletonRenderer.skeleton.SetAttachment(rightGloveStretch, null);
        }
    }

    // Update is called once per frame
    void Update () {

        if (hatSwitchHandler)
        {
            hatSwitchHandler = false;
            changeHat(hatToggle);
            hatToggle = !hatToggle;
        }
        if (coatSwitchHandler)
        {
            coatSwitchHandler = false;
            changeCoat(coatToggle);
            coatToggle = !coatToggle;
        }
        if (shoesSwitchHandler)
        {
            shoesSwitchHandler = false;
            changeShoes(shoesToggle);
            shoesToggle = !shoesToggle;
        }
        if (glovesSwitchHandler)
        {
            glovesSwitchHandler = false;
            changeGloves(glovesToggle);
            glovesToggle = !glovesToggle;
        }

    }
}
