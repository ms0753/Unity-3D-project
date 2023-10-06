using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamagable> thingsToDamage = new List<IDamagable>();

    private void Start()
    {   // ���� ����(Invoke)�� �����ð� �Ŀ� ������ �ǵ��� �������
        // ������ �� ���� �޼ҵ� �̸��� String ���� �����־�� �Ѵ�.
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for(int i = 0; i<thingsToDamage.Count; i++)
        {
            thingsToDamage[i].TakePhysicalDamage(damage);
        }
    }

    // ����Ʈ�� ����ص� ��������, ���߿� �浹���� ������ ��쿡�� �ؽü�(?)�� ����ϴ� ���� ��õ�Ѵٰ� ��.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            thingsToDamage.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            thingsToDamage.Remove(damagable);
        }
    }
}
