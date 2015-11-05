using System.Collections.Generic;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;
using TraceParser.Eutra;
using TraceParser.Outputs;

namespace TraceParser.Test.RRC
{
    [TestFixture]
    public class MeasurementReportTest
    {
        [TestCase("08 01 39 70")]
        [TestCase("08 01 34 6C ")]
        [TestCase("08 11 38 74 04 65 80 00 ")]
        [TestCase("08 11 37 6C 03 7D 80 00 ")]
        [TestCase("08 11 31 68 00 E5 A5 0C ")]
        [TestCase("08 11 38 74 16 0D A9 01 19 60 00 ")]
        [TestCase("08 11 3C 88 12 31 AB 20 6A 6A 00 ")]
        public void Test_Decode(string source)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(signal);
        }

        [TestCase("08 01 39 70", 3)]
        [TestCase("08 01 34 6C ", 3)]
        [TestCase("08 11 38 74 04 65 80 00 ", 3)]
        [TestCase("08 11 37 6C 03 7D 80 00 ", 3)]
        [TestCase("08 11 31 68 00 E5 A5 0C ", 3)]
        [TestCase("08 11 38 74 16 0D A9 01 19 60 00 ", 3)]
        [TestCase("08 11 3C 88 12 31 AB 20 6A 6A 00 ", 3)]
        public void Test_MeasureResults(string source, int measId)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            MeasResults results = signal.criticalExtensions.c1.measurementReport_r8.measResults;
            Assert.IsNotNull(results);
            Assert.AreEqual(results.measId, measId);
        }

        [TestCase("08 01 39 70", "RSRP:57, RSRQ:28")]
        [TestCase("08 01 34 6C ", "RSRP:52, RSRQ:27")]
        [TestCase("08 11 38 74 04 65 80 00 ", "RSRP:56, RSRQ:29")]
        [TestCase("08 11 37 6C 03 7D 80 00 ", "RSRP:55, RSRQ:27")]
        [TestCase("08 11 31 68 00 E5 A5 0C ", "RSRP:49, RSRQ:26")]
        [TestCase("08 11 38 74 16 0D A9 01 19 60 00 ", "RSRP:56, RSRQ:29")]
        [TestCase("08 11 3C 88 12 31 AB 20 6A 6A 00 ", "RSRP:60, RSRQ:34")]
        public void Test_MeasPCell(string source, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            MeasResults results = signal.criticalExtensions.c1.measurementReport_r8.measResults;
            Assert.AreEqual(results.measResultPCell.GetOutputs(), description);
        }

        [TestCase("08 21 9F 54 0C 07 0C 0E FC", 4, "RSRP:31, RSRQ:21")]
        [TestCase("08 21 9E 50 0C 07 0B F2 20", 4, "RSRP:30, RSRQ:20")]
        public void Test_MeasId_And_PCell(string source, int measId, string description)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            MeasResults results = signal.criticalExtensions.c1.measurementReport_r8.measResults;
            Assert.IsNotNull(results);
            Assert.AreEqual(results.measId, measId);
            Assert.AreEqual(results.measResultPCell.GetOutputs(), description);
        }

        [TestCase("08 01 39 70")]
        [TestCase("08 01 34 6C ")]
        public void Test_NeighborCells_NoNeighbors(string source)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            Assert.IsNull(signal.criticalExtensions.c1.measurementReport_r8.measResults.measResultNeighCells);
        }

        [TestCase("08 11 38 74 04 65 80 00 ",
            new[] { "PCI:281, Measure results:RSRP:0, RSRQ:0" })]
        [TestCase("08 11 37 6C 03 7D 80 00 ",
            new[] { "PCI:223, Measure results:RSRP:0, RSRQ:0" })]
        [TestCase("08 11 31 68 00 E5 A5 0C ",
            new[] { "PCI:57, Measure results:RSRP:37, RSRQ:3" })]
        [TestCase("08 11 38 74 16 0D A9 01 19 60 00 ",
            new[]
            {
                "PCI:387, Measure results:RSRP:41, RSRQ:0",
                "PCI:281, Measure results:RSRP:0, RSRQ:0"
            })]
        [TestCase("08 11 3C 88 12 31 AB 20 6A 6A 00 ",
            new[]
            {
                "PCI:140, Measure results:RSRP:43, RSRQ:8",
                "PCI:106, Measure results:RSRP:40, RSRQ:0"
            })]
        public void Test_NeighborCells_Neighbors(string source, string[] descriptions)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            Assert.IsNotNull(signal.criticalExtensions.c1.measurementReport_r8.measResults.measResultNeighCells);
            List<MeasResultEUTRA> measResultEutras =
                signal.criticalExtensions.c1.measurementReport_r8.measResults.measResultNeighCells.measResultListEUTRA;
            for (int i = 0; i < measResultEutras.Count; i++)
            {
                Assert.AreEqual(measResultEutras[i].GetOutputs(), descriptions[i]);
            }
        }

        [TestCase("08 11 23 54 12 4D A1 58 2B 64 80", 3, "RSRP:35, RSRQ:21",
            new[]
            {
                "PCI:147, Measure results:RSRP:33, RSRQ:22",
                "PCI:43, Measure results:RSRP:18, RSRQ:0"
            })]
        [TestCase("08 12 23 54 02 4D 21", 5, "RSRP:35, RSRQ:21",
            new[]
            {
                "PCI:147, Measure results:RSRP:33, RSRQ:"
            })]
        [TestCase("08 10 9C 60 04 A5 95 24 ", 2, "RSRP:28, RSRQ:24",
            new[]
            {
                "PCI:297, Measure results:RSRP:21, RSRQ:9"
            })]
        [TestCase("08 10 9D 64 13 89 96 3D 29 65 47", 2, "RSRP:29, RSRQ:25",
            new[]
            {
                "PCI:226, Measure results:RSRP:22, RSRQ:15",
                "PCI:297, Measure results:RSRP:21, RSRQ:7"
            })]
        public void Test_Primary_And_Neighbors(string source, int measId, string pDescription, string[] neighbors)
        {
            BitArrayInputStream stream = source.GetInputStream();
            Assert.AreEqual(stream.readBits(5), 1);
            MeasurementReport signal = MeasurementReport.PerDecoder.Instance.Decode(stream);
            MeasResults results = signal.criticalExtensions.c1.measurementReport_r8.measResults;
            Assert.IsNotNull(results);
            Assert.AreEqual(results.measId, measId);
            Assert.AreEqual(results.measResultPCell.GetOutputs(), pDescription);
            List<MeasResultEUTRA> measResultEutras =
                signal.criticalExtensions.c1.measurementReport_r8.measResults.measResultNeighCells.measResultListEUTRA;
            for (int i = 0; i < measResultEutras.Count; i++)
            {
                Assert.AreEqual(measResultEutras[i].GetOutputs(), neighbors[i]);
            }
        }
    }
}
