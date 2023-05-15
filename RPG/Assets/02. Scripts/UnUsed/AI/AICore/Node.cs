using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.UnUsed
{
    public abstract class Node
    {
        public Context context;

        public Stats stats;
        bool isStart = false;

        public Stats Update()
        {
            if (!isStart)
            {
                isStart = true;
                OnStart();
            }

            stats = OnUpdate();

            if (stats == Stats.SUCCESS || stats == Stats.FAILURE)
            {
                isStart = false;
                OnStop();
            }

            return stats;
        }

        public abstract void Init(Context context);
        public abstract void OnStart();
        public abstract void OnStop();
        public abstract Stats OnUpdate();
    }
}