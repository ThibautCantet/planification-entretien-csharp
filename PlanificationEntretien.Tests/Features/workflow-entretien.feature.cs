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
    public partial class WorkflowDunEntretienDeRecrutementChezSoatFeature : object, Xunit.IClassFixture<WorkflowDunEntretienDeRecrutementChezSoatFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "workflow-entretien.feature"
#line hidden
        
        public WorkflowDunEntretienDeRecrutementChezSoatFeature(WorkflowDunEntretienDeRecrutementChezSoatFeature.FixtureData fixtureData, PlanificationEntretien_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("fr"), "Features", "Workflow d\'un entretien de recrutement chez Soat", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [Xunit.SkippableFactAttribute(DisplayName="Valider un entretien planifié")]
        [Xunit.TraitAttribute("FeatureTitle", "Workflow d\'un entretien de recrutement chez Soat")]
        [Xunit.TraitAttribute("Description", "Valider un entretien planifié")]
        public virtual void ValiderUnEntretienPlanifie()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Valider un entretien planifié", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
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
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "id",
                            "email",
                            "language",
                            "xp"});
                table7.AddRow(new string[] {
                            "1",
                            "recruteur@soat.fr",
                            "Java",
                            "10"});
#line 5
        testRunner.Given("les recruteurs existants ci-dessous", ((string)(null)), table7, "Etant donné ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "id",
                            "email",
                            "language",
                            "xp"});
                table8.AddRow(new string[] {
                            "1",
                            "candidat@mail.com",
                            "Java",
                            "5"});
#line 8
        testRunner.And("les candidats existants ci-dessous", ((string)(null)), table8, "Et ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "id",
                            "recruteur",
                            "candidat",
                            "horaire",
                            "status"});
                table9.AddRow(new string[] {
                            "1",
                            "recruteur@soat.fr",
                            "candidat@mail.com",
                            "16/04/2019 15:00",
                            "PLANIFIE"});
#line 11
        testRunner.And("les entretiens existants ci-dessous", ((string)(null)), table9, "Et ");
#line hidden
#line 14
        testRunner.When("on valide l\'entretien 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "id",
                            "recruteur",
                            "candidat",
                            "language",
                            "horaire",
                            "status"});
                table10.AddRow(new string[] {
                            "1",
                            "recruteur@soat.fr",
                            "candidat@mail.com",
                            "Java",
                            "16/04/2019 15:00",
                            "VALIDE"});
#line 15
        testRunner.Then("on récupères les entretiens suivants en base", ((string)(null)), table10, "Alors ");
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
                WorkflowDunEntretienDeRecrutementChezSoatFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                WorkflowDunEntretienDeRecrutementChezSoatFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
