// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Exceptions;
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
        Title = Rosetta.TextForId("astroconfigwindow.title");
        FormTitle.Text = Rosetta.TextForId("astroconfigwindow.formtitle");
        TitleGeneral.Text = Rosetta.TextForId("astroconfigwindow.titlegeneral");
        tabGeneral.Header = Rosetta.TextForId("astroconfigwindow.tabgeneral");
        tabBasicPoints.Header = Rosetta.TextForId("astroconfigwindow.tabbasicpoints");
        tabMathMinorPoints.Header = Rosetta.TextForId("astroconfigwindow.tabminorpoints");
        tabHypoPoints.Header = Rosetta.TextForId("astroconfigwindow.tabhypopoints");
        tabAspects.Header = Rosetta.TextForId("astroconfigwindow.tabaspects");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnOk.Content = Rosetta.TextForId("common.btnok");
        // Tab General
        tbHouseSystemExpl.Text = Rosetta.TextForId("astroconfigwindow.housesystemexpl");
        tbHouseSystem.Text = Rosetta.TextForId("astroconfigwindow.housesystem");
        tbZodiacTypeExpl.Text = Rosetta.TextForId("astroconfigwindow.zodiactypeexpl");
        tbZodiacType.Text = Rosetta.TextForId("astroconfigwindow.zodiactype");
        tbAyanamshaExpl.Text = Rosetta.TextForId("astroconfigwindow.ayanamshaexpl");
        tbAyanamsha.Text = Rosetta.TextForId("astroconfigwindow.ayanamsha");
        tbObserverPosExpl.Text = Rosetta.TextForId("astroconfigwindow.observerposexpl");
        tbObserverPos.Text = Rosetta.TextForId("astroconfigwindow.observerpos");
        tbProjectionTypeExpl.Text = Rosetta.TextForId("astroconfigwindow.projectiontypeexpl");
        tbProjectionType.Text = Rosetta.TextForId("astroconfigwindow.projectiontype");
        tbBaseOrbExpl.Text = Rosetta.TextForId("astroconfigwindow.baseorbexpl");
        tbAspectBaseOrb.Text = Rosetta.TextForId("astroconfigwindow.baseorbaspect");
        tbMidpointBaseOrb.Text = Rosetta.TextForId("astroconfigwindow.baseorbmidpoint");

        // Tab Basic celestial points
        TitleBasicCelPoints.Text = Rosetta.TextForId("astroconfigwindow.basiccelpoints");
        tbArabicParts.Text = Rosetta.TextForId("astroconfigwindow.arabicparts");
        tbBasicExplanation.Text = Rosetta.TextForId("astroconfigwindow.basiccelpointsexpl");
        tbClassical.Text = Rosetta.TextForId("astroconfigwindow.classicalpoints");
        tbModern.Text = Rosetta.TextForId("astroconfigwindow.modernpoints");
        tbMundanePoints.Text = Rosetta.TextForId("astroconfigwindow.mundanepoints");
        tbBasicPointLeft.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbBasicPointRight.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbBasicOrbFactorLeft.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbBasicOrbFactorRight.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextSun.Text = Rosetta.TextForId("ref.enum.celpoint.sun");
        tbTextMoon.Text = Rosetta.TextForId("ref.enum.celpoint.moon");
        tbTextMercury.Text = Rosetta.TextForId("ref.enum.celpoint.mercury");
        tbTextVenus.Text = Rosetta.TextForId("ref.enum.celpoint.venus");
        tbTextMars.Text = Rosetta.TextForId("ref.enum.celpoint.mars");
        tbTextJupiter.Text = Rosetta.TextForId("ref.enum.celpoint.jupiter");
        tbTextSaturn.Text = Rosetta.TextForId("ref.enum.celpoint.saturn");
        tbTextUranus.Text = Rosetta.TextForId("ref.enum.celpoint.uranus");
        tbTextNeptune.Text = Rosetta.TextForId("ref.enum.celpoint.neptune");
        tbTextPluto.Text = Rosetta.TextForId("ref.enum.celpoint.pluto");
        tbTextMc.Text = Rosetta.TextForId("ref.enum.mundanepoint.id.mc");
        tbTextAsc.Text = Rosetta.TextForId("ref.enum.mundanepoint.id.asc");
        tbTextVertex.Text = Rosetta.TextForId("ref.enum.mundanepoint.id.vertex");
        tbTextEastpoint.Text = Rosetta.TextForId("ref.enum.mundanepoint.id.eastpoint");
        tbTextParsSect.Text = Rosetta.TextForId("ref.enum.arabicpoint.fortunasect");
        tbTextParsNoSect.Text = Rosetta.TextForId("ref.enum.arabicpoint.fortunanosect");
        // Tab Math and Minor points
        TitleMathMinorPoints.Text = Rosetta.TextForId("astroconfigwindow.mathminorcelpoints");
        tbMathMinorExplanation.Text = Rosetta.TextForId("astroconfigwindow.mathminorcelpointsexpl");
        tbMathematical.Text = Rosetta.TextForId("astroconfigwindow.mathematical");
        tbCentaurs.Text = Rosetta.TextForId("astroconfigwindow.centaurs");
        tbPlanetoids.Text = Rosetta.TextForId("astroconfigwindow.planetoids");
        tbPlutoids.Text = Rosetta.TextForId("astroconfigwindow.plutoids");
        tbMathMinorPointLeft.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbMathMinorPointRight.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbMathMinorOrbFactorLeft.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbMathMinorOrbFactorRight.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextMeanNode.Text = Rosetta.TextForId("ref.enum.celpoint.meannode");
        tbTextTrueNode.Text = Rosetta.TextForId("ref.enum.celpoint.truenode");
        tbTextZeroAries.Text = Rosetta.TextForId("ref.enum.zodiacpoints.id.zeroar");
        tbTextMeanBlackMoon.Text = Rosetta.TextForId("ref.enum.celpoint.meanblackmoon");
        tbTextCorrBlackMoon.Text = Rosetta.TextForId("ref.enum.celpoint.corrblackmoon");
        tbTextInterpolatedBlackMoon.Text = Rosetta.TextForId("ref.enum.celpoint.interpblackmoon");
        tbTextDuvalBlackMoon.Text = Rosetta.TextForId("ref.enum.celpoint.duvalblackmoon");

        tbTextHuya.Text = Rosetta.TextForId("ref.enum.celpoint.huya");
        tbTextVaruna.Text = Rosetta.TextForId("ref.enum.celpoint.varuna");
        tbTextIxion.Text = Rosetta.TextForId("ref.enum.celpoint.ixion");
        tbTextQuaoar.Text = Rosetta.TextForId("ref.enum.celpoint.quaoar");
        tbTextHaumea.Text = Rosetta.TextForId("ref.enum.celpoint.haumea");
        tbTextEris.Text = Rosetta.TextForId("ref.enum.celpoint.eris");
        tbTextSedna.Text = Rosetta.TextForId("ref.enum.celpoint.sedna");
        tbTextOrcus.Text = Rosetta.TextForId("ref.enum.celpoint.orcus");
        tbTextMakemake.Text = Rosetta.TextForId("ref.enum.celpoint.makemake");

        tbTextChiron.Text = Rosetta.TextForId("ref.enum.celpoint.chiron");
        tbTextNessus.Text = Rosetta.TextForId("ref.enum.celpoint.nessus");
        tbTextPholus.Text = Rosetta.TextForId("ref.enum.celpoint.pholus");

        tbTextCeres.Text = Rosetta.TextForId("ref.enum.celpoint.ceres");
        tbTextPallas.Text = Rosetta.TextForId("ref.enum.celpoint.pallas");
        tbTextJuno.Text = Rosetta.TextForId("ref.enum.celpoint.juno");
        tbTextVesta.Text = Rosetta.TextForId("ref.enum.celpoint.vesta");
        tbTextHygieia.Text = Rosetta.TextForId("ref.enum.celpoint.hygieia");
        tbTextAstraea.Text = Rosetta.TextForId("ref.enum.celpoint.astraea");

        // Tab hypothetical points
        TitleHypotheticalPoints.Text = Rosetta.TextForId("astroconfigwindow.hypotheticalpoints");
        tbHypotheticalExplanation.Text = Rosetta.TextForId("astroconfigwindow.hypotheticalpointsexpl");

        tbUranianWitte.Text = Rosetta.TextForId("astroconfigwindow.uranian_witte");
        tbUranianSieggrun.Text = Rosetta.TextForId("astroconfigwindow.uranian_sieggrun");
        tbSchoolRam.Text = Rosetta.TextForId("astroconfigwindow.schoolofram");
        tbCarteret.Text = Rosetta.TextForId("astroconfigwindow.carteret");
        tbHypotOthers.Text = Rosetta.TextForId("astroconfigwindow.hypotothers");

        tbUranianPointLeft.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbUranianPointRight.Text = Rosetta.TextForId("astroconfigwindow.celpoint");
        tbUranianFactorLeft.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbUranianFactorRight.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");

        tbTextCupido.Text = Rosetta.TextForId("ref.enum.celpoint.cupido_ura");
        tbTextHades.Text = Rosetta.TextForId("ref.enum.celpoint.hades_ura");
        tbTextZeus.Text = Rosetta.TextForId("ref.enum.celpoint.zeus_ura");
        tbTextKronos.Text = Rosetta.TextForId("ref.enum.celpoint.kronos_ura");
        tbTextApollon.Text = Rosetta.TextForId("ref.enum.celpoint.apollon_ura");
        tbTextAdmetos.Text = Rosetta.TextForId("ref.enum.celpoint.admetos_ura");
        tbTextVulkanusUra.Text = Rosetta.TextForId("ref.enum.celpoint.vulcanus_ura");
        tbTextPoseidon.Text = Rosetta.TextForId("ref.enum.celpoint.poseidon_ura");
        tbTextPersephoneRam.Text = Rosetta.TextForId("ref.enum.celpoint.persephone_ram");
        tbTextHermes.Text = Rosetta.TextForId("ref.enum.celpoint.hermes_ram");
        tbTextDemeter.Text = Rosetta.TextForId("ref.enum.celpoint.demeter_ram");
        tbTextVulcanusCarteret.Text = Rosetta.TextForId("ref.enum.celpoint.vulcanus_carteret");
        tbTextPersephoneCarteret.Text = Rosetta.TextForId("ref.enum.celpoint.persephone_carteret");
        tbTextTransPluto.Text = Rosetta.TextForId("ref.enum.celpoint.transpluto");

        // Tab aspects
        TitleAspects.Text = Rosetta.TextForId("astroconfigwindow.aspects");
        tbAspectsExplanation.Text = Rosetta.TextForId("astroconfigwindow.aspectsexpl");
        tbOrbMethod.Text = Rosetta.TextForId("astroconfigwindow.orbmethod");
        tbOrbMethodExpl.Text = Rosetta.TextForId("astroconfigwindow.orbmethodexpl");

        tbIncludeCusps.Text = Rosetta.TextForId("astroconfigwindow.includecusps");

        tbMajorAspects.Text = Rosetta.TextForId("astroconfigwindow.majoraspects");
        tbMinorAspects.Text = Rosetta.TextForId("astroconfigwindow.minoraspects");
        tbMicroAspects.Text = Rosetta.TextForId("astroconfigwindow.microaspects");
        tbAspectLabelLeft.Text = Rosetta.TextForId("astroconfigwindow.aspect");
        tbAspectLabelRight.Text = Rosetta.TextForId("astroconfigwindow.aspect");
        tbOrbFactorLeft.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbOrbFactorRight.Text = Rosetta.TextForId("astroconfigwindow.orbfactor");
        tbTextConjunction.Text = Rosetta.TextForId("ref.enum.aspect.conjunction");
        tbTextOpposition.Text = Rosetta.TextForId("ref.enum.aspect.opposition");
        tbTextTriangle.Text = Rosetta.TextForId("ref.enum.aspect.triangle");
        tbTextSquare.Text = Rosetta.TextForId("ref.enum.aspect.square");
        tbTextSextile.Text = Rosetta.TextForId("ref.enum.aspect.sextile");
        tbTextSemiSextile.Text = Rosetta.TextForId("ref.enum.aspect.semisextile");
        tbTextInconjunct.Text = Rosetta.TextForId("ref.enum.aspect.inconjunct");
        tbTextSemiSquare.Text = Rosetta.TextForId("ref.enum.aspect.semisquare");
        tbTextSesquiquadrate.Text = Rosetta.TextForId("ref.enum.aspect.sesquiquadrate");
        tbTextQuintile.Text = Rosetta.TextForId("ref.enum.aspect.quintile");
        tbTextBiQuintile.Text = Rosetta.TextForId("ref.enum.aspect.biquintile");
        tbTextSeptile.Text = Rosetta.TextForId("ref.enum.aspect.septile");
        tbTextVigintile.Text = Rosetta.TextForId("ref.enum.aspect.vigintile");
        tbTextUnDecile.Text = Rosetta.TextForId("ref.enum.aspect.undecile");
        tbTextSemiQuintile.Text = Rosetta.TextForId("ref.enum.aspect.semiquintile");
        tbTextNovile.Text = Rosetta.TextForId("ref.enum.aspect.novile");
        tbTextBiNovile.Text = Rosetta.TextForId("ref.enum.aspect.binovile");
        tbTextCentile.Text = Rosetta.TextForId("ref.enum.aspect.centile");
        tbTextBiSeptile.Text = Rosetta.TextForId("ref.enum.aspect.biseptile");
        tbTextTriDecile.Text = Rosetta.TextForId("ref.enum.aspect.tridecile");
        tbTextTriSeptile.Text = Rosetta.TextForId("ref.enum.aspect.triseptile");
        tbTextQuadraNovile.Text = Rosetta.TextForId("ref.enum.aspect.quadranovile");
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
            comboHouseSystem.Items.Add(Rosetta.TextForId(detail.TextId));
        }
        comboHouseSystem.SelectedIndex = (int)_controller.GetConfig().HouseSystem;

        comboZodiacType.Items.Clear();
        foreach (ZodiacTypeDetails details in ZodiacTypes.Tropical.AllDetails())
        {
            comboZodiacType.Items.Add(Rosetta.TextForId(details.TextId));
        }
        comboZodiacType.SelectedIndex = (int)_controller.GetConfig().ZodiacType;

        comboAyanamsha.Items.Clear();
        foreach (AyanamshaDetails detail in Ayanamshas.None.AllDetails())
        {
            comboAyanamsha.Items.Add(Rosetta.TextForId(detail.TextId));
        }
        comboAyanamsha.SelectedIndex = (int)_controller.GetConfig().Ayanamsha;

        comboObserverPos.Items.Clear();
        foreach (ObserverPositionDetails detail in ObserverPositions.GeoCentric.AllDetails())
        {
            comboObserverPos.Items.Add(Rosetta.TextForId(detail.TextId));
        }
        comboObserverPos.SelectedIndex = (int)_controller.GetConfig().ObserverPosition;

        comboProjectionType.Items.Clear();
        foreach (ProjectionTypeDetails detail in ProjectionTypes.TwoDimensional.AllDetails())
        {
            comboProjectionType.Items.Add(Rosetta.TextForId(detail.TextId));
        }
        comboProjectionType.SelectedIndex = (int)_controller.GetConfig().ProjectionType;

        tboxAspectBaseOrb.Text = _controller.GetConfig().BaseOrbAspects.ToString();
        tboxMidpointAspectBaseOrb.Text = _controller.GetConfig().BaseOrbMidpoints.ToString();


        // chart points
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartConfigPoints = _controller.GetConfig().ChartPoints;
        tboxSunFactor.Text = chartConfigPoints[ChartPoints.Sun].PercentageOrb.ToString();
        tboxMoonFactor.Text = chartConfigPoints[ChartPoints.Moon].PercentageOrb.ToString();
        tboxMercuryFactor.Text = chartConfigPoints[ChartPoints.Mercury].PercentageOrb.ToString();
        tboxVenusFactor.Text = chartConfigPoints[ChartPoints.Venus].PercentageOrb.ToString();
        tboxMarsFactor.Text = chartConfigPoints[ChartPoints.Mars].PercentageOrb.ToString();
        tboxJupiterFactor.Text = chartConfigPoints[ChartPoints.Jupiter].PercentageOrb.ToString();
        tboxSaturnFactor.Text = chartConfigPoints[ChartPoints.Saturn].PercentageOrb.ToString();
        tboxUranusFactor.Text = chartConfigPoints[ChartPoints.Uranus].PercentageOrb.ToString();
        cboxUranus.IsChecked = chartConfigPoints[ChartPoints.Uranus].IsUsed;
        tboxNeptuneFactor.Text = chartConfigPoints[ChartPoints.Neptune].PercentageOrb.ToString();
        cboxNeptune.IsChecked = chartConfigPoints[ChartPoints.Neptune].IsUsed;
        tboxPlutoFactor.Text = chartConfigPoints[ChartPoints.Pluto].PercentageOrb.ToString();
        cboxPluto.IsChecked = chartConfigPoints[ChartPoints.Pluto].IsUsed;
        tboxMeanNodeFactor.Text = chartConfigPoints[ChartPoints.MeanNode].PercentageOrb.ToString();
        cboxMeanNode.IsChecked = chartConfigPoints[ChartPoints.MeanNode].IsUsed;
        tboxTrueNodeFactor.Text = chartConfigPoints[ChartPoints.TrueNode].PercentageOrb.ToString();
        cboxTrueNode.IsChecked = chartConfigPoints[ChartPoints.TrueNode].IsUsed;
        tboxChironFactor.Text = chartConfigPoints[ChartPoints.Chiron].PercentageOrb.ToString();
        cboxChiron.IsChecked = chartConfigPoints[ChartPoints.Chiron].IsUsed;
        tboxPersephoneRamFactor.Text = chartConfigPoints[ChartPoints.PersephoneRam].PercentageOrb.ToString();
        cboxPersephoneRam.IsChecked = chartConfigPoints[ChartPoints.PersephoneRam].IsUsed;
        tboxHermesFactor.Text = chartConfigPoints[ChartPoints.HermesRam].PercentageOrb.ToString();
        cboxHermes.IsChecked = chartConfigPoints[ChartPoints.HermesRam].IsUsed;
        tboxDemeterFactor.Text = chartConfigPoints[ChartPoints.DemeterRam].PercentageOrb.ToString();
        cboxDemeter.IsChecked = chartConfigPoints[ChartPoints.DemeterRam].IsUsed;
        tboxCupidoFactor.Text = chartConfigPoints[ChartPoints.CupidoUra].PercentageOrb.ToString();
        cboxCupido.IsChecked = chartConfigPoints[ChartPoints.CupidoUra].IsUsed;
        tboxHadesFactor.Text = chartConfigPoints[ChartPoints.HadesUra].PercentageOrb.ToString();
        cboxHades.IsChecked = chartConfigPoints[ChartPoints.HadesUra].IsUsed;
        tboxZeusFactor.Text = chartConfigPoints[ChartPoints.ZeusUra].PercentageOrb.ToString();
        cboxZeus.IsChecked = chartConfigPoints[ChartPoints.ZeusUra].IsUsed;
        tboxKronosFactor.Text = chartConfigPoints[ChartPoints.KronosUra].PercentageOrb.ToString();
        cboxKronos.IsChecked = chartConfigPoints[ChartPoints.KronosUra].IsUsed;
        tboxApollonFactor.Text = chartConfigPoints[ChartPoints.ApollonUra].PercentageOrb.ToString();
        cboxApollon.IsChecked = chartConfigPoints[ChartPoints.ApollonUra].IsUsed;
        tboxAdmetosFactor.Text = chartConfigPoints[ChartPoints.AdmetosUra].PercentageOrb.ToString();
        cboxAdmetos.IsChecked = chartConfigPoints[ChartPoints.AdmetosUra].IsUsed;
        tboxVulkanusUraFactor.Text = chartConfigPoints[ChartPoints.VulcanusUra].PercentageOrb.ToString();
        cboxVulkanusUra.IsChecked = chartConfigPoints[ChartPoints.VulcanusUra].IsUsed;
        tboxPoseidonFactor.Text = chartConfigPoints[ChartPoints.PoseidonUra].PercentageOrb.ToString();
        cboxPoseidon.IsChecked = chartConfigPoints[ChartPoints.PoseidonUra].IsUsed;
        tboxErisFactor.Text = chartConfigPoints[ChartPoints.Eris].PercentageOrb.ToString();
        cboxEris.IsChecked = chartConfigPoints[ChartPoints.Eris].IsUsed;
        tboxPholusFactor.Text = chartConfigPoints[ChartPoints.Pholus].PercentageOrb.ToString();
        cboxPholus.IsChecked = chartConfigPoints[ChartPoints.Pholus].IsUsed;
        tboxCeresFactor.Text = chartConfigPoints[ChartPoints.Ceres].PercentageOrb.ToString();
        cboxCeres.IsChecked = chartConfigPoints[ChartPoints.Ceres].IsUsed;
        tboxPallasFactor.Text = chartConfigPoints[ChartPoints.Pallas].PercentageOrb.ToString();
        cboxPallas.IsChecked = chartConfigPoints[ChartPoints.Pallas].IsUsed;
        tboxJunoFactor.Text = chartConfigPoints[ChartPoints.Juno].PercentageOrb.ToString();
        cboxJuno.IsChecked = chartConfigPoints[ChartPoints.Juno].IsUsed;
        tboxVestaFactor.Text = chartConfigPoints[ChartPoints.Vesta].PercentageOrb.ToString();
        cboxVesta.IsChecked = chartConfigPoints[ChartPoints.Vesta].IsUsed;
        tboxTransPlutoFactor.Text = chartConfigPoints[ChartPoints.Isis].PercentageOrb.ToString();
        cboxTransPluto.IsChecked = chartConfigPoints[ChartPoints.Isis].IsUsed;
        tboxNessusFactor.Text = chartConfigPoints[ChartPoints.Nessus].PercentageOrb.ToString();
        cboxNessus.IsChecked = chartConfigPoints[ChartPoints.Nessus].IsUsed;
        tboxHuyaFactor.Text = chartConfigPoints[ChartPoints.Huya].PercentageOrb.ToString();
        cboxHuya.IsChecked = chartConfigPoints[ChartPoints.Huya].IsUsed;
        tboxVarunaFactor.Text = chartConfigPoints[ChartPoints.Varuna].PercentageOrb.ToString();
        cboxVaruna.IsChecked = chartConfigPoints[ChartPoints.Varuna].IsUsed;
        tboxIxionFactor.Text = chartConfigPoints[ChartPoints.Ixion].PercentageOrb.ToString();
        cboxIxion.IsChecked = chartConfigPoints[ChartPoints.Ixion].IsUsed;
        tboxQuaoarFactor.Text = chartConfigPoints[ChartPoints.Quaoar].PercentageOrb.ToString();
        cboxQuaoar.IsChecked = chartConfigPoints[ChartPoints.Quaoar].IsUsed;
        tboxHaumeaFactor.Text = chartConfigPoints[ChartPoints.Haumea].PercentageOrb.ToString();
        cboxHaumea.IsChecked = chartConfigPoints[ChartPoints.Haumea].IsUsed;
        tboxOrcusFactor.Text = chartConfigPoints[ChartPoints.Orcus].PercentageOrb.ToString();
        cboxOrcus.IsChecked = chartConfigPoints[ChartPoints.Orcus].IsUsed;
        tboxMakemakeFactor.Text = chartConfigPoints[ChartPoints.Makemake].PercentageOrb.ToString();
        cboxMakemake.IsChecked = chartConfigPoints[ChartPoints.Makemake].IsUsed;
        tboxSednaFactor.Text = chartConfigPoints[ChartPoints.Sedna].PercentageOrb.ToString();
        cboxSedna.IsChecked = chartConfigPoints[ChartPoints.Sedna].IsUsed;
        tboxHygieiaFactor.Text = chartConfigPoints[ChartPoints.Hygieia].PercentageOrb.ToString();
        cboxHygieia.IsChecked = chartConfigPoints[ChartPoints.Hygieia].IsUsed;
        tboxAstraeaFactor.Text = chartConfigPoints[ChartPoints.Astraea].PercentageOrb.ToString();
        cboxAstraea.IsChecked = chartConfigPoints[ChartPoints.Astraea].IsUsed;
        tboxMeanBlackMoonFactor.Text = chartConfigPoints[ChartPoints.ApogeeMean].PercentageOrb.ToString();
        cboxMeanBlackMoon.IsChecked = chartConfigPoints[ChartPoints.ApogeeMean].IsUsed;
        tboxCorrBlackMoonFactor.Text = chartConfigPoints[ChartPoints.ApogeeCorrected].PercentageOrb.ToString();
        cboxCorrBlackMoon.IsChecked = chartConfigPoints[ChartPoints.ApogeeCorrected].IsUsed;
        tboxInterpolatedBlackMoonFactor.Text = chartConfigPoints[ChartPoints.ApogeeInterpolated].PercentageOrb.ToString();
        cboxInterpolatedBlackMoon.IsChecked = chartConfigPoints[ChartPoints.ApogeeInterpolated].IsUsed;
        tboxDuvalBlackMoonFactor.Text = chartConfigPoints[ChartPoints.ApogeeDuval].PercentageOrb.ToString();
        cboxDuvalBlackMoon.IsChecked = false;
        cboxDuvalBlackMoon.IsEnabled = false;
        tboxPersephoneCarteretFactor.Text = chartConfigPoints[ChartPoints.PersephoneCarteret].PercentageOrb.ToString();
        cboxPersephoneCarteret.IsChecked = false;
        cboxPersephoneCarteret.IsEnabled = false;
        tboxVulcanusCarteretFactor.Text = chartConfigPoints[ChartPoints.VulcanusCarteret].PercentageOrb.ToString();
        cboxVulcanusCarteret.IsChecked = false;
        cboxVulcanusCarteret.IsEnabled = false;
        tboxAscFactor.Text = chartConfigPoints[ChartPoints.Ascendant].PercentageOrb.ToString();
        cboxAsc.IsChecked = true;
        tboxMcFactor.Text = chartConfigPoints[ChartPoints.Mc].PercentageOrb.ToString();
        cboxMc.IsChecked = true;
        tboxEastpointFactor.Text = chartConfigPoints[ChartPoints.EastPoint].PercentageOrb.ToString();
        cboxEastpoint.IsChecked = chartConfigPoints[ChartPoints.EastPoint].IsUsed;
        tboxVertexFactor.Text = chartConfigPoints[ChartPoints.Vertex].PercentageOrb.ToString();
        cboxVertex.IsChecked = chartConfigPoints[ChartPoints.Vertex].IsUsed;
        tboxZeroAriesFactor.Text = chartConfigPoints[ChartPoints.ZeroAries].PercentageOrb.ToString();
        cboxZeroAries.IsChecked = chartConfigPoints[ChartPoints.ZeroAries].IsUsed;
        tboxParsSectFactor.Text = chartConfigPoints[ChartPoints.FortunaSect].PercentageOrb.ToString();
        cboxParsSect.IsChecked = chartConfigPoints[ChartPoints.FortunaSect].IsUsed;
        tboxParsNoSectFactor.Text = chartConfigPoints[ChartPoints.FortunaNoSect].PercentageOrb.ToString();
        cboxParsNoSect.IsChecked = chartConfigPoints[ChartPoints.FortunaNoSect].IsUsed;


        // aspects
        Dictionary<AspectTypes, AspectConfigSpecs> aspects = _controller.GetConfig().Aspects;
        comboOrbMethod.Items.Clear();
        foreach (OrbMethodDetails detail in OrbMethods.Weighted.AllDetails())
        {
            comboOrbMethod.Items.Add(Rosetta.TextForId(detail.TextId));
        }
        comboOrbMethod.SelectedIndex = (int)_controller.GetConfig().OrbMethod;
        cboxIncludeCusps.IsChecked = _controller.GetConfig().UseCuspsForAspects;
        tboxConjunctionFactor.Text = aspects[AspectTypes.Conjunction].PercentageOrb.ToString();
        cboxConjunction.IsChecked = aspects[AspectTypes.Conjunction].IsUsed;
        tboxOppositionFactor.Text = aspects[AspectTypes.Opposition].PercentageOrb.ToString();
        cboxOpposition.IsChecked = aspects[AspectTypes.Opposition].IsUsed;
        tboxTriangleFactor.Text = aspects[AspectTypes.Triangle].PercentageOrb.ToString();
        cboxTriangle.IsChecked = aspects[AspectTypes.Triangle].IsUsed;
        tboxSquareFactor.Text = aspects[AspectTypes.Square].PercentageOrb.ToString();
        cboxSquare.IsChecked = aspects[AspectTypes.Square].IsUsed;
        tboxSeptileFactor.Text = aspects[AspectTypes.Septile].PercentageOrb.ToString();
        cboxSeptile.IsChecked = aspects[AspectTypes.Septile].IsUsed;
        tboxSextileFactor.Text = aspects[AspectTypes.Sextile].PercentageOrb.ToString();
        cboxSextile.IsChecked = aspects[AspectTypes.Sextile].IsUsed;
        tboxQuintileFactor.Text = aspects[AspectTypes.Quintile].PercentageOrb.ToString();
        cboxQuintile.IsChecked = aspects[AspectTypes.Quintile].IsUsed;
        tboxSemiSextileFactor.Text = aspects[AspectTypes.SemiSextile].PercentageOrb.ToString();
        cboxSemiSextile.IsChecked = aspects[AspectTypes.SemiSextile].IsUsed;
        tboxSemiSquareFactor.Text = aspects[AspectTypes.SemiSquare].PercentageOrb.ToString();
        cboxSemiSquare.IsChecked = aspects[AspectTypes.SemiSquare].IsUsed;
        tboxSemiQuintileFactor.Text = aspects[AspectTypes.SemiQuintile].PercentageOrb.ToString();
        cboxSemiQuintile.IsChecked = aspects[AspectTypes.SemiQuintile].IsUsed;
        tboxBiQuintileFactor.Text = aspects[AspectTypes.BiQuintile].PercentageOrb.ToString();
        cboxBiQuintile.IsChecked = aspects[AspectTypes.BiQuintile].IsUsed;
        tboxInconjunctFactor.Text = aspects[AspectTypes.Inconjunct].PercentageOrb.ToString();
        cboxInconjunct.IsChecked = aspects[AspectTypes.Inconjunct].IsUsed;
        tboxSesquiquadrateFactor.Text = aspects[AspectTypes.SesquiQuadrate].PercentageOrb.ToString();
        cboxSesquiquadrate.IsChecked = aspects[AspectTypes.SesquiQuadrate].IsUsed;
        tboxTriDecileFactor.Text = aspects[AspectTypes.TriDecile].PercentageOrb.ToString();
        cboxTriDecile.IsChecked = aspects[AspectTypes.TriDecile].IsUsed;
        tboxBiSeptileFactor.Text = aspects[AspectTypes.BiSeptile].PercentageOrb.ToString();
        cboxBiSeptile.IsChecked = aspects[AspectTypes.BiSeptile].IsUsed;
        tboxTriSeptileFactor.Text = aspects[AspectTypes.TriSeptile].PercentageOrb.ToString();
        cboxTriSeptile.IsChecked = aspects[AspectTypes.TriSeptile].IsUsed;
        tboxNovileFactor.Text = aspects[AspectTypes.Novile].PercentageOrb.ToString();
        cboxNovile.IsChecked = aspects[AspectTypes.Novile].IsUsed;
        tboxBiNovileFactor.Text = aspects[AspectTypes.BiNovile].PercentageOrb.ToString();
        cboxBiNovile.IsChecked = aspects[AspectTypes.BiNovile].IsUsed;
        tboxQuadraNovileFactor.Text = aspects[AspectTypes.QuadraNovile].PercentageOrb.ToString();
        cboxQuadraNovile.IsChecked = aspects[AspectTypes.QuadraNovile].IsUsed;
        tboxUnDecileFactor.Text = aspects[AspectTypes.Undecile].PercentageOrb.ToString();
        cboxUnDecile.IsChecked = aspects[AspectTypes.Undecile].IsUsed;
        tboxCentileFactor.Text = aspects[AspectTypes.Centile].PercentageOrb.ToString();
        cboxCentile.IsChecked = aspects[AspectTypes.Centile].IsUsed;
        tboxVigintileFactor.Text = aspects[AspectTypes.Vigintile].PercentageOrb.ToString();
        cboxVigintile.IsChecked = aspects[AspectTypes.Vigintile].IsUsed;
    }

    public void CancelClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    public void HelpClick(object sender, RoutedEventArgs e)
    {
        AstroConfigController.ShowHelp();
    }

    public void OkClick(object sender, RoutedEventArgs e)
    {
        if (HandleInput())
        {
            Close(); 

        }
        else
        {
            MessageBox.Show(Rosetta.TextForId("astroconfigwindow.errorsfound"));
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
        bool helioFlag = false;
        try
        {
            HouseSystems houseSystem = HouseSystems.NoHouses.HouseSystemForIndex(comboHouseSystem.SelectedIndex);
            ZodiacTypes zodiacType = ZodiacTypes.Tropical.ZodiacTypeForIndex(comboZodiacType.SelectedIndex);
            Ayanamshas ayanamsha = Ayanamshas.None.AyanamshaForIndex(comboAyanamsha.SelectedIndex);
            ObserverPositions observerPosition = ObserverPositions.GeoCentric.ObserverPositionForIndex(comboObserverPos.SelectedIndex);
            ProjectionTypes projectionType = ProjectionTypes.TwoDimensional.ProjectionTypeForIndex(comboProjectionType.SelectedIndex);
            OrbMethods orbMethod = OrbMethods.Weighted.OrbMethodForIndex(comboOrbMethod.SelectedIndex);

            if (observerPosition == ObserverPositions.HelioCentric) 
            {
                helioFlag = true;
                cboxSun.IsChecked = false;
                cboxMoon.IsChecked = false;
                cboxMeanNode.IsChecked = false;
                cboxTrueNode.IsChecked = false;
                cboxMeanBlackMoon.IsChecked = false;
                cboxInterpolatedBlackMoon.IsChecked = false;
                cboxDuvalBlackMoon.IsChecked = false;
                cboxCorrBlackMoon.IsChecked = false;
                cboxPersephoneCarteret.IsChecked = false;
                cboxVulcanusCarteret.IsChecked = false;
                cboxVertex.IsChecked = false;
                cboxEastpoint.IsChecked = false;
                cboxParsNoSect.IsChecked = false;
                cboxParsSect.IsChecked = false;
                cboxZeroAries.IsChecked = false;
            }

            Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointSpecs = DefineChartPointSpecs();

            if (helioFlag)
            {
                chartPointSpecs.Add(ChartPoints.Earth, new ChartPointConfigSpecs(true, 'e', NumericFactor(tboxSunFactor.Text)));
            }


            double baseOrbAspects = Convert.ToDouble(tboxAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            double baseOrbMidpoints = Convert.ToDouble(tboxMidpointAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            Dictionary<AspectTypes, AspectConfigSpecs> aspectSpecs = DefineAspectSpecs();


            bool useCuspsForAspects = cboxIncludeCusps.IsChecked ?? false;               
            AstroConfig astroConfig = new(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod, chartPointSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
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



    private Dictionary<ChartPoints, ChartPointConfigSpecs> DefineChartPointSpecs()
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointSpecs = new()
        {
            { ChartPoints.Sun, new ChartPointConfigSpecs(cboxSun.IsChecked ?? false, 'a', NumericFactor(tboxSunFactor.Text)) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(cboxMoon.IsChecked ?? false, 'a', NumericFactor(tboxMoonFactor.Text)) },
            { ChartPoints.Mercury, new ChartPointConfigSpecs(cboxMercury.IsChecked ?? false, 'a', NumericFactor(tboxMercuryFactor.Text)) },
            { ChartPoints.Venus, new ChartPointConfigSpecs(cboxVenus.IsChecked ?? false, 'd', NumericFactor(tboxVenusFactor.Text))},    
            { ChartPoints.Mars, new ChartPointConfigSpecs(cboxMars.IsChecked ?? false, 'f', NumericFactor(tboxMarsFactor.Text))},
            { ChartPoints.Jupiter, new ChartPointConfigSpecs(cboxJupiter.IsChecked ?? false, 'g', NumericFactor(tboxJupiterFactor.Text))},
            { ChartPoints.Saturn, new ChartPointConfigSpecs(cboxSaturn.IsChecked ?? false, 'h', NumericFactor(tboxSaturnFactor.Text))},
            { ChartPoints.Uranus, new ChartPointConfigSpecs(cboxUranus.IsChecked ?? false, 'i', NumericFactor(tboxUranusFactor.Text))},
            { ChartPoints.Neptune, new ChartPointConfigSpecs(cboxNeptune.IsChecked ?? false, 'j', NumericFactor(tboxNeptuneFactor.Text))},
            { ChartPoints.Pluto, new ChartPointConfigSpecs(cboxPluto.IsChecked ?? false, 'k', NumericFactor(tboxPlutoFactor.Text))},
            { ChartPoints.MeanNode, new ChartPointConfigSpecs(cboxMeanNode.IsChecked ?? false, '{', NumericFactor(tboxMeanNodeFactor.Text))},
            { ChartPoints.TrueNode, new ChartPointConfigSpecs(cboxTrueNode.IsChecked ?? false, '}', NumericFactor(tboxTrueNodeFactor.Text))},
            { ChartPoints.Chiron, new ChartPointConfigSpecs(cboxChiron.IsChecked ?? false, 'w', NumericFactor(tboxChironFactor.Text))},
            { ChartPoints.PersephoneRam, new ChartPointConfigSpecs(cboxPersephoneRam.IsChecked ?? false, '/', NumericFactor(tboxPersephoneRamFactor.Text))},
            { ChartPoints.HermesRam, new ChartPointConfigSpecs(cboxHermes.IsChecked ?? false, '<', NumericFactor(tboxHermesFactor.Text))},
            { ChartPoints.DemeterRam, new ChartPointConfigSpecs(cboxDemeter.IsChecked ?? false, '>', NumericFactor(tboxDemeterFactor.Text))},
            { ChartPoints.CupidoUra, new ChartPointConfigSpecs(cboxCupido.IsChecked ?? false, 'y', NumericFactor(tboxCupidoFactor.Text))},
            { ChartPoints.HadesUra, new ChartPointConfigSpecs(cboxHades.IsChecked ?? false, 'z', NumericFactor(tboxHadesFactor.Text))},
            { ChartPoints.ZeusUra, new ChartPointConfigSpecs(cboxZeus.IsChecked ?? false, '!', NumericFactor(tboxZeusFactor.Text))},
            { ChartPoints.KronosUra, new ChartPointConfigSpecs(cboxKronos.IsChecked ?? false, '@', NumericFactor(tboxKronosFactor.Text))},
            { ChartPoints.ApollonUra, new ChartPointConfigSpecs(cboxApollon.IsChecked ?? false, '#', NumericFactor(tboxApollonFactor.Text))},
            { ChartPoints.AdmetosUra, new ChartPointConfigSpecs(cboxAdmetos.IsChecked ?? false, '$', NumericFactor(tboxAdmetosFactor.Text))},
            { ChartPoints.VulcanusUra, new ChartPointConfigSpecs(cboxVulkanusUra.IsChecked ?? false, '%', NumericFactor(tboxVulkanusUraFactor.Text))},
            { ChartPoints.PoseidonUra, new ChartPointConfigSpecs(cboxPoseidon.IsChecked ?? false, '&', NumericFactor(tboxPoseidonFactor.Text))},
            { ChartPoints.Eris, new ChartPointConfigSpecs(cboxEris.IsChecked ?? false, '*', NumericFactor(tboxErisFactor.Text))},
            { ChartPoints.Pholus, new ChartPointConfigSpecs(cboxPholus.IsChecked ?? false, ')', NumericFactor(tboxPholusFactor.Text))},
            { ChartPoints.Ceres, new ChartPointConfigSpecs(cboxCeres.IsChecked ?? false, '_', NumericFactor(tboxCeresFactor.Text))},
            { ChartPoints.Pallas, new ChartPointConfigSpecs(cboxPallas.IsChecked ?? false, 'û', NumericFactor(tboxPallasFactor.Text))},
            { ChartPoints.Juno, new ChartPointConfigSpecs(cboxJuno.IsChecked ?? false, 'ü', NumericFactor(tboxJunoFactor.Text))},
            { ChartPoints.Vesta, new ChartPointConfigSpecs(cboxVesta.IsChecked ?? false, 'À', NumericFactor(tboxVestaFactor.Text))},
            { ChartPoints.Isis, new ChartPointConfigSpecs(cboxTransPluto.IsChecked ?? false, 'â', NumericFactor(tboxTransPlutoFactor.Text))},
            { ChartPoints.Nessus, new ChartPointConfigSpecs(cboxNessus.IsChecked ?? false, '(', NumericFactor(tboxNessusFactor.Text))},
            { ChartPoints.Huya, new ChartPointConfigSpecs(cboxHuya.IsChecked ?? false, 'ï', NumericFactor(tboxHuyaFactor.Text))},
            { ChartPoints.Varuna, new ChartPointConfigSpecs(cboxVaruna.IsChecked ?? false, 'ò', NumericFactor(tboxVarunaFactor.Text))},
            { ChartPoints.Ixion, new ChartPointConfigSpecs(cboxIxion.IsChecked ?? false, 'ó', NumericFactor(tboxIxionFactor.Text))},
            { ChartPoints.Quaoar, new ChartPointConfigSpecs(cboxQuaoar.IsChecked ?? false, 'ô', NumericFactor(tboxQuaoarFactor.Text))},
            { ChartPoints.Haumea, new ChartPointConfigSpecs(cboxHaumea.IsChecked ?? false, 'í', NumericFactor(tboxHaumeaFactor.Text))},
            { ChartPoints.Orcus, new ChartPointConfigSpecs(cboxOrcus.IsChecked ?? false, 'ù', NumericFactor(tboxOrcusFactor.Text))},
            { ChartPoints.Makemake, new ChartPointConfigSpecs(cboxMakemake.IsChecked ?? false, 'î', NumericFactor(tboxMakemakeFactor.Text))},
            { ChartPoints.Sedna, new ChartPointConfigSpecs(cboxSedna.IsChecked ?? false, 'ö', NumericFactor(tboxSednaFactor.Text))},
            { ChartPoints.Hygieia, new ChartPointConfigSpecs(cboxHygieia.IsChecked ?? false, 'Á', NumericFactor(tboxHygieiaFactor.Text))},
            { ChartPoints.Astraea, new ChartPointConfigSpecs(cboxAstraea.IsChecked ?? false, 'Ã', NumericFactor(tboxAstraeaFactor.Text))},
            { ChartPoints.ApogeeMean, new ChartPointConfigSpecs(cboxMeanBlackMoon.IsChecked ?? false, ',', NumericFactor(tboxMeanBlackMoonFactor.Text))},
            { ChartPoints.ApogeeCorrected, new ChartPointConfigSpecs(cboxCorrBlackMoon.IsChecked ?? false, '.', NumericFactor(tboxCorrBlackMoonFactor.Text))},
            { ChartPoints.ApogeeInterpolated, new ChartPointConfigSpecs(cboxInterpolatedBlackMoon.IsChecked ?? false, '.', NumericFactor(tboxInterpolatedBlackMoonFactor.Text))},
            { ChartPoints.ApogeeDuval, new ChartPointConfigSpecs(cboxDuvalBlackMoon.IsChecked ?? false, '.', NumericFactor(tboxDuvalBlackMoonFactor.Text))},
            { ChartPoints.PersephoneCarteret, new ChartPointConfigSpecs(cboxPersephoneCarteret.IsChecked ?? false, 'à', NumericFactor(tboxPersephoneCarteretFactor.Text))},
            { ChartPoints.VulcanusCarteret, new ChartPointConfigSpecs(cboxVulcanusCarteret.IsChecked ?? false, 'Ï', NumericFactor(tboxVulcanusCarteretFactor.Text))},
            { ChartPoints.Mc, new ChartPointConfigSpecs(cboxMc.IsChecked ?? false, 'M', NumericFactor(tboxMcFactor.Text))},
            { ChartPoints.Ascendant, new ChartPointConfigSpecs(cboxAsc.IsChecked ?? false, 'A', NumericFactor(tboxAscFactor.Text))},
            { ChartPoints.Vertex, new ChartPointConfigSpecs(cboxVertex.IsChecked ?? false, ' ', NumericFactor(tboxVertexFactor.Text))},
            { ChartPoints.EastPoint, new ChartPointConfigSpecs(cboxEastpoint.IsChecked ?? false, ' ', NumericFactor(tboxEastpointFactor.Text))},
            { ChartPoints.FortunaNoSect, new ChartPointConfigSpecs(cboxParsNoSect.IsChecked ?? false, 'e', NumericFactor(tboxParsNoSectFactor.Text))},
            { ChartPoints.FortunaSect, new ChartPointConfigSpecs(cboxParsSect.IsChecked ?? false, 'e', NumericFactor(tboxParsSectFactor.Text))},
            { ChartPoints.ZeroAries, new ChartPointConfigSpecs(cboxZeroAries.IsChecked ?? false, '1', NumericFactor(tboxZeroAriesFactor.Text))}
        };
        return chartPointSpecs;
    }


    private static int NumericFactor(string valueTxt)
    {
        bool correct = int.TryParse(valueTxt, out int value);
        if (correct)
        {
            return value;
        }
        else
        {
            string errorTxt = "AstroConfigWindows.xaml.cs: NumericFactor, received wrong value :" + valueTxt;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
    }




    private Dictionary<AspectTypes, AspectConfigSpecs> DefineAspectSpecs()
    {
        Dictionary<AspectTypes, AspectConfigSpecs> aspectSpecs = new()
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(cboxConjunction.IsChecked ?? false,  'B', NumericFactor(tboxConjunctionFactor.Text)) },
            { AspectTypes.Opposition, new AspectConfigSpecs(cboxOpposition.IsChecked ?? false,  'C', NumericFactor(tboxOppositionFactor.Text)) },
            { AspectTypes.Triangle, new AspectConfigSpecs(cboxTriangle.IsChecked ?? false,  'D', NumericFactor(tboxTriangleFactor.Text)) },
            { AspectTypes.Square, new AspectConfigSpecs(cboxSquare.IsChecked ?? false,  'E', NumericFactor(tboxSquareFactor.Text)) },
            { AspectTypes.Septile, new AspectConfigSpecs(cboxSeptile.IsChecked ?? false, 'N', NumericFactor(tboxSeptileFactor.Text)) },
            { AspectTypes.Sextile, new AspectConfigSpecs(cboxSextile.IsChecked ?? false, 'F', NumericFactor(tboxSextileFactor.Text)) },
            { AspectTypes.Quintile, new AspectConfigSpecs(cboxQuintile.IsChecked ?? false, 'K', NumericFactor(tboxQuintileFactor.Text)) },
            { AspectTypes.SemiSextile, new AspectConfigSpecs(cboxSemiSextile.IsChecked ?? false, 'G', NumericFactor(tboxSemiSextileFactor.Text)) },
            { AspectTypes.SemiSquare, new AspectConfigSpecs(cboxSemiSquare.IsChecked ?? false, 'I', NumericFactor(tboxSemiSquareFactor.Text)) },
            { AspectTypes.SemiQuintile, new AspectConfigSpecs(cboxSemiQuintile.IsChecked ?? false, 'Ô', NumericFactor(tboxSemiQuintileFactor.Text)) },
            { AspectTypes.BiQuintile, new AspectConfigSpecs(cboxBiQuintile.IsChecked ?? false, 'L', NumericFactor(tboxBiQuintileFactor.Text)) },
            { AspectTypes.Inconjunct, new AspectConfigSpecs(cboxInconjunct.IsChecked ?? false, 'H', NumericFactor(tboxInconjunctFactor.Text)) },
            { AspectTypes.SesquiQuadrate, new AspectConfigSpecs(cboxSesquiquadrate.IsChecked ?? false, 'J', NumericFactor(tboxSesquiquadrateFactor.Text)) },
            { AspectTypes.TriDecile, new AspectConfigSpecs(cboxTriDecile.IsChecked ?? false, 'Õ', NumericFactor(tboxTriDecileFactor.Text)) },
            { AspectTypes.BiSeptile, new AspectConfigSpecs(cboxBiSeptile.IsChecked ?? false, 'Ú', NumericFactor(tboxBiSeptileFactor.Text)) },
            { AspectTypes.TriSeptile, new AspectConfigSpecs(cboxTriSeptile.IsChecked ?? false, 'Û', NumericFactor(tboxTriSeptileFactor.Text)) },
            { AspectTypes.Novile, new AspectConfigSpecs(cboxNovile.IsChecked ?? false, 'Ü', NumericFactor(tboxNovileFactor.Text)) },
            { AspectTypes.BiNovile, new AspectConfigSpecs(cboxBiNovile.IsChecked ?? false, 'Ñ', NumericFactor(tboxBiNovileFactor.Text)) },
            { AspectTypes.QuadraNovile, new AspectConfigSpecs(cboxQuadraNovile.IsChecked ?? false, '|', NumericFactor(tboxQuadraNovileFactor.Text)) },
            { AspectTypes.Undecile, new AspectConfigSpecs(cboxUnDecile.IsChecked ?? false, 'ç', NumericFactor(tboxUnDecileFactor.Text)) },
            { AspectTypes.Centile, new AspectConfigSpecs(cboxCentile.IsChecked ?? false, 'Ç', NumericFactor(tboxCentileFactor.Text)) },
            { AspectTypes.Vigintile, new AspectConfigSpecs(cboxVigintile.IsChecked ?? false, 'Ï', NumericFactor(tboxVigintileFactor.Text)) }
        };

        return aspectSpecs;
    }

}
