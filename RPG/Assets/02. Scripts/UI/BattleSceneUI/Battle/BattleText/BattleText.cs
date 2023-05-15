using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float speed;
        [SerializeField] float deleteTiming;
        [SerializeField] List<DamageTextMaterial> materials;

        float dir;

        private void OnEnable()
        {
            dir = Random.Range(-0.2f, 0.2f);
            text.DOFade(0, deleteTiming).OnComplete(() => { ReleaseText(); });
        }

        private void Update()
        {
            transform.position += (new Vector3(dir, 1, 0) * speed * Time.deltaTime);
        }

        #region Initialize
        public void Init(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            try
            {
                text.fontMaterial = materials.Find(mat => mat.type.Equals(type)).material;
            }
            catch
            {
                Debug.Log("마테리얼 변경 실패");
            }


            this.text.alpha = 1;
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.text.text = textStr;
        }

        public void ReleaseText()
        {
            BattleManager.ObjectPool.ReturnText(this);
        }
        #endregion


    }
}