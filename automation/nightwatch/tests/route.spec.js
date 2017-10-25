module.exports = {

  'Submitting to, from, and distance should render route': function(client) {
    var mainPage = client.page.MainPage();

    mainPage.navigate()
      .waitForElementPresent('@map')
      .waitForElementNotPresent('@originMarker')
      .waitForElementNotPresent('@destMarker')
      .setValue('@fromInput', 'cvg')
      .setValue('@toInput', 'pgh')
      .setValue('@distInput', '30')
      .click('@submitBtn')
      .waitForElementVisible('@originMarker')
      .waitForElementVisible('@destMarker');

      client.end();
  }
  
};
