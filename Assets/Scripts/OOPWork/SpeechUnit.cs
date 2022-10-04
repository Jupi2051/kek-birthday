using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Assets.Scripts.OOPWork
{
    public class SpeechUnit
    {
        public int CurrentReplyIndex = 0;

        public List<SpeechSingleTextContent> SpokenTexts = new List<SpeechSingleTextContent>();
        
        public SpeechUnit(List<SpeechSingleTextContent> content)
        {
            SpokenTexts = content;
        }

        public Tuple<bool, SpeechSingleTextContent> Next()
        {
            Tuple<bool, SpeechSingleTextContent> tuple = new Tuple<bool, SpeechSingleTextContent>(
                CurrentReplyIndex == SpokenTexts.Count,
                CurrentReplyIndex == SpokenTexts.Count? new SpeechSingleTextContent("Null", "Null") : SpokenTexts[CurrentReplyIndex]
                );
            CurrentReplyIndex++;
            if (CurrentReplyIndex == SpokenTexts.Count + 1) CurrentReplyIndex = 0;
            return tuple;
        }
    }
}
