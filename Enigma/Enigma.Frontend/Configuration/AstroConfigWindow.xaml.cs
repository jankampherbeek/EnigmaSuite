// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Enigma.Frontend.Ui.Configuration;

/// <summary>
/// Interaction logic for AstroConfigWindow.xaml
/// </summary>
public partial class AstroConfigWindow : Window
{
    private readonly AstroConfigController _controller;
    private readonly Rosetta _rosetta = Rosetta.Instance;


    public AstroConfigWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<AstroConfigController>();
        PopulateTexts();
        PopulateGlyphs();
        DefaultValues();
    }

    private void PopulateTexts()
    {
        // Overall
        Title = _rosetta.TextForId("astroconfigwindow.title");
        FormTitle.Text = _rosetta.TextForId("astroconfigwindow.title");
        TitleGeneral.Text = _rosetta.TextForId("astroconfigwindow.titlegeneral");
        tabGeneral.Header = _rosetta.TextForId("astroconfigwindow.tabgeneral");
        tabBasicPoints.Header = _rosetta.TextForId("astroconfigwindow.tabbasicpoints");
        tabMathMinorPoints.Header = _rosetta.TextForId("astroconfigwindow.tabminorpoints");
        tabHypoPoints.Header = _rosetta.TextForId("astroconfigwindow.tabhypopoints");
        tabAspects.Header = _rosetta.TextForId("astroconfigwindow.tabaspects");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        BtnCancel.Content = _rosetta.TextForId("common.btncancel");
        BtnOk.Content = _rosetta.TextForId("common.btnok");
        // Tab General
        tbHouseSystemExpl.Text = _rosetta.TextForId("astroconfigwindow.housesystemexpl");
        tbHouseSystem.Text = _rosetta.TextForId("astroconfigwindow.housesystem");
        tbZodiacTypeExpl.Text = _rosetta.TextForId("astroconfigwindow.zodiactypeexpl");
        tbZodiacType.Text = _rosetta.TextForId("astroconfigwindow.zodiactype");
        tbAyanamshaExpl.Text = _rosetta.TextForId("astroconfigwindow.ayanamshaexpl");
        tbAyanamsha.Text = _rosetta.TextForId("astroconfigwindow.ayanamsha");
        tbObserverPosExpl.Text = _rosetta.TextForId("astroconfigwindow.observerposexpl");
        tbObserverPos.Text = _rosetta.TextForId("astroconfigwindow.observerpos");
        tbProjectionTypeExpl.Text = _rosetta.TextForId("astroconfigwindow.projectiontypeexpl");
        tbProjectionType.Text = _rosetta.TextForId("astroconfigwindow.projectiontype");
        tbBaseOrbExpl.Text = _rosetta.TextForId("astroconfigwindow.baseorbexpl");
        tbAspectBaseOrb.Text = _rosetta.TextForId("astroconfigwindow.baseorbaspect");
        tbMidpointBaseOrb.Text = _rosetta.TextForId("astroconfigwindow.baseorbmidpoint");

        // Tab Basic celestial points
        TitleBasicCelPoints.Text = _rosetta.TextForId("astroconfigwindow.basiccelpoints");
        tbArabicParts.Text = _rosetta.TextForId("astroconfigwindow.arabicparts");
        tbBasicExplanation.Text = _rosetta.TextForId("astroconfigwindow.basiccelpointsexpl");
        tbClassical.Text = _rosetta.TextForId("astroconfigwindow.classicalpoints");
        tbModern.Text = _rosetta.TextForId("astroconfigwindow.modernpoints");
        tbMundanePoints.Text = _rosetta.TextForId("astroconfigwindow.mundanepoints");
        tbBasicPointLeft.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbBasicPointRight.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbBasicOrbFactorLeft.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbBasicOrbFactorRight.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextSun.Text = _rosetta.TextForId("ref.enum.celpoint.sun");
        tbTextMoon.Text = _rosetta.TextForId("ref.enum.celpoint.moon");
        tbTextMercury.Text = _rosetta.TextForId("ref.enum.celpoint.mercury");
        tbTextVenus.Text = _rosetta.TextForId("ref.enum.celpoint.venus");
        tbTextMars.Text = _rosetta.TextForId("ref.enum.celpoint.mars");
        tbTextJupiter.Text = _rosetta.TextForId("ref.enum.celpoint.jupiter");
        tbTextSaturn.Text = _rosetta.TextForId("ref.enum.celpoint.saturn");
        tbTextUranus.Text = _rosetta.TextForId("ref.enum.celpoint.uranus");
        tbTextNeptune.Text = _rosetta.TextForId("ref.enum.celpoint.neptune");
        tbTextPluto.Text = _rosetta.TextForId("ref.enum.celpoint.pluto");
        tbTextMc.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.mc");
        tbTextAsc.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.asc");
        tbTextVertex.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.vertex");
        tbTextEastpoint.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.eastpoint");
        tbTextParsSect.Text = _rosetta.TextForId("ref.enum.arabicpoint.fortunasect");
        tbTextParsNoSect.Text = _rosetta.TextForId("ref.enum.arabicpoint.fortunanosect");
        // Tab Math and Minor points
        TitleMathMinorPoints.Text = _rosetta.TextForId("astroconfigwindow.mathminorcelpoints");
        tbMathMinorExplanation.Text = _rosetta.TextForId("astroconfigwindow.mathminorcelpointsexpl");
        tbMathematical.Text = _rosetta.TextForId("astroconfigwindow.mathematical");
        tbCentaurs.Text = _rosetta.TextForId("astroconfigwindow.centaurs");
        tbPlanetoids.Text = _rosetta.TextForId("astroconfigwindow.planetoids");
        tbPlutoids.Text = _rosetta.TextForId("astroconfigwindow.plutoids");
        tbMathMinorPointLeft.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbMathMinorPointRight.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbMathMinorOrbFactorLeft.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbMathMinorOrbFactorRight.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextMeanNode.Text = _rosetta.TextForId("ref.enum.celpoint.meannode");
        tbTextTrueNode.Text = _rosetta.TextForId("ref.enum.celpoint.truenode");
        tbTextZeroAries.Text = _rosetta.TextForId("ref.enum.zodiacpoints.id.zeroar");
        tbTextMeanBlackMoon.Text = _rosetta.TextForId("ref.enum.celpoint.meanblackmoon");
        tbTextCorrBlackMoon.Text = _rosetta.TextForId("ref.enum.celpoint.corrblackmoon");
        tbTextInterpolatedBlackMoon.Text = _rosetta.TextForId("ref.enum.celpoint.interpblackmoon");
        tbTextDuvalBlackMoon.Text = _rosetta.TextForId("ref.enum.celpoint.duvalblackmoon");

        tbTextHuya.Text = _rosetta.TextForId("ref.enum.celpoint.huya");
        tbTextVaruna.Text = _rosetta.TextForId("ref.enum.celpoint.varuna");
        tbTextIxion.Text = _rosetta.TextForId("ref.enum.celpoint.ixion");
        tbTextQuaoar.Text = _rosetta.TextForId("ref.enum.celpoint.quaoar");
        tbTextHaumea.Text = _rosetta.TextForId("ref.enum.celpoint.haumea");
        tbTextEris.Text = _rosetta.TextForId("ref.enum.celpoint.eris");
        tbTextSedna.Text = _rosetta.TextForId("ref.enum.celpoint.sedna");
        tbTextOrcus.Text = _rosetta.TextForId("ref.enum.celpoint.orcus");
        tbTextMakemake.Text = _rosetta.TextForId("ref.enum.celpoint.makemake");

        tbTextChiron.Text = _rosetta.TextForId("ref.enum.celpoint.chiron");
        tbTextNessus.Text = _rosetta.TextForId("ref.enum.celpoint.nessus");
        tbTextPholus.Text = _rosetta.TextForId("ref.enum.celpoint.pholus");

        tbTextCeres.Text = _rosetta.TextForId("ref.enum.celpoint.ceres");
        tbTextPallas.Text = _rosetta.TextForId("ref.enum.celpoint.pallas");
        tbTextJuno.Text = _rosetta.TextForId("ref.enum.celpoint.juno");
        tbTextVesta.Text = _rosetta.TextForId("ref.enum.celpoint.vesta");
        tbTextHygieia.Text = _rosetta.TextForId("ref.enum.celpoint.hygieia");
        tbTextAstraea.Text = _rosetta.TextForId("ref.enum.celpoint.astraea");

        // Tab hypothetical points
        TitleHypotheticalPoints.Text = _rosetta.TextForId("astroconfigwindow.hypotheticalpoints");
        tbHypotheticalExplanation.Text = _rosetta.TextForId("astroconfigwindow.hypotheticalpointsexpl");

        tbUranianWitte.Text = _rosetta.TextForId("astroconfigwindow.uranian_witte");
        tbUranianSieggrun.Text = _rosetta.TextForId("astroconfigwindow.uranian_sieggrun");
        tbSchoolRam.Text = _rosetta.TextForId("astroconfigwindow.schoolofram");
        tbCarteret.Text = _rosetta.TextForId("astroconfigwindow.carteret");
        tbHypotOthers.Text = _rosetta.TextForId("astroconfigwindow.hypotothers");

        tbUranianPointLeft.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbUranianPointRight.Text = _rosetta.TextForId("astroconfigwindow.celpoint");
        tbUranianFactorLeft.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbUranianFactorRight.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");

        tbTextCupido.Text = _rosetta.TextForId("ref.enum.celpoint.cupido_ura");
        tbTextHades.Text = _rosetta.TextForId("ref.enum.celpoint.hades_ura");
        tbTextZeus.Text = _rosetta.TextForId("ref.enum.celpoint.zeus_ura");
        tbTextKronos.Text = _rosetta.TextForId("ref.enum.celpoint.kronos_ura");
        tbTextApollon.Text = _rosetta.TextForId("ref.enum.celpoint.apollon_ura");
        tbTextAdmetos.Text = _rosetta.TextForId("ref.enum.celpoint.admetos_ura");
        tbTextVulkanusUra.Text = _rosetta.TextForId("ref.enum.celpoint.vulcanus_ura");
        tbTextPoseidon.Text = _rosetta.TextForId("ref.enum.celpoint.poseidon_ura");
        tbTextPersephoneRam.Text = _rosetta.TextForId("ref.enum.celpoint.persephone_ram");
        tbTextHermes.Text = _rosetta.TextForId("ref.enum.celpoint.hermes_ram");
        tbTextDemeter.Text = _rosetta.TextForId("ref.enum.celpoint.demeter_ram");
        tbTextVulcanusCarteret.Text = _rosetta.TextForId("ref.enum.celpoint.vulcanus_carteret");
        tbTextPersephoneCarteret.Text = _rosetta.TextForId("ref.enum.celpoint.persephone_carteret");
        tbTextTransPluto.Text = _rosetta.TextForId("ref.enum.celpoint.transpluto");

        // Tab aspects
        TitleAspects.Text = _rosetta.TextForId("astroconfigwindow.aspects");
        tbAspectsExplanation.Text = _rosetta.TextForId("astroconfigwindow.aspectsexpl");
        tbOrbMethod.Text = _rosetta.TextForId("astroconfigwindow.orbmethod");
        tbOrbMethodExpl.Text = _rosetta.TextForId("astroconfigwindow.orbmethodexpl");
        tbMajorAspects.Text = _rosetta.TextForId("astroconfigwindow.majoraspects");
        tbMinorAspects.Text = _rosetta.TextForId("astroconfigwindow.minoraspects");
        tbMicroAspects.Text = _rosetta.TextForId("astroconfigwindow.microaspects");
        tbAspectLabelLeft.Text = _rosetta.TextForId("astroconfigwindow.aspect");
        tbAspectLabelRight.Text = _rosetta.TextForId("astroconfigwindow.aspect");
        tbOrbFactorLeft.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbOrbFactorRight.Text = _rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextConjunction.Text = _rosetta.TextForId("ref.enum.aspect.conjunction");
        tbTextOpposition.Text = _rosetta.TextForId("ref.enum.aspect.opposition");
        tbTextTriangle.Text = _rosetta.TextForId("ref.enum.aspect.triangle");
        tbTextSquare.Text = _rosetta.TextForId("ref.enum.aspect.square");
        tbTextSextile.Text = _rosetta.TextForId("ref.enum.aspect.sextile");
        tbTextSemiSextile.Text = _rosetta.TextForId("ref.enum.aspect.semisextile");
        tbTextInconjunct.Text = _rosetta.TextForId("ref.enum.aspect.inconjunct");
        tbTextSemiSquare.Text = _rosetta.TextForId("ref.enum.aspect.semisquare");
        tbTextSesquiquadrate.Text = _rosetta.TextForId("ref.enum.aspect.sesquiquadrate");
        tbTextQuintile.Text = _rosetta.TextForId("ref.enum.aspect.quintile");
        tbTextBiQuintile.Text = _rosetta.TextForId("ref.enum.aspect.biquintile");
        tbTextSeptile.Text = _rosetta.TextForId("ref.enum.aspect.septile");
        tbTextVigintile.Text = _rosetta.TextForId("ref.enum.aspect.vigintile");
        tbTextUnDecile.Text = _rosetta.TextForId("ref.enum.aspect.undecile");
        tbTextSemiQuintile.Text = _rosetta.TextForId("ref.enum.aspect.semiquintile");
        tbTextNovile.Text = _rosetta.TextForId("ref.enum.aspect.novile");
        tbTextBiNovile.Text = _rosetta.TextForId("ref.enum.aspect.binovile");
        tbTextCentile.Text = _rosetta.TextForId("ref.enum.aspect.centile");
        tbTextBiSeptile.Text = _rosetta.TextForId("ref.enum.aspect.biseptile");
        tbTextTriDecile.Text = _rosetta.TextForId("ref.enum.aspect.tridecile");
        tbTextTriSeptile.Text = _rosetta.TextForId("ref.enum.aspect.triseptile");
        tbTextQuadraNovile.Text = _rosetta.TextForId("ref.enum.aspect.quadranovile");
    }

    private void PopulateGlyphs()
    {
        tbGlyphSun.Text = _controller.DefineGlyph(ChartPoints.Sun).ToString();
        tbGlyphMoon.Text = _controller.DefineGlyph(ChartPoints.Moon).ToString();
        tbGlyphMercury.Text = _controller.DefineGlyph(ChartPoints.Mercury).ToString();
        tbGlyphVenus.Text = _controller.DefineGlyph(ChartPoints.Venus).ToString();
        tbGlyphMars.Text = _controller.DefineGlyph(ChartPoints.Mars).ToString();
        tbGlyphJupiter.Text = _controller.DefineGlyph(ChartPoints.Jupiter).ToString();
        tbGlyphSaturn.Text = _controller.DefineGlyph(ChartPoints.Saturn).ToString();
        tbGlyphUranus.Text = _controller.DefineGlyph(ChartPoints.Uranus).ToString();
        tbGlyphNeptune.Text = _controller.DefineGlyph(ChartPoints.Neptune).ToString();
        tbGlyphPluto.Text = _controller.DefineGlyph(ChartPoints.Pluto).ToString();
        tbGlyphMc.Text = _controller.DefineGlyph(ChartPoints.Mc).ToString();
        tbGlyphAsc.Text = _controller.DefineGlyph(ChartPoints.Ascendant).ToString();
        tbGlyphVertex.Text = _controller.DefineGlyph(ChartPoints.Vertex).ToString();
        tbGlyphEastpoint.Text = _controller.DefineGlyph(ChartPoints.EastPoint).ToString();

        tbGlyphParsSect.Text = _controller.DefineGlyph(ChartPoints.FortunaSect).ToString();
        tbGlyphParsNoSect.Text = _controller.DefineGlyph(ChartPoints.FortunaNoSect).ToString();

        tbGlyphMeanNode.Text = _controller.DefineGlyph(ChartPoints.MeanNode).ToString();
        tbGlyphTrueNode.Text = _controller.DefineGlyph(ChartPoints.TrueNode).ToString();
        tbGlyphMeanBLackMoon.Text = _controller.DefineGlyph(ChartPoints.ApogeeMean).ToString();
        tbGlyphCorrBlackMoon.Text = _controller.DefineGlyph(ChartPoints.ApogeeCorrected).ToString();
        tbGlyphInterpolatedBlackMoon.Text = _controller.DefineGlyph(ChartPoints.ApogeeInterpolated).ToString();
        tbGlyphDuvalBlackMoon.Text = _controller.DefineGlyph(ChartPoints.ApogeeDuval).ToString();
        tbGlyphZeroAries.Text = _controller.DefineGlyph(ChartPoints.ZeroAries).ToString();

        tbGlyphHuya.Text = _controller.DefineGlyph(ChartPoints.Huya).ToString();
        tbGlyphVaruna.Text = _controller.DefineGlyph(ChartPoints.Varuna).ToString();
        tbGlyphIxion.Text = _controller.DefineGlyph(ChartPoints.Ixion).ToString();
        tbGlyphQuaoar.Text = _controller.DefineGlyph(ChartPoints.Quaoar).ToString();
        tbGlyphHaumea.Text = _controller.DefineGlyph(ChartPoints.Haumea).ToString();
        tbGlyphEris.Text = _controller.DefineGlyph(ChartPoints.Eris).ToString();
        tbGlyphSedna.Text = _controller.DefineGlyph(ChartPoints.Sedna).ToString();
        tbGlyphOrcus.Text = _controller.DefineGlyph(ChartPoints.Orcus).ToString();
        tbGlyphMakemake.Text = _controller.DefineGlyph(ChartPoints.Makemake).ToString();

        tbGlyphChiron.Text = _controller.DefineGlyph(ChartPoints.Chiron).ToString();
        tbGlyphNessus.Text = _controller.DefineGlyph(ChartPoints.Nessus).ToString();
        tbGlyphPholus.Text = _controller.DefineGlyph(ChartPoints.Pholus).ToString();

        tbGlyphCeres.Text = _controller.DefineGlyph(ChartPoints.Ceres).ToString();
        tbGlyphPallas.Text = _controller.DefineGlyph(ChartPoints.Pallas).ToString();
        tbGlyphJuno.Text = _controller.DefineGlyph(ChartPoints.Juno).ToString();
        tbGlyphVesta.Text = _controller.DefineGlyph(ChartPoints.Vesta).ToString();
        tbGlyphHygieia.Text = _controller.DefineGlyph(ChartPoints.Hygieia).ToString();
        tbGlyphAstraea.Text = _controller.DefineGlyph(ChartPoints.Astraea).ToString();

        tbGlyphCupido.Text = _controller.DefineGlyph(ChartPoints.CupidoUra).ToString();
        tbGlyphHades.Text = _controller.DefineGlyph(ChartPoints.HadesUra).ToString();
        tbGlyphZeus.Text = _controller.DefineGlyph(ChartPoints.ZeusUra).ToString();
        tbGlyphKronos.Text = _controller.DefineGlyph(ChartPoints.KronosUra).ToString();
        tbGlyphApollon.Text = _controller.DefineGlyph(ChartPoints.ApollonUra).ToString();
        tbGlyphAdmetos.Text = _controller.DefineGlyph(ChartPoints.AdmetosUra).ToString();
        tbGlyphVulkanusUra.Text = _controller.DefineGlyph(ChartPoints.VulcanusUra).ToString();
        tbGlyphPoseidon.Text = _controller.DefineGlyph(ChartPoints.PoseidonUra).ToString();
        tbGlyphPersephoneRam.Text = _controller.DefineGlyph(ChartPoints.PersephoneRam).ToString();
        tbGlyphHermes.Text = _controller.DefineGlyph(ChartPoints.HermesRam).ToString();
        tbGlyphDemeter.Text = _controller.DefineGlyph(ChartPoints.DemeterRam).ToString();
        tbGlyphVulcanusCarteret.Text = _controller.DefineGlyph(ChartPoints.VulcanusCarteret).ToString();
        tbGlyphPersephoneCarteret.Text = _controller.DefineGlyph(ChartPoints.PersephoneCarteret).ToString();
        tbGlyphTransPluto.Text = _controller.DefineGlyph(ChartPoints.Isis).ToString();

        tbGlyphConjunction.Text = (AspectTypes.Conjunction.GetDetails().Glyph).ToString();
        tbGlyphOpposition.Text = (AspectTypes.Opposition.GetDetails().Glyph).ToString();
        tbGlyphTriangle.Text = (AspectTypes.Triangle.GetDetails().Glyph).ToString();
        tbGlyphSquare.Text = (AspectTypes.Square.GetDetails().Glyph).ToString();
        tbGlyphSextile.Text = (AspectTypes.Sextile.GetDetails().Glyph).ToString();
        tbGlyphSemiSextile.Text = (AspectTypes.SemiSextile.GetDetails().Glyph).ToString();
        tbGlyphInconjunct.Text = (AspectTypes.Inconjunct.GetDetails().Glyph).ToString();
        tbGlyphSemiSquare.Text = (AspectTypes.SemiSquare.GetDetails().Glyph).ToString();
        tbGlyphSesquiquadrate.Text = (AspectTypes.SesquiQuadrate.GetDetails().Glyph).ToString();
        tbGlyphQuintile.Text = (AspectTypes.Quintile.GetDetails().Glyph).ToString();
        tbGlyphBiQuintile.Text = (AspectTypes.BiQuintile.GetDetails().Glyph).ToString();
        tbGlyphSeptile.Text = (AspectTypes.Septile.GetDetails().Glyph).ToString();
        tbGlyphVigintile.Text = (AspectTypes.Vigintile.GetDetails().Glyph).ToString();
        tbGlyphUnDecile.Text = (AspectTypes.Undecile.GetDetails().Glyph).ToString();
        tbGlyphSemiQuintile.Text = (AspectTypes.SemiQuintile.GetDetails().Glyph).ToString();
        tbGlyphNovile.Text = (AspectTypes.Novile.GetDetails().Glyph).ToString();
        tbGlyphBiNovile.Text = (AspectTypes.BiNovile.GetDetails().Glyph).ToString();
        tbGlyphCentile.Text = (AspectTypes.Centile.GetDetails().Glyph).ToString();
        tbGlyphBiSeptile.Text = (AspectTypes.BiSeptile.GetDetails().Glyph).ToString();
        tbGlyphTriDecile.Text = (AspectTypes.TriDecile.GetDetails().Glyph).ToString();
        tbGlyphTriSeptile.Text = (AspectTypes.TriSeptile.GetDetails().Glyph).ToString();
        tbGlyphQuadraNovile.Text = (AspectTypes.QuadraNovile.GetDetails().Glyph).ToString();

    }



    private void DefaultValues()
    {

        comboHouseSystem.Items.Clear();
        foreach (HouseSystemDetails detail in HouseSystems.NoHouses.AllDetails())
        {
            comboHouseSystem.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboHouseSystem.SelectedIndex = (int)_controller.GetConfig().HouseSystem;

        comboZodiacType.Items.Clear();
        foreach (ZodiacTypeDetails details in ZodiacTypes.Tropical.AllDetails())
        {
            comboZodiacType.Items.Add(_rosetta.TextForId(details.TextId));
        }
        comboZodiacType.SelectedIndex = (int)_controller.GetConfig().ZodiacType;

        comboAyanamsha.Items.Clear();
        foreach (AyanamshaDetails detail in Ayanamshas.None.AllDetails())
        {
            comboAyanamsha.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboAyanamsha.SelectedIndex = (int)_controller.GetConfig().Ayanamsha;

        comboObserverPos.Items.Clear();
        foreach (ObserverPositionDetails detail in ObserverPositions.GeoCentric.AllDetails())
        {
            comboObserverPos.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboObserverPos.SelectedIndex = (int)_controller.GetConfig().ObserverPosition;

        comboProjectionType.Items.Clear();
        foreach (ProjectionTypeDetails detail in ProjectionTypes.TwoDimensional.AllDetails())
        {
            comboProjectionType.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboProjectionType.SelectedIndex = (int)_controller.GetConfig().ProjectionType;

        tboxAspectBaseOrb.Text = _controller.GetConfig().BaseOrbAspects.ToString();
        tboxMidpointAspectBaseOrb.Text = _controller.GetConfig().BaseOrbMidpoints.ToString();


        // main celestial points

        List<ChartPointConfigSpecs> chartConfigPoints = new();
        foreach (var configPoint in _controller.GetConfig().ChartPoints)
        {
            PointCats cat = configPoint.Point.GetDetails().PointCat;
            chartConfigPoints.Add(configPoint);
        }
        tboxSunFactor.Text = chartConfigPoints[(int)ChartPoints.Sun].PercentageOrb.ToString();
        tboxMoonFactor.Text = chartConfigPoints[(int)ChartPoints.Moon].PercentageOrb.ToString();
        tboxMercuryFactor.Text = chartConfigPoints[(int)ChartPoints.Mercury].PercentageOrb.ToString();
        tboxVenusFactor.Text = chartConfigPoints[(int)ChartPoints.Venus].PercentageOrb.ToString();
        tboxMarsFactor.Text = chartConfigPoints[(int)ChartPoints.Mars].PercentageOrb.ToString();
        tboxJupiterFactor.Text = chartConfigPoints[(int)ChartPoints.Jupiter].PercentageOrb.ToString();
        tboxSaturnFactor.Text = chartConfigPoints[(int)ChartPoints.Saturn].PercentageOrb.ToString();
        tboxUranusFactor.Text = chartConfigPoints[(int)ChartPoints.Uranus].PercentageOrb.ToString();
        cboxUranus.IsChecked = chartConfigPoints[(int)ChartPoints.Uranus].IsUsed;
        tboxNeptuneFactor.Text = chartConfigPoints[(int)ChartPoints.Neptune].PercentageOrb.ToString();
        cboxNeptune.IsChecked = chartConfigPoints[(int)ChartPoints.Neptune].IsUsed;
        tboxPlutoFactor.Text = chartConfigPoints[(int)ChartPoints.Pluto].PercentageOrb.ToString();
        cboxPluto.IsChecked = chartConfigPoints[(int)ChartPoints.Pluto].IsUsed;
        tboxMeanNodeFactor.Text = chartConfigPoints[(int)ChartPoints.MeanNode].PercentageOrb.ToString();
        cboxMeanNode.IsChecked = chartConfigPoints[(int)ChartPoints.MeanNode].IsUsed;
        tboxTrueNodeFactor.Text = chartConfigPoints[(int)ChartPoints.TrueNode].PercentageOrb.ToString();
        cboxTrueNode.IsChecked = chartConfigPoints[(int)ChartPoints.TrueNode].IsUsed;
        tboxChironFactor.Text = chartConfigPoints[(int)ChartPoints.Chiron].PercentageOrb.ToString();
        cboxChiron.IsChecked = chartConfigPoints[(int)ChartPoints.Chiron].IsUsed;
        tboxPersephoneRamFactor.Text = chartConfigPoints[(int)ChartPoints.PersephoneRam].PercentageOrb.ToString();
        cboxPersephoneRam.IsChecked = chartConfigPoints[(int)ChartPoints.PersephoneRam].IsUsed;
        tboxHermesFactor.Text = chartConfigPoints[(int)ChartPoints.HermesRam].PercentageOrb.ToString();
        cboxHermes.IsChecked = chartConfigPoints[(int)ChartPoints.HermesRam].IsUsed;
        tboxDemeterFactor.Text = chartConfigPoints[(int)ChartPoints.DemeterRam].PercentageOrb.ToString();
        cboxDemeter.IsChecked = chartConfigPoints[(int)ChartPoints.DemeterRam].IsUsed;
        tboxCupidoFactor.Text = chartConfigPoints[(int)ChartPoints.CupidoUra].PercentageOrb.ToString();
        cboxCupido.IsChecked = chartConfigPoints[(int)ChartPoints.CupidoUra].IsUsed;
        tboxHadesFactor.Text = chartConfigPoints[(int)ChartPoints.HadesUra].PercentageOrb.ToString();
        cboxHades.IsChecked = chartConfigPoints[(int)ChartPoints.HadesUra].IsUsed;
        tboxZeusFactor.Text = chartConfigPoints[(int)ChartPoints.ZeusUra].PercentageOrb.ToString();
        cboxZeus.IsChecked = chartConfigPoints[(int)ChartPoints.ZeusUra].IsUsed;
        tboxKronosFactor.Text = chartConfigPoints[(int)ChartPoints.KronosUra].PercentageOrb.ToString();
        cboxKronos.IsChecked = chartConfigPoints[(int)ChartPoints.KronosUra].IsUsed;
        tboxApollonFactor.Text = chartConfigPoints[(int)ChartPoints.ApollonUra].PercentageOrb.ToString();
        cboxApollon.IsChecked = chartConfigPoints[(int)ChartPoints.ApollonUra].IsUsed;
        tboxAdmetosFactor.Text = chartConfigPoints[(int)ChartPoints.AdmetosUra].PercentageOrb.ToString();
        cboxAdmetos.IsChecked = chartConfigPoints[(int)ChartPoints.AdmetosUra].IsUsed;
        tboxVulkanusUraFactor.Text = chartConfigPoints[(int)ChartPoints.VulcanusUra].PercentageOrb.ToString();
        cboxVulkanusUra.IsChecked = chartConfigPoints[(int)ChartPoints.VulcanusUra].IsUsed;
        tboxPoseidonFactor.Text = chartConfigPoints[(int)ChartPoints.PoseidonUra].PercentageOrb.ToString();
        cboxPoseidon.IsChecked = chartConfigPoints[(int)ChartPoints.PoseidonUra].IsUsed;
        tboxErisFactor.Text = chartConfigPoints[(int)ChartPoints.Eris].PercentageOrb.ToString();
        cboxEris.IsChecked = chartConfigPoints[(int)ChartPoints.Eris].IsUsed;
        tboxPholusFactor.Text = chartConfigPoints[(int)ChartPoints.Pholus].PercentageOrb.ToString();
        cboxPholus.IsChecked = chartConfigPoints[(int)ChartPoints.Pholus].IsUsed;
        tboxCeresFactor.Text = chartConfigPoints[(int)ChartPoints.Ceres].PercentageOrb.ToString();
        cboxCeres.IsChecked = chartConfigPoints[(int)ChartPoints.Ceres].IsUsed;
        tboxPallasFactor.Text = chartConfigPoints[(int)ChartPoints.Pallas].PercentageOrb.ToString();
        cboxPallas.IsChecked = chartConfigPoints[(int)ChartPoints.Pallas].IsUsed;
        tboxJunoFactor.Text = chartConfigPoints[(int)ChartPoints.Juno].PercentageOrb.ToString();
        cboxJuno.IsChecked = chartConfigPoints[(int)ChartPoints.Juno].IsUsed;
        tboxVestaFactor.Text = chartConfigPoints[(int)ChartPoints.Vesta].PercentageOrb.ToString();
        cboxVesta.IsChecked = chartConfigPoints[(int)ChartPoints.Vesta].IsUsed;
        tboxTransPlutoFactor.Text = chartConfigPoints[(int)ChartPoints.Isis].PercentageOrb.ToString();
        cboxTransPluto.IsChecked = chartConfigPoints[(int)ChartPoints.Isis].IsUsed;
        tboxNessusFactor.Text = chartConfigPoints[(int)ChartPoints.Nessus].PercentageOrb.ToString();
        cboxNessus.IsChecked = chartConfigPoints[(int)ChartPoints.Nessus].IsUsed;
        tboxHuyaFactor.Text = chartConfigPoints[(int)ChartPoints.Huya].PercentageOrb.ToString();
        cboxHuya.IsChecked = chartConfigPoints[(int)ChartPoints.Huya].IsUsed;
        tboxVarunaFactor.Text = chartConfigPoints[(int)ChartPoints.Varuna].PercentageOrb.ToString();
        cboxVaruna.IsChecked = chartConfigPoints[(int)ChartPoints.Varuna].IsUsed;
        tboxIxionFactor.Text = chartConfigPoints[(int)ChartPoints.Ixion].PercentageOrb.ToString();
        cboxIxion.IsChecked = chartConfigPoints[(int)ChartPoints.Ixion].IsUsed;
        tboxQuaoarFactor.Text = chartConfigPoints[(int)ChartPoints.Quaoar].PercentageOrb.ToString();
        cboxQuaoar.IsChecked = chartConfigPoints[(int)ChartPoints.Quaoar].IsUsed;
        tboxHaumeaFactor.Text = chartConfigPoints[(int)ChartPoints.Haumea].PercentageOrb.ToString();
        cboxHaumea.IsChecked = chartConfigPoints[(int)ChartPoints.Haumea].IsUsed;
        tboxOrcusFactor.Text = chartConfigPoints[(int)ChartPoints.Orcus].PercentageOrb.ToString();
        cboxOrcus.IsChecked = chartConfigPoints[(int)ChartPoints.Orcus].IsUsed;
        tboxMakemakeFactor.Text = chartConfigPoints[(int)ChartPoints.Makemake].PercentageOrb.ToString();
        cboxMakemake.IsChecked = chartConfigPoints[(int)ChartPoints.Makemake].IsUsed;
        tboxSednaFactor.Text = chartConfigPoints[(int)ChartPoints.Sedna].PercentageOrb.ToString();
        cboxSedna.IsChecked = chartConfigPoints[(int)ChartPoints.Sedna].IsUsed;
        tboxHygieiaFactor.Text = chartConfigPoints[(int)ChartPoints.Hygieia].PercentageOrb.ToString();
        cboxHygieia.IsChecked = chartConfigPoints[(int)ChartPoints.Hygieia].IsUsed;
        tboxAstraeaFactor.Text = chartConfigPoints[(int)ChartPoints.Astraea].PercentageOrb.ToString();
        cboxAstraea.IsChecked = chartConfigPoints[(int)ChartPoints.Astraea].IsUsed;
        tboxMeanBlackMoonFactor.Text = chartConfigPoints[(int)ChartPoints.ApogeeMean].PercentageOrb.ToString();
        cboxMeanBlackMoon.IsChecked = chartConfigPoints[(int)ChartPoints.ApogeeMean].IsUsed;
        tboxCorrBlackMoonFactor.Text = chartConfigPoints[(int)ChartPoints.ApogeeCorrected].PercentageOrb.ToString();
        cboxCorrBlackMoon.IsChecked = chartConfigPoints[(int)ChartPoints.ApogeeCorrected].IsUsed;
        tboxInterpolatedBlackMoonFactor.Text = chartConfigPoints[(int)ChartPoints.ApogeeInterpolated].PercentageOrb.ToString();
        cboxInterpolatedBlackMoon.IsChecked = chartConfigPoints[(int)ChartPoints.ApogeeInterpolated].IsUsed;
        tboxDuvalBlackMoonFactor.Text = chartConfigPoints[(int)ChartPoints.ApogeeDuval].PercentageOrb.ToString();
        cboxDuvalBlackMoon.IsChecked = false;
        cboxDuvalBlackMoon.IsEnabled = false;
        tboxPersephoneCarteretFactor.Text = chartConfigPoints[(int)ChartPoints.PersephoneCarteret].PercentageOrb.ToString();
        cboxPersephoneCarteret.IsChecked = false;
        cboxPersephoneCarteret.IsEnabled = false;
        tboxVulcanusCarteretFactor.Text = chartConfigPoints[(int)ChartPoints.VulcanusCarteret].PercentageOrb.ToString();
        cboxVulcanusCarteret.IsChecked = false;
        cboxVulcanusCarteret.IsEnabled = false;


        // mundane points
        // TODO 0.1  add mundane and other points to configuration.
        List<ChartPointConfigSpecs> chartMundaneConfigPoints = new()
        {
            new ChartPointConfigSpecs(ChartPoints.Mc, true, 'M', 100),
            new ChartPointConfigSpecs(ChartPoints.Ascendant, true, 'A', 100),
            new ChartPointConfigSpecs(ChartPoints.EastPoint, false, ' ', 0),
            new ChartPointConfigSpecs(ChartPoints.Vertex, false, ' ', 0)
        };

        int offsetMundane = 1001;
        tboxAscFactor.Text = chartMundaneConfigPoints[((int)ChartPoints.Ascendant) - offsetMundane].PercentageOrb.ToString();
        cboxAsc.IsChecked = chartMundaneConfigPoints[((int)ChartPoints.Ascendant) - offsetMundane].IsUsed;
        tboxMcFactor.Text = chartMundaneConfigPoints[((int)ChartPoints.Mc) - offsetMundane].PercentageOrb.ToString();
        cboxMc.IsChecked = chartMundaneConfigPoints[((int)ChartPoints.Mc) - offsetMundane].IsUsed;
        tboxEastpointFactor.Text = chartMundaneConfigPoints[((int)ChartPoints.EastPoint) - offsetMundane].PercentageOrb.ToString();
        cboxEastpoint.IsChecked = chartMundaneConfigPoints[((int)ChartPoints.EastPoint) - offsetMundane].IsUsed;
        tboxVertexFactor.Text = chartMundaneConfigPoints[((int)ChartPoints.Vertex) - offsetMundane].PercentageOrb.ToString();
        cboxVertex.IsChecked = chartMundaneConfigPoints[((int)ChartPoints.Vertex) - offsetMundane].IsUsed;

        List<ChartPointConfigSpecs> chartZodiacConfigPoints = new()
        {
            new ChartPointConfigSpecs(ChartPoints.ZeroAries, false, '1', 0)
        };
        int offsetZodiac = 3001;
        tboxZeroAriesFactor.Text = chartZodiacConfigPoints[((int)ChartPoints.ZeroAries) - offsetZodiac].PercentageOrb.ToString();
        cboxZeroAries.IsChecked = chartZodiacConfigPoints[((int)ChartPoints.ZeroAries) - offsetZodiac].IsUsed;
        // TODO 0.1 add ZeroCancer

        List<ChartPointConfigSpecs> chartArabicConfigPoints = new()
        {
            new ChartPointConfigSpecs(ChartPoints.FortunaSect, false, 'e', 0),
            new ChartPointConfigSpecs(ChartPoints.FortunaNoSect, false, 'e', 0)
        };
        int offsetArabic = 4001;
        tboxParsSectFactor.Text = chartArabicConfigPoints[((int)ChartPoints.FortunaSect) - offsetArabic].PercentageOrb.ToString();
        cboxParsSect.IsChecked = chartArabicConfigPoints[((int)ChartPoints.FortunaSect) - offsetArabic].IsUsed;
        tboxParsNoSectFactor.Text = chartArabicConfigPoints[((int)ChartPoints.FortunaNoSect) - offsetArabic].PercentageOrb.ToString();
        cboxParsNoSect.IsChecked = chartArabicConfigPoints[((int)ChartPoints.FortunaNoSect) - offsetArabic].IsUsed;


        // aspects
        List<AspectConfigSpecs> aspects = _controller.GetConfig().Aspects;
        comboOrbMethod.Items.Clear();
        foreach (OrbMethodDetails detail in OrbMethods.Weighted.AllDetails())
        {
            comboOrbMethod.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboOrbMethod.SelectedIndex = (int)_controller.GetConfig().OrbMethod;


        tboxConjunctionFactor.Text = aspects[0].PercentageOrb.ToString();
        cboxConjunction.IsChecked = aspects[0].IsUsed;
        tboxOppositionFactor.Text = aspects[1].PercentageOrb.ToString();
        cboxOpposition.IsChecked = aspects[1].IsUsed;
        tboxTriangleFactor.Text = aspects[2].PercentageOrb.ToString();
        cboxTriangle.IsChecked = aspects[2].IsUsed;
        tboxSquareFactor.Text = aspects[3].PercentageOrb.ToString();
        cboxSquare.IsChecked = aspects[3].IsUsed;
        tboxSeptileFactor.Text = aspects[4].PercentageOrb.ToString();
        cboxSeptile.IsChecked = aspects[4].IsUsed;
        tboxSextileFactor.Text = aspects[5].PercentageOrb.ToString();
        cboxSextile.IsChecked = aspects[5].IsUsed;
        tboxQuintileFactor.Text = aspects[6].PercentageOrb.ToString();
        cboxQuintile.IsChecked = aspects[6].IsUsed;
        tboxSemiSextileFactor.Text = aspects[7].PercentageOrb.ToString();
        cboxSemiSextile.IsChecked = aspects[7].IsUsed;
        tboxSemiSquareFactor.Text = aspects[8].PercentageOrb.ToString();
        cboxSemiSquare.IsChecked = aspects[8].IsUsed;
        tboxSemiQuintileFactor.Text = aspects[9].PercentageOrb.ToString();
        cboxSemiQuintile.IsChecked = aspects[9].IsUsed;
        tboxBiQuintileFactor.Text = aspects[10].PercentageOrb.ToString();
        cboxBiQuintile.IsChecked = aspects[10].IsUsed;
        tboxInconjunctFactor.Text = aspects[11].PercentageOrb.ToString();
        cboxInconjunct.IsChecked = aspects[11].IsUsed;
        tboxSesquiquadrateFactor.Text = aspects[12].PercentageOrb.ToString();
        cboxSesquiquadrate.IsChecked = aspects[12].IsUsed;
        tboxTriDecileFactor.Text = aspects[13].PercentageOrb.ToString();
        cboxTriDecile.IsChecked = aspects[13].IsUsed;
        tboxBiSeptileFactor.Text = aspects[14].PercentageOrb.ToString();
        cboxBiSeptile.IsChecked = aspects[14].IsUsed;
        tboxTriSeptileFactor.Text = aspects[15].PercentageOrb.ToString();
        cboxTriSeptile.IsChecked = aspects[15].IsUsed;
        tboxNovileFactor.Text = aspects[16].PercentageOrb.ToString();
        cboxNovile.IsChecked = aspects[16].IsUsed;
        tboxBiNovileFactor.Text = aspects[17].PercentageOrb.ToString();
        cboxBiNovile.IsChecked = aspects[17].IsUsed;
        tboxQuadraNovileFactor.Text = aspects[18].PercentageOrb.ToString();
        cboxQuadraNovile.IsChecked = aspects[18].IsUsed;
        tboxUnDecileFactor.Text = aspects[19].PercentageOrb.ToString();
        cboxUnDecile.IsChecked = aspects[19].IsUsed;
        tboxCentileFactor.Text = aspects[20].PercentageOrb.ToString();
        cboxCentile.IsChecked = aspects[20].IsUsed;
        tboxVigintileFactor.Text = aspects[21].PercentageOrb.ToString();
        cboxVigintile.IsChecked = aspects[21].IsUsed;
    }

    public void CancelClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }
    public void HelpClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowHelp();
    }

    public void OkClick(object sender, RoutedEventArgs e)
    {
        if (HandleInput())
        {
            Hide();   // TODO check, change into Close() ?

        }
        else
        {
            MessageBox.Show(_rosetta != null ? _rosetta.TextForId("astroconfigwindow.errorsfound") : "An error occurred, check the logfile");     // TODO use RB
        }

    }

    public void AyanamshaChanged(object sender, RoutedEventArgs e)
    {
        comboZodiacType.SelectedIndex = comboAyanamsha.SelectedIndex == 0 ? 1 : 0;
    }

    public void ZodiacTypeChanged(object sender, RoutedEventArgs e)
    {
        if (comboZodiacType.SelectedIndex == 1)
        {
            comboAyanamsha.SelectedIndex = 0;
        }
        else if (comboAyanamsha.SelectedIndex == 0)
        {
            comboAyanamsha.SelectedIndex = 2;  // Lahiri
        }
    }

    private bool HandleInput()
    {
        // TODO 0.2 Add validation for input, also take acceptable ranges into account.

        bool noErrors = true;
        try
        {
            HouseSystems houseSystem = HouseSystems.NoHouses.HouseSystemForIndex(comboHouseSystem.SelectedIndex);
            ZodiacTypes zodiacType = ZodiacTypes.Tropical.ZodiacTypeForIndex(comboZodiacType.SelectedIndex);
            Ayanamshas ayanamsha = Ayanamshas.None.AyanamshaForIndex(comboAyanamsha.SelectedIndex);
            ObserverPositions observerPosition = ObserverPositions.GeoCentric.ObserverPositionForIndex(comboObserverPos.SelectedIndex);
            ProjectionTypes projectionType = ProjectionTypes.TwoDimensional.ProjectionTypeForIndex(comboProjectionType.SelectedIndex);
            OrbMethods orbMethod = OrbMethods.Weighted.OrbMethodForIndex(comboOrbMethod.SelectedIndex);
            List<ChartPointConfigSpecs> celPointsSpecs = DefineCelPointSpecs();
            List<ChartPointConfigSpecs> mundanePointSpecs = DefineMundanePointSpecs();
            List<ChartPointConfigSpecs> arabicPointSpecs = DefineArabicPointSpecs();
            List<ChartPointConfigSpecs> zodiacPointSpecs = DefineZodiacPointSpecs();
            double baseOrbAspects = Convert.ToDouble(tboxAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            double baseOrbMidpoints = Convert.ToDouble(tboxMidpointAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            List<AspectConfigSpecs> aspectSpecs = DefineAspectSpecs();
            bool useCuspsForAspects = false;                // TODO 0.1 Read vaue for UseCuspsForAspects from config form
            AstroConfig astroConfig = new(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod, celPointsSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
            Log.Information("Created new configuration: {@astroConfig}", astroConfig);
            _controller.UpdateConfig(astroConfig);

        }
        catch (Exception e)
        {
            Log.Error("Error: exception occurred when updating configuration. Exception msg: {e}", e);
            noErrors = false;
        }
        return noErrors;
    }

    private List<ChartPointConfigSpecs> DefineCelPointSpecs()
    {
        List<ChartPointConfigSpecs> celPointSpecs = new()
        {
            DefineSingleChartPointSpec(ChartPoints.Sun, cboxSun.IsChecked ?? false, 'a', tboxSunFactor.Text ),
            DefineSingleChartPointSpec(ChartPoints.Moon, cboxMoon.IsChecked ?? false, 'b', tboxMoonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Mercury, cboxMercury.IsChecked ?? false, 'c', tboxMercuryFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Venus, cboxVenus.IsChecked ?? false, 'd', tboxVenusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Earth, false, 'e',  "100"),                                       // TODO 0.4 handle earth for heliocentric
            DefineSingleChartPointSpec(ChartPoints.Mars, cboxMars.IsChecked ?? false, 'f', tboxMarsFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Jupiter, cboxJupiter.IsChecked ?? false, 'g', tboxJupiterFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Saturn, cboxSaturn.IsChecked ?? false, 'h', tboxSaturnFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Uranus, cboxUranus.IsChecked ?? false, 'i', tboxUranusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Neptune, cboxNeptune.IsChecked ?? false, 'j', tboxNeptuneFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Pluto, cboxPluto.IsChecked ?? false, 'k', tboxPlutoFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.MeanNode, cboxMeanNode.IsChecked ?? false, '{', tboxMeanNodeFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.TrueNode, cboxTrueNode.IsChecked ?? false, '}', tboxTrueNodeFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Chiron, cboxChiron.IsChecked ?? false, 'w', tboxChironFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.PersephoneRam, cboxPersephoneRam.IsChecked ?? false, '/', tboxPersephoneRamFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.HermesRam, cboxHermes.IsChecked ?? false, '<', tboxHermesFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.DemeterRam, cboxDemeter.IsChecked ?? false, '>', tboxDemeterFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.CupidoUra, cboxCupido.IsChecked ?? false, 'y', tboxCupidoFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.HadesUra, cboxHades.IsChecked ?? false, 'z', tboxHadesFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ZeusUra, cboxZeus.IsChecked ?? false, '!', tboxZeusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.KronosUra, cboxKronos.IsChecked ?? false, '@', tboxKronosFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ApollonUra, cboxApollon.IsChecked ?? false, '#', tboxApollonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.AdmetosUra, cboxAdmetos.IsChecked ?? false, '$', tboxAdmetosFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.VulcanusUra, cboxVulkanusUra.IsChecked ?? false, '%', tboxVulkanusUraFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.PoseidonUra, cboxPoseidon.IsChecked ?? false, '&', tboxPoseidonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Eris, cboxEris.IsChecked ?? false, '*', tboxErisFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Pholus, cboxPholus.IsChecked ?? false, ')', tboxPholusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Ceres, cboxCeres.IsChecked ?? false, '_', tboxCeresFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Pallas, cboxPallas.IsChecked ?? false, 'û', tboxPallasFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Juno, cboxJuno.IsChecked ?? false, 'ü', tboxJunoFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Vesta, cboxVesta.IsChecked ?? false, 'À', tboxVestaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Isis, cboxTransPluto.IsChecked ?? false, 'â', tboxTransPlutoFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Nessus, cboxNessus.IsChecked ?? false, '(', tboxNessusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Huya, cboxHuya.IsChecked ?? false, 'ï', tboxHuyaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Varuna, cboxVaruna.IsChecked ?? false, 'ò', tboxVarunaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Ixion, cboxIxion.IsChecked ?? false, 'ó', tboxIxionFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Quaoar, cboxQuaoar.IsChecked ?? false, 'ô', tboxQuaoarFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Haumea, cboxHaumea.IsChecked ?? false, 'í', tboxHaumeaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Orcus, cboxOrcus.IsChecked ?? false, 'ù', tboxOrcusFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Makemake, cboxMakemake.IsChecked ?? false, 'î', tboxMakemakeFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Sedna, cboxSedna.IsChecked ?? false, 'ö', tboxSednaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Hygieia, cboxHygieia.IsChecked ?? false, 'Á', tboxHygieiaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Astraea, cboxAstraea.IsChecked ?? false, 'Ã', tboxAstraeaFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ApogeeMean, cboxMeanBlackMoon.IsChecked ?? false, ',', tboxMeanBlackMoonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ApogeeCorrected, cboxCorrBlackMoon.IsChecked ?? false, '.', tboxCorrBlackMoonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ApogeeInterpolated, cboxInterpolatedBlackMoon.IsChecked ?? false, '.', tboxInterpolatedBlackMoonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.ApogeeDuval, cboxDuvalBlackMoon.IsChecked ?? false, '.', tboxDuvalBlackMoonFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.PersephoneCarteret, cboxPersephoneCarteret.IsChecked ?? false, 'à', tboxPersephoneCarteretFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.VulcanusCarteret, cboxVulcanusCarteret.IsChecked ?? false,  'Ï', tboxVulcanusCarteretFactor.Text)
        };
        return celPointSpecs;
    }



    private List<ChartPointConfigSpecs> DefineMundanePointSpecs()
    {
        List<ChartPointConfigSpecs> mundanePointSpecs = new()
        {
            DefineSingleChartPointSpec(ChartPoints.Mc, cboxMc.IsChecked ?? false, 'M', tboxMcFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Ascendant, cboxAsc.IsChecked ?? false, 'A', tboxAscFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.Vertex, cboxVertex.IsChecked ?? false, ' ', tboxVertexFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.EastPoint, cboxEastpoint.IsChecked ?? false, ' ', tboxEastpointFactor.Text)
        };

        return mundanePointSpecs;
    }


    private List<ChartPointConfigSpecs> DefineArabicPointSpecs()
    {
        List<ChartPointConfigSpecs> arabicPointConfigSpecs = new()
        {
            DefineSingleChartPointSpec(ChartPoints.FortunaNoSect, cboxParsNoSect.IsChecked ?? false, 'e', tboxParsNoSectFactor.Text),
            DefineSingleChartPointSpec(ChartPoints.FortunaSect, cboxParsSect.IsChecked ?? false, 'e', tboxParsSectFactor.Text)
        };
        return arabicPointConfigSpecs;
    }


    private List<ChartPointConfigSpecs> DefineZodiacPointSpecs()
    {
        List<ChartPointConfigSpecs> zodiacPointConfigSpecs = new()
        {
            DefineSingleChartPointSpec(ChartPoints.ZeroAries, cboxZeroAries.IsChecked ??  false, '1', tboxZeroAriesFactor.Text)
        };
        return zodiacPointConfigSpecs;
    }


    private static ChartPointConfigSpecs DefineSingleChartPointSpec(ChartPoints celPoint, bool isUsed, char glyph, string orbFactorText)
    {
        int orbFactorValue = Convert.ToInt32(orbFactorText, CultureInfo.InvariantCulture);
        return new ChartPointConfigSpecs(celPoint, isUsed, glyph, orbFactorValue);
    }




    private List<AspectConfigSpecs> DefineAspectSpecs()
    {
        List<AspectConfigSpecs> aspectSpecs = new()
        {
            DefineSingleAspectSpec(AspectTypes.Conjunction, 'B', tboxConjunctionFactor.Text, cboxConjunction.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Opposition, 'C', tboxOppositionFactor.Text, cboxOpposition.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Triangle, 'D', tboxTriangleFactor.Text, cboxTriangle.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Square, 'E', tboxSquareFactor.Text, cboxSquare.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Septile, 'N', tboxSeptileFactor.Text, cboxSeptile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Sextile, 'F', tboxSextileFactor.Text, cboxSextile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Quintile, 'K', tboxQuintileFactor.Text, cboxQuintile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.SemiSextile, 'G', tboxSemiSextileFactor.Text, cboxSemiSextile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.SemiSquare, 'I', tboxSemiSquareFactor.Text, cboxSemiSquare.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.SemiQuintile, 'Ô', tboxSemiQuintileFactor.Text, cboxSemiQuintile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.BiQuintile, 'L', tboxBiQuintileFactor.Text, cboxBiQuintile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Inconjunct, 'H', tboxInconjunctFactor.Text, cboxInconjunct.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.SesquiQuadrate, 'J', tboxSesquiquadrateFactor.Text, cboxSesquiquadrate.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.TriDecile, 'Õ', tboxTriDecileFactor.Text, cboxTriDecile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.BiSeptile, 'Ú', tboxBiSeptileFactor.Text, cboxBiSeptile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.TriSeptile, 'Û', tboxTriSeptileFactor.Text, cboxTriSeptile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Novile, 'Ü', tboxNovileFactor.Text, cboxNovile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.BiNovile, 'Ñ', tboxBiNovileFactor.Text, cboxBiNovile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.QuadraNovile, '|', tboxQuadraNovileFactor.Text, cboxQuadraNovile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Undecile, 'ç', tboxUnDecileFactor.Text, cboxUnDecile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Centile, 'Ç', tboxCentileFactor.Text, cboxCentile.IsChecked ?? false),
            DefineSingleAspectSpec(AspectTypes.Vigintile, 'Ï', tboxVigintileFactor.Text, cboxVigintile.IsChecked ?? false)
        };

        return aspectSpecs;
    }
    private static AspectConfigSpecs DefineSingleAspectSpec(AspectTypes aspectType, char glyph, string percentageText, bool isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new AspectConfigSpecs(aspectType, isUsed, glyph, percentageValue);
    }
}
