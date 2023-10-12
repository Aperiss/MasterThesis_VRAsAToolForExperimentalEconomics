using System.Collections.Generic;
using CPRE.SOFramework.DataContainers.Sets;
using UnityEngine;

namespace CPRE.Scripts.Experiment.DataContainers {
    [CreateAssetMenu(menuName = "Runtime Sets/Experiment/Participant Runtime Set")]
    public class ParticipantRuntimeSet : BaseRuntimeSet<Participant> {
        public static implicit operator List<Participant>(ParticipantRuntimeSet set) {
            return set.Items;
        }
    }
}
