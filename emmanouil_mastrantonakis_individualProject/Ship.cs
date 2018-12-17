using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    public class Ship 
    {
        public int ImoNumber { get; set; } //Primary Key
        public string Name { get; set; }
        public string VesselType {get;set;}
        public float Dwt { get; set; }
        public string Flag { get; set; }  

        //Overloaded Constructor
        public Ship(int imoNumber, string name, string vesselType, float dwt, string flag)
        {
            ImoNumber = imoNumber;
            Name = name;
            VesselType = vesselType;
            Dwt = dwt;
            Flag = flag;
        }

        //Default Constructor
        public Ship()
        {
        }
    }
}
