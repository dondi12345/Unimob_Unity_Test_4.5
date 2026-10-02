using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.Functions;
using Rubik.VFX;
using Unimob.Construction;
using Unimob.Customer;
using Unimob.Human;
using Unimob.Market;
using Unimob.Player;
using Unimob.Product;
using UnityEngine;
using UnityEngine.AI;

namespace Unimob.Delivery
{
    public enum DeliveryMoveTarget
    {
        Construction,
        Customer,
        End,
    }

    public class DeliveryRender : MonoBehaviour
    {
        public HumanAIMove HumanAIMove;
        public HumanRenderSkin HumanRenderSkin;
        public float Speed = 3f; 
        public float RotateSpeed = 10f;
        public float ArriveTolerance = 0.3f;
        public float StuckTime = 0.5f;
        public float GiveDelay = 0.4f;
        public ConstructionRender ConstructionRender;
        public CustomerRender CustomerRender;

        private Coroutine _moveRoutine;

        public List<Transform> ListPointProductPosition;
        public List<ProductSkin> ListProduct;
        public BigNumber Price;

        public void GoToConstruction(ConstructionRender constructionRender, CustomerRender customerRender)
        {
            this.ConstructionRender = constructionRender;
            this.CustomerRender = customerRender;
            this.HumanAIMove.NavMeshAgent.Warp(this.transform.position);
            this.HumanRenderSkin.Move();
            this.MoveTo(constructionRender.HarvestPoint.position, DeliveryMoveTarget.Construction);
        }

        public void GoToCustomer()
        {
            this.ConstructionRender = null;
            this.HumanRenderSkin.CarryMove();
            this.MoveTo(this.CustomerRender.Dock.Delivery.position, DeliveryMoveTarget.Customer);
        }

        private void MoveTo(Vector3 destination, DeliveryMoveTarget moveTarget)
        {
            NavMeshAgent agent = this.HumanAIMove.NavMeshAgent;
            agent.speed = this.Speed;
            agent.updateRotation = true;
            agent.SetDestination(destination);

            if (this._moveRoutine != null)
            {
                this.StopCoroutine(this._moveRoutine);
            }
            this._moveRoutine = this.StartCoroutine(this.MoveRoutine(agent, moveTarget));
        }

        private IEnumerator MoveRoutine(NavMeshAgent agent, DeliveryMoveTarget moveTarget)
        {
            float stuckTime = 0f;
            while (true)
            {
                yield return null;
                if (agent.pathPending)
                {
                    continue;
                }
                if (agent.remainingDistance <= agent.stoppingDistance + this.ArriveTolerance)
                {
                    break;
                }
                stuckTime = agent.velocity.sqrMagnitude < 0.01f ? stuckTime + Time.deltaTime : 0f;
                if (stuckTime > this.StuckTime)
                {
                    break;
                }
            }

            agent.ResetPath();
            agent.updateRotation = false;
            this.HumanRenderSkin.Idle();

            switch (moveTarget)
            {
                case DeliveryMoveTarget.Construction:
                    this.ConstructionRender.Harvesting();
                    this.Price = MathBigNumber.Clone(this.ConstructionRender.Construction.FinalIncome);
                    yield return this.RotateTo(this.ConstructionRender.transform.position);
                    break;

                case DeliveryMoveTarget.Customer:
                    CustomerRender customer = this.CustomerRender;
                    this.GiveProducts();
                    customer.OnReceived();
                    yield return this.RotateTo(customer.transform.position);
                    float time = 0f;
                    while (time < this.GiveDelay)
                    {
                        time += Time.deltaTime;
                        yield return null;
                    }
                    this.HumanRenderSkin.Move();
                    agent.updateRotation = true;
                    agent.SetDestination(MarketController.Instance.DeleveryEnd.position);
                    yield return this.MoveRoutine(agent, DeliveryMoveTarget.End);
                    yield break;

                case DeliveryMoveTarget.End:
                    this._moveRoutine = null;
                    this.Clear();
                    yield break;
            }

            this._moveRoutine = null;
        }

        private IEnumerator RotateTo(Vector3 lookPosition)
        {
            Vector3 dir = lookPosition - this.transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude <= 0.0001f)
            {
                yield break;
            }
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

        public void AddProduct(ProductSkin productSkin)
        {
            int index = this.ListProduct.Count;
            if (index >= this.ListPointProductPosition.Count)
            {
                Debug.LogWarning($"[DeliveryRender] Thiếu ListPointProductPosition (index {index})", this);
                return;
            }
            productSkin.FlyTo(this.ListPointProductPosition[index]);
            this.ListProduct.Add(productSkin);
        }

        private void GiveProducts()
        {
            for (int i = 0; i < this.ListProduct.Count; i++)
            {
                this.CustomerRender.AddProduct(this.ListProduct[i]);
            }
            PlayerManager.Instance.AddGold(this.Price);
            VFXGameEntity vfx = VFXGameManager.Instance.InstantiateFX(VFXGameConfig.EffPay, this.CustomerRender.Dock.Currency.position, Quaternion.identity);
            vfx.SetMoveTo(this.transform, 0.5f);

            this.ListProduct.Clear();
            this.CustomerRender = null;
        }

        public void Clear()
        {
            this.ListProduct.Clear();
            this.ConstructionRender = null;
            this.CustomerRender = null;
            this.name = ObjectPoolingConfig.DeliveryRender;
            ObjectPoolingManager.Instance.PushObjectIntoPooling(this.transform);
        }
    }
}
