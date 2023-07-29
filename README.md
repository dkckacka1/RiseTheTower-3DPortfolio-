라이즈 더 타워
===


게임 장르 : 3D 자동전투 등반 게임
---

개발 목적 : 유니티 3D 게임 개발 능력 향상
---

사용 엔진 : UNITY 2021.3.25f1
---


개발 기간 : 약 12주 (2023.02.20 ~ 2023.05.15)
---


포트폴리오 영상
---
[유튜브 영상 링크](https://youtu.be/K_3etDquoiI)


빌드 파일
---
[구들 드라이브 다운로드 링크](https://drive.google.com/file/d/1qSuFW2CfGxNXUO3ecfuxjRgxlbR9l___/view?usp=sharing)


사용 서드파티
---
* Dotween
* TextMeshPro


주요 활용 기술
---
* #01)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/Core/Battle/BattleManager.cs#L22)) [싱글턴 패턴 사용 예시](https://copractice.tistory.com/90)

<details>
<summary>예시 코드</summary>
  
```csharp
private static BattleManager instance;
public static BattleManager Instance
{
    get
    {
        if (instance == null)
        {
            Debug.LogWarning("BattleManager is NULL");
            return null;
        }

        return instance;
    }
}
```
```csharp
private void Awake()
{
    // 싱글톤 패턴
    if (instance == null)
    {
        instance = this;
        battleUI = GetComponentInChildren<BattleSceneUIManager>();
        objectPool = GetComponentInChildren<ObjectPooling>();
    }
    else
    {
        Destroy(this.gameObject);
        return;
    }
}
```
</details>

---

* #02)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/Core/Battle/ObjectPooling.cs#L14)) [오브젝트 풀 사용 예시](https://copractice.tistory.com/91)


<details>
<summary>예시 코드</summary>
  
```csharp
Queue<BattleText> battleTextPool = new Queue<BattleText>(); // 전투 텍스트 오브젝트 풀

// 전투 텍스트를 생성합니다.
public BattleText CreateText()
{
    // 부모 오브젝트 하위에 전투 텍스트를 생성합니다.
    GameObject obj = Instantiate(battleTextPrefab, battleTextParent.transform);
    BattleText text = obj.GetComponent<BattleText>();
    return text;
}

// 오브젝트 풀에 있는 전투 텍스트를 반환합니다.
public BattleText GetText(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
{
    BattleText text;

    if (battleTextPool.Count > 0)
    {
        // 풀에 있는 것 사용
        text = battleTextPool.Dequeue();

    }
    else
    {
        // 새로 만들어서 풀에 넣기
        text = CreateText();
    }

    // 전투 텍스트를 세팅합니다.
    text.Init(textStr, position, type);
    text.gameObject.SetActive(true);

    return text;
}

// 전투 텍스트를 풀에 반환합니다.
public void ReturnText(BattleText text)
{
    text.gameObject.SetActive(false);
    battleTextPool.Enqueue(text);
}
```
</details>

---

* #03)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/Controller/Controller.cs#L213)) [현재 상태에 따라 컨트롤러의 동작을 변경하는 상태패턴 구현](https://copractice.tistory.com/92)


<details>
<summary>UML 구조</summary>
  
![img](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/assets/125544460/cf9572d8-c607-4ab3-86ff-b2e86c357572)

</details>

---

* #04)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/main/RPG/Assets/02.%20Scripts/Core/Main/GameManager.cs#L158)) [제네릭 형식 제약조건으로 장비아이템 데이터를 가져오는 함수](https://copractice.tistory.com/93)


<details>
<summary>예시 코드</summary>
  
```csharp
// 장비 아이템 데이터를 가져옵니다.
public bool GetEquipmentData<T>(int id,out T sourceData) where T : EquipmentData
{
    EquipmentData data;
    if (!equipmentDataDic.TryGetValue(id, out data))
        // 찾는 ID가 없다면
    {
        Debug.LogError("찾는 데이터가 없습니다.");
        sourceData = null;
        return false;
    }

    // 찾은 데이터를 T 로 변환합니다.
    sourceData = data as T;
    if (sourceData == null)
        // 변환 값이 없다면
    {
        Debug.LogError("찾은 데이터가 잘못된 데이터입니다.");
        return false;
    }

    return true;
}
```
</details>

---

* #05)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/Core/Battle/BattleManager.cs#L487)) [현재 자신의 위치에서 가장 가까운 컨트롤러를 반환하는 함수](https://copractice.tistory.com/94)


<details>
<summary>예시 코드</summary>
  
```csharp
/// <summary>
/// 가장 가까운 T를 찾아서 리턴합니다.
/// </summary>
/// <typeparam name="T">Controller한정</typeparam>
public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
{
    if (typeof(T) == typeof(PlayerController))
    // 찾는 컨트롤러가 플레이어 컨트롤러라면
    {
        if (livePlayer != null)
        {
            return livePlayer as T;
        }
    }
    else if (typeof(T) == typeof(EnemyController))
        // 찾는 컨트롤러가 에너미컨트롤러 라면
    {
        // 생존한 적리스트를 가져옵니다.
        List<EnemyController> list = liveEnemies.Where(enemy => !enemy.battleStatus.isDead).ToList();
        // 생존한 적이 없다면 null 리턴
        if (list.Count == 0) return null;

        // 리스트를 순회하면서 가장 가까운 적을 찾습니다.
        Controller nearTarget = list[0];
        float distance = Vector3.Distance(nearTarget.transform.position, transform.position);
        for (int i = 1; i < list.Count; i++)
        {
            float newDistance = Vector3.Distance(list[i].transform.position, transform.position);

            if (distance > newDistance)
            {
                nearTarget = list[i];
                distance = newDistance;
            }
        }

        // T 타입으로 형변환 해서 전달 해줍니다.
        return (T)nearTarget;
    }

    return null;
}
```
</details>

---

* #06)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/main/RPG/Assets/02.%20Scripts/Character/CharacterAppearance.cs)) [현재 무기 외형에 따라 나올 애니메이션 컨트롤러를 변경](https://copractice.tistory.com/95)


<details>
<summary>예시 코드</summary>
  
```csharp
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
```
</details>

---

* #07)([스크립트](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/UI/BattleSceneUI/BattleSceneUIManager.cs#L110)) [전투 시 플레이어 캐릭터의 바지와 헬멧의 인챈트를 확인하여 액티브 스킬을 장착](https://copractice.tistory.com/96)


<details>
<summary>예시 코드</summary>
  
```csharp
// 액티브 스킬을 세팅합니다.
public void InitAbility(Helmet helmet, Pants pants, BattleStatus status)
{
    if (helmet.suffix != null && helmet.suffix.isIncantAbility)
        // 헬멧의 접미 인챈트가 있고 인챈트에 따로 효과가 있다면
    {
        HelmetIncant incant = helmet.suffix as HelmetIncant;

        // 인챈트에서 효과 정보를 가지고 와서 세팅합니다.
        helmetAbility.gameObject.SetActive(true);
        helmetAbility.Init(helmet.suffix.abilityIcon, incant.skillCoolTime);
        helmetAbility.AbilityBtn.onClick.AddListener(() => 
        {
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

            incant.ActiveSkill(status);
        });
    }
    else
    {
        // 없다면 스킬 UI를 숨겨줍니다.
        helmetAbility.gameObject.SetActive(false);
    }

    if (pants.suffix != null && pants.suffix.isIncantAbility)
        // 바지에 접미 인챈트가 있고 인챈트에 따로 효과가 있다면
    {
        PantsIncant incant = pants.suffix as PantsIncant;

        // 인챈트에서 효과 정보를 가지고 와서 세팅합니다.
        PantsAbility.gameObject.SetActive(true);
        PantsAbility.Init(pants.suffix.abilityIcon, incant.skillCoolTime);
        PantsAbility.AbilityBtn.onClick.AddListener(() => 
        {
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

            incant.ActiveSkill(status);
        });
    }
    else
    {
        // 없다면 스킬 UI를 숨겨줍니다.
        PantsAbility.gameObject.SetActive(false);
    }
}
```
</details>

---

* #08)([스크립트](https://copractice.tistory.com/97)) [스테이지 선택 씬에서 무한 스크롤링 UI 구현](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/blob/a22175eea5794bc14b153e6e37f9398d5f7a520b/RPG/Assets/02.%20Scripts/UI/StageChoiceSceneUI/StageScrollViewController.cs#L10)

<details>
<summary>Ex</summary>
  
![ScrollView](https://github.com/dkckacka1/RiseTheTower-3DPortfolio-/assets/125544460/53e3f184-9ded-424a-8eeb-fd0c57fb0427)

</details>
