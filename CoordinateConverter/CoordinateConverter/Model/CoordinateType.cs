using System.ComponentModel;

namespace CoordinateConverter.ViewModel
{
    [TypeConverter(typeof(EnumToStringConverter))]
    public enum CoordinateType
    {
        [Description("МСК-46 зона 1")]
        MSK461,
        [Description("МСК-46 зона 2")]
        MSK462,
        [Description("СК-42 зона 2")]
        SK42zone2,
        [Description("СК-42 зона 3")]
        SK42zone3,
        [Description("СК-42 зона 4")]
        SK42zone4,
        [Description("СК-42 зона 5")]
        SK42zone5,
        [Description("СК-42 зона 6")]
        SK42zone6,
        [Description("СК-42 зона 7")]
        SK42zone7,
        [Description("СК-42 зона 8")]
        SK42zone8,
        [Description("СК-42 зона 9")]
        SK42zone9,
        [Description("СК-42 зона 10")]
        SK42zone10,
        [Description("СК-42 зона 11")]
        SK42zone11,
        [Description("СК-42 зона 12")]
        SK42zone12,
        [Description("СК-42 зона 13")]
        SK42zone13,
        [Description("СК-42 зона 14")]
        SK42zone14,
        [Description("СК-42 зона 15")]
        SK42zone15,
        [Description("СК-42 зона 16")]
        SK42zone16,
        [Description("СК-42 зона 17")]
        SK42zone17,
        [Description("СК-42 зона 18")]
        SK42zone18,
        [Description("СК-42 зона 19")]
        SK42zone19,
        [Description("СК-42 зона 20")]
        SK42zone20,
        [Description("СК-42 зона 21")]
        SK42zone21,
        [Description("СК-42 зона 22")]
        SK42zone22,
        [Description("СК-42 зона 23")]
        SK42zone23,
        [Description("СК-42 зона 24")]
        SK42zone24,
        [Description("СК-42 зона 25")]
        SK42zone25,
        [Description("СК-42 зона 26")]
        SK42zone26,
        [Description("СК-42 зона 27")]
        SK42zone27,
        [Description("СК-42 зона 28")]
        SK42zone28,
        [Description("СК-42 зона 29")]
        SK42zone29,
        [Description("СК-42 зона 30")]
        SK42zone30,
        [Description("СК-63 район C зона 1")]
        SK63AreaCzone1,
        [Description("СК-63 район C зона 2")]
        SK63AreaCzone2,
        [Description("СК-63 район C зона 3")]
        SK63AreaCzone3,             
        [Description("СК-63 район C зона 4")]
        SK63AreaCzone4,
        [Description("СК-63 район C зона 5")]
        SK63AreaCzone5,
        [Description("СК-63 район C зона 6")]
        SK63AreaCzone6,
        [Description("СК-63 район D зона 1")]
        SK63AreaDzone1,
        [Description("СК-63 район D зона 2")]
        SK63AreaDzone2,
        [Description("СК-63 район D зона 3")]
        SK63AreaDzone3,
        [Description("СК-63 район D зона 4")]
        SK63AreaDzone4,
        [Description("СК-63 район D зона 5")]
        SK63AreaDzone5,
        [Description("СК-63 район D зона 6")]
        SK63AreaDzone6,
        [Description("СК-63 район D зона 7")]
        SK63AreaDzone7,
        [Description("СК-63 район D зона 8")]
        SK63AreaDzone8,
        [Description("СК-63 район E зона 1")]
        SK63AreaEzone1,
        [Description("СК-63 район E зона 2")]
        SK63AreaEzone2,
        [Description("СК-63 район E зона 3")]
        SK63AreaEzone3,
        [Description("СК-63 район E зона 4")]
        SK63AreaEzone4,
        [Description("СК-63 район E зона 5")]
        SK63AreaEzone5,
        [Description("СК-63 район F зона 1")]
        SK63AreaFzone1,
        [Description("СК-63 район F зона 2")]
        SK63AreaFzone2,
        [Description("СК-63 район F зона 3")]
        SK63AreaFzone3,
        [Description("СК-63 район G зона 1")]
        SK63AreaGzone1,
        [Description("СК-63 район G зона 2")]
        SK63AreaGzone2,
        [Description("СК-63 район G зона 3")]
        SK63AreaGzone3,
        [Description("СК-63 район G зона 4")]
        SK63AreaGzone4,
        [Description("СК-63 район G зона 5")]
        SK63AreaGzone5,
        [Description("СК-63 район G зона 6")]
        SK63AreaGzone6,
        [Description("СК-63 район G зона 7")]
        SK63AreaGzone7,
        [Description("СК-63 район G зона 8")]
        SK63AreaGzone8,
        [Description("СК-63 район G зона 9")]
        SK63AreaGzone9,
        [Description("СК-63 район I зона 1")]
        SK63AreaIzone1,
        [Description("СК-63 район I зона 2")]
        SK63AreaIzone2,
        [Description("СК-63 район I зона 3")]
        SK63AreaIzone3,
        [Description("СК-63 район I зона 4")]
        SK63AreaIzone4,
        [Description("СК-63 район J зона 1")]
        SK63AreaJzone1,
        [Description("СК-63 район J зона 2")]
        SK63AreaJzone2,
        [Description("СК-63 район J зона 3")]
        SK63AreaJzone3,
        [Description("СК-63 район J зона 4")]
        SK63AreaJzone4,
        [Description("СК-63 район J зона 5")]
        SK63AreaJzone5,
        [Description("СК-63 район L зона 1")]
        SK63AreaLzone1,
        [Description("СК-63 район L зона 2")]
        SK63AreaLzone2,
        [Description("СК-63 район L зона 3")]
        SK63AreaLzone3,
        [Description("СК-63 район L зона 4")]
        SK63AreaLzone4,
        [Description("СК-63 район L зона 5")]
        SK63AreaLzone5,
        [Description("СК-63 район L зона 6")]
        SK63AreaLzone6,
        [Description("СК-63 район M зона 1")]
        SK63AreaMzone1,
        [Description("СК-63 район M зона 2")]
        SK63AreaMzone2,
        [Description("СК-63 район M зона 3")]
        SK63AreaMzone3,
        [Description("СК-63 район M зона 4")]
        SK63AreaMzone4,
        [Description("СК-63 район P зона 1")]
        SK63AreaPzone1,
        [Description("СК-63 район P зона 2")]
        SK63AreaPzone2,
        [Description("СК-63 район P зона 3")]
        SK63AreaPzone3,
        [Description("СК-63 район P зона 4")]
        SK63AreaPzone4,
        [Description("СК-63 район Q зона 1")]
        SK63AreaQzone1,
        [Description("СК-63 район Q зона 2")]
        SK63AreaQzone2,
        [Description("СК-63 район Q зона 3")]
        SK63AreaQzone3,
        [Description("СК-63 район Q зона 4")]
        SK63AreaQzone4,
        [Description("СК-63 район Q зона 5")]
        SK63AreaQzone5,
        [Description("СК-63 район R зона 1")]
        SK63AreaRzone1,
        [Description("СК-63 район R зона 2")]
        SK63AreaRzone2,
        [Description("СК-63 район R зона 3")]
        SK63AreaRzone3,
        [Description("СК-63 район S зона 1")]
        SK63AreaSzone1,
        [Description("СК-63 район S зона 2")]
        SK63AreaSzone2,
        [Description("СК-63 район S зона 3")]
        SK63AreaSzone3,
        [Description("СК-63 район S зона 4")]
        SK63AreaSzone4,
        [Description("СК-63 район S зона 5")]
        SK63AreaSzone5,
        [Description("СК-63 район S зона 6")]
        SK63AreaSzone6,
        [Description("СК-63 район S зона 7")]
        SK63AreaSzone7,
        [Description("СК-63 район S зона 8")]
        SK63AreaSzone8,
        [Description("СК-63 район S зона 9")]
        SK63AreaSzone9,
        [Description("СК-63 район S зона 10")]
        SK63AreaSzone10,
        [Description("СК-63 район S зона 11")]
        SK63AreaSzone11,
        [Description("СК-63 район S зона 12")]
        SK63AreaSzone12,
        [Description("СК-63 район S зона 13")]
        SK63AreaSzone13,
        [Description("СК-63 район T зона 1")]
        SK63AreaTzone1,
        [Description("СК-63 район T зона 2")]
        SK63AreaTzone2,
        [Description("СК-63 район T зона 3")]
        SK63AreaTzone3,
        [Description("СК-63 район T зона 4")]
        SK63AreaTzone4,
        [Description("СК-63 район V зона 1")]
        SK63AreaVzone1,
        [Description("СК-63 район V зона 2")]
        SK63AreaVzone2,
        [Description("СК-63 район V зона 3")]
        SK63AreaVzone3,
        [Description("СК-63 район V зона 4")]
        SK63AreaVzone4,
        [Description("СК-63 район V зона 5")]
        SK63AreaVzone5,
        [Description("СК-63 район V зона 6")]
        SK63AreaVzone6,
        [Description("СК-63 район W зона 1 (6 градусная зона)")]
        SK63AreaW6Degreezone1,
        [Description("СК-63 район W зона 2 (6 градусная зона)")]
        SK63AreaW6Degreezone2,
        [Description("СК-63 район W зона 3 (6 градусная зона)")]
        SK63AreaW6Degreezone3,
        [Description("СК-63 район W зона 4 (6 градусная зона)")]
        SK63AreaW6Degreezone4,
        [Description("СК-63 район W зона 1 (3 градусная зона)")]
        SK63AreaW3Degreezone1,
        [Description("СК-63 район W зона 2 (3 градусная зона)")]
        SK63AreaW3Degreezone2,
        [Description("СК-63 район W зона 3 (3 градусная зона)")]
        SK63AreaW3Degreezone3,
        [Description("СК-63 район W зона 4 (3 градусная зона)")]
        SK63AreaW3Degreezone4,
        [Description("СК-63 район W зона 5 (3 градусная зона)")]
        SK63AreaW3Degreezone5,
        [Description("СК-63 район W зона 6 (3 градусная зона)")]
        SK63AreaW3Degreezone6,
        [Description("СК-63 район W зона 7 (3 градусная зона)")]
        SK63AreaW3Degreezone7,
        [Description("СК-63 район W зона 8 (3 градусная зона)")]
        SK63AreaW3Degreezone8,
        [Description("СК-63 район X зона 1")]
        SK63AreaXzone1,
        [Description("СК-63 район X зона 2")]
        SK63AreaXzone2,
        [Description("СК-63 район X зона 3")]
        SK63AreaXzone3,
        [Description("СК-63 район X зона 4")]
        SK63AreaXzone4,
        [Description("СК-63 район X зона 5")]
        SK63AreaXzone5,
        [Description("СК-63 район X зона 6")]
        SK63AreaXzone6,

    }


}
