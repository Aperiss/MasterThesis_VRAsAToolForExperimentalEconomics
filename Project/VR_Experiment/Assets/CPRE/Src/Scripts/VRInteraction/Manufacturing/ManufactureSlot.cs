using System.Collections.Generic;
using System.Linq;
using CPRE.SOFramework.DataContainers.Variables;
using CPRE.SOFramework.EventSystem.Channels;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace CPRE.Scripts.VRInteraction.Manufacturing {
    public class ManufactureSlot : MonoBehaviour {
        [SerializeField] private IntEventChannel removeLogFromStockpileEventChannel;
        [SerializeField] private GameObject indicator;
        
        public bool isAvailable = true;
        public Log attachedLog;
        public bool isManufacturing = false;

        private Transform _previousRootTransform;
        private List<Log> candidateLogs = new List<Log>();
        
        private void Update()
        {
            if(!isAvailable && attachedLog != null && !attachedLog.isGrabbed && !isManufacturing) 
            {
                attachedLog.SnapToRootTransform();
            }
            if(!isAvailable && attachedLog != null && !attachedLog.isGrabbed && isManufacturing) 
            {
                attachedLog.SnapToRootPosition();
            }

            if (attachedLog != null && attachedLog.isGrabbed) 
            {
                DetachLog();
            }

            // If the slot is available and there are candidate logs, 
            // attach the first log in the list that is not grabbed
            if(isAvailable && candidateLogs.Count > 0)
            {
                Log logToAttach = candidateLogs.FirstOrDefault(log => !log.isGrabbed);
                if(logToAttach != null) 
                {
                    AttachLog(logToAttach);
                    candidateLogs.Remove(logToAttach); // Remove the attached log from candidate logs
                }
            }
            
            if(candidateLogs.Count == 0) 
            {
                indicator.GetComponent<Outline>().OutlineColor = Color.white;
            }
            else {
                indicator.GetComponent<Outline>().OutlineColor = Color.yellow;
            }

            if (!isAvailable) {
                indicator.GetComponent<Outline>().OutlineColor = Color.green;
            }
        }
    
        private void OnTriggerEnter(Collider other) 
        {
            Log log = other.GetComponent<Log>();
            if(log != null && isAvailable) 
            {
                indicator.GetComponent<Outline>().OutlineColor = Color.yellow;
                candidateLogs.Add(log); // Add the log to the list of candidate logs
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Log log = other.GetComponent<Log>();
            if (log != null)
            {
                candidateLogs.Remove(log); // Remove the log from the list of candidate logs
            }
        }
    
        public void AttachLog(Log log) 
        {
            attachedLog = log;
            _previousRootTransform = log.rootTransform;
            attachedLog.rootTransform = transform;
            isAvailable = false;
            var feedbacks = indicator.GetComponent<MMF_Player>().FeedbacksList;
            foreach (var feedback in feedbacks) {
                if(feedback.Label == "Grow") feedback.Active = true;
                else feedback.Active = false;
            }
            
            indicator.GetComponent<MMF_Player>().PlayFeedbacks();
        }
    
        public void DetachLog() 
        {
            var feedbacks = indicator.GetComponent<MMF_Player>().FeedbacksList;
            foreach (var feedback in feedbacks) {
                if(feedback.Label == "Shrink") feedback.Active = true;
                else feedback.Active = false;
            }
            indicator.GetComponent<MMF_Player>().PlayFeedbacks();
            
            if(attachedLog != null) 
            {
                attachedLog.rootTransform = _previousRootTransform;
                attachedLog = null;
                isAvailable = true;
            }
        }
        
        public void ManufactureLog() 
        {
            attachedLog.Manufacture();
            isManufacturing = true;
        }
        
        public void OnLogManufactured() 
        {
            isManufacturing = false;
            removeLogFromStockpileEventChannel.Raise(attachedLog.logIndex);
            DetachLog();
        }
    }
}