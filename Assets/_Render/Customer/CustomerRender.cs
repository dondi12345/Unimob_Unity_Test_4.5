using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Delivery;
using Unimob.Human;
using Unimob.Market;
using Unimob.Product;
using UnityEngine;
using UnityEngine.AI;

namespace Unimob.Customer
{
    public class CustomerRender : MonoBehaviour
    {
        public HumanAIMove HumanAIMove;
        public HumanRenderSkin HumanRenderSkin;
        public float Speed = 3f;
        public float RotateSpeed = 10f;
        public float ReceiveDelay = 0.4f;
        public Dock Dock;
        public List<Transform> ListPointProductPosition;
        public List<ProductSkin> ListProduct;

        private Coroutine _moveRoutine;

        public void GoToDock(Dock dock)
        {
            this.Dock = dock;
            NavMeshAgent agent = this.HumanAIMove.NavMeshAgent;
            agent.Warp(this.transform.position);
            this.HumanRenderSkin.Move();
            this.MoveTo(dock.Customer.position, false);
        }

        public void OnReceived()
        {
            if (this._moveRoutine != null)
            {
                this.StopCoroutine(this._moveRoutine);
            }
            this._moveRoutine = this.StartCoroutine(this.LeaveRoutine());
        }

        private IEnumerator LeaveRoutine()
        {
            float time = 0f;
            while (time < this.ReceiveDelay)
            {
                time += Time.deltaTime;
                yield return null;
            }

            this.Dock.CustomerRenderRegister = null;
            this.Dock = null;
            MarketController.Instance.CheckDocks();

            this.HumanRenderSkin.CarryMove();
            this.MoveTo(MarketController.Instance.CustomerEnd.position, true);
        }

        private void MoveTo(Vector3 destination, bool toEnd)
        {
            NavMeshAgent agent = this.HumanAIMove.NavMeshAgent;
            agent.speed = this.Speed;
            agent.updateRotation = true;
            agent.SetDestination(destination);

            if (this._moveRoutine != null)
            {
                this.StopCoroutine(this._moveRoutine);
            }
            this._moveRoutine = this.StartCoroutine(this.MoveRoutine(agent, toEnd));
        }

        private IEnumerator MoveRoutine(NavMeshAgent agent, bool toEnd)
        {
            Transform end = MarketController.Instance.CustomerEnd;
            do
            {
                yield return null;
                if (toEnd && MarketController.Instance.IsInEnd(this.transform.position, end))
                {
                    break;
                }
            }
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance);

            agent.ResetPath();
            this._moveRoutine = null;

            if (toEnd)
            {
                this.Clear();
                yield break;
            }

            agent.updateRotation = false;
            this.HumanRenderSkin.Idle();

            Vector3 dir = this.Dock.Currency.position - this.transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(dir);
                float time = 0f;
                while (time < 0.5f && Quaternion.Angle(this.transform.rotation, target) > 1f)
                {
                    time += Time.deltaTime;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, this.RotateSpeed * Time.deltaTime);
                    yield return null;
                }
                this.transform.rotation = target;
            }
        }

        public void AddProduct(ProductSkin productSkin)
        {
            int index = this.ListProduct.Count;
            if (index >= this.ListPointProductPosition.Count)
            {
                Debug.LogWarning($"[CustomerRender] Thiếu ListPointProductPosition (index {index})", this);
                return;
            }
            productSkin.FlyTo(this.ListPointProductPosition[index]);
            this.ListProduct.Add(productSkin);
        }

        public void Clear()
        {
            for (int i = 0; i < this.ListProduct.Count; i++)
            {
                ProductSkin productSkin = this.ListProduct[i];
                productSkin.name = ObjectPoolingConfig.ProductSkin;
                ObjectPoolingManager.Instance.PushObjectIntoPooling(productSkin.transform);
            }
            this.ListProduct.Clear();
            this.Dock = null;
            this.name = ObjectPoolingConfig.CustomerRender;
            ObjectPoolingManager.Instance.PushObjectIntoPooling(this.transform);
        }
    }
}
