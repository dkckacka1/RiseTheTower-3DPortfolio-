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

        public void EquipWeapon(int weaponApparenceID, weaponHandleType weaponType)
        {
            var childCount =  weaponHandle.childCount;
            for (int i = 0; i < childCount; i++)
            {
                weaponHandle.GetChild(i).gameObject.SetActive(false);
            }
            weaponHandle.GetChild(weaponApparenceID).gameObject.SetActive(true);

            Animator animator;
            if ((animator = GetComponent<Animator>()) != null)
            {

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
            else
            {
                Debug.Log("Animator is Null");
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