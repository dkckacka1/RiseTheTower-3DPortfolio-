using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;
using DG.Tweening;

/*
 * 전투 텍스트 UI 클래스
 */

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;                  // 텍스트 변수
        [SerializeField] float speed;                           // 텍스트 속도
        [SerializeField] float deleteTiming;                    // 텍스트가 페이드 아웃될 시간
        [SerializeField] List<DamageTextMaterial> materials;    // 전투 텍스트 마테리얼

        float dir;  // 전투 텍스트가 랜덤한 위치에 자리잡을수 있도록 하는 오프셋

        // 활성화되면 바로 페이드아웃을 시작합니다
        private void OnEnable()
        {
            dir = Random.Range(-0.2f, 0.2f);
            // 페이드 아웃이 완료되면 전투텍스트를 오브젝트 풀에 반환합니다.
            text.DOFade(0, deleteTiming).OnComplete(() => { ReleaseText(); });
        }

        private void Update()
        {
            // 위쪽으로 이동합니다.
            transform.position += (new Vector3(dir, 1, 0) * speed * Time.deltaTime);
        }

        #region Initialize
        // 전투 텍스트를 초기화 합니다
        public void Init(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            try
            {
                // 들어온 전투 타입에 맞게 텍스트의 마테리얼을 변경합니다
                text.fontMaterial = materials.Find(mat => mat.type.Equals(type)).material;
            }
            catch
            {
                Debug.Log("마테리얼 변경 실패");
            }


            // 텍스트의 알파값을 1로 설정하고 텍스트의 위치를 설정합니다.
            this.text.alpha = 1;
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.text.text = textStr;
        }

        // 오브젝트 풀에 반환합니다.
        public void ReleaseText()
        {
            BattleManager.ObjectPool.ReturnText(this);
        }
        #endregion


    }
}