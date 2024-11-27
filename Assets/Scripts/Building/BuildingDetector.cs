using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float chechRaius = 3.0f;                 //������ ���� ����
    public Vector3 lastPosition;                    //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold = 0.1f;              //�̵� ���� �Ӱ谪
    public ConstructibleBuilding currentNearbyBuilding;       //���� ������ �ִ� ���� ������ ������
    public BuildingCrafter currentBuildingCrafter;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;              //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForBuilding();                                //�ʱ� ������ üũ
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if(Vector3.Distance(lastPosition, transform.position) > moveThreshold)      //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        {
            CheckForBuilding();                                                        //�̵��� ������ üũ
            lastPosition = transform.position;                                      //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        //����� �ǹ��� �ְ� EŰ�� ������ �� ������ ����
        if(currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if(!currentNearbyBuilding.isConstructed)                //�ǹ��� �Ǽ��Ǿ� ���� �ʴٸ�
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());         //PlayerInventory �����Ͽ� ������ ����
            }
            else if(currentBuildingCrafter != null)                 //ũ�����Ͱ� �ִ� ���
            {
                Debug.Log($"{currentNearbyBuilding.buildingName} �� ���� �޴� ����");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);
            }
        }
            
    }

    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�
    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chechRaius);        //���� ���� ���� ��� �ݶ��̴��� ã��

        float closestDistance = float.MaxValue;                     //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                         //���� ����� �ǹ� �ʱⰪ
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders)                 //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �ǹ��� ã��
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();        //�ǹ��� ����
            if (building != null)           //��� �ǹ� ������ ����
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //�Ÿ� ���
                if(distance < closestDistance)                                                      //�� ����� �ǹ��� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();                       //���⼭ ũ������ ��������
                }
            }
        }

        if(closestBuilding != currentNearbyBuilding)                //���� ����� �ǹ��� ���� �Ǿ��� �� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;                //���� ����� �ǹ� ������Ʈ
            currentBuildingCrafter = closesCrafter;

            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
