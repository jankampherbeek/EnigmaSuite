// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
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
        tbGlyphSun.Text = _controller.DefineGlyph(CelPoints.Sun);
        tbGlyphMoon.Text = _controller.DefineGlyph(CelPoints.Moon);
        tbGlyphMercury.Text = _controller.DefineGlyph(CelPoints.Mercury);
        tbGlyphVenus.Text = _controller.DefineGlyph(CelPoints.Venus);
        tbGlyphMars.Text = _controller.DefineGlyph(CelPoints.Mars);
        tbGlyphJupiter.Text = _controller.DefineGlyph(CelPoints.Jupiter);
        tbGlyphSaturn.Text = _controller.DefineGlyph(CelPoints.Saturn);
        tbGlyphUranus.Text = _controller.DefineGlyph(CelPoints.Uranus);
        tbGlyphNeptune.Text = _controller.DefineGlyph(CelPoints.Neptune);
        tbGlyphPluto.Text = _controller.DefineGlyph(CelPoints.Pluto);
        tbGlyphMc.Text = _controller.DefineGlyph(MundanePoints.Mc);
        tbGlyphAsc.Text = _controller.DefineGlyph(MundanePoints.Ascendant);
        tbGlyphVertex.Text = _controller.DefineGlyph(MundanePoints.Vertex);
        tbGlyphEastpoint.Text = _controller.DefineGlyph(MundanePoints.EastPoint);

        tbGlyphParsSect.Text = _controller.DefineGlyph(ArabicPoints.FortunaSect);
        tbGlyphParsNoSect.Text = _controller.DefineGlyph(ArabicPoints.FortunaNoSect);

        tbGlyphMeanNode.Text = _controller.DefineGlyph(CelPoints.MeanNode);
        tbGlyphTrueNode.Text = _controller.DefineGlyph(CelPoints.TrueNode);
        tbGlyphMeanBLackMoon.Text = _controller.DefineGlyph(CelPoints.ApogeeMean);
        tbGlyphCorrBlackMoon.Text = _controller.DefineGlyph(CelPoints.ApogeeCorrected);
        tbGlyphInterpolatedBlackMoon.Text = _controller.DefineGlyph(CelPoints.ApogeeInterpolated);
 //       tbGlyphDuvalBlackMoon.Text = _controller.DefineGlyph(CelPoints.ApogeeDuval);
        tbGlyphZeroAries.Text = _controller.DefineGlyph(ZodiacPoints.ZeroAries);

        tbGlyphHuya.Text = _controller.DefineGlyph(CelPoints.Huya);
        tbGlyphVaruna.Text = _controller.DefineGlyph(CelPoints.Varuna);
        tbGlyphIxion.Text = _controller.DefineGlyph(CelPoints.Ixion);
        tbGlyphQuaoar.Text = _controller.DefineGlyph(CelPoints.Quaoar);
        tbGlyphHaumea.Text = _controller.DefineGlyph(CelPoints.Haumea);
        tbGlyphEris.Text = _controller.DefineGlyph(CelPoints.Eris);
        tbGlyphSedna.Text = _controller.DefineGlyph(CelPoints.Sedna);
        tbGlyphOrcus.Text = _controller.DefineGlyph(CelPoints.Orcus);
        tbGlyphMakemake.Text = _controller.DefineGlyph(CelPoints.Makemake);

        tbGlyphChiron.Text = _controller.DefineGlyph(CelPoints.Chiron);
        tbGlyphNessus.Text = _controller.DefineGlyph(CelPoints.Nessus);
        tbGlyphPholus.Text = _controller.DefineGlyph(CelPoints.Pholus);

        tbGlyphCeres.Text = _controller.DefineGlyph(CelPoints.Ceres);
        tbGlyphPallas.Text = _controller.DefineGlyph(CelPoints.Pallas);
        tbGlyphJuno.Text = _controller.DefineGlyph(CelPoints.Juno);
        tbGlyphVesta.Text = _controller.DefineGlyph(CelPoints.Vesta);
        tbGlyphHygieia.Text = _controller.DefineGlyph(CelPoints.Hygieia);
        tbGlyphAstraea.Text = _controller.DefineGlyph(CelPoints.Astraea);

        tbGlyphCupido.Text = _controller.DefineGlyph(CelPoints.CupidoUra);
        tbGlyphHades.Text = _controller.DefineGlyph(CelPoints.HadesUra);
        tbGlyphZeus.Text = _controller.DefineGlyph(CelPoints.ZeusUra);
        tbGlyphKronos.Text = _controller.DefineGlyph(CelPoints.KronosUra);
        tbGlyphApollon.Text = _controller.DefineGlyph(CelPoints.ApollonUra);
        tbGlyphAdmetos.Text = _controller.DefineGlyph(CelPoints.AdmetosUra);
        tbGlyphVulkanusUra.Text = _controller.DefineGlyph(CelPoints.VulcanusUra);
        tbGlyphPoseidon.Text = _controller.DefineGlyph(CelPoints.PoseidonUra);
        tbGlyphPersephoneRam.Text = _controller.DefineGlyph(CelPoints.PersephoneRam);
        tbGlyphHermes.Text = _controller.DefineGlyph(CelPoints.HermesRam);
        tbGlyphDemeter.Text = _controller.DefineGlyph(CelPoints.DemeterRam);
   //     tbGlyphVulcanusCarteret.Text = _controller.DefineGlyph(CelPoints.VulcanusCarteret);
   //     tbGlyphPersephoneCarteret.Text = _controller.DefineGlyph(CelPoints.PersephoneCarteret);
        tbGlyphTransPluto.Text = _controller.DefineGlyph(CelPoints.Isis);

        tbGlyphConjunction.Text = _controller.DefineGlyph(AspectTypes.Conjunction);
        tbGlyphOpposition.Text = _controller.DefineGlyph(AspectTypes.Opposition);
        tbGlyphTriangle.Text = _controller.DefineGlyph(AspectTypes.Triangle);
        tbGlyphSquare.Text = _controller.DefineGlyph(AspectTypes.Square);
        tbGlyphSextile.Text = _controller.DefineGlyph(AspectTypes.Sextile);
        tbGlyphSemiSextile.Text = _controller.DefineGlyph(AspectTypes.SemiSextile);
        tbGlyphInconjunct.Text = _controller.DefineGlyph(AspectTypes.Inconjunct);
        tbGlyphSemiSquare.Text = _controller.DefineGlyph(AspectTypes.SemiSquare);
        tbGlyphSesquiquadrate.Text = _controller.DefineGlyph(AspectTypes.SesquiQuadrate);
        tbGlyphQuintile.Text = _controller.DefineGlyph(AspectTypes.Quintile);
        tbGlyphBiQuintile.Text = _controller.DefineGlyph(AspectTypes.BiQuintile);
        tbGlyphSeptile.Text = _controller.DefineGlyph(AspectTypes.Septile);
        tbGlyphVigintile.Text = _controller.DefineGlyph(AspectTypes.Vigintile);
        tbGlyphUnDecile.Text = _controller.DefineGlyph(AspectTypes.Undecile);
        tbGlyphSemiQuintile.Text = _controller.DefineGlyph(AspectTypes.SemiQuintile);
        tbGlyphNovile.Text = _controller.DefineGlyph(AspectTypes.Novile);
        tbGlyphBiNovile.Text = _controller.DefineGlyph(AspectTypes.BiNovile);
        tbGlyphCentile.Text = _controller.DefineGlyph(AspectTypes.Centile);
        tbGlyphBiSeptile.Text = _controller.DefineGlyph(AspectTypes.BiSeptile);
        tbGlyphTriDecile.Text = _controller.DefineGlyph(AspectTypes.TriDecile);
        tbGlyphTriSeptile.Text = _controller.DefineGlyph(AspectTypes.TriSeptile);
        tbGlyphQuadraNovile.Text = _controller.DefineGlyph(AspectTypes.QuadraNovile);

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

        List<CelPointConfigSpecs> celPoints = _controller.GetConfig().CelPoints;

        tboxSunFactor.Text = celPoints[0].PercentageOrb.ToString();
        tboxMoonFactor.Text = celPoints[1].PercentageOrb.ToString();
        tboxMercuryFactor.Text = celPoints[2].PercentageOrb.ToString();
        tboxVenusFactor.Text = celPoints[3].PercentageOrb.ToString();
        tboxMarsFactor.Text = celPoints[5].PercentageOrb.ToString();
        tboxJupiterFactor.Text = celPoints[6].PercentageOrb.ToString();
        tboxSaturnFactor.Text = celPoints[7].PercentageOrb.ToString();
        tboxUranusFactor.Text = celPoints[8].PercentageOrb.ToString();
        cboxUranus.IsChecked = celPoints[8].IsUsed;
        tboxNeptuneFactor.Text = celPoints[9].PercentageOrb.ToString();
        cboxNeptune.IsChecked = celPoints[9].IsUsed;
        tboxPlutoFactor.Text = celPoints[10].PercentageOrb.ToString();
        cboxPluto.IsChecked = celPoints[10].IsUsed;
        tboxMeanNodeFactor.Text = celPoints[11].PercentageOrb.ToString();
        cboxMeanNode.IsChecked = celPoints[11].IsUsed;
        tboxTrueNodeFactor.Text = celPoints[12].PercentageOrb.ToString();
        cboxTrueNode.IsChecked = celPoints[12].IsUsed;
        tboxChironFactor.Text = celPoints[13].PercentageOrb.ToString();
        cboxChiron.IsChecked = celPoints[13].IsUsed;
        tboxPersephoneRamFactor.Text = celPoints[14].PercentageOrb.ToString();
        cboxPersephoneRam.IsChecked = celPoints[14].IsUsed;
        tboxHermesFactor.Text = celPoints[15].PercentageOrb.ToString();
        cboxHermes.IsChecked = celPoints[15].IsUsed;
        tboxDemeterFactor.Text = celPoints[16].PercentageOrb.ToString();
        cboxDemeter.IsChecked = celPoints[16].IsUsed;
        tboxCupidoFactor.Text = celPoints[17].PercentageOrb.ToString();
        cboxCupido.IsChecked = celPoints[17].IsUsed;
        tboxHadesFactor.Text = celPoints[18].PercentageOrb.ToString();
        cboxHades.IsChecked = celPoints[18].IsUsed;
        tboxZeusFactor.Text = celPoints[19].PercentageOrb.ToString();
        cboxZeus.IsChecked = celPoints[19].IsUsed;
        tboxKronosFactor.Text = celPoints[20].PercentageOrb.ToString();
        cboxKronos.IsChecked = celPoints[20].IsUsed;
        tboxApollonFactor.Text = celPoints[21].PercentageOrb.ToString();
        cboxApollon.IsChecked = celPoints[21].IsUsed;
        tboxAdmetosFactor.Text = celPoints[22].PercentageOrb.ToString();
        cboxAdmetos.IsChecked = celPoints[22].IsUsed;
        tboxVulkanusUraFactor.Text = celPoints[23].PercentageOrb.ToString();
        cboxVulkanusUra.IsChecked = celPoints[23].IsUsed;
        tboxPoseidonFactor.Text = celPoints[24].PercentageOrb.ToString();
        cboxPoseidon.IsChecked = celPoints[24].IsUsed;
        tboxErisFactor.Text = celPoints[25].PercentageOrb.ToString();
        cboxEris.IsChecked = celPoints[25].IsUsed;
        tboxPholusFactor.Text = celPoints[26].PercentageOrb.ToString();
        cboxPholus.IsChecked = celPoints[26].IsUsed;
        tboxCeresFactor.Text = celPoints[27].PercentageOrb.ToString();
        cboxCeres.IsChecked = celPoints[27].IsUsed;
        tboxPallasFactor.Text = celPoints[28].PercentageOrb.ToString();
        cboxPallas.IsChecked = celPoints[28].IsUsed;
        tboxJunoFactor.Text = celPoints[29].PercentageOrb.ToString();
        cboxJuno.IsChecked = celPoints[29].IsUsed;
        tboxVestaFactor.Text = celPoints[30].PercentageOrb.ToString();
        cboxVesta.IsChecked = celPoints[30].IsUsed;
        tboxTransPlutoFactor.Text = celPoints[31].PercentageOrb.ToString();
        cboxTransPluto.IsChecked = celPoints[31].IsUsed;
        tboxNessusFactor.Text = celPoints[32].PercentageOrb.ToString();
        cboxNessus.IsChecked = celPoints[32].IsUsed;
        tboxHuyaFactor.Text = celPoints[33].PercentageOrb.ToString();
        cboxHuya.IsChecked = celPoints[33].IsUsed;
        tboxVarunaFactor.Text = celPoints[34].PercentageOrb.ToString();
        cboxVaruna.IsChecked = celPoints[34].IsUsed;
        tboxIxionFactor.Text = celPoints[35].PercentageOrb.ToString();
        cboxIxion.IsChecked = celPoints[35].IsUsed;
        tboxQuaoarFactor.Text = celPoints[36].PercentageOrb.ToString();
        cboxQuaoar.IsChecked = celPoints[36].IsUsed;
        tboxHaumeaFactor.Text = celPoints[37].PercentageOrb.ToString();
        cboxHaumea.IsChecked = celPoints[37].IsUsed;
        tboxOrcusFactor.Text = celPoints[38].PercentageOrb.ToString();
        cboxOrcus.IsChecked = celPoints[38].IsUsed;
        tboxMakemakeFactor.Text = celPoints[39].PercentageOrb.ToString();
        cboxMakemake.IsChecked = celPoints[39].IsUsed;
        tboxSednaFactor.Text = celPoints[40].PercentageOrb.ToString();
        cboxSedna.IsChecked = celPoints[40].IsUsed;
        tboxHygieiaFactor.Text = celPoints[41].PercentageOrb.ToString();
        cboxHygieia.IsChecked = celPoints[41].IsUsed;
        tboxAstraeaFactor.Text = celPoints[42].PercentageOrb.ToString();
        cboxAstraea.IsChecked = celPoints[42].IsUsed;
        tboxMeanBlackMoonFactor.Text = celPoints[43].PercentageOrb.ToString();
        cboxMeanBlackMoon.IsChecked = celPoints[43].IsUsed;
        tboxCorrBlackMoonFactor.Text = celPoints[44].PercentageOrb.ToString();
        cboxCorrBlackMoon.IsChecked = celPoints[44].IsUsed;
        tboxInterpolatedBlackMoonFactor.Text = celPoints[45].PercentageOrb.ToString();
        cboxInterpolatedBlackMoon.IsChecked = celPoints[45].IsUsed;
        tboxDuvalBlackMoonFactor.Text = celPoints[46].PercentageOrb.ToString();
        cboxDuvalBlackMoon.IsChecked = false;   // celPoints[46].IsUsed;
        cboxDuvalBlackMoon.IsEnabled= false;
        tboxPersephoneCarteretFactor.Text = celPoints[47].PercentageOrb.ToString();
        cboxPersephoneCarteret.IsChecked = false;  
        cboxPersephoneCarteret.IsEnabled= false;
        tboxVulcanusCarteretFactor.Text = celPoints[48].PercentageOrb.ToString();
        cboxVulcanusCarteret.IsChecked = false;  
        cboxVulcanusCarteret.IsEnabled= false;

        List<ZodiacPointConfigSpecs> zodiacPoints = _controller.GetConfig().ZodiacPoints;
        tboxZeroAriesFactor.Text = zodiacPoints[0].PercentageOrb.ToString();
        cboxZeroAries.IsChecked = zodiacPoints[0].IsUsed;

        List<ArabicPointConfigSpecs> arabicPoints = _controller.GetConfig().ArabicPoints;
        tboxParsSectFactor.Text = arabicPoints[0].PercentageOrb.ToString();
        cboxParsSect.IsChecked = arabicPoints[0].IsUsed;
        tboxParsNoSectFactor.Text = arabicPoints[1].PercentageOrb.ToString();
        cboxParsNoSect.IsChecked = arabicPoints[1].IsUsed;


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


        // MundanePoints
        List<MundanePointConfigSpecs> mundanePoints = _controller.GetConfig().MundanePoints;
        tboxAscFactor.Text = mundanePoints[0].PercentageOrb.ToString();
        cboxAsc.IsChecked = mundanePoints[0].IsUsed;
        tboxMcFactor.Text = mundanePoints[1].PercentageOrb.ToString();
        cboxMc.IsChecked = mundanePoints[1].IsUsed;
        tboxEastpointFactor.Text = mundanePoints[2].PercentageOrb.ToString();
        cboxEastpoint.IsChecked = mundanePoints[2].IsUsed;
        tboxVertexFactor.Text = mundanePoints[3].PercentageOrb.ToString();
        cboxVertex.IsChecked = mundanePoints[3].IsUsed;



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
            List<CelPointConfigSpecs> celPointsSpecs = DefineCelPointSpecs();
            List<AspectConfigSpecs> aspectSpecs = DefineAspectSpecs();
            List<MundanePointConfigSpecs> mundanePointSpecs = DefineMundanePointSpecs();
            List<ArabicPointConfigSpecs> arabicPointSpecs = DefineArabicPointSpecs();
            List<ZodiacPointConfigSpecs> zodiacPointSpecs = DefineZodiacPointSpecs();
            double baseOrbAspects = Convert.ToDouble(tboxAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            double baseOrbMidpoints = Convert.ToDouble(tboxMidpointAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

            AstroConfig astroConfig = new(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod, celPointsSpecs, aspectSpecs, mundanePointSpecs, arabicPointSpecs, zodiacPointSpecs, baseOrbAspects, baseOrbMidpoints);
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

    private List<CelPointConfigSpecs> DefineCelPointSpecs()
    {
        List<CelPointConfigSpecs> celPointSpecs = new()
        {
            DefineSingleCelPointSpec(CelPoints.Sun, tboxSunFactor.Text, cboxSun.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Moon, tboxMoonFactor.Text, cboxMoon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Mercury, tboxMercuryFactor.Text, cboxMercury.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Venus, tboxVenusFactor.Text, cboxVenus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Earth, "100", false),                                       // TODO 0.4 handle earth for heliocentric
            DefineSingleCelPointSpec(CelPoints.Mars, tboxMarsFactor.Text, cboxMars.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Jupiter, tboxJupiterFactor.Text, cboxJupiter.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Saturn, tboxSaturnFactor.Text, cboxSaturn.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Uranus, tboxUranusFactor.Text, cboxUranus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Neptune, tboxNeptuneFactor.Text, cboxNeptune.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Pluto, tboxPlutoFactor.Text, cboxPluto.IsChecked),
            DefineSingleCelPointSpec(CelPoints.MeanNode, tboxMeanNodeFactor.Text, cboxMeanNode.IsChecked),
            DefineSingleCelPointSpec(CelPoints.TrueNode, tboxTrueNodeFactor.Text, cboxTrueNode.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Chiron, tboxChironFactor.Text, cboxChiron.IsChecked),
            DefineSingleCelPointSpec(CelPoints.PersephoneRam, tboxPersephoneRamFactor.Text, cboxPersephoneRam.IsChecked),
            DefineSingleCelPointSpec(CelPoints.DemeterRam, tboxDemeterFactor.Text, cboxDemeter.IsChecked),
            DefineSingleCelPointSpec(CelPoints.HermesRam, tboxHermesFactor.Text, cboxHermes.IsChecked),
            DefineSingleCelPointSpec(CelPoints.CupidoUra, tboxCupidoFactor.Text, cboxCupido.IsChecked),
            DefineSingleCelPointSpec(CelPoints.HadesUra, tboxHadesFactor.Text, cboxHades.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ZeusUra, tboxZeusFactor.Text, cboxZeus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.KronosUra, tboxKronosFactor.Text, cboxKronos.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ApollonUra, tboxApollonFactor.Text, cboxApollon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.AdmetosUra, tboxAdmetosFactor.Text, cboxAdmetos.IsChecked),
            DefineSingleCelPointSpec(CelPoints.VulcanusUra, tboxVulkanusUraFactor.Text, cboxVulkanusUra.IsChecked),
            DefineSingleCelPointSpec(CelPoints.PoseidonUra, tboxPoseidonFactor.Text, cboxPoseidon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Eris, tboxErisFactor.Text, cboxEris.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Pholus, tboxPholusFactor.Text, cboxPholus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Ceres, tboxCeresFactor.Text, cboxCeres.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Pallas, tboxPallasFactor.Text, cboxPallas.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Juno, tboxJunoFactor.Text, cboxJuno.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Vesta, tboxVestaFactor.Text, cboxVesta.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Isis, tboxTransPlutoFactor.Text, cboxTransPluto.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Nessus, tboxNessusFactor.Text, cboxNessus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Huya, tboxHuyaFactor.Text, cboxHuya.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Varuna, tboxVarunaFactor.Text, cboxVaruna.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Ixion, tboxIxionFactor.Text, cboxIxion.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Quaoar, tboxQuaoarFactor.Text, cboxQuaoar.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Haumea, tboxHaumeaFactor.Text, cboxHaumea.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Orcus, tboxOrcusFactor.Text, cboxOrcus.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Makemake, tboxMakemakeFactor.Text, cboxMakemake.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Sedna, tboxSednaFactor.Text, cboxSedna.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Hygieia, tboxHygieiaFactor.Text, cboxHygieia.IsChecked),
            DefineSingleCelPointSpec(CelPoints.Astraea, tboxAstraeaFactor.Text, cboxAstraea.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ApogeeMean, tboxMeanBlackMoonFactor.Text, cboxMeanBlackMoon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ApogeeCorrected, tboxCorrBlackMoonFactor.Text, cboxCorrBlackMoon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ApogeeInterpolated, tboxInterpolatedBlackMoonFactor.Text, cboxInterpolatedBlackMoon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.ApogeeDuval, tboxDuvalBlackMoonFactor.Text, cboxDuvalBlackMoon.IsChecked),
            DefineSingleCelPointSpec(CelPoints.PersephoneCarteret, tboxPersephoneCarteretFactor.Text, cboxPersephoneCarteret.IsChecked),
            DefineSingleCelPointSpec(CelPoints.VulcanusCarteret, tboxVulcanusCarteretFactor.Text, cboxVulcanusCarteret.IsChecked)
        };
        return celPointSpecs;
    }

    private static CelPointConfigSpecs DefineSingleCelPointSpec(CelPoints celPoint, string orbFactorText, bool? isUsed)
    {
        int orbFactorValue = Convert.ToInt32(orbFactorText, CultureInfo.InvariantCulture);
        return new CelPointConfigSpecs(celPoint, orbFactorValue, isUsed ?? false);
    }




    private List<AspectConfigSpecs> DefineAspectSpecs()
    {
        List<AspectConfigSpecs> aspectSpecs = new()
        {
            DefineSingleAspectSpec(AspectTypes.Conjunction, tboxConjunctionFactor.Text, cboxConjunction.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Opposition, tboxOppositionFactor.Text, cboxOpposition.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Triangle, tboxTriangleFactor.Text, cboxTriangle.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Square, tboxSquareFactor.Text, cboxSquare.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Septile, tboxSeptileFactor.Text, cboxSeptile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Sextile, tboxSextileFactor.Text, cboxSextile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Quintile, tboxQuintileFactor.Text, cboxQuintile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.SemiSextile, tboxSemiSextileFactor.Text, cboxSemiSextile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.SemiSquare, tboxSemiSquareFactor.Text, cboxSemiSquare.IsChecked),
            DefineSingleAspectSpec(AspectTypes.SemiQuintile, tboxSemiQuintileFactor.Text, cboxSemiQuintile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.BiQuintile, tboxBiQuintileFactor.Text, cboxBiQuintile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Inconjunct, tboxInconjunctFactor.Text, cboxInconjunct.IsChecked),
            DefineSingleAspectSpec(AspectTypes.SesquiQuadrate, tboxSesquiquadrateFactor.Text, cboxSesquiquadrate.IsChecked),
            DefineSingleAspectSpec(AspectTypes.TriDecile, tboxTriDecileFactor.Text, cboxTriDecile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.BiSeptile, tboxBiSeptileFactor.Text, cboxBiSeptile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.TriSeptile, tboxTriSeptileFactor.Text, cboxTriSeptile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Novile, tboxNovileFactor.Text, cboxNovile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.BiNovile, tboxBiNovileFactor.Text, cboxBiNovile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.QuadraNovile, tboxQuadraNovileFactor.Text, cboxQuadraNovile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Undecile, tboxUnDecileFactor.Text, cboxUnDecile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Centile, tboxCentileFactor.Text, cboxCentile.IsChecked),
            DefineSingleAspectSpec(AspectTypes.Vigintile, tboxVigintileFactor.Text, cboxVigintile.IsChecked)
        };

        return aspectSpecs;
    }

    private static AspectConfigSpecs DefineSingleAspectSpec(AspectTypes aspectType, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new AspectConfigSpecs(aspectType, percentageValue, isUsed ?? false);
    }


    private List<MundanePointConfigSpecs> DefineMundanePointSpecs()
    {
        List<MundanePointConfigSpecs> mundanePointSpecs = new()
        {
            DefineSingleMundanePointSpec(MundanePoints.Mc, tboxMcFactor.Text, cboxMc.IsChecked),
            DefineSingleMundanePointSpec(MundanePoints.Ascendant, tboxAscFactor.Text, cboxAsc.IsChecked),
            DefineSingleMundanePointSpec(MundanePoints.Vertex, tboxVertexFactor.Text, cboxVertex.IsChecked),
            DefineSingleMundanePointSpec(MundanePoints.EastPoint, tboxEastpointFactor.Text, cboxEastpoint.IsChecked)
        };

        return mundanePointSpecs;
    }


    private static MundanePointConfigSpecs DefineSingleMundanePointSpec(MundanePoints point, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new MundanePointConfigSpecs(point, percentageValue, isUsed ?? false);
    }


    private List<ArabicPointConfigSpecs> DefineArabicPointSpecs()
    {
        List<ArabicPointConfigSpecs> arabicPointConfigSpecs = new()
        {
            DefineSingleArabicPoint(ArabicPoints.FortunaNoSect, tboxParsNoSectFactor.Text, cboxParsNoSect.IsChecked),
            DefineSingleArabicPoint(ArabicPoints.FortunaSect, tboxParsSectFactor.Text, cboxParsSect.IsChecked)
        };
        return arabicPointConfigSpecs;
    }

    private static ArabicPointConfigSpecs DefineSingleArabicPoint(ArabicPoints point, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new ArabicPointConfigSpecs(point, percentageValue, isUsed ?? false);
    }


    private List<ZodiacPointConfigSpecs> DefineZodiacPointSpecs()
    {
        List<ZodiacPointConfigSpecs> zodiacPointConfigSpecs = new()
        {
            DefineSingleZodiacPoint(ZodiacPoints.ZeroAries, tboxZeroAriesFactor.Text, cboxZeroAries.IsChecked)
        };
        return zodiacPointConfigSpecs;
    }


    private static ZodiacPointConfigSpecs DefineSingleZodiacPoint(ZodiacPoints point, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new ZodiacPointConfigSpecs(point, percentageValue, isUsed ?? false);
    }


}
