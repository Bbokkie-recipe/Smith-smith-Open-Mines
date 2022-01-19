using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        //EnemyFOVŬ������ ����
        EnemyFOV fov = (EnemyFOV)target;

        //���� ���� �������� ��ǥ�� ���(�־��� ������ 1/2)
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        //���� ������ ������� ����
        //Handles.color = Color.white;
        Handles.color = new Color(1, 1, 1, 0.2f);

        //�ܰ����� ǥ���ϴ� ������ �׸�
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);
                            //������ǥ , ��ֺ���, ���� ������

        //��ä�� ��������
        Handles.DrawSolidArc(fov.transform.position, Vector3.up,fromAnglePos,fov.viewAngle,fov.viewRange);
        //������ǥ, ��ֺ���, ��ä���� ������ǥ, ��ä���� ����, ��ä���� ������

        //�þ߰��� �ؽ�Ʈ ǥ��
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f), fov.viewAngle.ToString());
    }

}
