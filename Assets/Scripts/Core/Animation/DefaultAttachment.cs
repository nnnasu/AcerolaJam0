using System.Collections;
using System.Collections.Generic;
using Core.Animation;
using UnityEngine;

public class DefaultAttachment : MonoBehaviour {
    public GameObject ToEquip;
    public AttachmentSocket attachmentSocket;

    void Start() {
        if (ToEquip) {
            var obj = GlobalPool.Current.GetObject(ToEquip);
            var poolObj = obj.GetComponent<PoolableBehaviour>();
            if (!poolObj) return;
            attachmentSocket.ReplaceAttachment(poolObj);
        }

    }

}
