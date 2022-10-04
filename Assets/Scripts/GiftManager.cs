using Assets.Scripts.OOPWork;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    public GiftController Gifts;

    public Sprite YumiBox;
    public Sprite YumiBG;
    public string YumiText;

    public Sprite JenniBox;
    public Sprite JenniGift;

    public Sprite SethBox;
    public Sprite SethGift;

    public Sprite YuuBox;
    public Sprite YuuGift;

    public Sprite TaltusBox;
    public Sprite TaltusGift;

    public Sprite SyrenBox;
    public Sprite SyrenGift;

    public Sprite ChloeBox;
    public Sprite ChloeGift;

    public List<string> TriggeredFunctions = new List<string>();

    public void Start()
    {
        Gifts = FindObjectOfType<GiftController>();
    }

    public void Give_Yumi_Gift()
    {
        SpeechUnits units = new SpeechUnits(new List<SpeechUnit>()
            {
                new SpeechUnit(new List<SpeechSingleTextContent>()
                {
                    new SpeechSingleTextContent("Kek", "! :D"),
                    new SpeechSingleTextContent("Jenni", "Did you just steal?"),
                    new SpeechSingleTextContent("Kek", "yes"),
                })
            });

        Gifts.OpenGift(YumiBox, YumiBG, YumiText, units);
    }

    public void Give_Jenni_Gift()
    {
        if (TriggeredFunctions.Contains(nameof(Give_Jenni_Gift))) return;

        SpeechUnits units = new SpeechUnits(new List<SpeechUnit>()
            {
                new SpeechUnit(new List<SpeechSingleTextContent>()
                {
                    new SpeechSingleTextContent("Raiden", "Who may this be?"),
                    new SpeechSingleTextContent("Kek", "MOMMY RAIDENN!!!!!!!!!"),
                    new SpeechSingleTextContent("Raiden", "Hello... mortal."),
                    new SpeechSingleTextContent("Kek", "..."),
                    new SpeechSingleTextContent("Kek", "can you go out on a date with me?.."),
                    new SpeechSingleTextContent("Raiden", ". . ."),
                    new SpeechSingleTextContent("Raiden", "A date?"),
                    new SpeechSingleTextContent("Jenni", "He wants to go... explore the vass lands with you and protect Inazuma."),
                    new SpeechSingleTextContent("Raiden", "I see."),
                    new SpeechSingleTextContent("Raiden", " I then acknowledge that you are a person that is very brave.. Henceforth, going on a so called.. date? might be possible. Worry not. Should any danger arise during that, I shall dispose of it immediately."),
                    new SpeechSingleTextContent("Kek", "*Levitates*"),
                    new SpeechSingleTextContent("Jenni", "..."),
                    new SpeechSingleTextContent("Raiden", "well.. until then.. see you on our.. date warrior."),
                    new SpeechSingleTextContent("Kek", "AUNAFFEGBUJDBFEJDAFZGJ...MMOME. DONT GO."),

                    new SpeechSingleTextContent("Jenni", "kek.. shut the fuck up. glares you'll talk to raiden later... maybe"),
                    new SpeechSingleTextContent("Kek", "WHERE IS THE PHONE NUMBER, YOU SAID YOU HAVE IT???"),
                    new SpeechSingleTextContent("Jenni", "*sigh* fine."),
                    new SpeechSingleTextContent("Jenni", "(+81) 559-6969"),
                    new SpeechSingleTextContent("Jenni", "anyways.. back to what i was saying"),
                    new SpeechSingleTextContent("Jenni", "*looks at yumi asleep*"),
                    new SpeechSingleTextContent("Jenni", "looks at yumi asleep yumi worked so hard on her letter, bless her little soul, SO CUTE >U<"),
                    new SpeechSingleTextContent("Jenni", "she fell asleep during the time she was writing her letter and said she wanted to give it to you herself.. but well she fell asleep"),
                    new SpeechSingleTextContent("Jenni", "well get going kek, the others want to see you"),
                    new SpeechSingleTextContent("Chloe", "COME TALK TO ME AGAIN!! I MISS YOU"),
                    new SpeechSingleTextContent("Kek", "Woah."),
                    new SpeechSingleTextContent("Chloe", "*eyes flutter* UwU"),
                })
            });

        Gifts.OpenGift(JenniBox, JenniGift, units);
    }

    public void Seth_Give_Gift()
    {
        Gifts.OpenGift(SethBox, SethGift);
    }

    public void Give_Taltus_Gift()
    {
        Gifts.OpenGift(TaltusBox, TaltusGift);
    }

    public void Give_yuu_gift()
    {
        Gifts.OpenGift(YuuBox, YuuGift);
    }

    public void Syren_Give_Gift()
    {
        Gifts.OpenGift(SyrenBox, SyrenGift);
    }

    public void Chloe_GiveGift()
    {
        SpeechUnits units = new SpeechUnits(new List<SpeechUnit>()
            {
                new SpeechUnit(new List<SpeechSingleTextContent>()
                {
                    new SpeechSingleTextContent("Chloe", "I'm more than sure this piece beats anything that girl on the bench has ever made"),
                    new SpeechSingleTextContent("Jenni", "Wait what did you just say?"),
                    new SpeechSingleTextContent("Chloe", "Nothing NOTHING I WAS JUST KIDDING"),
                    new SpeechSingleTextContent("Chloe", "Anyways happy birthdy babi ily ?"),
                    new SpeechSingleTextContent("Kek", "Thank you"),
                    new SpeechSingleTextContent("Chloe", "AWWW THANK YOU'RE SO SWEETAOKOAOKFAS"),
                })
            });
        Gifts.OpenGift(ChloeBox, ChloeGift, units);

    }
}
