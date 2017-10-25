// Base dependecies
var assert = require('chai').assert,
    test = require('selenium-webdriver/testing'),
    selenium = require('selenium-webdriver');

// Chromedriver-specific dependencies/setup
var chrome = require('selenium-webdriver/chrome'),
    path = require('chromedriver').path,
    service = new chrome.ServiceBuilder(path).build();
chrome.setDefaultService(service);

// Page Object dependencies
// EX: var IndexPage = require('../page_objects/IndexPage.js');

// Test block
test.describe('New Tests', function () {
    this.timeout(10000);
    var driver;
    beforeEach("Build webdriver", function() {
        driver = new selenium.Builder()
            .withCapabilities(selenium.Capabilities.chrome())
            .build();
    });

    test.it('test descrip', function () {
        // New test
    });

    afterEach("Quit webdriver", function() {
        driver.quit();
    });
});