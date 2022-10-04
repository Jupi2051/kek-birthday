using Assets.Scripts.OOPWork;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GiftController : MonoBehaviour
{
    public AudioSource GiftShake;
    public AudioSource GiftOpen;

    public Image Darkbackground;

    public Image GiftBoxUI;
    public Image FButton;

    public Image GiftItemImage;
    public Image GiftItemText;
    public TMP_Text GiftText;


    public bool AllowGiftInteraction = false;

    public float FadeAnimationLoad = 0;
    public bool LoadFadeAnimation = false;
    public float FadeAnimationLoadSpeed = 0.01f;

    public bool CanShakeGift = false;


    public int ShakeTimes = 10;
    public int ShakenTimes = 0;
    public float ShakeCountDown;
    public float ShakeTimeLimit = 1;

    public bool OpenGiftNow = false;

    public Animator GiftBox;

    public SpeechUnits PostOpeningDialogue;

    public float OpenGiftLoad = 0;
    public float OpenGiftSpeed = 0.01f;


    public bool CloseGiftUI = false;

    public bool ViewingGiftrnwym = false;

    public MarioController player;

    // Start is called before the first frame update
    void Start()
    {
        GiftBox = GameObject.Find("GiftBoxUI").GetComponent<Animator>();
        GiftItemImage = GameObject.FindWithTag("GiftImage").GetComponent<Image>();
        GiftItemText = GameObject.FindWithTag("GiftText").GetComponent<Image>();
        GiftText = GetComponentInChildren<TMP_Text>();
        ViewingGiftrnwym = false;
        player = FindObjectOfType<MarioController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanShakeGift)
        {
            ShakeCountDown += 0.1f;
            if (ShakeCountDown > ShakeTimeLimit)
            {
                ShakenTimes = 0;
                ShakeCountDown = 0;
            }
        }

        if (OpenGiftNow)
        {
            if (OpenGiftLoad >= 1)
            {
                OpenGiftLoad = 1;
                OpenGiftNow = false;
            }
            Color GiftBoxAlpha = Color.Lerp(new Color(GiftBoxUI.color.r, GiftBoxUI.color.g, GiftBoxUI.color.b, 1), new Color(GiftBoxUI.color.r, GiftBoxUI.color.g, GiftBoxUI.color.b, 0), OpenGiftLoad);
            Color GiftTextItemAlpha = Color.Lerp(new Color(GiftItemText.color.r, GiftItemText.color.g, GiftItemText.color.b, 0), new Color(GiftItemText.color.r, GiftItemText.color.g, GiftItemText.color.b, 1), OpenGiftLoad);
            Color GiftItemAlpha = Color.Lerp(new Color(GiftItemImage.color.r, GiftItemImage.color.g, GiftItemImage.color.b, 0), new Color(GiftItemImage.color.r, GiftItemImage.color.g, GiftItemImage.color.b, 1), OpenGiftLoad);
            float GiftBoxScale = Mathf.Lerp(0, 1, OpenGiftLoad);
            GiftItemText.transform.localScale = new Vector3(GiftBoxScale, GiftBoxScale, GiftBoxScale);
            GiftItemImage.transform.localScale = new Vector3(GiftBoxScale, GiftBoxScale, GiftBoxScale);
            GiftBoxUI.color = GiftBoxAlpha;
            GiftItemText.color = GiftTextItemAlpha;
            FButton.color = GiftBoxAlpha;
            OpenGiftLoad += OpenGiftSpeed;
        }

        if (LoadFadeAnimation)
        {
            float AlphaValue = Mathf.Lerp(0, 1, FadeAnimationLoad);
            FadeAnimationLoad += FadeAnimationLoadSpeed;
            Darkbackground.color = new Color(Darkbackground.color.r, Darkbackground.color.g, Darkbackground.color.b, AlphaValue >= 0.6f? 0.6f : AlphaValue);
            GiftBoxUI.color = new Color(GiftBoxUI.color.r, GiftBoxUI.color.g, GiftBoxUI.color.b, AlphaValue); ;
            FButton.color = new Color(FButton.color.r, FButton.color.g, FButton.color.b, AlphaValue); ;
            if (FadeAnimationLoad >= 1)
            {
                LoadFadeAnimation = false;
                CanShakeGift = true;
            }
        }

        if (CloseGiftUI)
        {
            FadeAnimationLoad -= FadeAnimationLoadSpeed + 0.05f;
            float AlphaValue = Mathf.Lerp(0, 1, FadeAnimationLoad);
            Darkbackground.color = new Color(Darkbackground.color.r, Darkbackground.color.g, Darkbackground.color.b, AlphaValue >= 0.6f ? 0.6f : AlphaValue);
            GiftBoxUI.color = new Color(GiftBoxUI.color.r, GiftBoxUI.color.g, GiftBoxUI.color.b, 0);
            FButton.color = new Color(FButton.color.r, FButton.color.g, FButton.color.b, AlphaValue);

            GiftItemText.transform.localScale = new Vector3(AlphaValue, AlphaValue, AlphaValue);
            GiftItemImage.transform.localScale = new Vector3(AlphaValue, AlphaValue, AlphaValue);

            if (FadeAnimationLoad <= 0)
            {
                player.CanPlayerMove = true;
                CloseGiftUI = false;
                OpenGiftLoad = 0;
                ShakenTimes = 0;
                ViewingGiftrnwym = false;
                OpenGiftNow = false;
                if (PostOpeningDialogue != null)
                {
                    DialogueUI Dialogue = FindObjectOfType<DialogueUI>();
                    Dialogue.StartDialogue(PostOpeningDialogue.GetActiveDialogue(), PostOpeningDialogue);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanShakeGift)
        {
            GiftBox.SetTrigger("Wobble");
            ShakenTimes++;
            ShakeCountDown = 0;
            GiftShake.Play();
            if (ShakenTimes > ShakeTimes)
            {
                GiftOpen.Play();
                OpenGiftNow = true;
                ViewingGiftrnwym = true;
                CanShakeGift = false;
                ShakeCountDown = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ViewingGiftrnwym)
            {
                FadeAnimationLoad = 1;
                CloseGiftUI = true;
                ViewingGiftrnwym = false;
            }
        }
    }

    public void OpenGift(Sprite BoxImage, Sprite GiftImage, SpeechUnits PostOpenDialogue = null)
    {
        GiftItemText.gameObject.SetActive(false);
        GiftItemImage.gameObject.SetActive(true);
        GiftItemImage.sprite = GiftImage;
        GiftBoxUI.sprite = BoxImage;
        GiftText.text = "";
        PostOpeningDialogue = PostOpenDialogue;
        LoadFadeAnimation = true;
        player.CanPlayerMove = false;
    }

    public void OpenGift(Sprite BoxImage, Sprite Background, string Text, SpeechUnits PostOpenDialogue = null)
    {
        GiftItemText.gameObject.SetActive(true);
        GiftItemImage.gameObject.SetActive(false);
        GiftItemText.sprite = Background;
        GiftBoxUI.sprite = BoxImage;
        GiftText.text = Text;
        PostOpeningDialogue = PostOpenDialogue;
        LoadFadeAnimation = true;
        player.CanPlayerMove = false;
    }
}
