﻿using System;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;
namespace CSF.Screenplay.Web.Tests.Actions
{
  [TestFixture]
  public class EnterTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void Enter_text_into_an_input_box_produces_expected_result_on_page()
    {
      // Arrange
      var pageTwo = new PageTwo();
      var openPageTwo = new Open(pageTwo);
      var enterTheText = new Enter(pageTwo.SpecialInputField, "The right value");
      var seeTheValue = new GetValue(pageTwo.TheDynamicTextArea);

      Given(joe).WasAbleTo(openPageTwo);

      // Act
      When(joe).AttemptsTo(enterTheText);

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(Enter_text_into_an_input_box_produces_expected_result_on_page));
      var result = Then(joe).Should(seeTheValue);
      Assert.AreEqual("different value", result);
    }

    [Test]
    public void Enter_different_text_into_an_input_box_produces_expected_result_on_page()
    {
      // Arrange
      var pageTwo = new PageTwo();
      var openPageTwo = new Open(pageTwo);
      var enterTheText = new Enter(pageTwo.SpecialInputField, "The wrong value");
      var seeTheValue = new GetValue(pageTwo.TheDynamicTextArea);

      Given(joe).WasAbleTo(openPageTwo);

      // Act
      When(joe).AttemptsTo(enterTheText);

      // Assert
      WebdriverTestSetup.TakeScreenshot(GetType(), nameof(Enter_different_text_into_an_input_box_produces_expected_result_on_page));
      var result = Then(joe).Should(seeTheValue);
      Assert.AreEqual("dynamic value", result);
    }
  }
}
