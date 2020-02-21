using System;

namespace CDZ.Common.Models
{
    [Serializable]
    public class Knight
    {
        public const string BaseAddressForImage = "_content/CDZ.Web.Common/img";

        public string Name { get; set; }
        public string Constellation { get; set; }
        public double Cosmo { get; set; }
        public string ProfileImg => $"{BaseAddressForImage}/{Name}.jpg";
    }
}
