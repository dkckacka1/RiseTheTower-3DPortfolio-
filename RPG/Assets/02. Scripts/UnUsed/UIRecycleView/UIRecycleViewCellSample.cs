using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;
using RPG.Battle.UI;
using RPG.Battle.Control;

namespace RPG.UnUsed
{
    public class UICellSampleDate
    {
        public int index;
        public string name;
    }

    public class UIRecycleViewCellSample : UIRecycleViewCell<UICellSampleDate>
    {
        [SerializeField]
        private Text nIndex;
        [SerializeField]
        private Text txtName;
        public override void UpdateContent(UICellSampleDate itemData)
        {
            nIndex.text = itemData.index.ToString();
            txtName.text = itemData.name;
        }

        public void onClickButton()
        {
            Debug.Log(nIndex.text.ToString());
        }
    }
}
