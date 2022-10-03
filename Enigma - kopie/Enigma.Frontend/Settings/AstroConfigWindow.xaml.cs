// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Frontend.Support;
using System.Windows;
using Enigma.Domain.Analysis;
using System;
using Enigma.Frontend.Charts;
using Enigma.Domain.Configuration;
using System.Collections.Generic;
using System.Globalization;

namespace Enigma.Frontend.Settings;

/// <summary>
/// Interaction logic for AstroConfigWindow.xaml
/// </summary>
public partial class AstroConfigWindow : Window
{
    private AstroConfigController _controller;
    private IRosetta _rosetta;
    private IChartsEnumFacade _enumFacade;

    public AstroConfigWindow(AstroConfigController controller, IRosetta rosetta, IChartsEnumFacade enumFacade)
    {
        InitializeComponent(); 
        _controller = controller;
        _rosetta = rosetta;
        _enumFacade = enumFacade;
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
        tbTextSun.Text = _rosetta.TextForId("ref.enum.solsyspoint.sun");
        tbTextMoon.Text = _rosetta.TextForId("ref.enum.solsyspoint.moon");
        tbTextMercury.Text = _rosetta.TextForId("ref.enum.solsyspoint.mercury");
        tbTextVenus.Text = _rosetta.TextForId("ref.enum.solsyspoint.venus");
        tbTextMars.Text = _rosetta.TextForId("ref.enum.solsyspoint.mars");
        tbTextJupiter.Text = _rosetta.TextForId("ref.enum.solsyspoint.jupiter");
        tbTextSaturn.Text = _rosetta.TextForId("ref.enum.solsyspoint.saturn");
        tbTextUranus.Text = _rosetta.TextForId("ref.enum.solsyspoint.uranus");
        tbTextNeptune.Text = _rosetta.TextForId("ref.enum.solsyspoint.neptune");
        tbTextPluto.Text = _rosetta.TextForId("ref.enum.solsyspoint.pluto");
        tbTextMc.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.mc");
        tbTextAsc.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.asc");
        tbTextVertex.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.vertex");
        tbTextEastpoint.Text = _rosetta.TextForId("ref.enum.mundanepoint.id.eastpoint");

        tbTextParsSect.Text = _rosetta.TextForId("ref.enum.solsyspoint.parssect");
        tbTextParsNoSect.Text = _rosetta.TextForId("ref.enum.solsyspoint.parsnosect");
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
        tbTextMeanNode.Text = _rosetta.TextForId("ref.enum.solsyspoint.meannode");
        tbTextTrueNode.Text = _rosetta.TextForId("ref.enum.solsyspoint.truenode");
        tbTextZeroAries.Text = _rosetta.TextForId("ref.enum.solsyspoint.zeroaries");
        tbTextMeanBlackMoon.Text = _rosetta.TextForId("ref.enum.solsyspoint.meanblackmoon");
        tbTextCorrBlackMoon.Text = _rosetta.TextForId("ref.enum.solsyspoint.corrblackmoon");
        tbTextInterpolatedBlackMoon.Text = _rosetta.TextForId("ref.enum.solsyspoint.interpblackmoon");
        tbTextDuvalBlackMoon.Text = _rosetta.TextForId("ref.enum.solsyspoint.duvalblackmoon");

        tbTextHuya.Text = _rosetta.TextForId("ref.enum.solsyspoint.huya");
        tbTextVaruna.Text = _rosetta.TextForId("ref.enum.solsyspoint.varuna");
        tbTextIxion.Text = _rosetta.TextForId("ref.enum.solsyspoint.ixion");
        tbTextQuaoar.Text = _rosetta.TextForId("ref.enum.solsyspoint.quaoar");
        tbTextHaumea.Text = _rosetta.TextForId("ref.enum.solsyspoint.haumea");
        tbTextEris.Text = _rosetta.TextForId("ref.enum.solsyspoint.eris");
        tbTextSedna.Text = _rosetta.TextForId("ref.enum.solsyspoint.sedna");
        tbTextOrcus.Text = _rosetta.TextForId("ref.enum.solsyspoint.orcus");
        tbTextMakemake.Text = _rosetta.TextForId("ref.enum.solsyspoint.makemake");

        tbTextChiron.Text = _rosetta.TextForId("ref.enum.solsyspoint.chiron");
        tbTextNessus.Text = _rosetta.TextForId("ref.enum.solsyspoint.nessus");
        tbTextPholus.Text = _rosetta.TextForId("ref.enum.solsyspoint.pholus");

        tbTextCeres.Text = _rosetta.TextForId("ref.enum.solsyspoint.ceres");
        tbTextPallas.Text = _rosetta.TextForId("ref.enum.solsyspoint.pallas");
        tbTextJuno.Text = _rosetta.TextForId("ref.enum.solsyspoint.juno");
        tbTextVesta.Text = _rosetta.TextForId("ref.enum.solsyspoint.vesta");
        tbTextHygieia.Text = _rosetta.TextForId("ref.enum.solsyspoint.hygieia");
        tbTextAstraea.Text = _rosetta.TextForId("ref.enum.solsyspoint.astraea");

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

        tbTextCupido.Text = _rosetta.TextForId("ref.enum.solsyspoint.cupido_ura");
        tbTextHades.Text = _rosetta.TextForId("ref.enum.solsyspoint.hades_ura");
        tbTextZeus.Text = _rosetta.TextForId("ref.enum.solsyspoint.zeus_ura");
        tbTextKronos.Text = _rosetta.TextForId("ref.enum.solsyspoint.kronos_ura");
        tbTextApollon.Text = _rosetta.TextForId("ref.enum.solsyspoint.apollon_ura");
        tbTextAdmetos.Text = _rosetta.TextForId("ref.enum.solsyspoint.admetos_ura");
        tbTextVulkanusUra.Text = _rosetta.TextForId("ref.enum.solsyspoint.vulcanus_ura");
        tbTextPoseidon.Text = _rosetta.TextForId("ref.enum.solsyspoint.poseidon_ura");
        tbTextPersephoneRam.Text = _rosetta.TextForId("ref.enum.solsyspoint.persephone_ram");
        tbTextHermes.Text = _rosetta.TextForId("ref.enum.solsyspoint.hermes_ram");
        tbTextDemeter.Text = _rosetta.TextForId("ref.enum.solsyspoint.demeter_ram");
        tbTextVulcanusCarteret.Text = _rosetta.TextForId("ref.enum.solsyspoint.vulcanus_carteret");
        tbTextPersephoneCarteret.Text = _rosetta.TextForId("ref.enum.solsyspoint.persephone_carteret");
        tbTextTransPluto.Text = _rosetta.TextForId("ref.enum.solsyspoint.transpluto");

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
        tbTextBiSeptile.Text = _rosetta.TextForId("ref.enum.aspect.septile");
        tbTextTriDecile.Text = _rosetta.TextForId("ref.enum.aspect.tridecile");
        tbTextTriSeptile.Text = _rosetta.TextForId("ref.enum.aspect.triseptile");
        tbTextQuadraNovile.Text = _rosetta.TextForId("ref.enum.aspect.quadranovile");
    }

