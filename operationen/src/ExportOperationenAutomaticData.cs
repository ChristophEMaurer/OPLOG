using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Operationen
{
    /// <summary>
    /// Create a dictionary which contains all operatione for expoort to mobile device.
    /// This is a pain to do manually. 100 groups have to be created.
    /// </summary>
    public partial class ExportOperationenAutomatic
    {
        /// <summary>
        /// Contains all group swhich will be created. Each of these will contain all entries up until the next group.
        /// 5-18
        /// 5-21
        /// means: group 5-18 contains all entries up to but excluding entry 5-21, this includes 6-19 and 5-20.
        /// Therefore we cannot do a text compare but will use the primary keys.
        /// This requires that all ops-codes are sorted in ascending pk order.
        /// </summary>
        string[] _groups = new string[] 
        {
            "1",
            "3",
            "5-01",
            "5-02",
            "5-03",
            "5-04",
            "5-05",
            "5-06",
            "5-07",
            "5-08",
            "5-09",
            "5-10",
            "5-18",
            "5-21",
            "5-23",
            "5-24",
            "5-25",
            "5-26",
            "5-27",
            "5-28",
            "5-32",
            "5-34",
            "5-35",
            "5-38",
            "5-40",
            "5-41",
            "5-42",
            "5-43",
            "5-45",
            "5-47",
            "5-48",
            "5-49",
            "5-50",
            "5-51",
            "5-52",
            "5-53",
            "5-54",
            "5-55",
            "5-56",
            "5-60",
            "5-65",
            "5-75",
            "5-76",
            "5-78",
            "5-79",
            "5-80",
            "5-83",
            "5-84",
            "5-85",
            "5-86",
            "5-87",
            "5-89",
            "5-93",
            "5-98",
            "5-99",
            "6-001",
            "6-002",
            "6-003",
            "6-004",
            "6-005",
            "6-006",
            "8-01",
            "8-10",
            "8-11",
            "8-12",
            "8-13",
            "8-14",
            "8-15",
            "8-17",
            "8-19",
            "8-20",
            "8-21",
            "8-22",
            "8-31",
            "8-40",
            "8-50",
            "8-51",
            "8-52",
            "8-53",
            "8-54",
            "8-55",
            "8-56",
            "8-60",
            "8-63",
            "8-65",
            "8-66",
            "8-70",
            "8-80",
            "8-81",
            "8-82",
            "8-83",
            "8-84",
            "8-85",
            "8-86",
            "8-90",
            "8-91",
            "8-92",
            "8-93",
            "8-97",
            "9",
        };
        
    }
}
