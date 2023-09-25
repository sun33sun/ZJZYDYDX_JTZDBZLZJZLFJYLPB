using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ProjectBase
{
    public class CameraManager : SingletonMono<CameraManager>
    {
        public enum RenderType
        {
            Camera,RenderTexture
        }
        List<IPersonView> personViews = new List<IPersonView>();
        PersonViewField pvField;
        [SerializeField] bool isEnable = false;
        public PersonViewType pvType = PersonViewType.FirstPerson;
        [SerializeField] private RenderTexture rt;


        [SerializeField] Camera mainC;
        [SerializeField] CinemachineVirtualCamera noneC = null;
        [SerializeField] CinemachineVirtualCamera firstC = null;
        [SerializeField] CinemachineVirtualCamera thirdC = null;
        [SerializeField] CinemachineVirtualCamera roundC = null;
        [SerializeField] CinemachineVirtualCamera followC = null;

        //漫游相机的刚体
        Rigidbody roamRig = null;

        Vector3 originPos;
        Vector3 originAngle;
        float originFieldOfView;
        Vector3 nowPos;

        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                if (value)
                    roamRig.constraints = RigidbodyConstraints.FreezeRotation;
                else
                    roamRig.constraints = RigidbodyConstraints.FreezeAll;
                isEnable = value;
            }
        }

        bool isRotate = false;

        private void Start()
        {
            //查找组件
            roamRig = firstC.GetComponent<Rigidbody>();
            //记录初始位置
            originPos = firstC.transform.position;
            originAngle = firstC.transform.rotation.eulerAngles;
            originFieldOfView = firstC.m_Lens.FieldOfView;

            #region 移动与旋转

            //移动
            InputMgr.GetInstance().ChangerInput(true);
            //上下移动
            EventCenter.GetInstance().AddEventListener(KeyCode.LeftControl + "保持", OnEState);
            // EventCenter.GetInstance().AddEventListener<float>("鼠标滚轮", OnMouseScrollWheel);
            //旋转
            EventCenter.GetInstance().AddEventListener("鼠标右键按下", OnMouseRightDown);
            EventCenter.GetInstance().AddEventListener("鼠标右键抬起", OnMouseRightUp);
            EventCenter.GetInstance().AddEventListener<Vector2>("鼠标滑动", OnMouseSliding);
            EventCenter.GetInstance().AddEventListener(KeyCode.Space + "保持", OnQState);
            EventCenter.GetInstance().AddEventListener<Vector2>("移动方向", UpdateMovement);

            #endregion

            #region 初始化属性

            pvField.moveSpeed = 3;
            pvField.upSpeed = 2;
            pvField.rotateSpeed = 3;
            pvField.viewSpeed = 10;
            personViews.Add(new NonePersonView());
            personViews.Add(new FirstPersonView(firstC.transform, pvField));
            personViews.Add(new ThirdPersonView(thirdC, pvField));

            #endregion
        }

        #region 移动、上下移动、旋转、缩放视野

        void UpdateMovement(Vector2 dir)
        {
            personViews[(int)pvType].UpdateMovement(dir);
        }

        private void OnEState()
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnEState();
        }

        private void OnQState()
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnQState();
        }

        private void OnMouseRightDown()
        {
            if (!IsEnable)
                return;
            isRotate = true;
        }

        private void OnMouseRightUp()
        {
            if (!IsEnable)
                return;
            isRotate = false;
        }

        private void OnMouseSliding(Vector2 slidingValue)
        {
            if (!isRotate || !IsEnable)
                return;
            
            personViews[(int)pvType].OnMouseSliding(slidingValue);
        }

        private void OnMouseScrollWheel(float distance)
        {
            if (!IsEnable)
                return;
            personViews[(int)pvType].OnMouseScrollWheel(distance);
        }

        #endregion

        #region 调整人称

        public void ThirPersonView(PlayerController playerController)
        {
            pvType = PersonViewType.ThirdPerson;
            (personViews[(int)pvType] as ThirdPersonView).Player = playerController;
            thirdC.Priority = 2;
            noneC.Priority = 1;
            firstC.Priority = 1;
            followC.Priority = 1;
        }

        public void NonePersonView()
        {
            pvType = PersonViewType.None;
            noneC.Priority = 2;
            thirdC.Priority = 1;
            firstC.Priority = 1;
            roundC.Priority = 1;
            followC.Priority = 1;
        }

        public void FirstPersonView()
        {
            pvType = PersonViewType.FirstPerson;
            firstC.Priority = 2;
            noneC.Priority = 1;
            thirdC.Priority = 1;
            roundC.Priority = 1;
            followC.Priority = 1;
        }
        
        public void RoundPersonView(Transform target)
        {
            pvType = PersonViewType.RoundObject;
            roundC.Follow = target;
            roundC.Priority = 2;
            noneC.Priority = 1;
            firstC.Priority = 1;
            thirdC.Priority = 1;
            followC.Priority = 1;
        }

        public void FollowPersonView(Transform target)
        {
            pvType = PersonViewType.FollowObject;
            followC.Follow = target;
            followC.Priority = 2;
            noneC.Priority = 1;
            firstC.Priority = 1;
            thirdC.Priority = 1;
            roundC.Priority = 1;
        }

        public void SwitchRenderType(RenderType renderType)
        {
            switch (renderType)
            {
                case RenderType.Camera:
                    mainC.targetTexture = null; 
                    break;
                case RenderType.RenderTexture:
                    mainC.targetTexture = rt;
                    break;
            }
        }
        #endregion

        private void OnDestroy()
        {
            //上下移动
            EventCenter.GetInstance().RemoveEventListener(KeyCode.LeftControl + "保持", OnEState);
            // EventCenter.GetInstance().RemoveEventListener<float>("鼠标滚轮", OnMouseScrollWheel);
            //旋转
            EventCenter.GetInstance().RemoveEventListener("鼠标右键按下", OnMouseRightDown);
            EventCenter.GetInstance().RemoveEventListener("鼠标右键抬起", OnMouseRightUp);
            EventCenter.GetInstance().RemoveEventListener<Vector2>("鼠标滑动", OnMouseSliding);
            EventCenter.GetInstance().RemoveEventListener(KeyCode.Space + "保持", OnQState);
            EventCenter.GetInstance().RemoveEventListener<Vector2>("移动方向", UpdateMovement);
        }
    }
}