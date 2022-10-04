using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.OOPWork
{
    [Serializable]
    public sealed class SpeechSingleTextContent
    {
        public string CharacterName { get; }
        public string Speech { get; }

        public string TriggerFunction { get; }

        public SpeechSingleTextContent(string characterName, string speech, string triggerFunction = null)
        {
            CharacterName = characterName;
            Speech = speech;
            TriggerFunction = triggerFunction == "" ? null : triggerFunction;
        }
    }
}
