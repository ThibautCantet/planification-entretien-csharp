﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PlanificationEntretien.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CreationDunCandidatFeature : object, Xunit.IClassFixture<CreationDunCandidatFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "creation-candidat.feature"
#line hidden
        
        public CreationDunCandidatFeature(CreationDunCandidatFeature.FixtureData fixtureData, PlanificationEntretien_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("fr"), "Features", "Création d\'un candidat", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat est crée quand toutes ses informations sont complètes")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat est crée quand toutes ses informations sont complètes")]
        public virtual void UnCandidatEstCreeQuandToutesSesInformationsSontCompletes()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat est crée quand toutes ses informations sont complètes", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 4
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 5
    testRunner.Given("un candidat \"Java\" (\"candidat@email.com\") avec \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 6
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 7
    testRunner.Then("le candidat est correctement enregistré avec ses informations \"Java\", \"candidat@e" +
                        "mail.com\" et \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand il a un email soat.fr")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand il a un email soat.fr")]
        public virtual void UnCandidatNestPasCreeQuandIlAUnEmailSoat_Fr()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand il a un email soat.fr", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 9
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 10
    testRunner.Given("un candidat \"Java\" (\"candidat@soat.fr\") avec \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 11
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 12
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand sa techno principale est vide")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand sa techno principale est vide")]
        public virtual void UnCandidatNestPasCreeQuandSaTechnoPrincipaleEstVide()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand sa techno principale est vide", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 14
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 15
    testRunner.Given("un candidat \"\" (\"candidat@email.com\") avec \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 16
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 17
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est vide")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est vide")]
        public virtual void UnCandidatNestPasCreeQuandSonNombreDanneesDexperienceEstVide()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est vide", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 19
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 20
    testRunner.Given("un candidat \"Java\" (\"candidat@email.com\") avec \"\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 21
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 22
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est négatif")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est négatif")]
        public virtual void UnCandidatNestPasCreeQuandSonNombreDanneesDexperienceEstNegatif()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand son nombre d\'années d\'expérience est négatif", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 24
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 25
    testRunner.Given("un candidat \"Java\" (\"candidat@email.com\") avec \"-1\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 26
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 27
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand son email est vide")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand son email est vide")]
        public virtual void UnCandidatNestPasCreeQuandSonEmailEstVide()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand son email est vide", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 29
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 30
    testRunner.Given("un candidat \"Java\" (\"\") avec \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 31
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 32
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Un candidat n\'est pas crée quand son email est incorrect")]
        [Xunit.TraitAttribute("FeatureTitle", "Création d\'un candidat")]
        [Xunit.TraitAttribute("Description", "Un candidat n\'est pas crée quand son email est incorrect")]
        public virtual void UnCandidatNestPasCreeQuandSonEmailEstIncorrect()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Un candidat n\'est pas crée quand son email est incorrect", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 34
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 35
    testRunner.Given("un candidat \"Java\" (\"candidat\") avec \"2\" ans d’expériences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Etant donné ");
#line hidden
#line 36
    testRunner.When("on tente d\'enregistrer le candidat", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 37
    testRunner.Then("le candidat n\'est pas enregistré", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                CreationDunCandidatFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                CreationDunCandidatFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
