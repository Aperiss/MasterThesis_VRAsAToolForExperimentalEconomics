using System.Collections;
using CPRE.SOFramework.EventSystem.Channels;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction {
    public class Log : MonoBehaviour {
        
        public int logIndex;
        public Quaternion rotationOffset;
        public Vector3 positionOffset;
        public Transform rootTransform;
        public bool isGrabbed = false;

        public VoidEventChannel logManufacturedEventChannel;
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") && !isGrabbed)
            {
                SnapToRootTransform();
            }
        }
    
        public void SnapToRootTransform()
        {
            transform.position = rootTransform.position + positionOffset;
            transform.rotation = rootTransform.rotation;
            
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        public void SnapToRootPosition()
        {
            transform.position = rootTransform.position + positionOffset;
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    
        public void Grab()
        {
            isGrabbed = true;
        }

        public void Release()
        {
            isGrabbed = false;
        }
        
        public void Manufacture()
        {
            MMF_Player player = this.gameObject.GetComponent<MMF_Player>();
            if(player != null)
            {
                player.PlayFeedbacks();
                float duration = player.TotalDuration - 0.5f;
                Debug.Log(duration);
                StartCoroutine(AnimationDelay(duration));
            }
        }

        private IEnumerator AnimationDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            NotifyManufactured();
            yield return new WaitForSeconds(0.5f);
            OnAnimationEnded();
        }

        private void OnAnimationEnded()
        {
            this.gameObject.GetComponent<MMF_Player>().StopFeedbacks();
            Debug.Log("Finished manufacturing log");
        }
        private void NotifyManufactured()
        {
            logManufacturedEventChannel.Raise();
        }
    }
}
