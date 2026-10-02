using System.Collections;
using Unimob.Delivery;
using Unimob.Human;
using Unimob.Market;
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
        public Dock Dock;

        private Coroutine _moveRoutine;

        public void GoToDock(Dock dock)
        {
            this.Dock = dock;
            NavMeshAgent agent = this.HumanAIMove.NavMeshAgent;
            agent.speed = this.Speed;
            agent.Warp(this.transform.position);
            agent.SetDestination(dock.Customer.position);
            this.HumanRenderSkin.Move();

            if (this._moveRoutine != null)
            {
                this.StopCoroutine(this._moveRoutine);
            }
            this._moveRoutine = this.StartCoroutine(this.MoveRoutine(agent));
        }

        private IEnumerator MoveRoutine(NavMeshAgent agent)
        {
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            agent.ResetPath();
            this.HumanRenderSkin.Idle();

            Vector3 dir = this.Dock.Currency.position - this.transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(dir);
                while (Quaternion.Angle(this.transform.rotation, target) > 1f)
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, this.RotateSpeed * Time.deltaTime);
                    yield return null;
                }
                this.transform.rotation = target;
            }

            this._moveRoutine = null;
        }
    }
}
