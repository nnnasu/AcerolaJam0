using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Core.Targeting {
    [RequireComponent(typeof(DecalProjector))]
    public class ProjectionManager : MonoBehaviour {
        public DecalProjector projector;
        public static float DefaultProjectionHeight = 2;

        ActionInstance CurrentInstance;
        Vector3 mousePosition;

        public void SetAction(ActionInstance action = null) {
            if (action == null) {
                gameObject.SetActive(false);
                return;
            }
            CurrentInstance = action;
        }

        internal void Tick(Vector3 mousePosition) {
            if (CurrentInstance == null || CurrentInstance.definition.TargetingType == Abilities.Enums.TargetingType.NoTarget) {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            switch (CurrentInstance.definition.TargetingType) {
                case Abilities.Enums.TargetingType.SelfAOE:
                    projector.pivot = Vector3.zero;
                    projector.size = CurrentInstance.definition.TargetSize;
                    break;
                case Abilities.Enums.TargetingType.PointAOE:
                    projector.size = CurrentInstance.definition.TargetSize;
                    projector.pivot = mousePosition - transform.position;                     
                    break;
                case Abilities.Enums.TargetingType.Direction:
                    projector.size = CurrentInstance.definition.TargetSize;
                    projector.pivot = Vector3.forward;
                    break;
                default: break;
            }


        }
    }
}