// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Points;

/// <summary>All points that can be used in analysis, progressions etc.</summary>
/// <remarks>Combines celestial points, mundane points, cusps, zodiacal points and arabic points. The original indexes (from the enums) are used, combined with an offset.
/// For celestiap points the offset is 0, for zodiac points the offset is 1000, for arabic points 2000, for mundane points 3000 and for cusps 4000.</remarks>
public class PointDefinitions
{
    public List<GeneralPoint> AllGenericPoints { get; }

    public PointDefinitions() 
    {
        AllGenericPoints = ConstructAlPoints();
    }

    private List<GeneralPoint> ConstructAlPoints()
    {
        PointTypes cp = PointTypes.CelestialPoint;
        PointTypes mp = PointTypes.MundaneSpecialPoint;
        PointTypes zp = PointTypes.ZodiacalPoint;
        PointTypes cu = PointTypes.Cusp;
        PointTypes ap = PointTypes.ArabicPoint;

        List<GeneralPoint> points = new()
        {
            // Celestial Points
            new GeneralPoint(0, "Sun", cp, "ref.enum.celpoint.sun"),
            new GeneralPoint(1, "Moon", cp, "ref.enum.celpoint.moon"),
            new GeneralPoint(2, "Mercury", cp, "ref.enum.celpoint.mercury"),
            new GeneralPoint(3, "Venus", cp, "ref.enum.celpoint.venus"),
            new GeneralPoint(4, "Earth", cp, "ref.enum.celpoint.earth"),
            new GeneralPoint(5, "Mars", cp, "ref.enum.celpoint.mars"),
            new GeneralPoint(6, "Jupiter", cp, "ref.enum.celpoint.jupiter"),
            new GeneralPoint(7, "Saturn", cp, "ref.enum.celpoint.saturn"),
            new GeneralPoint(8, "Uranus", cp, "ref.enum.celpoint.uranus"),
            new GeneralPoint(9, "Neptune", cp, "ref.enum.celpoint.neptune"),
            new GeneralPoint(10, "Pluto", cp, "ref.enum.celpoint.pluto"),
            new GeneralPoint(11, "Node (mean)", cp, "ref.enum.celpoint.meannode"),
            new GeneralPoint(12, "Node (true)", cp, "ref.enum.celpoint.truenode"),
            new GeneralPoint(13, "Chiron", cp, "ref.enum.celpoint.chiron"),
            new GeneralPoint(14, "Persephone", cp, "ref.enum.celpoint.persephone_ram"),
            new GeneralPoint(15, "Hermes", cp, "ref.enum.celpoint.hermes_ram"),
            new GeneralPoint(16, "Demeter", cp, "ref.enum.celpoint.demeter_ram"),
            new GeneralPoint(17, "Cupido", cp, "ref.enum.celpoint.cupido_ura"),
            new GeneralPoint(18, "Hades", cp, "ref.enum.celpoint.hades_ura"),
            new GeneralPoint(19, "Zeus", cp, "ref.enum.celpoint.zeus_ura"),
            new GeneralPoint(20, "Kronos", cp, "ref.enum.celpoint.kronos_ura"),
            new GeneralPoint(21, "Apollon", cp, "ref.enum.celpoint.apollon_ura"),
            new GeneralPoint(22, "Admetos", cp, "ref.enum.celpoint.admetos_ura"),
            new GeneralPoint(23, "Vulcanus", cp, "ref.enum.celpoint.vulcanus_ura"),
            new GeneralPoint(24, "Poseidon", cp, "ref.enum.celpoint.poseidon_ura"),
            new GeneralPoint(25, "Eris", cp, "ref.enum.celpoint.eris"),
            new GeneralPoint(26, "Pholus", cp, "ref.enum.celpoint.pholus"),
            new GeneralPoint(27, "Ceres", cp, "ref.enum.celpoint.ceres"),
            new GeneralPoint(28, "Pallas", cp, "ref.enum.celpoint.pallas"),
            new GeneralPoint(29, "Juno", cp, "ref.enum.celpoint.juno"),
            new GeneralPoint(30, "Vesta", cp, "ref.enum.celpoint.vesta"),
            new GeneralPoint(31, "Isis", cp, "ref.enum.celpoint.isis"),
            new GeneralPoint(32, "Nessus", cp, "ref.enum.celpoint.nessus"),
            new GeneralPoint(33, "Huya", cp, "ref.enum.celpoint.huya"),
            new GeneralPoint(34, "Varuna", cp, "ref.enum.celpoint.varuna"),
            new GeneralPoint(35, "Ixion", cp, "ref.enum.celpoint.ixion"),
            new GeneralPoint(36, "Quaoar", cp, "ref.enum.celpoint.quaoar"),
            new GeneralPoint(37, "Haumea", cp, "ref.enum.celpoint.haumea"),
            new GeneralPoint(38, "Orcus", cp, "ref.enum.celpoint.orcus"),
            new GeneralPoint(39, "Makemake", cp, "ref.enum.celpoint.makemake"),
            new GeneralPoint(40, "Sedna", cp, "ref.enum.celpoint.sedna"),
            new GeneralPoint(41, "Hygieia", cp, "ref.enum.celpoint.hygieia"),
            new GeneralPoint(42, "Astraea", cp, "ref.enum.celpoint.astraea"),
            new GeneralPoint(43, "Apogee (mean)", cp, "ref.enum.celpoint.apogee_mean"),
            new GeneralPoint(44, "Apogee (corr.)", cp, "ref.enum.celpoint.apogee_corrected"),
            new GeneralPoint(45, "Apogee (interp.)", cp, "ref.enum.celpoint.apogee_interpolated"),
            new GeneralPoint(46, "Apogee (Duval)", cp, "ref.enum.celpoint.apogee_duval"),
            new GeneralPoint(47, "Persephone (Carteret)", cp, "ref.enum.celpoint.persephone_carteret"),
            new GeneralPoint(48, "Vulcanus (Carteret)", cp, "ref.enum.celpoint.vulcanus_carteret"),
            // Zodiac points
            new GeneralPoint(1000, "Zero Aries", zp, "ref.enum.zodiacpoints.id.zeroar"),
            new GeneralPoint(1001, "Zero Cancer", zp, "ref.enum.zodiacpoints.id.zerocn"),
            // Arabic Points
            new GeneralPoint(2000, "Fortuna (sect)", ap, "ref.enum.arabicpoint.fortunasect"),
            new GeneralPoint(2001, "Fortuna (no sect)", ap, "ref.enum.arabicpoint.fortunanosect"),
            // Mundane points
            new GeneralPoint(3000, "Ascendant", mp, "ref.enum.mundanepoint.id.asc"),
            new GeneralPoint(3001, "MC", mp, "ref.enum.mundanepoint.id.mc"),
            new GeneralPoint(3002, "Eastpoint", mp, "ref.enum.mundanepoint.id.eastpoint"),
            new GeneralPoint(3003, "Vertex", mp, "ref.enum.mundanepoint.id.vertex"),
        };
        // Cusps
        int offset = 4000;
        for (int i = 0; i < 36; i++)
        {
            points.Add(new(offset + i, "Cusp " + i.ToString(), cu, "ref.enum.cusps." + (i + 1).ToString()));
        }
        return points;
    }

}