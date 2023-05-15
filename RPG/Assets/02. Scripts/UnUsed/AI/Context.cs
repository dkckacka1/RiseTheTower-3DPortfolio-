using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Behaviour;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.UnUsed
{
    public class Context 
    {
        public GameObject gameobject;

        public Transform transform;
        public Rigidbody rigidbody;
        public Animator animator;

        public BattleStatus stats;
        public Movement movement;
        public Attack attack;
        public Controller controller;

        public Context(GameObject gameobject)
        {
            this.gameobject = gameobject;

            transform = gameobject.transform;
            rigidbody = gameobject.GetComponent<Rigidbody>();
            animator = gameobject.GetComponent<Animator>();

            stats = gameobject.GetComponent<BattleStatus>();
            movement = gameobject.GetComponent<Movement>();
            attack = gameobject.GetComponent<Attack>();
            controller = gameobject.GetComponent<Controller>();
        }
    }

}