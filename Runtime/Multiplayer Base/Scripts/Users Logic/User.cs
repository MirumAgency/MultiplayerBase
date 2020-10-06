using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XennialDigital
{
    public class User
    {
        public int ID;
        public string Name;
        public GameObject Avatar;
        public DevicesList Platform;

        public User(int id, string name, GameObject avatar, DevicesList platform)
        {
            ID = id;
            Name = name;
            Avatar = avatar;
            Platform = platform;
        }
    }
}