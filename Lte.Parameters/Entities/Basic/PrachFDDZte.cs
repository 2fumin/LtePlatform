using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Basic
{
    public class PrachFDDZte : IEntity<ObjectId>, IZteMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int eNodeB_Id { get; set; }

        public string eNodeB_Name { get; set; }

        public string lastModifedTime { get; set; }

        public string iDate { get; set; }

        public string parentLDN { get; set; }

        public string description { get; set; }

        public int preambleTransMax { get; set; }

        public int highSpeedFlag { get; set; }

        public int numContRA { get; set; }

        public int groupBEnable { get; set; }

        public int powerRampingStep { get; set; }

        public int prachFreqOffsetFlag { get; set; }

        public int prachUltiCapSwch { get; set; }

        public int macContResTimer { get; set; }

        public int raResponseWindowSize { get; set; }

        public int pathlossThrd { get; set; }

        public int ueAvgSpeed { get; set; }

        public int PrachFDD { get; set; }

        public int preambLifeTime { get; set; }

        public int numContFreeRA { get; set; }

        public int preambleIniReceivedPower { get; set; }

        public int macNonContenPreamble { get; set; }

        public int rootSequenceIndex { get; set; }

        public int prachFreqOffset { get; set; }

        public int messagePowerOffsetGroupB { get; set; }

        public int prachConfigIndex { get; set; }

        public int sizeOfRAPreamblesGroupA { get; set; }

        public double raCollProb { get; set; }

        public int maxHarqMsg3Tx { get; set; }

        public int messageSizeGroupA { get; set; }

        public int ncs { get; set; }

        public int ueSpeedThrd { get; set; }

        public int numberOfRAPreambles { get; set; }
    }
}