    private void PopulateGlyphs()
    {
        tbGlyphSun.Text = _controller.DefineGlyph(SolarSystemPoints.Sun);
        tbGlyphMoon.Text = _controller.DefineGlyph(SolarSystemPoints.Moon);
        tbGlyphMercury.Text = _controller.DefineGlyph(SolarSystemPoints.Mercury);
        tbGlyphVenus.Text = _controller.DefineGlyph(SolarSystemPoints.Venus);
        tbGlyphMars.Text = _controller.DefineGlyph(SolarSystemPoints.Mars);
        tbGlyphJupiter.Text = _controller.DefineGlyph(SolarSystemPoints.Jupiter);
        tbGlyphSaturn.Text = _controller.DefineGlyph(SolarSystemPoints.Saturn);
        tbGlyphUranus.Text = _controller.DefineGlyph(SolarSystemPoints.Uranus);
        tbGlyphNeptune.Text = _controller.DefineGlyph(SolarSystemPoints.Neptune);
        tbGlyphPluto.Text = _controller.DefineGlyph(SolarSystemPoints.Pluto);
        tbGlyphMc.Text = _controller.DefineGlyph(MundanePoints.Mc);
        tbGlyphAsc.Text = _controller.DefineGlyph(MundanePoints.Ascendant);
        tbGlyphVertex.Text = _controller.DefineGlyph(MundanePoints.Vertex);
        tbGlyphEastpoint.Text = _controller.DefineGlyph(MundanePoints.EastPoint);

        tbGlyphParsSect.Text = _controller.DefineGlyph(SolarSystemPoints.ParsFortunaSect);
        tbGlyphParsNoSect.Text = _controller.DefineGlyph(SolarSystemPoints.ParsFortunaNoSect);

        tbGlyphMeanNode.Text = _controller.DefineGlyph(SolarSystemPoints.MeanNode);
        tbGlyphTrueNode.Text = _controller.DefineGlyph(SolarSystemPoints.TrueNode);
        tbGlyphMeanBLackMoon.Text = _controller.DefineGlyph(SolarSystemPoints.ApogeeMean);
        tbGlyphCorrBlackMoon.Text = _controller.DefineGlyph(SolarSystemPoints.ApogeeCorrected);
        tbGlyphInterpolatedBlackMoon.Text = _controller.DefineGlyph(SolarSystemPoints.ApogeeInterpolated);
        tbGlyphDuvalBlackMoon.Text = _controller.DefineGlyph(SolarSystemPoints.ApogeeDuval);
        tbGlyphZeroAries.Text = _controller.DefineGlyph(SolarSystemPoints.ZeroAries);

        tbGlyphHuya.Text = _controller.DefineGlyph(SolarSystemPoints.Huya);
        tbGlyphVaruna.Text = _controller.DefineGlyph(SolarSystemPoints.Varuna);
        tbGlyphIxion.Text = _controller.DefineGlyph(SolarSystemPoints.Ixion);
        tbGlyphQuaoar.Text = _controller.DefineGlyph(SolarSystemPoints.Quaoar);
        tbGlyphHaumea.Text = _controller.DefineGlyph(SolarSystemPoints.Haumea);
        tbGlyphEris.Text = _controller.DefineGlyph(SolarSystemPoints.Eris);
        tbGlyphSedna.Text = _controller.DefineGlyph(SolarSystemPoints.Sedna);
        tbGlyphOrcus.Text = _controller.DefineGlyph(SolarSystemPoints.Orcus);
        tbGlyphMakemake.Text = _controller.DefineGlyph(SolarSystemPoints.Makemake);

        tbGlyphChiron.Text = _controller.DefineGlyph(SolarSystemPoints.Chiron);
        tbGlyphNessus.Text = _controller.DefineGlyph(SolarSystemPoints.Nessus);
        tbGlyphPholus.Text = _controller.DefineGlyph(SolarSystemPoints.Pholus);

        tbGlyphCeres.Text = _controller.DefineGlyph(SolarSystemPoints.Ceres);
        tbGlyphPallas.Text = _controller.DefineGlyph(SolarSystemPoints.Pallas);
        tbGlyphJuno.Text = _controller.DefineGlyph(SolarSystemPoints.Juno);
        tbGlyphVesta.Text = _controller.DefineGlyph(SolarSystemPoints.Vesta);
        tbGlyphHygieia.Text = _controller.DefineGlyph(SolarSystemPoints.Hygieia);
        tbGlyphAstraea.Text = _controller.DefineGlyph(SolarSystemPoints.Astraea);

        tbGlyphCupido.Text = _controller.DefineGlyph(SolarSystemPoints.CupidoUra);
        tbGlyphHades.Text = _controller.DefineGlyph(SolarSystemPoints.HadesUra);
        tbGlyphZeus.Text = _controller.DefineGlyph(SolarSystemPoints.ZeusUra);
        tbGlyphKronos.Text = _controller.DefineGlyph(SolarSystemPoints.KronosUra);
        tbGlyphApollon.Text = _controller.DefineGlyph(SolarSystemPoints.ApollonUra);
        tbGlyphAdmetos.Text = _controller.DefineGlyph(SolarSystemPoints.AdmetosUra);
        tbGlyphVulkanusUra.Text = _controller.DefineGlyph(SolarSystemPoints.VulcanusUra);
        tbGlyphPoseidon.Text = _controller.DefineGlyph(SolarSystemPoints.PoseidonUra);
        tbGlyphPersephoneRam.Text = _controller.DefineGlyph(SolarSystemPoints.PersephoneRam);
        tbGlyphHermes.Text = _controller.DefineGlyph(SolarSystemPoints.HermesRam);
        tbGlyphDemeter.Text = _controller.DefineGlyph(SolarSystemPoints.DemeterRam);
        tbGlyphVulcanusCarteret.Text = _controller.DefineGlyph(SolarSystemPoints.VulcanusCarteret);
        tbGlyphPersephoneCarteret.Text = _controller.DefineGlyph(SolarSystemPoints.PersephoneCarteret);
        tbGlyphTransPluto.Text = _controller.DefineGlyph(SolarSystemPoints.Isis);

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
        foreach (HouseSystemDetails detail in _enumFacade.Houses().AllHouseSystemDetails())
        {
            comboHouseSystem.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboHouseSystem.SelectedIndex = (int)_controller.GetConfig().HouseSystem;

        comboZodiacType.Items.Clear();
        foreach (ZodiacTypeDetails details in _enumFacade.ZodiacTypes().AllZodiacTypeDetails())
        {
            comboZodiacType.Items.Add(_rosetta.TextForId(details.TextId));
        }
        comboZodiacType.SelectedIndex = (int)_controller.GetConfig().ZodiacType;

                comboAyanamsha.Items.Clear();
        foreach (AyanamshaDetails detail in _enumFacade.Ayanamshas().AllAyanamshaDetails())
        {
            comboAyanamsha.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboAyanamsha.SelectedIndex = (int)_controller.GetConfig().Ayanamsha;

        comboObserverPos.Items.Clear();
        foreach (ObserverPositionDetails detail in _enumFacade.ObserverPositions().AllObserverPositionDetails())
        {
            comboObserverPos.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboObserverPos.SelectedIndex = (int)_controller.GetConfig().ObserverPosition;

        comboProjectionType.Items.Clear();
        foreach (ProjectionTypeDetails detail in _enumFacade.ProjectionTypes().AllProjectionTypeDetails())
        {
            comboProjectionType.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboProjectionType.SelectedIndex = (int)_controller.GetConfig().ProjectionType;


        tboxAspectBaseOrb.Text = (_controller.GetConfig().BaseOrbAspects).ToString();
        tboxMidpointAspectBaseOrb.Text = (_controller.GetConfig().BaseOrbMidpoints).ToString();

        List<CelPointSpecs> celPoints = _controller.GetConfig().CelPoints;

        tboxSunFactor.Text = (celPoints[0].PercentageAspectOrb).ToString();
        tboxMoonFactor.Text = (celPoints[1].PercentageAspectOrb).ToString();
        tboxMercuryFactor.Text = (celPoints[2].PercentageAspectOrb).ToString();
        tboxVenusFactor.Text = (celPoints[3].PercentageAspectOrb).ToString();
        tboxMarsFactor.Text = (celPoints[5].PercentageAspectOrb).ToString();
        tboxJupiterFactor.Text = (celPoints[6].PercentageAspectOrb).ToString();
        tboxSaturnFactor.Text = (celPoints[7].PercentageAspectOrb).ToString();
        tboxUranusFactor.Text = (celPoints[8].PercentageAspectOrb).ToString();
        cboxUranus.IsChecked = celPoints[8].IsUsed;
        tboxNeptuneFactor.Text = (celPoints[9].PercentageAspectOrb).ToString();
        cboxNeptune.IsChecked = celPoints[9].IsUsed;
        tboxPlutoFactor.Text = (celPoints[10].PercentageAspectOrb).ToString();
        cboxPluto.IsChecked = celPoints[10].IsUsed;
        tboxMeanNodeFactor.Text = (celPoints[11].PercentageAspectOrb).ToString();
        cboxMeanNode.IsChecked = celPoints[11].IsUsed;
        tboxTrueNodeFactor.Text = (celPoints[12].PercentageAspectOrb).ToString();
        cboxTrueNode.IsChecked = celPoints[12].IsUsed;
        tboxChironFactor.Text = (celPoints[13].PercentageAspectOrb).ToString();
        cboxChiron.IsChecked = celPoints[13].IsUsed;
        tboxPersephoneRamFactor.Text = (celPoints[14].PercentageAspectOrb).ToString();
        cboxPersephoneRam.IsChecked = celPoints[14].IsUsed;
        tboxHermesFactor.Text = (celPoints[15].PercentageAspectOrb).ToString();
        cboxHermes.IsChecked = celPoints[15].IsUsed;
        tboxDemeterFactor.Text = (celPoints[16].PercentageAspectOrb).ToString();
        cboxDemeter.IsChecked = celPoints[16].IsUsed;
        tboxCupidoFactor.Text = (celPoints[17].PercentageAspectOrb).ToString();
        cboxCupido.IsChecked = celPoints[17].IsUsed;
        tboxHadesFactor.Text = (celPoints[18].PercentageAspectOrb).ToString();
        cboxHades.IsChecked = celPoints[18].IsUsed;
        tboxZeusFactor.Text = (celPoints[19].PercentageAspectOrb).ToString();
        cboxZeus.IsChecked = celPoints[19].IsUsed;
        tboxKronosFactor.Text = (celPoints[20].PercentageAspectOrb).ToString();
        cboxKronos.IsChecked = celPoints[20].IsUsed;
        tboxApollonFactor.Text = (celPoints[21].PercentageAspectOrb).ToString();
        cboxApollon.IsChecked = celPoints[21].IsUsed;
        tboxAdmetosFactor.Text = (celPoints[22].PercentageAspectOrb).ToString();
        cboxAdmetos.IsChecked = celPoints[22].IsUsed;
        tboxVulkanusUraFactor.Text = (celPoints[23].PercentageAspectOrb).ToString();
        cboxVulkanusUra.IsChecked = celPoints[23].IsUsed;
        tboxPoseidonFactor.Text = (celPoints[24].PercentageAspectOrb).ToString();
        cboxPoseidon.IsChecked = celPoints[24].IsUsed;
        tboxErisFactor.Text = (celPoints[25].PercentageAspectOrb).ToString();
        cboxEris.IsChecked = celPoints[25].IsUsed;
        tboxPholusFactor.Text = (celPoints[26].PercentageAspectOrb).ToString();
        cboxPholus.IsChecked = celPoints[26].IsUsed;
        tboxCeresFactor.Text = (celPoints[27].PercentageAspectOrb).ToString();
        cboxCeres.IsChecked = celPoints[27].IsUsed;
        tboxPallasFactor.Text = (celPoints[28].PercentageAspectOrb).ToString();
        cboxPallas.IsChecked = celPoints[28].IsUsed;
        tboxJunoFactor.Text = (celPoints[29].PercentageAspectOrb).ToString();
        cboxJuno.IsChecked = celPoints[29].IsUsed;
        tboxVestaFactor.Text = (celPoints[30].PercentageAspectOrb).ToString();
        cboxVesta.IsChecked = celPoints[30].IsUsed;
        tboxTransPlutoFactor.Text = (celPoints[31].PercentageAspectOrb).ToString();
        cboxTransPluto.IsChecked = celPoints[31].IsUsed;
        tboxNessusFactor.Text = (celPoints[32].PercentageAspectOrb).ToString();
        cboxNessus.IsChecked = celPoints[32].IsUsed;
        tboxHuyaFactor.Text = (celPoints[33].PercentageAspectOrb).ToString();
        cboxHuya.IsChecked = celPoints[33].IsUsed;
        tboxVarunaFactor.Text = (celPoints[34].PercentageAspectOrb).ToString();
        cboxVaruna.IsChecked = celPoints[34].IsUsed;
        tboxIxionFactor.Text = (celPoints[35].PercentageAspectOrb).ToString();
        cboxIxion.IsChecked = celPoints[35].IsUsed;
        tboxQuaoarFactor.Text = (celPoints[36].PercentageAspectOrb).ToString();
        cboxQuaoar.IsChecked = celPoints[36].IsUsed;
        tboxHaumeaFactor.Text = (celPoints[37].PercentageAspectOrb).ToString();
        cboxHaumea.IsChecked = celPoints[37].IsUsed;
        tboxOrcusFactor.Text = (celPoints[38].PercentageAspectOrb).ToString();
        cboxOrcus.IsChecked = celPoints[38].IsUsed;
        tboxMakemakeFactor.Text = (celPoints[39].PercentageAspectOrb).ToString();
        cboxMakemake.IsChecked = celPoints[39].IsUsed;
        tboxSednaFactor.Text = (celPoints[40].PercentageAspectOrb).ToString();
        cboxSedna.IsChecked = celPoints[40].IsUsed;
        tboxHygieiaFactor.Text = (celPoints[41].PercentageAspectOrb).ToString();
        cboxHygieia.IsChecked = celPoints[41].IsUsed;
        tboxAstraeaFactor.Text = (celPoints[42].PercentageAspectOrb).ToString();
        cboxAstraea.IsChecked = celPoints[42].IsUsed;
        tboxMeanBlackMoonFactor.Text = (celPoints[43].PercentageAspectOrb).ToString();
        cboxMeanBlackMoon.IsChecked = celPoints[43].IsUsed;
        tboxCorrBlackMoonFactor.Text = (celPoints[44].PercentageAspectOrb).ToString();
        cboxCorrBlackMoon.IsChecked = celPoints[44].IsUsed;
        tboxInterpolatedBlackMoonFactor.Text = (celPoints[45].PercentageAspectOrb).ToString();
        cboxInterpolatedBlackMoon.IsChecked = celPoints[45].IsUsed;
        tboxDuvalBlackMoonFactor.Text = (celPoints[46].PercentageAspectOrb).ToString();
        cboxDuvalBlackMoon.IsChecked = celPoints[46].IsUsed;
        tboxZeroAriesFactor.Text = (celPoints[47].PercentageAspectOrb).ToString();
        cboxZeroAries.IsChecked = celPoints[47].IsUsed;
        tboxParsNoSectFactor.Text = (celPoints[48].PercentageAspectOrb).ToString();
        cboxParsNoSect.IsChecked = celPoints[48].IsUsed;
        tboxParsSectFactor.Text = (celPoints[49].PercentageAspectOrb).ToString();
        cboxParsSect.IsChecked = celPoints[49].IsUsed;
        tboxPersephoneCarteretFactor.Text = (celPoints[50].PercentageAspectOrb).ToString();
        cboxPersephoneCarteret.IsChecked = celPoints[50].IsUsed;
        tboxVulcanusCarteretFactor.Text = (celPoints[51].PercentageAspectOrb).ToString();
        cboxVulcanusCarteret.IsChecked = celPoints[51].IsUsed;

        // aspects
        List<AspectSpecs> aspects = _controller.GetConfig().Aspects;
        comboOrbMethod.Items.Clear();
        foreach (OrbMethodDetails detail in _enumFacade.OrbMethods().AllOrbMethodDetails())
        {
            comboOrbMethod.Items.Add(_rosetta.TextForId(detail.TextId));
        }
        comboOrbMethod.SelectedIndex = (int)_controller.GetConfig().OrbMethod;


        tboxConjunctionFactor.Text = (aspects[0].PercentageAspectOrb).ToString();
        cboxConjunction.IsChecked = aspects[0].IsUsed;
        tboxOppositionFactor.Text = (aspects[1].PercentageAspectOrb).ToString();
        cboxOpposition.IsChecked = aspects[1].IsUsed;
        tboxTriangleFactor.Text = (aspects[2].PercentageAspectOrb).ToString();
        cboxTriangle.IsChecked = aspects[2].IsUsed;
        tboxSquareFactor.Text = (aspects[3].PercentageAspectOrb).ToString();
        cboxSquare.IsChecked = aspects[3].IsUsed;
        tboxSeptileFactor.Text = (aspects[4].PercentageAspectOrb).ToString();
        cboxSeptile.IsChecked = aspects[4].IsUsed;
        tboxSextileFactor.Text = (aspects[5].PercentageAspectOrb).ToString();
        cboxSextile.IsChecked = aspects[5].IsUsed;
        tboxQuintileFactor.Text = (aspects[6].PercentageAspectOrb).ToString();
        cboxQuintile.IsChecked = aspects[6].IsUsed;
        tboxSemiSextileFactor.Text = (aspects[7].PercentageAspectOrb).ToString();
        cboxSemiSextile.IsChecked = aspects[7].IsUsed;
        tboxSemiSquareFactor.Text = (aspects[8].PercentageAspectOrb).ToString();
        cboxSemiSquare.IsChecked = aspects[8].IsUsed;
        tboxSemiQuintileFactor.Text = (aspects[9].PercentageAspectOrb).ToString();
        cboxSemiQuintile.IsChecked = aspects[9].IsUsed;
        tboxBiQuintileFactor.Text = (aspects[10].PercentageAspectOrb).ToString();
        cboxBiQuintile.IsChecked = aspects[10].IsUsed;
        tboxInconjunctFactor.Text = (aspects[11].PercentageAspectOrb).ToString();
        cboxInconjunct.IsChecked = aspects[11].IsUsed;
        tboxSesquiquadrateFactor.Text = (aspects[12].PercentageAspectOrb).ToString();
        cboxSesquiquadrate.IsChecked = aspects[12].IsUsed;
        tboxTriDecileFactor.Text = (aspects[13].PercentageAspectOrb).ToString();
        cboxTriDecile.IsChecked = aspects[13].IsUsed;
        tboxBiSeptileFactor.Text = (aspects[14].PercentageAspectOrb).ToString();
        cboxBiSeptile.IsChecked = aspects[14].IsUsed;
        tboxTriSeptileFactor.Text = (aspects[15].PercentageAspectOrb).ToString();
        cboxTriSeptile.IsChecked = aspects[15].IsUsed;
        tboxNovileFactor.Text = (aspects[16].PercentageAspectOrb).ToString();
        cboxNovile.IsChecked = aspects[16].IsUsed;
        tboxBiNovileFactor.Text = (aspects[17].PercentageAspectOrb).ToString();
        cboxBiNovile.IsChecked = aspects[17].IsUsed;
        tboxQuadraNovileFactor.Text = (aspects[18].PercentageAspectOrb).ToString();
        cboxQuadraNovile.IsChecked = aspects[18].IsUsed;
        tboxUnDecileFactor.Text = (aspects[19].PercentageAspectOrb).ToString();
        cboxUnDecile.IsChecked = aspects[19].IsUsed;
        tboxCentileFactor.Text = (aspects[20].PercentageAspectOrb).ToString();
        cboxCentile.IsChecked = aspects[20].IsUsed;
        tboxVigintileFactor.Text = (aspects[21].PercentageAspectOrb).ToString();
        cboxVigintile.IsChecked = aspects[21].IsUsed;


        // MundanePoints
        List<MundanePointSpecs> mundanePoints = _controller.GetConfig().MundanePoints;
        tboxAscFactor.Text = (mundanePoints[0].PercentageOrb).ToString();
        cboxAsc.IsChecked = mundanePoints[0].IsUsed;
        tboxMcFactor.Text = (mundanePoints[1].PercentageOrb).ToString();
        cboxMc.IsChecked = mundanePoints[1].IsUsed;
        tboxEastpointFactor.Text = (mundanePoints[2].PercentageOrb).ToString();
        cboxEastpoint.IsChecked = mundanePoints[2].IsUsed;
        tboxVertexFactor.Text = (mundanePoints[3].PercentageOrb).ToString();
        cboxVertex.IsChecked = mundanePoints[3].IsUsed;

        // Arabic Points
        List<ArabicPointSpecs> arabicPoints = _controller.GetConfig().ArabicPoints;
        tboxParsSectFactor.Text = (arabicPoints[0].PercentageOrb).ToString();
        cboxParsSect.IsChecked = arabicPoints[0].IsUsed;
        tboxParsNoSectFactor.Text = (arabicPoints[1].PercentageOrb).ToString();
        cboxParsNoSect.IsChecked = arabicPoints[1].IsUsed;

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
            // create line in logfile.
            Hide();

        } else
        {
            MessageBox.Show(_rosetta.TextForId("astroconfigwindow.errorsfound"));
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
        } else if (comboAyanamsha.SelectedIndex == 0)
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
            HouseSystems houseSystem =  _enumFacade.Houses().HouseSystemForIndex(comboHouseSystem.SelectedIndex);
            ZodiacTypes zodiacType = _enumFacade.ZodiacTypes().ZodiacTypeForIndex(comboZodiacType.SelectedIndex);
            Ayanamshas ayanamsha = _enumFacade.Ayanamshas().AyanamshaForIndex(comboAyanamsha.SelectedIndex);
            ObserverPositions observerPosition = _enumFacade.ObserverPositions().ObserverPositionForIndex(comboObserverPos.SelectedIndex);
            ProjectionTypes projectionType = _enumFacade.ProjectionTypes().ProjectionTypeForIndex(comboProjectionType.SelectedIndex);
            OrbMethods orbMethod = _enumFacade.OrbMethods().OrbMethodForIndex(comboOrbMethod.SelectedIndex);
            List<CelPointSpecs> celPointsSpecs = DefineCelPointSpecs();
            List<AspectSpecs> aspectSpecs = DefineAspectSpecs();
            List<MundanePointSpecs> mundanePointSpecs = DefineMundanePointSpecs();
            List<ArabicPointSpecs> arabicPointSpecs = DefineArabicPointSpecs();
            double baseOrbAspects = Convert.ToDouble(tboxAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture); 
            double baseOrbMidpoints = Convert.ToDouble(tboxMidpointAspectBaseOrb.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

            AstroConfig astroConfig = new(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod, celPointsSpecs, aspectSpecs, mundanePointSpecs, arabicPointSpecs, baseOrbAspects, baseOrbMidpoints);
            _controller.UpdateConfig(astroConfig);

        }
        catch (Exception)
        {
            noErrors = false;
        }
        return noErrors;
    }

    private List<CelPointSpecs> DefineCelPointSpecs()
    {
        List<CelPointSpecs> celPointSpecs = new();
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Sun, tboxSunFactor.Text, cboxSun.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Moon, tboxMoonFactor.Text, cboxMoon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Mercury, tboxMercuryFactor.Text, cboxMercury.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Venus, tboxVenusFactor.Text, cboxVenus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Earth, "100", false));                                       // TODO 0.4 handle earth for heliocentric
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Mars, tboxMarsFactor.Text, cboxMars.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Jupiter, tboxJupiterFactor.Text, cboxJupiter.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Saturn, tboxSaturnFactor.Text, cboxSaturn.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Uranus, tboxUranusFactor.Text, cboxUranus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Neptune, tboxNeptuneFactor.Text, cboxNeptune.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Pluto, tboxPlutoFactor.Text, cboxPluto.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.MeanNode, tboxMeanNodeFactor.Text, cboxMeanNode.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.TrueNode, tboxTrueNodeFactor.Text, cboxTrueNode.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Chiron, tboxChironFactor.Text, cboxChiron.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.PersephoneRam, tboxPersephoneRamFactor.Text, cboxPersephoneRam.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.DemeterRam, tboxDemeterFactor.Text, cboxDemeter.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.HermesRam, tboxHermesFactor.Text, cboxHermes.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.CupidoUra, tboxCupidoFactor.Text, cboxCupido.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.HadesUra, tboxHadesFactor.Text, cboxHades.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ZeusUra, tboxZeusFactor.Text, cboxZeus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.KronosUra, tboxKronosFactor.Text, cboxKronos.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ApollonUra, tboxApollonFactor.Text, cboxApollon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.AdmetosUra, tboxAdmetosFactor.Text, cboxAdmetos.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.VulcanusUra, tboxVulkanusUraFactor.Text, cboxVulkanusUra.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.PoseidonUra, tboxPoseidonFactor.Text, cboxPoseidon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Eris, tboxErisFactor.Text, cboxEris.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Pholus, tboxPholusFactor.Text, cboxPholus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Ceres, tboxCeresFactor.Text, cboxCeres.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Pallas, tboxPallasFactor.Text, cboxPallas.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Juno, tboxJunoFactor.Text, cboxJuno.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Vesta, tboxVestaFactor.Text, cboxVesta.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Isis, tboxTransPlutoFactor.Text, cboxTransPluto.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Nessus, tboxNessusFactor.Text, cboxNessus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Huya, tboxHuyaFactor.Text, cboxHuya.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Varuna, tboxVarunaFactor.Text, cboxVaruna.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Ixion, tboxIxionFactor.Text, cboxIxion.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Quaoar, tboxQuaoarFactor.Text, cboxQuaoar.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Haumea, tboxHaumeaFactor.Text, cboxHaumea.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Orcus, tboxOrcusFactor.Text, cboxOrcus.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Makemake, tboxMakemakeFactor.Text, cboxMakemake.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Sedna, tboxSednaFactor.Text, cboxSedna.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Hygieia, tboxHygieiaFactor.Text, cboxHygieia.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.Astraea, tboxAstraeaFactor.Text, cboxAstraea.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ApogeeMean, tboxMeanBlackMoonFactor.Text, cboxMeanBlackMoon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ApogeeCorrected, tboxCorrBlackMoonFactor.Text, cboxCorrBlackMoon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ApogeeInterpolated, tboxInterpolatedBlackMoonFactor.Text, cboxInterpolatedBlackMoon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ApogeeDuval, tboxDuvalBlackMoonFactor.Text, cboxDuvalBlackMoon.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ZeroAries, tboxZeroAriesFactor.Text, cboxZeroAries.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ParsFortunaNoSect, tboxParsNoSectFactor.Text, cboxParsNoSect.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.ParsFortunaSect, tboxParsSectFactor.Text, cboxParsSect.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.PersephoneCarteret, tboxPersephoneCarteretFactor.Text, cboxPersephoneCarteret.IsChecked));
        celPointSpecs.Add(DefineSingleCelPointSpec(SolarSystemPoints.VulcanusCarteret, tboxVulcanusCarteretFactor.Text, cboxVulcanusCarteret.IsChecked));
        return celPointSpecs;
    }

    private CelPointSpecs DefineSingleCelPointSpec(SolarSystemPoints solSysPoint, string orbFactorText, bool? isUsed)
    {
        int orbFactorValue = Convert.ToInt32(orbFactorText, CultureInfo.InvariantCulture);
        return new CelPointSpecs(solSysPoint, orbFactorValue, isUsed ?? false);
    }

    private List<AspectSpecs> DefineAspectSpecs()
    {
        List<AspectSpecs> aspectSpecs = new();
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Conjunction, tboxConjunctionFactor.Text, cboxConjunction.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Opposition, tboxOppositionFactor.Text, cboxOpposition.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Triangle, tboxTriangleFactor.Text, cboxTriangle.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Square, tboxSquareFactor.Text, cboxSquare.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Septile, tboxSeptileFactor.Text, cboxSeptile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Sextile, tboxSextileFactor.Text, cboxSextile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Quintile, tboxQuintileFactor.Text, cboxQuintile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.SemiSextile, tboxSemiSextileFactor.Text, cboxSemiSextile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.SemiSquare, tboxSemiSquareFactor.Text, cboxSemiSquare.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.SemiQuintile, tboxSemiQuintileFactor.Text, cboxSemiQuintile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.BiQuintile, tboxBiQuintileFactor.Text, cboxBiQuintile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Inconjunct, tboxInconjunctFactor.Text, cboxInconjunct.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.SesquiQuadrate, tboxSesquiquadrateFactor.Text, cboxSesquiquadrate.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.TriDecile, tboxTriDecileFactor.Text, cboxTriDecile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.BiSeptile, tboxBiSeptileFactor.Text, cboxBiSeptile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.TriSeptile, tboxTriSeptileFactor.Text, cboxTriSeptile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Novile, tboxNovileFactor.Text, cboxNovile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.BiNovile, tboxBiNovileFactor.Text, cboxBiNovile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.QuadraNovile, tboxQuadraNovileFactor.Text, cboxQuadraNovile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Undecile, tboxUnDecileFactor.Text, cboxUnDecile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Centile, tboxCentileFactor.Text, cboxCentile.IsChecked));
        aspectSpecs.Add(DefineSingleAspectSpec(AspectTypes.Vigintile, tboxVigintileFactor.Text, cboxVigintile.IsChecked));

        return aspectSpecs;
    }

    private AspectSpecs DefineSingleAspectSpec(AspectTypes aspectType, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new AspectSpecs(aspectType, percentageValue, isUsed ?? false);
    }


    private List<MundanePointSpecs> DefineMundanePointSpecs()
    {
        List<MundanePointSpecs> mundanePointSpecs = new();
        mundanePointSpecs.Add(DefineSingleMundanePointSpec(MundanePoints.Mc, tboxMcFactor.Text, cboxMc.IsChecked));
        mundanePointSpecs.Add(DefineSingleMundanePointSpec(MundanePoints.Ascendant, tboxAscFactor.Text, cboxAsc.IsChecked));
        mundanePointSpecs.Add(DefineSingleMundanePointSpec(MundanePoints.Vertex, tboxVertexFactor.Text, cboxVertex.IsChecked));
        mundanePointSpecs.Add(DefineSingleMundanePointSpec(MundanePoints.EastPoint, tboxEastpointFactor.Text, cboxEastpoint.IsChecked));

        return mundanePointSpecs;
    }


    private MundanePointSpecs DefineSingleMundanePointSpec(MundanePoints point, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new MundanePointSpecs(point, percentageValue, isUsed ?? false);
    }


    private List<ArabicPointSpecs> DefineArabicPointSpecs()
    {
        List<ArabicPointSpecs> arabicPointSpecs = new();
        arabicPointSpecs.Add(DefineSingleArabicPoint(ArabicPoints.FortunaNoSect, tboxParsNoSectFactor.Text, cboxParsNoSect.IsChecked));
        arabicPointSpecs.Add(DefineSingleArabicPoint(ArabicPoints.FortunaSect, tboxParsSectFactor.Text, cboxParsSect.IsChecked));
        return arabicPointSpecs;
    }

    private ArabicPointSpecs DefineSingleArabicPoint(ArabicPoints point, string percentageText, bool? isUsed)
    {
        int percentageValue = Convert.ToInt32(percentageText, CultureInfo.InvariantCulture);
        return new ArabicPointSpecs(point, percentageValue, isUsed ?? false);
    }

}
