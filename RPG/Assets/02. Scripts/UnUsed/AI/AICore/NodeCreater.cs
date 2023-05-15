using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public static class NodeCreater 
    {
        public static SequenceComposite CreateSequence()
        {
            return new SequenceComposite();
        }

        public static DebugAction CreateDebug(string str)
        {
            return new DebugAction(str);
        }
    }
}