using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.UnUsed
{
    public class PlayerTree : RPG.UnUsed.BehaviourTree
    {
        public override void SetRootNode()
        {
            rootNode = SetStartNode();
        }

        private Node SetStartNode()
        {
            CompositeNode sequence = new SequenceComposite();
            // 전투 시작시 살아있는 적 확인
            sequence.GetChilds().Add(new FindControllerAction());
            // 전투 노드
            sequence.GetChilds().Add(SetBaattleNode());
            // 승리 노드
            sequence.GetChilds().Add(SetWinNode());
            return sequence;
        }

        private Node SetWinNode()
        {
            throw new NotImplementedException();
        }

        private Node SetBaattleNode()
        {
            DecoratorNode repeatUntillFail = new UntillFailureRepeatDecorator();
            CompositeNode selector = new SelectorComposite();
            repeatUntillFail.child = selector;
            IfDecorator ifs = new IfDecorator(() => { return false; });

            /*
                                                                   (실패 반복)
                                                                   [시퀀스]
                [셀렉터]                                           [시퀀스]
                (타겟한 적이 있는가?)
                {없다면}
                {살아있는적이있는가?)           
                {있다면 타겟설정}
             
             
             
             */


            throw new NotImplementedException();
        }
    }
}