using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class CharacterAppearance : MonoBehaviour
    {
        public Transform weaponHandle;

        public AnimatorOverrideController oneHandedWeaponAniamtor;
        public AnimatorOverrideController twoHandedWeaponAttackAnimator;

        public void EquipWeapon(GameObject item)
        {
            Instantiate(item, weaponHandle);
        }

        // ORDER : #6) 현재 무기 외형에 따라 나올 애니메이션 컨트롤러를 변경
        public void EquipWeapon(int weaponApparenceID, weaponHandleType weaponType)
        {
            // 현재 무기 외형 ID 값을 가져옵니다.
            var childCount =  weaponHandle.childCount;
            // 현재 무기 외형을 제외한 다른 외형은 모두 꺼줍니다.
            for (int i = 0; i < childCount; i++)
            {
                weaponHandle.GetChild(i).gameObject.SetActive(false);
            }
            weaponHandle.GetChild(weaponApparenceID).gameObject.SetActive(true);

            Animator animator;
            if ((animator = GetComponent<Animator>()) != null)
            {
                // 현재 무기의 외형에 따라 애니메이션 컨트롤러를 변경합니다.
                // 런타임 도중 애니메이터의 애니메이션 컨트롤러를 변경하려면
                // animator.runtimeAnimatorController를 변경해주어야 한다.
                switch (weaponType)
                {
                    case weaponHandleType.OneHandedWeapon:
                        animator.runtimeAnimatorController = oneHandedWeaponAniamtor as RuntimeAnimatorController;
                        animator.Rebind();
                        break;
                    case weaponHandleType.TwoHandedWeapon:
                        animator.runtimeAnimatorController = twoHandedWeaponAttackAnimator as RuntimeAnimatorController;
                        animator.Rebind();
                        break;
                }
            }
        }

        public void SetCurrentAnimation(Animator animator, string prevAttack)
        {
            RuntimeAnimatorController myController = animator.runtimeAnimatorController;

            AnimatorOverrideController myOverrideController = myController as AnimatorOverrideController;
            if (myOverrideController != null)
                myController = myOverrideController.runtimeAnimatorController;

            AnimatorOverrideController animatorOverride = new AnimatorOverrideController();
            animatorOverride.runtimeAnimatorController = myController;
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}