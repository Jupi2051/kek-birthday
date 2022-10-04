using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.OOPWork
{
    public class SpeechUnits
    {
        public List<SpeechUnit> Interactions = new List<SpeechUnit>();
        public int CurrentDialoguePointer = 0;
        public bool LoopAtTheEnd;

        public SpeechUnit MoveToNextDialogue()
        {
            CurrentDialoguePointer++;
            if (CurrentDialoguePointer >= Interactions.Count)
                CurrentDialoguePointer = LoopAtTheEnd? 0 : Interactions.Count - 1;

            return Interactions[CurrentDialoguePointer];
        }

        public SpeechUnit GetActiveDialogue()
        {
            return Interactions[CurrentDialoguePointer];
        }

        public SpeechUnits(List<SpeechUnit> interactions, bool LoopAtTheEnd = false)
        {
            this.LoopAtTheEnd = LoopAtTheEnd;
            Interactions = interactions;
        }

    }
}
