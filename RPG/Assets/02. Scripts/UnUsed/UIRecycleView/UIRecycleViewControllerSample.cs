using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class UIRecycleViewControllerSample : UIRecycleViewController<UICellSampleDate>
    {
        private void LoadData()
        {
            int index  = 1;
            tableData = new List<UICellSampleDate>()
            {
                new UICellSampleDate{index = index++,name ="One"},
                new UICellSampleDate{index = index++,name ="Two"},
                new UICellSampleDate{index = index++,name ="Three"},
                new UICellSampleDate{index = index++,name ="Four"},
                new UICellSampleDate{index = index++,name ="Five"},
                new UICellSampleDate{index = index++,name ="Six"},
                new UICellSampleDate{index = index++,name ="Seven"},
                new UICellSampleDate{index = index++,name ="Eight"},
                new UICellSampleDate{index = index++,name ="Nine"},
                new UICellSampleDate{index = index++,name ="Ten"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
                new UICellSampleDate{index = index++,name ="Checker"},
            };

            InitializeTableView();
        }

        protected override void Start()
        {
            base.Start();

            LoadData();
        }

        public void OnPressCell(UIRecycleViewCellSample cellSample)
        {
            Debug.Log("Cell Click");
            Debug.Log(tableData[cellSample.Index].name);
        }
    }

}